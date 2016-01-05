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
            Dictionary<int, List<ClassificationInstance>> instanceData = new Dictionary<int, List<ClassificationInstance>>();
            //for each instance add the instance index for later reference and the list of instance values
            for (var instanceIndex=0; instanceIndex < instances.Count; instanceIndex++)
            {
                instanceData.Add(instanceIndex, new List<ClassificationInstance>());
                instanceData[instanceIndex].Add(new ClassificationInstance(instances[instanceIndex].Class, instances[instanceIndex].Values, classes));
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

        public Dictionary<int, string> EvaluateRegression(ObservableCollection<Instance> instances, ObservableCollection<string> classes)
        {
            //########### Prepare #############
            Dictionary<int, List<RegressionInstance>> instanceData = new Dictionary<int, List<RegressionInstance>>();
            //for each instance add the instance index for later reference and the list of instance values
            for (var instanceIndex = 0; instanceIndex < instances.Count; instanceIndex++)
            {
                instanceData.Add(instanceIndex, new List<RegressionInstance>());
                instanceData[instanceIndex].Add(new RegressionInstance(instances[instanceIndex].Number, instances[instanceIndex].Values));
            }

            //########### Evaluate ############
            //run each instance value evaluation for all forest trees (N features * M trees * X instances)
            EvaluateTreesRegression(instanceData);

            //########### Summarize ###########
            var result = new Dictionary<int, string>();
            //made a decision for every instance: aggregate all instance evaluated values into class counts. the highest class is the winner.
            foreach (var entry in instanceData)
            {
                //initialize counters
                var instanceClassCounters = new Dictionary<string, int>();
                instanceClassCounters.Add("yes", 0);
                instanceClassCounters.Add("no", 0);

                //aggregate counts from instance values
                var instanceValues = entry.Value;
                foreach (var instVal in instanceValues)
                {
                    foreach (var vote in instVal.Votes)
                    {
                        instanceClassCounters[vote.Key] += vote.Value;
                    }
                }

                //decision
                var mostVoted = 0;
                var mostVotedClass = string.Empty;
                foreach (var counter in instanceClassCounters)
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

        public void EvaluateTrees(Dictionary<int,List<ClassificationInstance>> instanceData)
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
        public void EvaluateTreesRegression(Dictionary<int, List<RegressionInstance>> instanceData)
        {
            foreach (var tree in Trees)
            {
                foreach (var instanceEntry in instanceData)
                {
                    foreach (var instVal in instanceEntry.Value)
                    {
                        EvaluateNodeRegression(tree.RootNode, instVal);
                    }
                }
            }
        }

        public void EvaluateNode(ITreeNode<DecisionNode> node, ClassificationInstance instanceValue)
        {
            //if reached the leaf node or the split value equals to the current value, stop the evaluation and vote for the appropriate class
            if (node.IsLeaf)// || (node.Item.SplitValue.CompareTo(instanceValue.Value)==0))
            {
                instanceValue.ClassVotes[node.Item.Classification]++;
                return;
            }

            //go left
            if (node.Item.SplitValue.CompareTo(instanceValue.Values[node.Item.SplitFeatureIndex]) >= 0)
            {
                EvaluateNode(node.Left, instanceValue);
            }
            //go right
            else
            {
                EvaluateNode(node.Right, instanceValue);
            }
        }
        public void EvaluateNodeRegression(ITreeNode<DecisionNode> node, RegressionInstance instanceValue)
        {
            //if reached the leaf node or the split value equals to the current value, stop the evaluation and vote for the appropriate class
            if (node.IsLeaf)// || (node.Item.SplitValue.CompareTo(instanceValue.Value)==0))
            {
                double realNum = instanceValue.Number;
                var diff = System.Math.Abs( (node.Item.PredictedMean - realNum) / System.Math.Sqrt(node.Item.PredictedError) );

                string vote = diff <= 2.5 ? "yes" : "no";
                instanceValue.Votes[vote]++;
                return;
            }

            //go left
            if (node.Item.SplitValue.CompareTo(instanceValue.Values[node.Item.SplitFeatureIndex]) >= 0)
            {
                EvaluateNodeRegression(node.Left, instanceValue);
            }
            //go right
            else
            {
                EvaluateNodeRegression(node.Right, instanceValue);
            }
        }
    }
}
