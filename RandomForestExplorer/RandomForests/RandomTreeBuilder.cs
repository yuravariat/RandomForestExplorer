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
        private TreeNode CreateNode(List<Instance> instances)
        {
            TreeNode node = new TreeNode();
            node.Item = new DecisionNode();
            // stop condition
            var giniScore = GiniScore(instances,0, instances.Count);

            // Gini score of the group is 0, the group is pure. or there is too few instances 
            if (giniScore.Item1==0 || instances.Count <= 20)
            {
                node.Item.Clacification = giniScore.Item2;
                return node;
            }

            // get random features
            var features = RandomizeFeatures();


            double minScore = Double.MaxValue;
            double splitValue = 0;
            Feature minScoreFeature = null;

            //Metric
            Stopwatch swSplit = new Stopwatch();
            Stopwatch swTotalAllSplits = new Stopwatch();
            List<double> miliseconds = new List<double>(instances.Count);

            swTotalAllSplits.Start();

            // Loop thought all feature and all values.
            foreach (Feature feature in features)
            {
                instances.Sort((a, b) => a.Values[feature.ID].CompareTo(b.Values[feature.ID]));

                /////////////////// Dictnary approach //////////////////////////////////////////
                Dictionary<string, int> totalCalssesG1 = new Dictionary<string, int>();       //
                Dictionary<string, int> totalCalssesG2 = new Dictionary<string, int>();       //
                foreach (string classe in _model.Classes)                                     //
                {                                                                             //
                    totalCalssesG1.Add(classe, 0);                                            //
                    totalCalssesG2.Add(classe, 0);                                            //
                }                                                                             //
                for (int i = 0; i < instances.Count; i++)                                     //
                {                                                                             //
                    totalCalssesG2[instances[i].Class] += 1;                                  //
                }                                                                             //
                                                                                              //
                int totalGroup1 = 1;                                                          //
                int totalGroup2 = instances.Count - totalGroup1;                              //
                totalCalssesG1[instances[0].Class] += 1;                                      //
                totalCalssesG2[instances[0].Class] -= 1;                                      //
                ////////////////////////////////////////////////////////////////////////////////

                for (int i = 1; i < instances.Count; i++)
                {
                    swSplit.Reset();
                    swSplit.Start();

                    // if previous value was the same dont calculate gini.
                    if (i > 0 && instances[i].Values[feature.ID] == instances[i - 1].Values[feature.ID])
                    {
                        continue;
                    }

                    // var totalScore = GiniScore(instances, 0, i).Item1 + GiniScore(instances, i, instances.Count).Item1;

                    ///////////// Dictnary approach ////////////////////////////////////////////
                    totalCalssesG1[instances[0].Class] += 1;                                  //
                    totalCalssesG2[instances[0].Class] -= 1;                                  //
                    totalGroup1+= 1;                                                          //
                    totalGroup2-= 1;                                                          //
                                                                                              //
                    var max1 = totalCalssesG1.OrderByDescending(k => k.Value).First();        //
                    var P1 = (double)max1.Value / (double)totalGroup1;                        //
                    var gini1 = (2 * P1 * (1 - P1));                                          //
                                                                                              //
                    var max2 = totalCalssesG2.OrderByDescending(k => k.Value).First();        //
                    var P2 = (double)max2.Value / (double)totalGroup2;                        //
                    var gini2 = (2 * P2 * (1 - P2));                                          //
                                                                                              //
                    var totalScore = gini1 + gini2;                                           //
                    ////////////////////////////////////////////////////////////////////////////

                    if (totalScore < minScore)
                    {
                        minScore = totalScore;
                        splitValue = instances[i].Values[feature.ID];
                        minScoreFeature = feature;
                    }

                    swSplit.Stop();
                    miliseconds.Add(swSplit.Elapsed.TotalMilliseconds);
                }
            }
            swTotalAllSplits.Stop();
            double totalMilisecondsAllSplits = swTotalAllSplits.Elapsed.TotalMilliseconds;

            // There is no improvment no need to split.
            // Yura: not sure about this
            if (minScore >= giniScore.Item1)
            {
                node.Item.Clacification = giniScore.Item2;
                return node;
            }

            // Create decision item
            node.Item = new DecisionNode();
            node.Item.SplitFeature = minScoreFeature.Name;
            node.Item.SplitValue = splitValue;

            // Create right and left nodes
            node.Left = CreateNode(instances.Where(i => i.Values[minScoreFeature.ID] < splitValue).ToList());
            node.Right = CreateNode(instances.Where(i => i.Values[minScoreFeature.ID] >= splitValue).ToList());
            return node;

        }
        /// <summary>
        /// Gini score calculation, returns gini score and most dominant class.
        /// </summary>
        /// <param name="instances"></param>
        /// <returns></returns>
        private Tuple<double,string> GiniScore(List<Instance> instances,int startIndex, int endIndex)
        {
            
            Dictionary<string, int>  counts = new Dictionary<string, int>();
            foreach (string classe in _model.Classes)
            {
                counts.Add(classe, 0);
            }
            for (int i = startIndex; i < endIndex && i < instances.Count; i++)
            {
                counts[instances[i].Class] += 1;
            }
            var max = counts.OrderBy(k => k.Value).First();
            var P1 = (double)max.Value / ((double)(endIndex - startIndex));
            return new Tuple<double, string>((2 * P1 * (1 - P1)), max.Key);

            //var MaxGroup = instances.Skip(startIndex).Take(endIndex - startIndex).GroupBy(g => g.Class).OrderBy(g => g.Count()).First();
            //var P1 = (double)MaxGroup.Count() / (double)instances.Count();
            //return new Tuple<double, string>((2 * P1 * (1 - P1)), MaxGroup.Key);
        }
        /// <summary>
        /// Returns random features
        /// </summary>
        /// <returns></returns>
        private IEnumerable<Feature> RandomizeFeatures()
        {
            if (_model != null && _model.Features != null)
            {
                return _model.Features.Take(_randomFeaturesNum);
            }
            return null;
        }
    }
}
