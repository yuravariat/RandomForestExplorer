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
        private readonly int _numOfFeatures;
        private int _treeDepth;
        private readonly int _seed;
        private RandomForest _forest;
        private CancellationTokenSource _source;
        #endregion

        #region Constructor
        public RandomForestSolver(DataModel p_dataModel, int p_numOfTrees, int p_numOfFeatures, int p_treeDepth, int p_seed)
        {
            _dataModel = p_dataModel;
            _numOfTrees = p_numOfTrees;
            _numOfFeatures = AdjustNumOfFeatures(p_numOfFeatures);
            _treeDepth = AdjustTreeDepth(p_treeDepth);
            _seed = p_seed;
            _source = new CancellationTokenSource();
        }
        #endregion

        #region Public Methods
        public Action<Tuple<double[,], Dictionary<int, string>>> OnEvaluationCompletion { get; set; }

        public Action<int> OnForestCompletion { get; set; }

        public Action OnProgress { get; set; }

        public Action<Exception> OnError { get; set; } 

        public void Run()
        {
            _forest = new RandomForest();
            //all trees are built asynchronously
            for(var i=0; i < _numOfTrees; i++)
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
                    //then evaluate and build confusion matrix asynchronously as well as any other metrics
                    var confusionMatrix = await Task.Factory.StartNew(BuildConfusionMatrix, _source.Token);
                    //eventually report the confusion matrix to the GUI
                    if (OnEvaluationCompletion != null)
                        OnEvaluationCompletion(confusionMatrix);
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
            return new TreeBuilder(_dataModel, _numOfFeatures, _seed, _treeDepth).Build();
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
            if (_dataModel.DataType == TreeOutput.ClassifiedCategory)
            {
                evaluationData = _forest.Evaluate(_dataModel.Instances, _dataModel.Classes);
                confusionMatrix = new double[_dataModel.Classes.Count, _dataModel.Classes.Count];
                foreach (var entry in evaluationData)
                {
                    var orignalClass = int.Parse(_dataModel.Instances[entry.Key].Class);
                    var votedClass = int.Parse(entry.Value);
                    confusionMatrix[orignalClass, votedClass] += 1;
                }
            }
            else
            {
                evaluationData = _forest.EvaluateRegression(_dataModel.Instances, _dataModel.Classes);
                var yeses = evaluationData.Values.Count(v => v == "yes");
            }


            return new Tuple<double[,], Dictionary<int, string>>(confusionMatrix, evaluationData);
        }

        /// <summary>
        /// Adjusts the number of features based on: Log2(total features) + 1
        /// When the value is 0, using this formula, otherwise using user's choice
        /// </summary>
        /// <param name="p_numOfFeatures"></param>
        /// <returns></returns>
        private int AdjustNumOfFeatures(int p_numOfFeatures)
        {
            if (p_numOfFeatures > 0)
                return p_numOfFeatures;

            return (int)Math.Log(_dataModel.TotalFeatures, 2) + 1;
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
