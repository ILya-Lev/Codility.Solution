using System;
using System.Collections.Generic;
using System.Linq;

namespace LeetCode.Tasks
{
    public class P0004MedianOfTwoSortedArrays
    {
        public double FindMedianSortedArrays(int[] nums1, int[] nums2)
        {
            if (nums1.Length == 0 && nums2.Length == 0) return 0;
            if (nums1.Length == 0) return GetMedian(nums2);
            if (nums2.Length == 0) return GetMedian(nums1);

            if (NoOverlap(nums1, nums2))
                return GetNoOverlapMedian(nums1, nums2);

            if (nums1.First() < nums2.First())
                return FindMedian(nums1.ToList(), 0, nums1.Length, nums2.ToList(), 0, nums2.Length);
            return FindMedian(nums2.ToList(), 0, nums2.Length, nums1.ToList(), 0, nums1.Length);
        }

        /// <summary>
        /// arguments are of type list to ease BinarySearch
        /// </summary>
        private double FindMedian(List<int> lhs, int lhsStart, int lhsEnd, List<int> rhs, int rhsStart, int rhsEnd)
        {
            if (lhsEnd - lhsStart <= 1 && rhsEnd - rhsStart <= 1)
                return ExtractMedianOnConverged(lhs, lhsStart, lhsEnd, rhs, rhsStart, rhsEnd);

            var lhsMiddle = (lhsEnd + lhsStart) / 2;
            var lhsMiddleInRhs = rhs.BinarySearch(rhsStart, rhsEnd - rhsStart, lhs[lhsMiddle], default);

            var rhsMiddle = (rhsEnd + rhsStart) / 2;
            var rhsMiddleInLhs = lhs.BinarySearch(lhsStart, lhsEnd - lhsStart, rhs[rhsMiddle], default);

            var lhsNewBoundary = Math.Min(Math.Max(rhsMiddleInLhs, lhsStart), lhsEnd);

            var rhsNewBoundary = Math.Min(Math.Max(lhsMiddleInRhs, rhsStart), rhsEnd);

            return FindMedian(lhs, Math.Min(lhsMiddle, lhsNewBoundary), Math.Max(lhsMiddle, lhsNewBoundary),
                rhs, Math.Min(rhsMiddle, rhsNewBoundary), Math.Max(rhsMiddle, rhsNewBoundary));
        }

        private double ExtractMedianOnConverged(List<int> lhs, int lhsStart, int lhsEnd, List<int> rhs, int rhsStart, int rhsEnd)
        {
            int? lhsItem = lhsEnd - lhsStart == 1 ? lhs[lhsStart] : null;
            int? rhsItem = rhsEnd - rhsStart == 1 ? rhs[rhsStart] : null;

            if (lhsItem.HasValue && rhsItem.HasValue)
                return (lhsItem + rhsItem).Value / 2.0;

            return lhsItem
                   ?? rhsItem
                   ?? throw new Exception($"median is not found" +
                                          $", converged to {lhsStart}-{lhsEnd}" +
                                          $" and {rhsStart}-{rhsEnd}");
        }

        private double GetMedian(int[] numbers)
        {
            var shift = numbers.Length % 2 == 0 ? -1 : 0;
            var middle1 = numbers.Length / 2 + shift;
            var middle2 = numbers.Length / 2;

            return (numbers[middle1] + numbers[middle2]) / 2.0;
        }

        private bool NoOverlap(int[] lhs, int[] rhs) =>
            lhs.Last() <= rhs.First()
            || rhs.Last() <= lhs.First();

        private double GetNoOverlapMedian(int[] lhs, int[] rhs)
        {
            //if odd, shift is 0 and in the middle 1 item => take it 2 times and divide by 2 => get itself
            //if even, shift is -1 and in the middle 2 items - one at totalLength /2 and totalLength/2-1
            var totalLength = lhs.Length + rhs.Length;
            var shift = totalLength % 2 == 0 ? -1 : 0;
            var middle1 = totalLength / 2 + shift;
            var middle2 = totalLength / 2;

            int firstMiddle, secondMiddle;

            if (lhs.Last() <= rhs.First())
            {
                firstMiddle = GetNoOverlapItem(lhs, rhs, middle1);
                secondMiddle = GetNoOverlapItem(lhs, rhs, middle2);

                return (firstMiddle + secondMiddle) / 2.0;
            }

            firstMiddle = GetNoOverlapItem(rhs, lhs, middle1);
            secondMiddle = GetNoOverlapItem(rhs, lhs, middle2);

            return (firstMiddle + secondMiddle) / 2.0;
        }

        private int GetNoOverlapItem(int[] smaller, int[] bigger, int index) =>
            smaller.Length > index
                ? smaller[index]
                : bigger[index - smaller.Length];
    }
}
