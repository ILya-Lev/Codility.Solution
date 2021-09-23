using System;
using System.Collections.Generic;

namespace Algorithms.Solutions
{
    public class BinarySearch<T> where T : IComparable<T>
    {
        /// <summary>
        /// Find a position for the key
        /// </summary>
        /// <param name="source">is expected to be sorted in ascending order</param>
        /// <param name="key">item to find position in the <see cref="source"/> </param>
        /// <returns>index it which <see cref="key"/> is found
        /// or negative value, if ~ operation is applied to it, gives a position where missing
        /// key should be inserted to maintain ascending sorted order</returns>
        public static int Find(IReadOnlyList<T> source, T key, int from = 0)
        {
            int start = from, end = source.Count-1;

            var challengeStart = source[start].CompareTo(key);
            if (challengeStart > 0) return ~start;
            if (challengeStart == 0) return start;

            var challengeEnd = source[end].CompareTo(key);
            if (challengeEnd < 0) return ~end;
            if (challengeEnd == 0) return end;

            while (start <= end)
            {
                var middle = (start + end) / 2;
                var challengeMiddle = source[middle].CompareTo(key);
                
                if (challengeMiddle == 0)
                    return middle;
                
                if (challengeMiddle > 0)
                {
                    end = middle - 1;
                }
                else
                {
                    start = middle + 1;
                }
            }

            return ~start;
        }
    }
}