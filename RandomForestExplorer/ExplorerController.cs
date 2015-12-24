using System;
using System.IO;
using System.Linq;
using RandomForestExplorer.Data;
using RandomForestExplorer.RandomForests;

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
        }

        private void OnStart()
        {
            _solver = new RandomForestSolver(_model,
                                            _view.NumberOfTrees, 
                                            _view.NumberOfFeatures,
                                            _view.TreeDepth,
                                            1,
                                            _view.PercentSplit);
            _solver.Run();
        }

        private void OnStop()
        {
            
        }

        private void OnFileLoad(string p_fileName)
        {
            FillModel(p_fileName);
        }

        private void FillModel(string p_fileName)
        {
            const string relationTag = "@relation";
            const string attributeTag = "@attribute";
            const string dataTag = "@data";
            const string classTag = "class";

            var lines = File.ReadAllLines(p_fileName);

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
