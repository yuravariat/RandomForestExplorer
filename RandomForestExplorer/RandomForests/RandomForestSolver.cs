using System;
using RandomForestExplorer.Data;

namespace RandomForestExplorer.RandomForests
{
    public class RandomForestSolver : IDisposable
    {
        private DataModel _dataModel;
        private readonly int _numOfTrees;
        private readonly int _numOfFeatures;
        private int _treeDepth;
        private readonly int _seed;
        private Randomizer _randomizer;

        public RandomForestSolver(DataModel p_dataModel, int p_numOfTrees, int p_numOfFeatures, int p_treeDepth, int p_seed)
        {
            _dataModel = p_dataModel;
            _numOfTrees = p_numOfTrees;
            _numOfFeatures = AdjustNumOfFeatures(p_numOfFeatures);
            _treeDepth = p_treeDepth;
            _seed = p_seed;
            _randomizer = new Randomizer();
        }

        public void Run()
        {
            var treeBuilder = new RandomTreeBuilder(_dataModel, _numOfFeatures, _seed, _treeDepth);
            treeBuilder.Build();
        }

        private int AdjustNumOfFeatures(int p_numOfFeatures)
        {
            if (p_numOfFeatures > 0)
                return p_numOfFeatures;

            return (int)Math.Log(_dataModel.TotalFeatures, 2) + 1;
        }

        public void Dispose()
        {
            _dataModel = null;
            _randomizer = null;
        }
    }
}
