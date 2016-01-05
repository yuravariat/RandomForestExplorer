using System;
using System.Collections.Generic;
using System.Linq;
using RandomForestExplorer.DecisionTrees;
using RandomForestExplorer.Data;
using System.Diagnostics;

namespace RandomForestExplorer.RandomForests
{
    class TreeBuilder
    {
        #region Members
        private DataModel _model;
        private Random _random;
        //private Randomizer _randomizer;
        private int _seed;
        private int _treeDepth;
        private int _numOfFeatures;
        private int _minimumInastancesInNode;
        private double _minVariance;
        #endregion

        #region Constructor
        public TreeBuilder(DataModel model, int numOfFeatures, int seed, int depth)
        {
            _model = model;
            _seed = seed;
            _treeDepth = depth;
            _numOfFeatures = numOfFeatures;
            //_randomizer = new Randomizer();
            _random = new Random(_seed);
        }
        #endregion

        #region Public Methods
        public DecisionTree Build()
        {
            if (_model != null && _model.Features.Count > 0)
            {
                DecisionTree tree = new DecisionTree();
                tree.OutputType = _model.DataType;
                _minimumInastancesInNode =  (int)(_model.Instances.Count * 0.003); // 0.03% of the data.

                var watch = Stopwatch.StartNew();
                var clonedList = new List<Instance>(from instance in _model.Instances select instance.Clone());

                if (tree.OutputType == TreeOutput.ClassifiedCategory)
                {
                    tree.RootNode = CreateNode(clonedList, 0);
                }
                else
                {
                    double totalVariance = Variance(clonedList);
                    _minVariance = totalVariance * 0.1; //  10% of the variance.
                    tree.RootNode = CreateNodeRegresion(clonedList, 0, totalVariance);
                }
                watch.Stop();
                double buildTreeTime = watch.Elapsed.TotalMilliseconds;
                return tree;
            }
            return null;
        }
        #endregion

        #region Private Members
        /// <summary>
        /// Splits one node into two groups or stops with  leaf (Clacification decision)
        /// </summary>
        /// <param name="node"></param>
        /// <param name="instances"></param>
        /// <param name="randomFeaturesNum"></param>
        private TreeNode CreateNode(List<Instance> instances, int treeDepth, Tuple<double, string> giniPrevScore = null)
        {
            TreeNode node = new TreeNode();
            node.Item = new DecisionNode();

            // stop condition
            if (giniPrevScore == null)
            {
                string @class;
                var giniScore = GiniScore(instances, out @class);
                giniPrevScore = new Tuple<double, string>(giniScore, @class);
            }

            // Gini score of the group is 0, the group is pure. or there is too few instances 
            if (giniPrevScore.Item1 == 0 || instances.Count <= _minimumInastancesInNode || treeDepth == _treeDepth)
            {
                node.Item.Classification = giniPrevScore.Item2;
                return node;
            }

            // get random features
            var featureIndexes = RandomizeFeatures();


            double minScore = Double.MaxValue;
            double splitValue = 0;
            int minScoreFeatureIndex = -1;

            //Metric
            //Stopwatch swTotalAllSplits = new Stopwatch();
            //Stopwatch swSplit = new Stopwatch();
            //List<double> miliseconds = new List<double>(instances.Count);

            //swTotalAllSplits.Start();

            // Classes counters
            Dictionary<string, int> totalCalssesS1 = new Dictionary<string, int>();
            Dictionary<string, int> totalCalssesS2 = new Dictionary<string, int>();
            foreach (string classe in _model.Classes)
            {
                totalCalssesS1.Add(classe, 0);
                totalCalssesS2.Add(classe, 0);
            }
            int totalSubset1, totalSubset2;
            Tuple<double, string> min_gini1 = null, min_gini2 = null;

            // Loop thought all feature and all values.
            foreach (var featureIndex in featureIndexes)
            {
                instances.Sort((a, b) => a.Values[featureIndex].CompareTo(b.Values[featureIndex]));

                // Reset counters
                foreach (string classe in _model.Classes)
                {
                    totalCalssesS1[classe] = 0;
                    totalCalssesS2[classe] = 0;
                }
                for (int i = 0; i < instances.Count; i++)
                {
                    totalCalssesS2[instances[i].Class] += 1;
                }
                totalSubset1 = 0;
                totalSubset2 = instances.Count;

                for (int i = 0; i < instances.Count-1; i++)
                {
                    // Update counters
                    totalCalssesS1[instances[i].Class] += 1;
                    totalCalssesS2[instances[i].Class] -= 1;
                    totalSubset1++;
                    totalSubset2--;

                    // if next value has the same value dont calculate gini split is not finished.
                    if (i < (instances.Count - 1) && instances[i].Values[featureIndex] == instances[i + 1].Values[featureIndex])
                    {
                        continue;
                    }

                    // Calculate gini from two subsets
                    string @class1;
                    var gini1 = GiniScore(totalCalssesS1, out @class1);
                    string @class2;
                    var gini2 = GiniScore(totalCalssesS2, out @class2);

                    //compare childs to parent: (Nl/N)*Sl + (Nr/N)*Sr                                
                    var totalScore = gini1 * ((double)totalSubset1 / (double)instances.Count) +
                                     gini2 * ((double)totalSubset2 / (double)instances.Count);

                    if (totalScore < minScore)
                    {
                        minScore = totalScore;
                        splitValue = instances[i].Values[featureIndex];
                        minScoreFeatureIndex = featureIndex;
                        min_gini1 = new Tuple<double, string>(gini1, @class1);
                        min_gini2 = new Tuple<double, string>(gini2, @class2);
                    }
                }
            }
            //swTotalAllSplits.Stop();
            //double totalMilisecondsAllSplits = swTotalAllSplits.Elapsed.TotalMilliseconds;

            // There is no improvment no need to split.
            // Yura: not sure about this
            if (minScore >= giniPrevScore.Item1)
            {
                node.Item.Classification = giniPrevScore.Item2;
                return node;
            }

            // Create decision item
            node.Item = new DecisionNode();
            node.Item.SplitFeatureIndex = minScoreFeatureIndex;
            node.Item.SplitValue = splitValue;

            // Create right and left nodes
            treeDepth++;
            var subsetLeft = instances.Where(i => i.Values[minScoreFeatureIndex] <= splitValue).ToList();
            var subsetRight = instances.Where(i => i.Values[minScoreFeatureIndex] > splitValue).ToList();
            node.Left = CreateNode(subsetLeft, treeDepth, min_gini1);
            node.Right = CreateNode(subsetRight, treeDepth, min_gini2);
            node.Right.Parent = node;
            node.Left.Parent = node;

            return node;

        }

