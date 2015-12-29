using System;
using RandomForestExplorer.Data;
using System.Threading.Tasks;
using System.Collections.Concurrent;
using System.Threading;

namespace RandomForestExplorer.RandomForests
{
    public class RandomForestSolver : IDisposable
    {
        private DataModel _dataModel;
        private readonly int _numOfTrees;
        private readonly int _numOfFeatures;
        private int _treeDepth;
        private readonly int _seed;
        private ConcurrentBag<DecisionTrees.DecisionTree> _trees;
        private CancellationTokenSource _source;

        public RandomForestSolver(DataModel p_dataModel, int p_numOfTrees, int p_numOfFeatures, int p_treeDepth, int p_seed)
        {
            _dataModel = p_dataModel;
            _numOfTrees = p_numOfTrees;
            _numOfFeatures = AdjustNumOfFeatures(p_numOfFeatures);
            _treeDepth = AdjustTreeDepth(p_treeDepth);
            _seed = p_seed;
            _trees = new ConcurrentBag<DecisionTrees.DecisionTree>();
            _source = new CancellationTokenSource();
        }

        public Action OnCompletion { get; set; }

        public void Run()
        {
            while(_trees.Count > 0)
            {
                DecisionTrees.DecisionTree tree;
                if (_trees.TryTake(out tree))
                {
                    tree = null;
                }
            }

            for(var i=0; i < _numOfTrees; i++)
            {
                RunAsync();
            }
        }

        public void Cancel()
        {
            _source.Cancel();
        }

        private async void RunAsync()
        {
            try
            {
                var tree = await Task.Factory.StartNew(BuildTree, _source.Token);
                _trees.Add(tree);
            }
            catch(Exception ex)
            {

            }


            if (_trees.Count == _numOfTrees && OnCompletion != null)
            {
                OnCompletion();
            }
        }

        private DecisionTrees.DecisionTree BuildTree()
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

        public void Dispose()
        {
            _dataModel = null;
        }
    }
}
