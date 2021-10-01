using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Algorithms.Solutions
{
    public class JosephusPermutation
    {
        /// <summary>
        /// the most important part: i + n - eliminatedAmount
        /// numbers between two eliminations should change its index to be (i + n - eliminatedAmount)
        /// in case (7,3), when 3 is eliminated, 1 becomes 8, 2 becomes 9
        /// </summary>
        /// <exception cref="ArgumentException">when n less than m</exception>
        public static int[] Construct(int n, int m)
        {
            if (m > n) throw new ArgumentException($"m must be less than n; but {m} > {n}");

            var numberByIndex = Enumerable.Range(1, n).ToDictionary(n => n, n => n);

            int[] permutation = new int[n];
            var permutationIndex = 0;

            var eliminatedAmount = 0;
            var previousEliminated = 0;
            var current = 1;
            
            while (permutationIndex < n)                                    //overall O(n*m) instead of n*n in 
            {
                if (current % m == 0)
                {

                    for (int i = previousEliminated + 1; i < current; i++)  //O(m) as current - previous = m
                    {
                        var value = numberByIndex[i];
                        numberByIndex.Remove(i);                            //O(1) according to https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.dictionary-2.remove?view=net-5.0#System_Collections_Generic_Dictionary_2_Remove__0_
                        numberByIndex.Add(i + n - eliminatedAmount, value); //O(1) asymptotically while capacity < n (it's true in this case)
                    }

                    permutation[permutationIndex++] = numberByIndex[current];

                    eliminatedAmount++;
                    previousEliminated = current;
                }

                current++;
            }

            return permutation;
        }

        /// <summary>
        /// O(n*m)
        /// </summary>
        /// <param name="n"></param>
        /// <param name="m"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public static int[] ConstructQueue(int n, int m)
        {
            if (m > n) throw new ArgumentException($"m must be less than n; but {m} > {n}");

            int[] permutation = new int[n];
            var queue = new Queue<int>(Enumerable.Range(1, n));

            int counter = 0;
            while (queue.Count > 0)     //O(n*m)
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