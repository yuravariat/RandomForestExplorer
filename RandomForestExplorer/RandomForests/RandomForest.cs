using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RandomForestExplorer.DecisionTrees;

namespace RandomForestExplorer.RandomForests
{
    class RandomForest
    {
        public List<DecisionTree> Trees;
        public TreeOutput OutputType;
    }
}
