using System;
using System.IO;
using System.Linq;
using RandomForestExplorer.Data;
using RandomForestExplorer.RandomForests;
using System.Collections.Generic;
using System.Text;
using System.Collections.ObjectModel;

namespace RandomForestExplorer
{
    public class ExplorerController : IDisposable
    {
        private Explorer _view;
        private DataModel _model;
        private RandomForestSolver _solver;

        public ExplorerController(Explorer p_view, DataModel p_model)
        {
            _view = p_view;
            _model = p_model;

            _view.OnStart = OnStart;
            _view.OnStop = OnStop;
            _view.OnFileLoad = OnFileLoad;
            _view.OnTrainingFileLoad = OnTrainingFileLoad;
        }

        private void OnStart()
        {
            if (!TryBuildTrainingData())
                return;

            _view.Write("Started training...");

            _model.IsReady = false;
            _model.InProcess = true;

            _view.ProgressBar.Value = 0;
            _view.ProgressBar.Minimum = 0;
            _view.ProgressBar.Maximum = _view.NumberOfTrees;

            _solver = new RandomForestSolver(_model,
                                            _view.NumberOfTrees, 
                                            _view.NumberOfFeatures,
                                            _view.TreeDepth,
                                            1);

            _solver.OnForestCompletion = new Action<int>(OnForestCompletion);
            _solver.OnEvaluationCompletion = new Action<Tuple<double[,], Dictionary<int, string>>>(OnEvaluationCompletion);
            _solver.OnEvaluationCompletionRegression = new Action<Dictionary<int, Tuple<bool, double>>>(OnEvaluationCompletionRegression);
            _solver.OnProgress = new Action(OnSolverProgress);
            _solver.OnError = (ex) => { _view.Write(string.Format("{0}\n{1}", ex.Message, ex.StackTrace)); };
            _solver.Run();
        }

        private void OnStop()
        {
            _solver.Cancel();
            OnForestCompletion(_solver.GetTreeCount());
        }

        private void OnSolverProgress()
        {
            _view.ProgressBar.Increment(1);
        }

        private void OnForestCompletion(int totalTrees)
        {
            _model.IsReady = true;
            _model.InProcess = false;

            var message = "Completed training";
            var info = string.Format("{0}\nTotal trees: {1}\nTree Depth: {2}\nRandom Features used: {3}\nTotal Features: {4}",
                message, 
                _solver.GetTreeCount(), 
                _solver.GetTreeDepth()==int.MaxValue ? "unlimited" : _solver.GetTreeDepth().ToString(), 
                _solver.GetNumOfFeatures(),
                _model.Features.Count);

            var strBld = new StringBuilder("\t\n=== Most important features ===\n");
            var sortedFeatures = _solver.GetFeautesSortedByImportance();
            foreach (var feature in sortedFeatures)
            {
                strBld.AppendLine("a" + (feature.Item1 + 1));
            }
            strBld.AppendLine();

            _view.Write(info + strBld.ToString());
        }

        private void OnEvaluationCompletion(Tuple<double[,], Dictionary<int, string>> tuple)
        {
            var strBld = new StringBuilder("Completed evaluation.\nThe results are:");
            strBld.AppendLine();
            strBld.AppendLine();
            strBld.Append("\t=== Confusion Matrix ===");
            strBld.AppendLine();
            strBld.Append("\t\t");

            var confusionMatrix = tuple.Item1;
            var evaluationData = tuple.Item2;

            for (var i = 0; i < confusionMatrix.GetLength(0); i++)
            {
                strBld.AppendFormat("{0}\t", _model.Classes[i]);
            }

            strBld.AppendLine();

            for (var i = 0; i < confusionMatrix.GetLength(0); i++)
            {
                strBld.AppendFormat("\tclass={0} | \t", _model.Classes[i]);
                for (var j = 0; j < confusionMatrix.GetLength(0); j++)
                {
                    strBld.AppendFormat("{0}\t", confusionMatrix[i, j]);
                }
                strBld.AppendLine();
            }

            _view.Write(strBld.ToString());

            var visual = new Visualization(new Tuple<string [],
                Dictionary<int, string>,
                ObservableCollection<Instance>,
                List<Instance>>(_model.Classes.ToArray(),tuple.Item2,_model.Instances, _model.TrainingInstances));

            //visual.ShowDialog(_view);
            _view.ShowVisualization(visual);
        }
        private void OnEvaluationCompletionRegression(Dictionary<int, Tuple<bool, double>> results)
        {
            var numOfPredictionSuccess = 0;
            double sumOfErrors = 0;

            foreach(var inst in results)
            {
                if (inst.Value.Item1)
                {
                    numOfPredictionSuccess++;
                }
                sumOfErrors += inst.Value.Item2;
            }
            double correlationCoefficient = numOfPredictionSuccess / (double)results.Count;
            double meanOfErrors = sumOfErrors / (double)results.Count;

            var strBld = new StringBuilder("Completed evaluation.\nThe results are:");
            strBld.AppendLine();
            strBld.AppendLine("============ Regression tree results ===========");
            strBld.AppendLine("Correlation coefficient\t" + correlationCoefficient);
            strBld.AppendLine("Mean error\t" + meanOfErrors);

            _view.Write(strBld.ToString());
        }

