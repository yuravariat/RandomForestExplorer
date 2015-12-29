using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RandomForestExplorer.DecisionTrees;
using RandomForestExplorer.Data;
using System.Collections;
using System.Diagnostics;

namespace RandomForestExplorer.RandomForests
{
    class TreeBuilder
    {
        private DataModel _model;
        private Randomizer _randomizer;
        private int _seed;
        private int _treeDepth;
        private int _numOfFeatures;

        public TreeBuilder(DataModel model, int numOfFeatures, int seed, int depth)
        {
            _model = model;
            _seed = seed;
            _treeDepth = depth;
            _numOfFeatures = numOfFeatures;
            _randomizer = new Randomizer();
        }

        public DecisionTree Build()
        {
            if (_model != null && _model.Features.Count > 0)
            {
                DecisionTree tree = new DecisionTree();
                tree.OutputType = TreeOutput.ClassifiedCategory;

                var watch = Stopwatch.StartNew();
                tree.RootNode = CreateNode(_model.Instances.ToList(), 0);
                watch.Stop();

                double buildTreeTime = watch.Elapsed.TotalMilliseconds;

                return tree;
            }
            return null;
        }

        /// <summary>
        /// Splits one node into two groups or stops with  leaf (Clacification decision)
        /// </summary>
        /// <param name="node"></param>
        /// <param name="instances"></param>
        /// <param name="randomFeaturesNum"></param>
        private TreeNode CreateNode(List<Instance> instances, int treeDepth, Tuple<double, string> giniPrevScore=null)
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
            if (giniPrevScore.Item1==0 || instances.Count <= 20 || treeDepth == _treeDepth)
            {
                node.Item.Clacification = giniPrevScore.Item2;
                return node;
            }

            // get random features
            var featureIndexes = RandomizeFeatures();


            double minScore = Double.MaxValue;
            double splitValue = 0;
            int minScoreFeatureIndex = -1;

            //Metric
            Stopwatch swTotalAllSplits = new Stopwatch();
            //Stopwatch swSplit = new Stopwatch();
            //List<double> miliseconds = new List<double>(instances.Count);

            swTotalAllSplits.Start();

            // Classes counters
            Dictionary<string, int> totalCalssesS1 = new Dictionary<string, int>();       
            Dictionary<string, int> totalCalssesS2 = new Dictionary<string, int>();       
            foreach (string classe in _model.Classes)                                     
            {                                                                             
                totalCalssesS1.Add(classe, 0);                                            
                totalCalssesS2.Add(classe, 0);                                            
            }
            int totalSubset1, totalSubset2;
            Tuple<double,string> min_gini1 = null, min_gini2 = null;

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

                for (int i = 0; i < instances.Count; i++)
                {
                    // Update counters
                    totalCalssesS1[instances[i].Class] += 1;                                 
                    totalCalssesS2[instances[i].Class] -= 1;                                 
                    totalSubset1 += 1;                                                        
                    totalSubset2 -= 1;                                                        

                    // if next value has the same value dont calculate gini split is not finished.
                    if (i < (instances.Count-1) && instances[i].Values[featureIndex] == instances[i + 1].Values[featureIndex])
                    {
                        continue;
                    }

                    // Calculate gini from two subsets
                    string @class1;
                    var gini1 = GiniScore(totalCalssesS1, out @class1);
                    string @class2;
                    var gini2 = GiniScore(totalCalssesS2, out @class2);    
                    
                    //compare childs to parent: (Nl/N)*Sl + (Nr/N)*Sr                                
                    var totalScore = gini1 * ((double)totalSubset1 / (double)instances.Count)  +  gini2 * ((double)totalSubset2 / (double)instances.Count);                               

                    if (totalScore < minScore)
                    {
                        minScore = totalScore;
                        splitValue = instances[i].Values[featureIndex];
                        minScoreFeatureIndex = featureIndex;
                        min_gini1 = new Tuple<double, string>(gini1,@class1);
                        min_gini2 = new Tuple<double, string>(gini2, @class2);
                    }
                }
            }
            swTotalAllSplits.Stop();
            double totalMilisecondsAllSplits = swTotalAllSplits.Elapsed.TotalMilliseconds;

            // There is no improvment no need to split.
            // Yura: not sure about this
            if (minScore >= giniPrevScore.Item1)
            {
                node.Item.Clacification = giniPrevScore.Item2;
                return node;
            }

            // Create decision item
            node.Item = new DecisionNode();
            node.Item.SplitFeature = _model.Features[minScoreFeatureIndex].Name; //minScoreFeature.Name;
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
            foreach(var classKey in classCounts.Keys)
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

        private List<int> RandomizeFeatures()
        {
            return _randomizer.Randomize(_seed, 0, _model.TotalFeatures, _numOfFeatures);
        }
    }
}
