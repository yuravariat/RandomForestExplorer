using System.Collections.Generic;

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
        public List<double> Values { get; set; }      
    }
}