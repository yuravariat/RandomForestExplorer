﻿using System.Collections.Generic;

namespace RandomForestExplorer.Data
{
    public class Instance
    {
        public Instance()
        {
            Class = string.Empty;
            Values = new List<double>();
        }

        public string Class { get; set; }
        public double Number { get; set; }
        public List<double> Values { get; set; }

        public Instance Clone()
        {
            return new Instance
            {
                Class = Class,
                Number = Number,
                Values = new List<double>(Values)
            };
        }
    }
}