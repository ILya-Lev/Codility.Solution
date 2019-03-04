using MoreLinq;
using System.Linq;

namespace Codility.Solvers
{
    public class CountTriangles
    {
        public int CountTriplets(int[] values)
        {
            var sortedValues = values.OrderBy(v => v).ToArray();
            return CountFirstValuesNumber(sortedValues);
        }

        private int CountFirstValuesNumber(int[] values)
        {
            var tripletAmount = 0;
            for (int i = values.Length - 1; i >= 2; --i)
            {
                var first = values[i];
                var secondValueAmount = CountSecondValueNumber(first, values, i - 1);
                if (secondValueAmount == 0)
                    break;
                tripletAmount += secondValueAmount;
            }

            return tripletAmount;
        }

        private int CountSecondValueNumber(int first, int[] values, int start)
        {
            var tripletAmount = 0;
            for (int j = start; j >= 1; --j)
            {
                var second = values[j];
                var threshold = first - second;
                var tripletExistSince = IndexOfFirstGreater(threshold, values, 0, j - 1);

                if (tripletExistSince < 0)
                    break;

                tripletAmount += j - tripletExistSince;
            }

            return tripletAmount;
        }

        private int IndexOfFirstGreater(int threshold, int[] values, int start, int end)
        {
            if (start == end) return values[start] > threshold ? start : -1;

            var middle = (end + start) / 2;
            return values[middle] > threshold
                ? IndexOfFirstGreater(threshold, values, start, middle)
                : IndexOfFirstGreater(threshold, values, middle + 1, end);
        }
    }
}
