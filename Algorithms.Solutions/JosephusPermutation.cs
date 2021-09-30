using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Algorithms.Solutions
{
    public class JosephusPermutation
    {
        public static int[] Construct(int n, int m)
        {
            if (m > n) throw new ArgumentException($"m must be less than n; but {m} > {n}");

            int[] permutation = new int[n];
            var current = 0;
            var generation = 0;
            while (current < n)
            {
                var number = n * generation + m * (current + 1) - current * (generation > 0 ? 1 : 0);
                permutation[current++] = number % n;
                generation = number / n;
            }

            return permutation;
        }

        public static int[] ConstructQueue(int n, int m)
        {
            if (m > n) throw new ArgumentException($"m must be less than n; but {m} > {n}");

            int[] permutation = new int[n];
            var queue = new Queue<int>(Enumerable.Range(1, n));

            int counter = 0;
            while (queue.Count > 0)
            {
                var current = queue.Dequeue();
                counter++;
                
                if (counter % m == 0)
                    permutation[counter / m - 1] = current;//more simple is to use List.Add()
                else
                    queue.Enqueue(current);
            }

            return permutation;
        }
    }
}