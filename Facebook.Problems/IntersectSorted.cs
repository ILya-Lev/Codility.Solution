using System.Collections.Generic;
using System.Linq;
using Algorithms.Solutions;

namespace Facebook.Problems
{
    public class IntersectSorted
    {
        /// <summary>
        /// given two ascending sorted arrays return its intersection
        /// </summary>
        /// <remarks>runtime is O(min len * Log(max len)) as linear * binary search</remarks>
        public static int[] Intersection(int[] a, int[] b)
        {
            var shorter = a.Length < b.Length ? a : b;
            var longer = a.Length < b.Length ? b : a;

            return GetIntersection(shorter, longer).ToArray();
        }

        private static IEnumerable<int> GetIntersection(int[] shorter, int[] longer)
        {
            var previousFound = 0;
            foreach (var item in shorter)
            {
                var currentFound = BinarySearch<int>.Find(longer, item, previousFound);
                if (currentFound >= 0)
                {
                    previousFound = currentFound;
                    yield return item;
                }
            }
        }
    }
}