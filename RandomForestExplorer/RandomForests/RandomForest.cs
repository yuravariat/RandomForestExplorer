using System.Collections.Concurrent;
using RandomForestExplorer.DecisionTrees;
using RandomForestExplorer.Data;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System;

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
            //########### Evaluate Instances #############
            List<ClassificationInstance> instanceData = EvaluateInstances(instances, TreeOutput.ClassifiedCategory, classes);

            //########### Summarize ###########
            var result = new Dictionary<int, string>();
            //made a decision for every instance: aggregate all instance evaluated values into class counts. the highest class is the winner.
            foreach (var entry in instanceData)
            {
                //initialize counters
                var instanceClassCounters = new Dictionary<string, int>();
                foreach (var @class in classes)
                {
                    instanceClassCounters.Add(@class, 0);
                }

                //aggregate counts from instance values

                foreach (var vote in entry.ClassVotes)
                {
                    instanceClassCounters[vote.Key] += vote.Value;
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
                result.Add(entry.TreeID, mostVotedClass);
            }

            return result;
        }

        public Dictionary<int, Tuple<bool,double>> EvaluateRegression(ObservableCollection<Instance> instances, ObservableCollection<string> classes)
        {
            //########### Evaluate Instances #############
            List<ClassificationInstance> instanceData = EvaluateInstances(instances, TreeOutput.Regression);

            //########### Summarize ###########
            var result = new Dictionary<int, Tuple<bool, double>>();
            //made a decision for every instance: aggregate all instance evaluated values into class counts. the highest class is the winner.
            foreach (var entry in instanceData)
            {
                //decision
                var numOfPredictionSuccess = 0;
                double sumOfErrors = 0;
                foreach (var tup in entry.RegressionVotes)
                {
                    if (tup.Item1)
                    {
                        numOfPredictionSuccess++;
                    }
                    sumOfErrors += tup.Item2;
                }
                bool ifPredictionSuccess = numOfPredictionSuccess > entry.RegressionVotes.Count;
                double averageError = sumOfErrors / (double)entry.RegressionVotes.Count;
                Tuple<bool, double> aggrigation = new Tuple<bool, double>(ifPredictionSuccess, averageError);
                result.Add(entry.TreeID, aggrigation);
            }

            return result;
        }
        private List<ClassificationInstance> EvaluateInstances(ObservableCollection<Instance> instances, TreeOutput treeType, ObservableCollection<string> classes = null)
        {
            //########### Prepare #############
            List<ClassificationInstance> instanceData = new List<ClassificationInstance>();
            //for each instance 
            for (var i = 0; i < instances.Count; i++)
            {
                if (treeType == TreeOutput.ClassifiedCategory)
                {
                    instanceData.Add(new ClassificationInstance(i, instances[i].Class, instances[i].Values, classes));
                }
                else
                {
                    instanceData.Add(new ClassificationInstance(i, instances[i].Number, instances[i].Values));
                }
            }

            //########### Evaluate ############
            //run each instance value evaluation for all forest trees (N features * M trees * X instances)
            EvaluateTrees(instanceData);

            return instanceData;
        }
        public void EvaluateTrees(List<ClassificationInstance> instanceData)
        {
            foreach (var tree in Trees)
            {
                foreach (var instanceEntry in instanceData)
                {
                    if (tree.OutputType == TreeOutput.ClassifiedCategory)
                    {
                        EvaluateNode(tree.RootNode, instanceEntry);
                    }
                    else
                    {
                        EvaluateNodeRegression(tree.RootNode, instanceEntry);
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
        public void EvaluateNodeRegression(ITreeNode<DecisionNode> node, ClassificationInstance instanceValue)
        {
            //if reached the leaf node or the split value equals to the current value, stop the evaluation and vote for the appropriate class
            if (node.IsLeaf)// || (node.Item.SplitValue.CompareTo(instanceValue.Value)==0))
            {
                double realNum = instanceValue.Number;
                var diff = System.Math.Abs((node.Item.PredictedMean - realNum) / System.Math.Sqrt(node.Item.PredictedError));

                instanceValue.RegressionVotes.Add(new Tuple<bool, double>(diff <= 2.5, diff));
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
