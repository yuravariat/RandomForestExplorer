using RandomForestExplorer.Data;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace RandomForestExplorer
{
    public partial class Visualization : Form
    {
        public Visualization(Tuple<string [], 
                             Dictionary<int, string>, 
                             ObservableCollection<Instance>, 
                             List<Instance>> tuple)
        {
            InitializeComponent();
            InitTrainingChart(tuple.Item1, tuple.Item4);
            InitTestChart(tuple.Item1, tuple.Item2, tuple.Item3);
        }

        private void InitTrainingChart(string[] classes, List<Instance> trainingInstances)
        {
            var seriesMap = new Dictionary<string, Series>();

            foreach (var @class in classes)
            {
                seriesMap.Add(@class, new Series(@class)
                {
                    ChartType = SeriesChartType.Point
                });
            }

            var classValues = new Dictionary<string, List<double>>();
            foreach (var instance in trainingInstances)
            {
                if (!classValues.ContainsKey(instance.Class))
                    classValues.Add(instance.Class, new List<double>());

                classValues[instance.Class].AddRange(instance.Values);
            }

            foreach (var keyValuePair in classValues)
            {
                seriesMap[keyValuePair.Key].Points.DataBindY(classValues[keyValuePair.Key]);
                trainingChart.Series.Add(seriesMap[keyValuePair.Key]);
            }
        }

        private void InitTestChart(string [] classes,
                                   Dictionary<int, string> testResults,
                                   ObservableCollection<Instance> testInstances)
        {
            var seriesMap = new Dictionary<string, Series>();

            foreach (var @class in classes)
            {
                seriesMap.Add(@class, new Series(@class)
                {
                    ChartType = SeriesChartType.Point
                });
            }


            var classValues = new Dictionary<string, List<double>>();
            foreach (var result in testResults)
            {
                var instance = testInstances[result.Key];
                var classification = result.Value;

                if (!classValues.ContainsKey(classification))
                    classValues.Add(classification, new List<double>());

                classValues[classification].AddRange(instance.Values);
            }

            foreach (var keyValuePair in classValues)
            {
                seriesMap[keyValuePair.Key].Points.DataBindY(classValues[keyValuePair.Key]);
                testChart.Series.Add(seriesMap[keyValuePair.Key]);
            }
        }
    }
}
