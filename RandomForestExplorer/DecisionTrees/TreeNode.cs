using System;
using System.Collections.Generic;
using System.Linq;

namespace RandomForestExplorer.DecisionTrees
{
    [Serializable]
    class TreeNode : ITreeNode<DecisionNode>
    {
        private ITreeNode<DecisionNode> _parent;
        private DecisionNode _item;
        private ITreeNode<DecisionNode> _right;
        private ITreeNode<DecisionNode> _left;

        public DecisionNode Item
        {
            get
            {
                return _item;
            }
            set
            {
                _item = value;
            }
        }

        public ITreeNode<DecisionNode> Parent
        {
            get
            {
                return _parent;
            }
            set
            {
                _parent = value;
            }
        }
        public ITreeNode<DecisionNode> Right
        {
            get
            {
                return _right;
            }
            set
            {
                _right = value;
            }
        }
        public ITreeNode<DecisionNode> Left
        {
            get
            {
                return _left;
            }
            set
            {
                _left = value;
            }
        }

        public IEnumerable<ITreeNode<DecisionNode>> Children()
        {
            return (new List<ITreeNode<DecisionNode>>() { _right, _left }).Where(a => a != null);
        }

        public bool IsRoot
        {
            get { return _parent == null; }
        }

        public bool IsLeaf
        {
            get { return _right == null && _left == null; }
        }

        public int GetHeight()
        {
            int height = 0;
            var parent = Parent;

            while (parent != null)
            {
                height++;
                parent = parent.Parent;
            }

            return height;
        }

        public SortedSet<MatrixItem> Values { get; set; }
    }

    class MatrixItem
    {
        public int Column { get; set; }
        public double Value { get; set; }
        public string Class { get; set; }
    }
}
