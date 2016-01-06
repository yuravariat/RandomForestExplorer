using System;
using RandomForestExplorer.Data;
using System.Threading.Tasks;
using System.Threading;
using RandomForestExplorer.DecisionTrees;
using System.Text;
using System.Collections.Generic;
using System.Linq;

namespace RandomForestExplorer.RandomForests
{
    public class RandomForestSolver : IDisposable
    {
        #region Private Members
        private DataModel _dataModel;
        private readonly int _numOfTrees;
        private readonly int _numOfTreesToTest;
        private readonly int _numOfFeatures;
        private int _treeDepth;
        private RandomForest _forest;
        private CancellationTokenSource _source;
        private Random _random;
        #endregion

        #region Constructor
        public RandomForestSolver(DataModel p_dataModel, int p_numOfTrees, int p_numOfTreesToTest, int p_numOfFeatures, int p_treeDepth, int p_seed)
        {
            _dataModel = p_dataModel;
            _numOfTrees = p_numOfTrees;
            _numOfTreesToTest = p_numOfTreesToTest;
            _numOfFeatures = AdjustNumOfFeatures(p_numOfFeatures);
            _treeDepth = AdjustTreeDepth(p_treeDepth);
            _source = new CancellationTokenSource();
            _random = new Random(p_seed);
        }
        #endregion

        #region Public Methods
        public Action<Tuple<double[,], Dictionary<int, string>>> OnEvaluationCompletion { get; set; }
        public Action<Dictionary<int, Tuple<bool, double>>> OnEvaluationCompletionRegression { get; set; }

        public Action<int> OnForestCompletion { get; set; }

        public Action OnProgress { get; set; }

        public Action<Exception> OnError { get; set; }

        public void Run()
        {
            _forest = new RandomForest(_numOfTreesToTest);
            //all trees are built asynchronously
            for (var i = 0; i < _numOfTrees; i++)
            {
                //async operation
                RunAsync();
            }
        }

        public void Cancel()
        {
            _source.Cancel();
        }

        public int GetTreeCount()
        {
            if (_forest != null)
                return _forest.Trees.Count;

            return 0;
        }

        public int GetNumOfFeatures()
        {
            return _numOfFeatures;
        }

        public int GetTreeDepth()
        {
            return _treeDepth;
        }

        public void Dispose()
        {
            _dataModel = null;
        }
        /// <summary>
        /// Get feutures sorted by importance
        /// </summary>
        /// <returns> Sorted List each Tuple-int int- is feauture id, feature order</returns>
        public List<Tuple<int, int>> GetFeautesSortedByImportance()
        {
            List<Tuple<int, int>> result = new List<Tuple<int, int>>();
            if(_forest.Trees!=null && _forest.Trees.Count > 0)
            {
                Dictionary<int, int> counts = new Dictionary<int, int>();

                foreach(var tree in _forest.Trees)
                {
                    if (tree.RootNode != null)
                    {
                        if (!counts.ContainsKey(tree.RootNode.Item.SplitFeatureIndex))
                        {
                            counts.Add(tree.RootNode.Item.SplitFeatureIndex, 2);
                        }
                        else
                        {
                            counts[tree.RootNode.Item.SplitFeatureIndex] += 2;
                        }
                        foreach(var node in tree.RootNode.Children())
                        {
                            if (node != null)
                            {
                                if (!counts.ContainsKey(node.Item.SplitFeatureIndex))
                                {
                                    counts.Add(node.Item.SplitFeatureIndex, 1);
                                }
                                else
                                {
                                    counts[node.Item.SplitFeatureIndex]++;
                                }
                            }
                        }
                    }
                }
                foreach(var f in _dataModel.Features)
                {
                    if (!counts.ContainsKey(f.ID))
                    {
                        counts.Add(f.ID, 1);
                    }
                }

                result = counts.Select(v => new Tuple<int, int>(v.Key, v.Value)).OrderBy(t => t.Item2).ToList();
            }
            
            return result;
        }
        #endregion

        #region Private Members
        /// <summary>
        /// Executes the random forest algorithm asynchronously.
        /// </summary>
        private async void RunAsync()
        {
            try
            {
                //build the tree asynchronously
                var tree = await Task.Factory.StartNew(BuildTree, _source.Token);
                //on completion add to the forest trees collection
                _forest.Trees.Add(tree);
                //and report progress
                if (OnProgress != null)
                    OnProgress();
                //if all trees were built we are ready to evaluate the forest
                if (_forest.Trees.Count == _numOfTrees)
                {
                    //first report on forest completion
                    if (OnForestCompletion != null)
                        OnForestCompletion(_forest.Trees.Count);

                    if (_dataModel.DataType == TreeOutput.ClassifiedCategory)
                    {
                        //then evaluate and build confusion matrix asynchronously as well as any other metrics
                        var confusionMatrix = await Task.Factory.StartNew(BuildConfusionMatrix, _source.Token);
                        //eventually report the confusion matrix to the GUI
                        if (OnEvaluationCompletion != null)
                            OnEvaluationCompletion(confusionMatrix);
                    }
                    else
                    {
                        var results = await Task.Factory.StartNew(GetRegressionResults, _source.Token);
                        if (OnEvaluationCompletionRegression != null)
                            OnEvaluationCompletionRegression(results);
                    }
                }
            }
            catch (Exception ex)
            {
                if (OnError != null)
                    OnError(ex);
            }
        }

        /// <summary>
        /// Build a single decision tree in the forest.
        /// </summary>
        /// <returns></returns>
        private DecisionTree BuildTree()
        {
            return new TreeBuilder(_dataModel, _random, _numOfFeatures, _treeDepth).Build();
        }

        /// <summary>
        /// Build the confusion matrix.
        /// </summary>
        /// <param name="instances"></param>
        /// <returns></returns>
        private Tuple<double[,], Dictionary<int, string>> BuildConfusionMatrix()
        {
            Dictionary<int, string> evaluationData = null;
            double[,] confusionMatrix = null;

            evaluationData = _forest.Evaluate(_dataModel.Instances, _dataModel.Classes);
            confusionMatrix = new double[_dataModel.Classes.Count, _dataModel.Classes.Count];
            foreach (var entry in evaluationData)
            {
                var orignalClass = int.Parse(_dataModel.Instances[entry.Key].Class);
                var votedClass = int.Parse(entry.Value);
                confusionMatrix[orignalClass, votedClass] += 1;
            }
            return new Tuple<double[,], Dictionary<int, string>>(confusionMatrix, evaluationData);
        }
        private Dictionary<int, Tuple<bool, double>> GetRegressionResults()
        {
            Dictionary<int, Tuple<bool, double>> results = _forest.EvaluateRegression(_dataModel.Instances);
            return results;
        }

        /// <summary>
        /// Adjusts the number of features based on: Log2(total features)
        /// When the value is 0, using this formula, otherwise using user's choice
        /// </summary>
        /// <param name="p_numOfFeatures"></param>
        /// <returns></returns>
        private int AdjustNumOfFeatures(int p_numOfFeatures)
        {
            if (p_numOfFeatures > 0)
                return p_numOfFeatures;

            return (int)Math.Log(_dataModel.TotalFeatures, 2);
        }

        /// <summary>
        /// Adjusts the tree depth. Value of 0 means the tree depth is unlimited.
        /// </summary>
        /// <param name="treeDepth"></param>
        /// <returns></returns>
        private int AdjustTreeDepth(int treeDepth)
        {
            if (treeDepth == 0)
                return int.MaxValue;

            return treeDepth;
        }
        #endregion
    }
}
