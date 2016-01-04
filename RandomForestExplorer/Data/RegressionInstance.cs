using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace RandomForestExplorer.Data
{
    public class RegressionInstance
    {
        public ConcurrentDictionary<string,int> Votes { get; set; }
        public double Number { get; private set; }
        public List<double> Values { get; private set; }

        public RegressionInstance(double @number, List<double> values)
        {
            Number = @number;
            Values = values;
            Votes = new ConcurrentDictionary<string, int>();
            Votes.TryAdd("yes", 0);
            Votes.TryAdd("no", 0);
        }
    }
}
