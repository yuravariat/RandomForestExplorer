using System;
using System.IO;
using System.Linq;
using RandomForestExplorer.Data;
using RandomForestExplorer.RandomForests;
using System.Collections.Generic;

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
            _model.IsReady = false;
            _model.InProcess = true;

            BuildTrainingData();
            _solver = new RandomForestSolver(_model,
                                            _view.NumberOfTrees, 
                                            _view.NumberOfFeatures,
                                            _view.TreeDepth,
                                            1);
            _solver.Run();

            _model.IsReady = true;
            _model.InProcess = false;
        }

        private void OnStop()
        {
            
        }

        private void OnFileLoad(string p_fileName)
        {
            FillModel(p_fileName);
            _model.TrainingEnabled = true;
            _model.TrainingFromFile = true;
            _model.IsReady = true;
        }

        private void OnTrainingFileLoad(string p_fileName)
        {
            _model.TrainingFileName = p_fileName;
        }

        private void BuildTrainingData()
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
                }
                else
                {
                    var lines = File.ReadAllLines(_model.TrainingFileName);
                    foreach (var line in lines.Skip(1))
                    {
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
            }
        }

        private void FillModel(string p_fileName)
        {
            const string relationTag = "@relation";
            const string attributeTag = "@attribute";
            const string dataTag = "@data";
            const string classTag = "class";

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
                    var instance = new Instance { Class = segments.Last() };
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
                            var classes = segments[2].Replace("{", "").Replace("}", "").Split(new[] {','});
                            foreach (var @class in classes)
                            {
                                _model.Classes.Add(@class);
                            }
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
            _model = null;
        }
    }
}
