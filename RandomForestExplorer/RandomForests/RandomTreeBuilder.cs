using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RandomForestExplorer.DecisionTrees;
using RandomForestExplorer.Data;

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

                if (!randomFeaturesNum.HasValue && randomFeaturesNum.Value > _model.Features.Count - 1)
                {
                    _randomFeaturesNum = _model.Features.Count - 1;
                }
                else
                {
                    _randomFeaturesNum = (int)Math.Sqrt(_model.Features.Count);
                }
                tree.RootNode = new TreeNode();

                tree.RootNode = CreateNode(_model.Instances.ToList());

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
            var giniScore = GiniScore(instances);

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
            // Loop thought all feature and all values.
            foreach(Feature feature in features)
            {
                instances.Sort((a, b) => a.Values[feature.ID].CompareTo(b.Values[feature.ID]));
                for(int i = 1; i < instances.Count; i++)
                {
                    var firstGroup = instances.Take(i);
                    var secondGroup = instances.Skip(i).Take(instances.Count - i);

                    var totalScore = GiniScore(firstGroup).Item1 + GiniScore(secondGroup).Item1;

                    if(totalScore < minScore)
                    {
                        minScore = totalScore;
                        splitValue = instances[i].Values[feature.ID];
                        minScoreFeature = feature;
                    }
                }
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
        private Tuple<double,string> GiniScore(IEnumerable<Instance> instances)
        {
            var MaxGroup = (from inst in instances
                            let totalCount = instances.Count()
                            group inst by inst.Class into groups
                            orderby groups.Count() select groups).First();

            return new Tuple<double, string>((2 * MaxGroup.Count() * (1 - MaxGroup.Count())), MaxGroup.Key);
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
