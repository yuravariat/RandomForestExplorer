using System;
using System.Linq;

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
        public string PrintTree()
        {
            string tree = this.RootNode.DescendantsAndSelf().Aggregate("",
                (bc, n) => bc + n.Ancestors().Aggregate("",
                  (ac, m) => (m.ElementsAfterSelf().Any() ? "| " : "  ") + ac,
                ac => ac + (n.ElementsAfterSelf().Any() ? "+-" : "\\-")) +
                  "f=" + ((TreeNode)n).Item.SplitFeatureIndex + ",v="+ ((TreeNode)n).Item.SplitValue + "\n");
            return tree;
        }
    }
}
