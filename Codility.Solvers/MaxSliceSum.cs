using System;
using System.Linq;

namespace Codility.Solvers
{
    public class MaxSliceSum
    {
        /// <summary>
        /// O(n) implementation from codility site materials
        /// total is a sum so far
        /// maxTotal is a maximum sum among all possible
        /// think it over!
        /// the only modification - for all negative input values
        /// </summary>
        public int GetMaxSliceSum(int[] values)
        {
            var maxSingle = values.Max();
            if (maxSingle <= 0)
                return maxSingle;

            var total = 0L;
            var maxTotal = 0L;
            for (int i = 0; i < values.Length; i++)
            {
                total = Math.Max(0, total + values[i]);
                maxTotal = Math.Max(maxTotal, total);
            }

            return Math.Abs(maxTotal) > int.MaxValue
                ? maxTotal > 0 ? int.MaxValue : int.MinValue
                : (int)maxTotal;
        }
    }
}
