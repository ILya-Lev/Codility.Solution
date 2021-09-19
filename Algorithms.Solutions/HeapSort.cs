using System;

namespace Algorithms.Solutions
{
    /// <summary>
    /// the same as a selection sort, but at first put the whole series into a heap (O(N) - non trivial)
    /// and then extract-min item from the hash (each time O(lgN) and N times => O(N*lgN) )
    /// </summary>
    public class HeapSort<T> where T : IComparable<T>
    {
        public static T[] SortAscending(T[] sequence)
        {
            var ascending = new T[sequence.Length];
            
            var minHeap = MinHeap<T>.Heapify(sequence);
            for (int i = 0; i < ascending.Length && minHeap.Count > 0; i++)
            {
                ascending[i] = minHeap.Extract();
            }

            return ascending;
        }

        public static T[] SortDescending(T[] sequence)
        {
            var descending = new T[sequence.Length];
            
            var minHeap = MaxHeap<T>.Heapify(sequence);
            for (int i = 0; i < descending.Length && minHeap.Count > 0; i++)
            {
                descending[i] = minHeap.Extract();
            }

            return descending;
        }
    }
}