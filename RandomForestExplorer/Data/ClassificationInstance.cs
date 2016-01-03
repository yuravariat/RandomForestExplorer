﻿using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace RandomForestExplorer.Data
{
    public class ClassificationInstance : Instance
    {
        public ConcurrentDictionary<String, int> ClassCounter { get; set; }
        public Dictionary<String, int> FeaturesIndexes { get; set; }
    }

    public class InstanceValue
    {
        public ConcurrentDictionary<string,int> ClassVotes { get; set; }
        public string Class { get; private set; }
        public double Value { get; private set; }

        public InstanceValue(string @class, double value, IEnumerable<string> classes)
        {
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