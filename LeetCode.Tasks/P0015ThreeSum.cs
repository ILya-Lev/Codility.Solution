using System.Collections.Generic;
using System.Linq;

namespace LeetCode.Tasks
{
    public class P0015ThreeSum
    {
        public IList<IList<int>> ThreeSum(int[] nums)
        {
            var lookup = nums.Select((n, idx) => new { n, idx })
                .GroupBy(p => p.n, p => p.idx)
                .ToDictionary(g => g.Key, g => new HashSet<int>(g));

            //var response = new List<IList<int>>();
            var triplets = new HashSet<(int, int, int)>();

            for (int i = 0; i < nums.Length; i++)
            {
                for (int j = i + 1; j < nums.Length; j++)
                {
                    var remaining = 0 - nums[i] - nums[j];
                    if (lookup.TryGetValue(remaining, out var indexes) && CanBeUsed(indexes, i, j))
                    {
                        triplets.Add(ComposeTriplet(nums[i], nums[j], remaining));
                    }
                }
            }

            return triplets.Select(t => new[] { t.Item1, t.Item2, t.Item3 }).ToArray();
        }

        private (int, int, int) ComposeTriplet(in int f, in int s, in int t)
        {
            int start, middle, end;
            if (f < s)
            {
                if (f < t)
                {
                    start = f;
                    if (t < s)
                    {
                        middle = t;
                        end = s;
                    }
                    else
                    {
                        middle = s;
                        end = t;
                    }
                }
                else
                {
                    end = s;
                    middle = f;
                    start = t;
                }
            }
            else
            {
                if (t < s)
                {
                    end = f;
                    middle = s;
                    start = t;
                }
                else
                {
                    start = s;
                    if (t < f)
                    {
                        middle = t;
                        end = f;
                    }
                    else
                    {
                        middle = f;
                        end = t;
                    }
                }
            }

            return (start, middle, end);
        }

        private bool CanBeUsed(HashSet<int> indexes, int firstIndex, int secondIndex)
        {
            if (indexes.Contains(firstIndex) && indexes.Contains(secondIndex))
                return indexes.Count > 2;

            if (indexes.Contains(firstIndex))
                return indexes.Count > 1;

            if (indexes.Contains(secondIndex))
                return indexes.Count > 1;

            return indexes.Count > 0;
        }
    }
}
