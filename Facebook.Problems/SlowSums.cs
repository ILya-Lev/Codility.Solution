using System.Collections.Generic;
using System.Linq;

namespace Facebook.Problems
{
    public class SlowSums
    {
        /// <summary>
        /// the main issue here is to prove the solution is optimal, as it's a sort of greedy algorithm
        /// </summary>
        public static int GetTotalTime(int[] arr)
        {
            if (arr.Length < 2) return 0;

            //O(N*logN) time; O(N) space
            IReadOnlyList<int> descending = arr.OrderByDescending(n => n).ToArray();
            long totalPenalty = 0, currentSum = descending[0];

            for (int i = 1; i < descending.Count; i++)//O(N) time
            {
                currentSum += descending[i];
                totalPenalty += currentSum;
            }

            return (int)(totalPenalty % int.MaxValue);//what about exception in case of overflow?
        }
    }
}