        private TreeNode CreateNodeRegresion(List<Instance> instances, int treeDepth, double? variancePrevScore = null, double? prevMean = null)
        {
            TreeNode node = new TreeNode();
            node.Item = new DecisionNode();

            // stop condition
            if (!variancePrevScore.HasValue)
            {
                variancePrevScore = Variance(instances);
            }

            // Variance of the subset is less then minimum or there is too few instances 
            if (variancePrevScore <= _minVariance || instances.Count <= _minimumInastancesInNode || treeDepth == _treeDepth)
            {
                node.Item.PredictedMean = prevMean.HasValue ? prevMean.Value : Mean(instances);
                node.Item.PredictedError = variancePrevScore.HasValue ? variancePrevScore.Value : Variance(instances);
                return node;
            }

            // get random features
            var featureIndexes = RandomizeFeatures();


            double minVar = Double.MaxValue;
            double minVarLeft = 0, minVarRight = 0;
            double minMeanLeft = 0, minMeanRight = 0;
            double splitValue = 0;
            int minScoreFeatureIndex = -1;

            //Metric
            //Stopwatch swTotalAllSplits = new Stopwatch();
            //Stopwatch swSplit = new Stopwatch();
            //List<double> miliseconds = new List<double>(instances.Count);

            //swTotalAllSplits.Start();

            // Counters
            int totalSubsetCount1, totalSubsetCount2;
            double totalSub1 = 0, totalSub2 = 0;
            double meanSub1 = 0, meanSub2 = 0;
            double varianceSub1 = 0, varianceSub2 = 0;
            double squearSumSub1 = 0, squearSumSub2 = 0;


            // Loop thought all feature and all values.
            foreach (var featureIndex in featureIndexes)
            {
                instances.Sort((a, b) => a.Values[featureIndex].CompareTo(b.Values[featureIndex]));

                // Reset counters
                totalSubsetCount1 = 0;
                totalSubsetCount2 = instances.Count;
                totalSub1 = 0;
                totalSub2 = instances.Sum(v => v.Number);
                meanSub1 = 0;
                meanSub2 = prevMean.HasValue ? prevMean.Value : Mean(instances);

                for (int i = 0; i < instances.Count-1; i++)
                {
                    // Update counters
                    totalSubsetCount1++;
                    totalSubsetCount2--;
                    totalSub1 += instances[i].Number;
                    totalSub2 -= instances[i].Number;
                    meanSub1 = totalSub1 / totalSubsetCount1;
                    meanSub2 = totalSub2 / totalSubsetCount2;

                    squearSumSub1 = 0;
                    squearSumSub2 = 0;
                    for (int k = 0; k < instances.Count; k++)
                    {
                        if (k <= i)
                        {
                            squearSumSub1 += (instances[k].Number - meanSub1) * (instances[k].Number - meanSub1);
                                // Math.Pow((instances[k].Number - meanSub1), 2);
                        }
                        else
                        {
                            squearSumSub2 += (instances[k].Number - meanSub2) * (instances[k].Number - meanSub2);
                                //Math.Pow((instances[k].Number - meanSub2), 2);
                        }
                    }
                    
                    varianceSub1 = squearSumSub1 / totalSubsetCount1;
                    varianceSub2 = squearSumSub2 / totalSubsetCount2;

                    // if next value has the same value dont calculate gini split is not finished.
                    if (i < (instances.Count - 1) && instances[i].Values[featureIndex] == instances[i + 1].Values[featureIndex])
                    {
                        continue;
                    }

                    //compare childs to parent: (Nl/N)*Sl + (Nr/N)*Sr                                
                    //var totalScore = varianceSub1 * ((double)totalSubsetCount1 / (double)instances.Count) +
                    //                 varianceSub2 * ((double)totalSubsetCount2 / (double)instances.Count);
                    var totalScore = varianceSub1  + varianceSub2;

                    if (totalScore < minVar)
                    {
                        minVar = totalScore;
                        splitValue = instances[i].Values[featureIndex];
                        minScoreFeatureIndex = featureIndex;
                        minVarLeft = varianceSub1;
                        minVarRight = varianceSub2;
                        minMeanLeft = meanSub1;
                        minMeanRight = meanSub2;
                    }
                }
            }
            //swTotalAllSplits.Stop();
            //double totalMilisecondsAllSplits = swTotalAllSplits.Elapsed.TotalMilliseconds;

            // There is no improvment no need to split.
            // Yura: not sure about this
            if (minVar >= variancePrevScore.Value)
            {
                node.Item.PredictedMean = prevMean.HasValue ? prevMean.Value : Mean(instances);
                node.Item.PredictedError = variancePrevScore.HasValue ? variancePrevScore.Value : Variance(instances);
                return node;
            }

            // Create decision item
            node.Item = new DecisionNode();
            node.Item.SplitFeatureIndex = minScoreFeatureIndex;
            node.Item.SplitValue = splitValue;

            // Create right and left nodes
            treeDepth++;
            var subsetLeft = instances.Where(i => i.Values[minScoreFeatureIndex] <= splitValue).ToList();
            var subsetRight = instances.Where(i => i.Values[minScoreFeatureIndex] > splitValue).ToList();
            node.Left = CreateNodeRegresion(subsetLeft, treeDepth, minVarLeft, minMeanLeft);
            node.Right = CreateNodeRegresion(subsetRight, treeDepth, minVarRight, minMeanRight);
            node.Right.Parent = node;
            node.Left.Parent = node;

            return node;

        }

