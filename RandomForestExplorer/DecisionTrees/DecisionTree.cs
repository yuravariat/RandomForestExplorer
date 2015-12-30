using RandomForestExplorer.Data;
using System;

namespace RandomForestExplorer.DecisionTrees
{
    [Serializable]
    class DecisionTree
    {
        public TreeOutput OutputType;
        public TreeNode RootNode;
        public string ClassifyInstance(ClassificationInstance inst, TreeNode node = null)
        {
            if (node == null)
            {
                node = RootNode;
            }
            if (node.IsLeaf)
            {
                return node.Item.Clacification;
            }
            string feature = node.Item.SplitFeature;
            if(inst.Values[inst.FeaturesIndexes[feature]] > node.Item.SplitValue)
            {
                return ClassifyInstance(inst, (TreeNode)node.Right);
            }
            else
            {
                return ClassifyInstance(inst, (TreeNode)node.Left);
            }
        }
    }
}
