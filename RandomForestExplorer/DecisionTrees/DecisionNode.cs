using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RandomForestExplorer.DecisionTrees
{
    [Serializable]
    class DecisionNode
    {
        // Case when it is root or internal node

        /// <summary>
        /// Feature/Column on which split will be applied.
        /// </summary>
        public string SplitFeature;
        /// <summary>
        /// Split value that splits the data into two groups. 
        /// x > value and x < value.
        /// </summary>
        public double SplitValue;

        // Case when it is leaf

        /// <summary>
        /// Class/Category
        /// </summary>
        public string Clacification;
        /// <summary>
        /// Predicted numeric value
        /// </summary>
        public decimal PredictedValue;
    }
}
