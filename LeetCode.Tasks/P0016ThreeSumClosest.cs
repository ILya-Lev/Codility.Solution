using System;
using System.Linq;

namespace LeetCode.Tasks
{
    public class P0016ThreeSumClosest
    {
        public int ThreeSumClosest(int[] nums, int target)
        {
            if (nums.Length < 3) throw new ArgumentException("input array is too short", nameof(nums));

            if (nums.Length == 3) return nums.Sum();

            var sorted = nums.OrderBy(n => n).ToList();
            var closestSum = sorted.Take(3).Sum();
            for (int i = 0; i < sorted.Count; i++)
            {
                for (int j = i + 1; j < sorted.Count; j++)
                {
                    var compliment = target - sorted[i] - sorted[j];

                    var complimentIndex = sorted.BinarySearch(compliment);
                    if (complimentIndex < 0)
                        complimentIndex = Math.Max(Math.Min(~complimentIndex - 1, sorted.Count - 1), 0);
                    if (complimentIndex == i)
                        complimentIndex++;
                    if (complimentIndex == j)
                        complimentIndex++;
                    if (complimentIndex >= sorted.Count)
                        continue;//or break?

                    var closestCompliment = sorted[complimentIndex];
                    var currentSum = target - compliment + closestCompliment;

                    closestSum = Math.Abs(closestCompliment - compliment) < Math.Abs(target - closestSum)
                        ? currentSum
                        : closestSum;
                    if (closestSum == target)
                        return target;
                }
            }

            return closestSum;
        }
    }
}
