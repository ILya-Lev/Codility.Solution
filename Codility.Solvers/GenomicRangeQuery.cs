using System;
using System.Collections.Generic;
using System.Linq;

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

        private readonly List<int>[] _indexByImpactFactor;

        public GenomicRangeQuery(string nucleotids)
        {
            _indexByImpactFactor = new[]
            {
                new List<int>(),
                new List<int>(),
                new List<int>(),
                new List<int>(),
            };

            var nucleotidsArray = nucleotids.ToCharArray();
            for (int i = 0; i < nucleotidsArray.Length; i++)
            {
                var nucleotid = nucleotidsArray[i];
                var groupIndex = _impactFactors[nucleotid] - 1;
                _indexByImpactFactor[groupIndex].Add(i);
            }
        }

        public IEnumerable<int> MinImpactFactors(int[] starts, int[] ends)
        {
            if (starts.Length != ends.Length)
            {
                throw new ArgumentException($"starts {starts.Length} should have the same count as ends {ends.Length}");
            }

            for (int pairIndex = 0; pairIndex < starts.Length; pairIndex++)
            {
                yield return GetMinimalImpactFactor(starts[pairIndex], ends[pairIndex]);
            }
        }

        private int GetMinimalImpactFactor(int start, int end)
        {
            for (int groupIndex = 0; groupIndex < _indexByImpactFactor.Length - 1; groupIndex++)
            {
                var currentGroup = _indexByImpactFactor[groupIndex];
                if (Intersects(start, end, currentGroup))
                {
                    if (currentGroup.Any(idx => idx >= start && idx <= end))
                        return groupIndex + 1;
                }
            }

            return 4;   //the largest one
        }

        private static bool Intersects(int start, int end, List<int> currentGroup)
        {
            return start <= currentGroup[currentGroup.Count - 1] && end >= currentGroup[0];
        }
    }
}
