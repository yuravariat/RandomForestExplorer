using System.Collections.Concurrent;
using RandomForestExplorer.DecisionTrees;
using RandomForestExplorer.Data;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace RandomForestExplorer.RandomForests
{
    class RandomForest
    {
        public ConcurrentBag<DecisionTree> Trees { get; private set; }

        public RandomForest()
        {
            Trees = new ConcurrentBag<DecisionTree>();
        }

        public Dictionary<int, string> Evaluate(ObservableCollection<Instance> instances, ObservableCollection<string> classes)
        {
            //########### Prepare #############
            Dictionary<int, List<InstanceValue>> instanceData = new Dictionary<int, List<InstanceValue>>();
            //for each instance add the instance index for later reference and the list of instance values
            for (var instanceIndex=0; instanceIndex < instances.Count; instanceIndex++)
            {
                //initialize instance entry with an empty list
                instanceData.Add(instanceIndex, new List<InstanceValue>());

                //fill the  instance values list
                for (var valueIndex = 0; valueIndex < instances[instanceIndex].Values.Count; valueIndex++)
                {
                    //valueIndex is also a feature index and we need it to match the split feature on the tree node
                    var value = instances[instanceIndex].Values[valueIndex];
                    instanceData[instanceIndex].Add(new InstanceValue(valueIndex, instances[instanceIndex].Class, value, classes));
                }
            }

            //########### Evaluate ############
            //run each instance value evaluation for all forest trees (N features * M trees * X instances)
            EvaluateTrees(instanceData);

            //########### Summarize ###########
            var result = new Dictionary<int, string>();
            //made a decision for every instance: aggregate all instance evaluated values into class counts. the highest class is the winner.
            foreach(var entry in instanceData)
            {
                //initialize counters
                var instanceClassCounters = new Dictionary<string, int>();
                foreach (var @class in classes)
                {
                    instanceClassCounters.Add(@class, 0);
                }

                //aggregate counts from instance values
                var instanceValues = entry.Value;
                foreach (var instVal in instanceValues)
                {
                    foreach(var vote in instVal.ClassVotes)
                    {
                        instanceClassCounters[vote.Key] += vote.Value;
                    }
                }

                //decision
                var mostVoted = 0;
                var mostVotedClass = string.Empty;
                foreach(var counter in instanceClassCounters)
                {
                    var votes = counter.Value;
                    var votedClass = counter.Key;

                    if (votes > mostVoted)
                    {
                        mostVoted = votes;
                        mostVotedClass = votedClass;
                    }
                }
                result.Add(entry.Key, mostVotedClass);
            }

            return result;
        }

        public void EvaluateTrees(Dictionary<int,List<InstanceValue>> instanceData)
        {
            foreach(var tree in Trees)
            {
                foreach(var instanceEntry in instanceData)
                {
                    foreach(var instVal in instanceEntry.Value)
                    {
                        EvaluateNode(tree.RootNode, instVal);
                    }
                }
            }
        }

        public void EvaluateNode(ITreeNode<DecisionNode> node, InstanceValue instanceValue)
        {
            //if reached the leaf node or the split value equals to the current value, stop the evaluation and vote for the appropriate class
            if (node.IsLeaf)// || (node.Item.SplitValue.CompareTo(instanceValue.Value)==0))
            {
                if (node.Item.SplitFeatureIndex==instanceValue.FeatureIndex)
                    instanceValue.ClassVotes[node.Item.Classification]++;
                return;
            }

            //go left
            if (node.Item.SplitValue.CompareTo(instanceValue.Value) >= 0)
            {
                EvaluateNode(node.Left, instanceValue);
            }
            //go right
            else
            {
                EvaluateNode(node.Right, instanceValue);
            }
        }
    }
}
