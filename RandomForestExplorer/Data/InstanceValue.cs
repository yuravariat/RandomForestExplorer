using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace RandomForestExplorer.Data
{
    public class InstanceValue
    {
        public ConcurrentDictionary<string,int> ClassVotes { get; set; }
        public string Class { get; private set; }
        public double Value { get; private set; }
        public int FeatureIndex { get; private set; }

        public InstanceValue(int featureIndex, string @class, double value, IEnumerable<string> classes)
        {
            FeatureIndex = featureIndex;
            Class = @class;
            Value = value;
            ClassVotes = new ConcurrentDictionary<string, int>();
            foreach(var cls in classes)
            {
                ClassVotes.TryAdd(cls, 0);
            }
        }
    }
}