        private void OnFileLoad(string p_fileName)
        {
            FillModel(p_fileName);
            _model.TrainingEnabled = true;
            _model.TrainingFromFile = true;
            _model.IsReady = true;

            var fileName = Path.GetFileNameWithoutExtension(p_fileName);
            var fileExtension = Path.GetExtension(p_fileName);
            var trainingFileName = fileName.EndsWith("2") ? fileName.Replace("2", "") : fileName + "2";
            var trainingFilePath = Path.Combine(Path.GetDirectoryName(p_fileName), trainingFileName);
            trainingFilePath += fileExtension;
            OnTrainingFileLoad(trainingFilePath);

            // For test
            //TreeBuilder tb = new TreeBuilder(_model, 4, 0, 7);
            //var tree = tb.Build();
        }

        private void OnTrainingFileLoad(string p_fileName)
        {
            _model.TrainingFileName = p_fileName;
        }

        private bool TryBuildTrainingData()
        {
            _model.TrainingInstances.Clear();
            if (_model.TrainingFromData)
            {
                var percent = _view.PercentSplit;

                var trainingInstancesCount = (int)(percent * _model.Instances.Count) / 100;
                _model.TrainingInstances.AddRange(_model.Instances.Take(trainingInstancesCount));
            }
            else
            {
                if (string.IsNullOrWhiteSpace(_model.TrainingFileName))
                {
                    System.Windows.Forms.MessageBox.Show(_view,
                                                         "No training file was specified.",
                                                         "Error",
                                                         System.Windows.Forms.MessageBoxButtons.OK,
                                                         System.Windows.Forms.MessageBoxIcon.Error);
                    return false;
                }

                if (!Path.GetFileNameWithoutExtension(_model.FileName).Replace("2", "").
                    Equals(Path.GetFileNameWithoutExtension(_model.TrainingFileName).Replace("2", "")))
                {
                    System.Windows.Forms.MessageBox.Show(_view,
                                     "The data and training files do not match.",
                                     "Error",
                                     System.Windows.Forms.MessageBoxButtons.OK,
                                     System.Windows.Forms.MessageBoxIcon.Error);
                    return false;
                }

                var lines = File.ReadAllLines(_model.TrainingFileName);
                foreach (var line in lines)
                {
                    if (line.StartsWith("@"))
                        continue;
                    var segments = line.TrimEnd().Split(new[] { ' ' });
                    if (segments.Length == 0)
                        continue;
                    var instance = new Instance { Class = segments.Last() };
                    for (var i = 0; i < segments.Length - 1; i++)
                    {
                        instance.Values.Add(double.Parse(segments[i]));
                    }
                    _model.TrainingInstances.Add(instance);
                }
            }
            return true;
        }

        private void FillModel(string p_fileName)
        {
            const string relationTag = "@relation";
            const string attributeTag = "@attribute";
            const string dataTag = "@data";
            const string classTag = "class";
            const string attrTag = "attr";

            var lines = File.ReadAllLines(p_fileName);

            _model.Reset();
            _model.FileName = p_fileName;

            bool isData = false;
            int attrCounter = 1;
            foreach (var line in lines)
            {
                var segments = line.TrimEnd().Split(new[] { ' ' });
                if (segments.Length == 0)
                    continue;

                if (isData)
                {
                    Instance instance = null;
                    if(_model.DataType == DecisionTrees.TreeOutput.ClassifiedCategory)
                    {
                        instance = new Instance { Class = segments.Last() };
                    }
                    else
                    {
                        instance = new Instance { Number = double.Parse(segments.Last()) };
                    }
                    for (var i = 0; i < segments.Length - 1; i++)
                    {
                        instance.Values.Add(double.Parse(segments[i]));
                    }
                    _model.Instances.Add(instance);
                    continue;
                }

                switch (segments.First())
                {
                    case relationTag:
                        _model.Relation = segments[1];
                        break;
                    case attributeTag:
                        if (segments[1] == classTag)
                        {
                            _model.DataType = DecisionTrees.TreeOutput.ClassifiedCategory;
                            var classes = segments[2].Replace("{", "").Replace("}", "").Split(new[] {','});
                            foreach (var @class in classes)
                            {
                                _model.Classes.Add(@class);
                            }
                        }
                        else if (segments[1] == attrTag)
                        {
                            _model.DataType = DecisionTrees.TreeOutput.Regression;
                        }
                        else
                        {                            
                            _model.Features.Add(new Feature
                            {
                                ID = attrCounter++,
                                Name = segments[1],
                                Type = segments[2]
                            });
                        }
                        break;
                    case dataTag:
                        isData = true;
                        break;
                }
            }
        }

        public void Dispose()
        {
            _view.OnStart = null;
            _view.OnStop = null;
            _view.OnFileLoad = null;
            _view.Dispose();
            _view = null;

            if (_solver != null)
            {
                _solver.OnForestCompletion = null;
                _solver.OnEvaluationCompletion = null;
                _solver.OnProgress = null;
            }

            _solver = null;
            _model = null;
        }
    }
}
