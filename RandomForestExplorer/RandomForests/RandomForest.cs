using System.Collections.Concurrent;
using RandomForestExplorer.DecisionTrees;
using RandomForestExplorer.Data;
using System.Collections.Generic;

namespace RandomForestExplorer.RandomForests
{
    class RandomForest
    {
        public ConcurrentBag<DecisionTree> Trees { get; set; }

        public RandomForest()
        {
            Trees = new ConcurrentBag<DecisionTree>();
        }

        public ConcurrentBag<InstanceValue> Evaluate(IEnumerable<Instance> instances, IEnumerable<string> classes)
        {
            var bagOfValues = new ConcurrentBag<InstanceValue>();

            foreach(var instance in instances)
            {               
                foreach(var value in instance.Values)
                {
                    bagOfValues.Add(new InstanceValue(instance.Class, value, classes));
                }
            }

            EvaluateTrees(bagOfValues);

            return bagOfValues;
        }

        public void EvaluateTrees(ConcurrentBag<InstanceValue> bagOfValues)
        {
            foreach(var tree in Trees)
            {
                foreach(var instVal in bagOfValues)
                {
                    Classify(tree.RootNode, instVal);
                }
            }
        }

        public void Classify(ITreeNode<DecisionNode> node, InstanceValue instanceValue)
        {
            //if reached the leaf node or the split value equals to the current value, stop the evaluation and vote for the appropriate class
            if (node.IsLeaf)// || (node.Item.SplitValue.CompareTo(instanceValue.Value)==0))
            {
                instanceValue.ClassVotes[node.Item.Classification]++;
                return;
            }

            //go left
            if (node.Item.SplitValue.CompareTo(instanceValue.Value) >= 0)
            {
                Classify(node.Left, instanceValue);
            }
            //go right
            else
            {
                Classify(node.Right, instanceValue);
            }
        }
    }
}
