using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace RandomForestExplorer.Data
{
    public class ClassificationInstance
    {
        public ConcurrentDictionary<string,int> ClassVotes { get; set; }
        public string Class { get; private set; }
        public List<double> Values { get; private set; }

        public ClassificationInstance(string @class, List<double> values, IEnumerable<string> classes)
        {
            Class = @class;
            Values = values;
            ClassVotes = new ConcurrentDictionary<string, int>();
            foreach(var cls in classes)
            {
                ClassVotes.TryAdd(cls, 0);
            }
        }
    }
}
