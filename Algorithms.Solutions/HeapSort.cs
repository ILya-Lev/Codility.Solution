using System;

namespace Algorithms.Solutions
{
    /// <summary>
    /// the same as a selection sort, but at first put the whole series into a heap (O(N) - non trivial)
    /// and then extract-min item from the hash (each time O(lgN) and N times => O(N*lgN) )
    /// </summary>
    public class HeapSort<T> where T : IComparable<T>
    {
        public static T[] Sort(T[] sequence)
        {
            var ascending = new T[sequence.Length];
            
            var minHeap = MinHeap<T>.Heapify(sequence);
            for (int i = 0; i < ascending.Length && minHeap.Count > 0; i++)
            {
                ascending[i] = minHeap.Extract();
            }

            return ascending;
        }
    }

    /// <summary>
    /// use heap low and heap high, half of items in low, another half in high one
    /// </summary>
    public class MedianMaintanence
    {

    }

    /// <summary>
    /// min path (by length of each edge) between the two given nodes
    /// </summary>
    public class DijkstraShortestPath
    {

    }
}