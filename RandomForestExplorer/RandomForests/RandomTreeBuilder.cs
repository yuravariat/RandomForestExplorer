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
    class RandomTreeBuilder
    {
        private DataModel _model;
        private int _randomFeaturesNum;

        public RandomTreeBuilder(DataModel model)
        {
            _model = model;
        }
        /// <summary>
        /// Creates random decision tree from dataset.
        /// </summary>
        /// <param name="model"></param>
        /// <param name="randomFeaturesNum"></param>
        /// <returns></returns>
        public DecisionTree BuildRandomDecisionTree(int? randomFeaturesNum = null)
        {
            if (_model != null && _model.Features.Count > 0)
            {
                DecisionTree tree = new DecisionTree();
                tree.OutputType = TreeOutput.ClassifiedCategory;

                if (randomFeaturesNum.HasValue && randomFeaturesNum.Value > _model.Features.Count - 1)
                {
                    if (randomFeaturesNum.Value > _model.Features.Count - 1)
                    {
                        _randomFeaturesNum = _model.Features.Count - 1;
                    }
                    else
                    {
                        _randomFeaturesNum = randomFeaturesNum.Value;
                    }
                }
                else
                {
                    _randomFeaturesNum = (int)Math.Sqrt(_model.Features.Count);
                }

                Stopwatch swTreeBuild = new Stopwatch();

                swTreeBuild.Start();
                tree.RootNode = CreateNode(_model.Instances.ToList());
                swTreeBuild.Stop();
                double buildTreeTime = swTreeBuild.Elapsed.TotalMilliseconds;

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
        private TreeNode CreateNode(List<Instance> instances, Tuple<double, string> giniPrevScore=null)
        {
            TreeNode node = new TreeNode();
            node.Item = new DecisionNode();

            // stop condition
            if (giniPrevScore == null)
            {
                giniPrevScore = GiniScore(instances);
            }

            // Gini score of the group is 0, the group is pure. or there is too few instances 
            if (giniPrevScore.Item1==0 || instances.Count <= 20)
            {
                node.Item.Clacification = giniPrevScore.Item2;
                return node;
            }

            // get random features
            var features = RandomizeFeatures();


            double minScore = Double.MaxValue;
            double splitValue = 0;
            Feature minScoreFeature = null;

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
            foreach (Feature feature in features)
            {
                int featureIndex = feature.ID - 1;
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
                    //swSplit.Reset();
                    //swSplit.Start();

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
                    var gini1 = GiniScore(totalCalssesS1);                                    
                    var gini2 = GiniScore(totalCalssesS2);                                    
                    var totalScore = gini1.Item1 * ((double)totalSubset1 / (double)instances.Count)  + 
                                     gini2.Item1 * ((double)totalSubset2 / (double)instances.Count);                               

                    if (totalScore < minScore)
                    {
                        minScore = totalScore;
                        splitValue = instances[i].Values[featureIndex];
                        minScoreFeature = feature;
                        min_gini1 = gini1;
                        min_gini2 = gini2;
                    }

                    //swSplit.Stop();
                    //miliseconds.Add(swSplit.Elapsed.TotalMilliseconds);
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
            node.Item.SplitFeature = minScoreFeature.Name;
            node.Item.SplitValue = splitValue;

            // Create right and left nodes
            var subsetLeft = instances.Where(i => i.Values[minScoreFeature.ID - 1] <= splitValue).ToList();
            var subsetRight = instances.Where(i => i.Values[minScoreFeature.ID - 1] > splitValue).ToList();
            node.Left = CreateNode(subsetLeft, min_gini1);
            node.Right = CreateNode(subsetRight, min_gini2);
            node.Right.Parent = node;
            node.Left.Parent = node;

            return node;

        }
        /// <summary>
        /// Gini score calculation, returns gini score and most dominant class.
        /// </summary>
        /// <param name="instances"></param>
        /// <returns></returns>
        private Tuple<double,string> GiniScore(Dictionary<string, int> classesCounts)
        {
            double sum = 0d;
            double totalInst = 0;
            foreach (string classe in classesCounts.Keys)
            {
                totalInst += classesCounts[classe];
            }
            foreach (string classe in classesCounts.Keys)
            {
                if (classesCounts[classe] > 0)
                {
                    double frefrequency = (((double)classesCounts[classe]) / totalInst);
                    sum += frefrequency * frefrequency;
                }
            }
            return new Tuple<double, string>(1-sum, classesCounts.OrderByDescending(k => k.Value).First().Key);
        }
        private Tuple<double, string> GiniScore(List<Instance> instances)
        {
            Dictionary<string, int> totalCalsses = new Dictionary<string, int>();       
            for (int i = 0; i < instances.Count; i++)                                     
            {
                if (!totalCalsses.ContainsKey(instances[i].Class))
                {
                    totalCalsses.Add(instances[i].Class, 1);
                }
                else {
                    totalCalsses[instances[i].Class] += 1;
                }                        
            }
            return GiniScore(totalCalsses);
        }
        /// <summary>
        /// Returns random features
        /// </summary>
        /// <returns></returns>
        private IEnumerable<Feature> RandomizeFeatures()
        {
            if (_model != null && _model.Features != null)
            {
                return _model.Features.OrderBy(arg => Guid.NewGuid()).Take(_randomFeaturesNum);
            }
            return null;
        }
    }
}
