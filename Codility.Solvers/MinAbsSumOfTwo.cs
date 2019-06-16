using MoreLinq;
using System;
using System.Linq;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("Codility.Tests")]

namespace Codility.Solvers
{
    public class MinAbsSumOfTwo
    {
        public int GetMinAbsSumOfTwo(int[] values)
        {
            var sortedArray = values.Distinct().OrderBy(n => n).ToArray();
            var maxNegativeItemIndex = FindNextSmallerIndex(sortedArray, 0, 0, sortedArray.Length);

            if (maxNegativeItemIndex < 0)
                return sortedArray[0] * 2;
            if (maxNegativeItemIndex == sortedArray.Length - 1)
                return -sortedArray[maxNegativeItemIndex] * 2;
            if (sortedArray[maxNegativeItemIndex + 1] == 0)
                return 0;

            var minSum = MinIndividualSum(sortedArray);
            minSum = MinSumForAllNegative(sortedArray, maxNegativeItemIndex, minSum);

            return minSum;
        }

        private int MinSumForAllNegative(int[] values, int negativeEndsAt, int minSum)
        {
            for (int i = negativeEndsAt; i >= 0; --i)
            {
                var current = values[i];

                var similarLowIndex = FindNextSmallerIndex(values, Math.Abs(current),
                    negativeEndsAt + 1, values.Length);
                if (similarLowIndex < negativeEndsAt)
                {
                    var firstPositiveNumber = values[negativeEndsAt + 1];
                    minSum = Math.Min(minSum, Math.Abs(current + firstPositiveNumber));
                    continue;
                }

                if (similarLowIndex == values.Length - 1)
                {
                    minSum = Math.Min(minSum, Math.Abs(current + values[values.Length - 1]));
                    break;
                }

                for (var shift = 0; similarLowIndex + shift < values.Length && shift < 3; shift++)
                {
                    minSum = Math.Min(minSum, Math.Abs(current + values[similarLowIndex + shift]));
                }
            }

            return minSum;
        }

        private int MinIndividualSum(int[] values)
        {
            return values.Select(n => Math.Abs(n * 2)).Min();
        }

        internal static int FindNextSmallerIndex(int[] values, int item, int from, int to)
        {
            var found = DoFind(values, item, @from, to);
            return found == to ? found - 1 : found;
        }

        private static int DoFind(int[] values, int item, int from, int to)
        {
            if (from >= to) return to;

            var middle = (from + to) / 2;
            if (values[middle] == item)
                return middle - 1;

            return values[middle] > item
                ? FindNextSmallerIndex(values, item, from, middle)
                : FindNextSmallerIndex(values, item, middle + 1, to);
        }
    }
}
