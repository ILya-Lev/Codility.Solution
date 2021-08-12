using System;
using System.Collections.Generic;
using System.Linq;

namespace LeetCode.Tasks
{
    public class P1493LongestOnesSequence
    {
        public int LongestSubarray(int[] nums)
        {
            var aggregated = CollapseOnes(nums).ToArray();
            if (aggregated.Length == 0) return nums.Length - 1;//there are 1s only

            if (aggregated.Length == 1) return Math.Max(aggregated[0] - 1, 0);

            if (aggregated.Length == 2) return Math.Max(aggregated[0], aggregated[1]);

            var max = 0;
            for (int i = 0; i < aggregated.Length - 2; i++)
            {
                max = Math.Max(max, aggregated[i] + aggregated[i + 1] + aggregated[i + 2]);
            }

            return max;
        }

        private static IEnumerable<int> CollapseOnes(int[] nums)
        {
            var currentSum = 0;
            foreach (var n in nums)
            {
                if (n != 1)
                {
                    if (currentSum != 0)
                    {
                        yield return currentSum;
                        currentSum = 0;
                    }

                    yield return 0;
                }
                else
                {
                    currentSum++;
                }
            }

            yield return currentSum;
        }
    }
}
