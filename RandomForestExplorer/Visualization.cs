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
    public partial class Visualization : UserControl
    {
        public Visualization(Tuple<string [], 
                             Dictionary<int, string>, 
                             ObservableCollection<Instance>, 
                             List<Instance>> tuple)
        {
            InitializeComponent();
            Dock = DockStyle.Fill;
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
                    ChartType = SeriesChartType.FastPoint
                });
            }

            var classValues = new Dictionary<string, List<KeyValuePair<double, double>>>();
            foreach (var instance in trainingInstances)
            {
                if (!classValues.ContainsKey(instance.Class))
                    classValues.Add(instance.Class, new List<KeyValuePair<double, double>>());

                classValues[instance.Class].Add(new KeyValuePair<double, double>(instance.Values[0], instance.Values[1]));
            }

            foreach (var keyValuePair in classValues)
            {
                var x = new List<double>();
                var y = new List<double>();
                foreach (var doublePairs in keyValuePair.Value)
                {
                    x.Add(doublePairs.Key);
                    y.Add(doublePairs.Value);
                }

                seriesMap[keyValuePair.Key].Points.DataBindXY(x, y);
                trainingChart.Series.Add(seriesMap[keyValuePair.Key]);
            }
        }

        private void InitTestChart(string [] classes,
                                   Dictionary<int, string> testResults,
                                   ObservableCollection<Instance> testInstances)
        {
            var classValues = new Dictionary<string, List<KeyValuePair<double, double>>>();
            foreach (var result in testResults)
            {
                var instance = testInstances[result.Key];
                var classification = result.Value;

                if (!instance.Class.Equals(classification))
                    classification = string.Format("{0} misclassified as {1}", instance.Class, classification);

                if (!classValues.ContainsKey(classification))
                    classValues.Add(classification, new List<KeyValuePair<double, double>>());

                classValues[classification].Add(new KeyValuePair<double, double>(instance.Values[0], instance.Values[1]));
            }

            var seriesMap = new Dictionary<string, Series>();
            foreach (var @class in classValues.Keys)
            {
                seriesMap.Add(@class, new Series(@class)
                {
                    ChartType = SeriesChartType.FastPoint
                });
            }

            foreach (var keyValuePair in classValues)
            {
                var x = new List<double>();
                var y = new List<double>();
                foreach (var doublePairs in keyValuePair.Value)
                {
                    x.Add(doublePairs.Key);
                    y.Add(doublePairs.Value);
                }

                seriesMap[keyValuePair.Key].Points.DataBindXY(x, y);
                testChart.Series.Add(seriesMap[keyValuePair.Key]);
            }
        }
    }
}
