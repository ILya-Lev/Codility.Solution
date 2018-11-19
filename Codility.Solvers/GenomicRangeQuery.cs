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

        private readonly List<int>[] _impactIndexes;

        public GenomicRangeQuery(string nucleotids)
        {
            var nucleotidsArray = nucleotids.ToCharArray();

            _impactIndexes = new[]
            {
                new List<int>(nucleotidsArray.Length/4),
                new List<int>(nucleotidsArray.Length/4),
                new List<int>(nucleotidsArray.Length/4),
                new List<int>(nucleotidsArray.Length/4)
            };

            for (int i = 0; i < nucleotidsArray.Length; i++)
            {
                var nucleotid = nucleotidsArray[i];
                var impactFactor = _impactFactors[nucleotid];

                _impactIndexes[impactFactor - 1].Add(i);
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
            for (int groupIndex = 0; groupIndex < _impactIndexes.Length; groupIndex++)
            {
                var factorIndexes = _impactIndexes[groupIndex];
                var firstGreaterOrEqual = GetFirstGreaterOrEqual(factorIndexes, start);

                if (firstGreaterOrEqual != -1 && firstGreaterOrEqual <= end)
                    return groupIndex + 1;
            }

            return 4;
        }

        private int GetFirstGreaterOrEqual(List<int> factorIndexes, int value)
        {
            if (factorIndexes.Count == 0)
                return -1;

            int start = 0, end = factorIndexes.Count;
            int targetIndex = -1;
            int middle = (start + end) / 2;
            int previousMiddle = 0;
            do
            {
                if (value == factorIndexes[middle])
                    return value;

                if (value > factorIndexes[middle])
                {
                    start = middle;
                }
                else if (value < factorIndexes[middle])
                {
                    targetIndex = middle;
                    end = middle;
                }

                previousMiddle = middle;
                middle = (start + end) / 2;

            } while (middle < end && previousMiddle != middle);

            return targetIndex >= 0 ? factorIndexes[targetIndex] : targetIndex;
        }
    }
}
