using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace RandomForestExplorer.Data
{
    public class ClassificationInstance : Instance
    {
        public ConcurrentDictionary<String, int> ClassCounter { get; set; }
        public Dictionary<String, int> FeaturesIndexes { get; set; }
    }
}
