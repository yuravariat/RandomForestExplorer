using System.Collections.Concurrent;
using RandomForestExplorer.DecisionTrees;
using RandomForestExplorer.Data;
using System.Collections.Generic;
using System.Linq;
using System;
using System.Collections.ObjectModel;

namespace RandomForestExplorer.RandomForests
{
    class RandomForest
    {
        public ConcurrentBag<DecisionTree> Trees { get; set; }

        public RandomForest()
        {
            Trees = new ConcurrentBag<DecisionTree>();
        }

        public Dictionary<Instance, string> Evaluate(ObservableCollection<Instance> instances, ObservableCollection<string> classes)
        {
            Dictionary<Instance, List<InstanceValue>> instanceData = new Dictionary<Instance, List<InstanceValue>>();

            for (var i=0; i < instances.Count; i++)
            {
                instanceData.Add(instances[i], new List<InstanceValue>());
                foreach (var value in instances[i].Values)
                {
                    instanceData[instances[i]].Add(new InstanceValue(instances[i].Class, value, classes));
                }
            }

            //evaluate internal instance values
            EvaluateTrees(instanceData);

            var result = new Dictionary<Instance, string>();
            //summarize all values per instance
            foreach(var entry in instanceData)
            {
                //initialize counters
                var instanceClassCounters = new Dictionary<string, int>();
                foreach (var @class in classes)
                {
                    instanceClassCounters.Add(@class, 0);
                }

                //aggregate counts from instance values
                foreach(var instVal in entry.Value)
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
                    if (counter.Value > mostVoted)
                    {
                        mostVoted = counter.Value;
                        mostVotedClass = counter.Key;
                    }
                }

                result.Add(entry.Key, mostVotedClass);
            }

            return result;
        }

        public void EvaluateTrees(Dictionary<Instance,List<InstanceValue>> instanceData)
        {
            foreach(var tree in Trees)
            {
                foreach(var instanceEntry in instanceData)
                {
                    foreach(var instVal in instanceEntry.Value)
                    {
                        Classify(tree.RootNode, instVal);
                    }
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
