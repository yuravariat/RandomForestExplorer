using System;

namespace RandomForestExplorer.DecisionTrees
{
    [Serializable]
    class DecisionTree
    {
        public TreeOutput OutputType;
        public TreeNode RootNode;
        public int MaxDepth(TreeNode node)
        {
            if (node == null)
                return 0;
            if (node.IsLeaf)
                return 1;
            return 1 + Math.Max(MaxDepth((TreeNode)node.Left), MaxDepth((TreeNode)node.Left));
        }
    }
}
