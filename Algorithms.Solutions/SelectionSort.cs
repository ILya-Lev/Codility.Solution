using System;

namespace Algorithms.Solutions
{
    public class SelectionSort<T> where T : IComparable<T>
    {
        public static T[] Sort(T[] input)
        {
            for (int i = 0; i < input.Length; i++)
            {
                var indexOfMin = FindIndexOfMin(input, i);
                Swap(input, i, indexOfMin);
            }
            return input;
        }

        private static void Swap(T[] input, int lhs, int rhs)
        {
            (input[rhs], input[lhs]) = (input[lhs], input[rhs]);
        }

        private static int FindIndexOfMin(T[] input, int start)
        {
            var indexOfMin = start;
            var currentMin = input[start];
            for (int i = start+1; i < input.Length; i++)
            {
                if (currentMin.CompareTo(input[i]) > 0)
                {
                    indexOfMin = i;
                    currentMin = input[i];
                }
            }

            return indexOfMin;
        }
    }
}
