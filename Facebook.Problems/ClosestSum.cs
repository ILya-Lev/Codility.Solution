using System;
using System.Collections.Generic;
using System.Linq;

namespace Facebook.Problems
{
    /// <summary>
    /// a task from the actual screening 2021-10-05
    /// this one was the second one; saw the solution from the beginning
    /// 
    /// I was hurrying up all the time!!!
    ///
    /// I've made a few mistakes which were not seen by the interviewer;
    /// e.g. indexA and currentA are not seen in the local function
    /// </summary>
    public class ClosestSum
    {
        public static (int, int) Find(int[] numbers, int x)
        {
            int a = 0, b = 0;
            var n = numbers.ToList();

            for (int i = 0; i < numbers.Length; i++)
            {
                var currentA = numbers[i];
                var bIndex = n.BinarySearch(i + 1, n.Count - i - 1, x - currentA, Comparer<int>.Default);

                if (bIndex >= 0 && bIndex < n.Count) //exact match
                {
                    return (currentA, x - currentA);
                }

                var bIndexRhs = ~bIndex;
                var bIndexLhs = bIndexRhs - 1;

                StoreIfCloser(bIndexLhs, i, currentA);
                StoreIfCloser(bIndexRhs, i, currentA);
            }

            return (a, b);

            void StoreIfCloser(int indexB, int indexA, int currentA)
            {
                if (indexB > indexA && indexB < n.Count)
                {
                    if (Math.Abs(x - currentA - n[indexB]) < Math.Abs(x - a - b))
                    {
                        a = currentA;
                        b = n[indexB];
                    }
                }
            }
        }
    }
}
