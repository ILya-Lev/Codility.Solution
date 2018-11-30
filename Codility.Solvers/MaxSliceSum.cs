using System;

namespace Codility.Solvers
{
    public class MaxSliceSum
    {
        /// <summary>
        /// O(n^2) implementation
        /// </summary>
        /// <param name="values"></param>
        /// <returns></returns>
        public int GetMaxSliceSum(int[] values)
        {
            long maxTotal = values[0];
            for (int i = 0; i < values.Length; i++)
            {
                var total = 0L;
                for (int j = i; j < values.Length; j++)
                {
                    total += values[j];
                    if (total > maxTotal)
                        maxTotal = total;
                }
            }

            return Math.Abs(maxTotal) > int.MaxValue
                ? maxTotal > 0 ? int.MaxValue : int.MinValue
                : (int)maxTotal;
        }
    }
}
