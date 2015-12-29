using System;
using System.Linq;
using System.Collections.Generic;

namespace RandomForestExplorer.RandomForests
{
    public class Randomizer
    {
        public List<int> Randomize(int p_seed, int p_minValue, int p_maxValue, int p_count)
        {
            var random = new Random();
            var items = new List<int>();

            while (items.Count < p_count)
            {
                items.Add(random.Next(p_minValue, p_maxValue));
            }

            return items.Distinct().ToList();
        }
    }
}
