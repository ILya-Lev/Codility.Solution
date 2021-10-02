using System;
using System.Collections.Generic;
using System.Linq;

namespace Algorithms.Solutions
{
    public class QuickSort<T> where T : IComparable<T>
    {
        public static T[] Sort(T[] raw)
        {
            var sorted = raw.ToArray();
            var randomGenerator = new Random(DateTime.UtcNow.Millisecond);

            var subsets = new Stack<(int start, int end)>();
            subsets.Push((0, sorted.Length));
            while (subsets.Any())
            {
                var (start, end) = subsets.Pop();
                var seed = randomGenerator.Next(start, end);
                Swap(sorted, start, seed);

                int head = start, tail = end - 1;
                while (head < tail)
                {
                    if (sorted[head].CompareTo(sorted[head + 1]) > 0)
                    {
                        Swap(sorted, head, head + 1);
                        head++;
                    }
                    else
                    {
                        Swap(sorted, head + 1, tail);
                        tail--;
                    }
                }

                if (start < head)
                    subsets.Push((start, head));
                if (tail + 1 < end)
                    subsets.Push((tail + 1, end));
            }

            return sorted;
        }

        private static void Swap(T[] source, int lhs, int rhs)
            => (source[lhs], source[rhs]) = (source[rhs], source[lhs]);
    }
}