        private double GiniScore(List<Instance> instances, out string @class)
        {
            Dictionary<string, int> totalCalsses = new Dictionary<string, int>();
            for (int i = 0; i < instances.Count; i++)
            {
                if (!totalCalsses.ContainsKey(instances[i].Class))
                    totalCalsses.Add(instances[i].Class, 0);

                totalCalsses[instances[i].Class]++;
            }
            return GiniScore(totalCalsses, out @class);
        }

        private double GiniScore(Dictionary<string, int> classCounts, out string @class)
        {
            var totalDataItems = classCounts.Sum(count => count.Value);
            var gini = 0d;

            foreach (var classKey in classCounts.Keys)
            {
                if (classCounts[classKey] > 0)
                {
                    var Pk = (double)classCounts[classKey] / totalDataItems;
                    var proportion = Pk * (1 - Pk);
                    gini += proportion;
                }
            }

            @class = classCounts.Count == 0 ?
                     string.Empty :
                     classCounts.OrderByDescending(k => k.Value).First().Key;

            return gini;
        }

        private double Variance(List<Instance> instances)
        {
            double mean = 0;
            for (int i = 0; i < instances.Count; i++)
            {
                mean += instances[i].Number;
            }
            mean = mean / (double)instances.Count;

            double variance = 0;
            for (int i = 0; i < instances.Count; i++)
            {
                variance += Math.Pow((instances[i].Number - mean), 2);
            }
            variance = variance / (double)instances.Count;

            return variance;
        }
        private double Mean(List<Instance> instances)
        {
            double mean = 0;
            for (int i = 0; i < instances.Count; i++)
            {
                mean += instances[i].Number;
            }
            return mean / (double)instances.Count;
        }

        private HashSet<int> RandomizeFeatures()
        {
            var featureIndexes = new HashSet<int>();

            while (featureIndexes.Count < _numOfFeatures)
            {
                var featureIndex = _random.Next(0, _model.TotalFeatures);
                if (!featureIndexes.Contains(featureIndex))
                    featureIndexes.Add(featureIndex);
            }

            return featureIndexes;
        }
        #endregion
    }
}
