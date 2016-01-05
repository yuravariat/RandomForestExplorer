using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace RandomForestExplorer.Data
{
    public class ClassificationInstance
    {
        public ConcurrentDictionary<string,int> ClassVotes { get; set; }
        public ConcurrentBag<Tuple<bool,double>> RegressionVotes { get; set; }
        public string Class { get; private set; }
        public double Number { get; private set; }
        public List<double> Values { get; private set; }
        public int Index { get; private set; }

        public ClassificationInstance(int @index, string @class, List<double> @values, IEnumerable<string> classes)
        {
            Index = @index;
            Class = @class;
            Values = @values;
            ClassVotes = new ConcurrentDictionary<string, int>();
            foreach(var cls in classes)
            {
                ClassVotes.TryAdd(cls, 0);
            }
        }
        public ClassificationInstance(int @index, double @number, List<double> @values)
        {
            Index = @index;
            Values = @values;
            Number = @number;
            RegressionVotes = new ConcurrentBag<Tuple<bool, double>>();
        }
    }
}
