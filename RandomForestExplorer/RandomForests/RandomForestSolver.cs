using System;
using System.Collections.Generic;
using System.Linq;
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
        private readonly float _percentSplit;
        private Randomizer _randomizer;

        public RandomForestSolver(DataModel p_dataModel, int p_numOfTrees, int p_numOfFeatures, int p_treeDepth, int p_seed, float p_percentSplit)
        {
            _dataModel = p_dataModel;
            _numOfTrees = p_numOfTrees;
            _numOfFeatures = AdjustNumOfFeatures(p_numOfFeatures);
            _treeDepth = p_treeDepth;
            _seed = p_seed;
            _percentSplit = p_percentSplit;
            _randomizer = new Randomizer();
        }

        public void Run()
        {
            var trees = GenerateRandomTrees(GetTrainingData());
        }

        private int AdjustNumOfFeatures(int p_numOfFeatures)
        {
            if (p_numOfFeatures > 0)
                return p_numOfFeatures;

            return (int)Math.Log(_dataModel.TotalFeatures, 2) + 1;
        }

        private IEnumerable<Instance> GetTrainingData()
        {
            var instances = new List<Instance>();
            var trainingInstancesCount = (int)(_percentSplit*_dataModel.Instances.Count)/100;
            instances.AddRange(_dataModel.Instances.Take(trainingInstancesCount));
            return instances;
        }

        private IEnumerable<Tree> GenerateRandomTrees(IEnumerable<Instance> p_trainingData)
        {
            var trainingData = p_trainingData.ToList();
            var countPerTree = trainingData.Count /_numOfTrees;

            var trees = new List<Tree>();
            for (var i = 0; i < _numOfTrees; i++)
            {
                var tree = new Tree {ID = i};

                //randomizing rows
                var instanceIndexes = _randomizer.Randomize(_seed, 0, trainingData.Count, countPerTree);
                foreach (var index in instanceIndexes)
                {
                    tree.Instances.Add(index);
                }

                //randomizing columns
                var featureIndexes = _randomizer.Randomize(_seed, 0, _dataModel.TotalFeatures, _numOfFeatures);
                foreach (var index in featureIndexes)
                {
                    tree.Features.Add(index);
                }

                trees.Add(tree);
            }

            return trees;
        }

        public void Dispose()
        {
            _dataModel = null;
            _randomizer = null;
        }
    }

    public class Tree
    {
        public Tree()
        {
            Instances = new List<int>();
            Features = new List<int>();
        }

        public int ID { get; set; }
        /// <summary>
        /// Gets or sets the instance indexes matched to the original data model.
        /// </summary>
        /// <value>
        /// The instance indexes.
        /// </value>
        public List<int> Instances { get; set; }
        /// <summary>
        /// Gets or sets the feature indexes matched to the original data model.
        /// </summary>
        /// <value>
        /// The feature indexes.
        /// </value>
        public List<int> Features { get; set; }
    }
}
