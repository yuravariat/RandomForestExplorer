using System;
using RandomForestExplorer.Data;
using System.Threading.Tasks;
using System.Threading;
using RandomForestExplorer.DecisionTrees;

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
        public Action OnCompletion { get; set; }

        public Action<int, int> OnProgress { get; set; }

        public void Run()
        {
            _forest = new RandomForest();

            for(var i=0; i < _numOfTrees; i++)
            {
                RunAsync();
            }
        }

        public void Cancel()
        {
            _source.Cancel();
        }

        public void Dispose()
        {
            _dataModel = null;
        }
        #endregion

        #region Private Members
        private async void RunAsync()
        {
            try
            {
                var tree = await Task.Factory.StartNew(BuildTree, _source.Token);
                _forest.Trees.Add(tree);

                if (OnProgress != null)
                    OnProgress.BeginInvoke(_forest.Trees.Count, _numOfTrees, null, null);

                if (_forest.Trees.Count == _numOfTrees && OnCompletion != null)
                {
                    OnCompletion();

                    var result = _forest.Evaluate(_dataModel.Instances, _dataModel.Classes);
                }
            }
            catch (Exception ex)
            {

            }
        }

        private DecisionTree BuildTree()
        {
            return new TreeBuilder(_dataModel, _numOfFeatures, _seed, _treeDepth).Build();
        }

        private int AdjustNumOfFeatures(int p_numOfFeatures)
        {
            if (p_numOfFeatures > 0)
                return p_numOfFeatures;

            return (int)Math.Log(_dataModel.TotalFeatures, 2) + 1;
        }

        private int AdjustTreeDepth(int treeDepth)
        {
            if (treeDepth == 0)
                return int.MaxValue;

            return treeDepth;
        }
        #endregion
    }
}
