using System;
using System.Collections.Generic;

namespace Facebook.Problems
{
    public class CountingTriangles
    {
        public static int CountDistinctTriangles(int[,] arr)
        {
            var uniqueTriplets = new HashSet<(int a, int b, int c)>(arr.GetLength(0));

            for (int i = 0; i < arr.GetLength(0); i++)
            {
                var sortedTriangle = Sort(arr[i, 0], arr[i, 1], arr[i, 2]);
                uniqueTriplets.Add(sortedTriangle);
            }
            return uniqueTriplets.Count;
        }

        private static (int a, int b, int c) Sort(int x, int y, int z)
        {
            var max = Math.Max(x, Math.Max(y, z));
            var min = Math.Min(x, Math.Min(y, z));
            var middle = x + y + z - max - min;
            return (a: max, b: middle, c: min);
        }
    }
}