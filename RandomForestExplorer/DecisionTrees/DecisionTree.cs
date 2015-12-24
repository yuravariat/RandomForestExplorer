using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RandomForestExplorer.DecisionTrees
{
    [Serializable]
    class DecisionTree
    {
        public TreeOutput OutputType;
        public TreeNode RootNode;
    }
}
