using System;
using System.Collections.Generic;

namespace Codility.Solvers
{
    public class GenomicRangeQuery
    {
        private static readonly Dictionary<char, int> _impactFactors = new Dictionary<char, int>
        {
            ['A'] = 1,
            ['C'] = 2,
            ['G'] = 3,
            ['T'] = 4,
        };

        public IEnumerable<int> MinImpactFactors(string nucleotids, int[] starts, int[] ends)
        {
            if (starts.Length != ends.Length)
            {
                throw new ArgumentException($"starts {starts.Length} should have the same count as ends {ends.Length}");
            }

            var nucleotidsArray = nucleotids.ToCharArray();

            for (int pairIndex = 0; pairIndex < starts.Length; pairIndex++)
            {
                yield return GetMinimalImpactFactor(nucleotidsArray, starts[pairIndex], ends[pairIndex]);
            }
        }

        private int GetMinimalImpactFactor(char[] nucleotids, int start, int end)
        {
            if (end > nucleotids.Length)
                throw new Exception($"end {end} is to big - there are only {nucleotids.Length} symbols");

            var minFactor = 4;
            for (int i = start; i <= end; i++)
            {
                var currentFactor = _impactFactors[nucleotids[i]];
                if (currentFactor < minFactor)
                {
                    if (currentFactor == 1)
                        return 1;
                    minFactor = currentFactor;
                }
            }

            return minFactor;
        }
    }
}
