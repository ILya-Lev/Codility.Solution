using System;
using System.Collections.Generic;

namespace Algorithms.Solutions
{
    /// <summary>
    /// based on https://www.coursera.org/learn/algorithms-graphs-data-structures/home/week/3
    /// XII. Heaps
    /// what is missing (was not described, only mentioned in the video) - how to do bulk insert into a heap
    /// Heapify() should be time-optimized for O(N) instead of O(N*lgN)....
    /// </summary>
    /// <remarks>
    /// instead of swap() one could use move (overriding child with parent)
    /// until we get correct position for a new item
    /// </remarks>
    /// <typeparam name="T"></typeparam>
    public abstract class Heap<T> where T : IComparable<T>
    {
        protected readonly List<T> _storage;
        protected Heap(int capacity = 2) => _storage = new List<T>(capacity);

        public IReadOnlyList<T> Items => _storage;
        public int Count => _storage.Count;

        /// <summary>
        /// get head item of the heap (depends on the heap type - will be either min or max one)
        /// time complexity is O(1)
        /// </summary>
        public T Head => _storage[0];

        /// <summary>
        /// time complexity is at most O(lgN) (could be 1)
        /// where N is the amount of items in the heap with new item
        /// </summary>
        public void Insert(T item)
        {
            _storage.Add(item);

            BubbleUp();
        }

        /// <summary>
        /// removes and returns the first item from the heap (either the smallest or the largest depending
        /// on the heap type)
        /// time complexity O(lgN)
        /// </summary>
        /// <returns></returns>
        public T Extract()
        {
            var head = _storage[0];
            Swap(0, Count - 1);
            _storage.RemoveAt(_storage.Count - 1);

            BubbleDown(0);

            return head;
        }

        protected abstract int GetFurthestChild(int left, int right);

        protected abstract bool DoesBreakHeapProperty(int parent, int current);

        /// <summary>
        /// leaves of the tree are in-fact 1-element heaps => no need to correct them
        /// heap property has to be restored for higher nodes only
        /// i.e. starting with the parent of the very last element of the input sequence
        ///
        /// time complexity O(N) asymptotically as  sum(k/2^(k+1)) = 2
        ///
        /// inspired by dotnet 6 RC implementation of priority queue (finally we have it in C#)
        /// https://github.com/dotnet/runtime/blob/360df71eadde0d1394ee7b89693f83913f75575d/src/libraries/System.Collections/src/System/Collections/Generic/PriorityQueue.cs#L591
        /// </summary>
        protected Heap<T> BulkInsert(IEnumerable<T> items)
        {
            _storage.AddRange(items);

            for (int parent = GetParentIndex(_storage.Count - 1); parent >= 0; parent--)
            {
                BubbleDown(parent);
            }

            return this;
        }
        
        private static int GetParentIndex(int current) => (current + 1) / 2 - 1;
        private static int GetLeftChildIndex(int current) => (current + 1) * 2 - 1;
        private static int GetRightChildIndex(int current) => (current + 1) * 2;
        
        private void BubbleUp()
        {
            var current = _storage.Count - 1;
            var parent = GetParentIndex(current);
            while (IsValidParent() && DoesBreakHeapProperty(parent, current))
            {
                Swap(parent, current);

                current = parent;
                parent = GetParentIndex(current);
            }
         
            bool IsValidParent() => parent >= 0;
        }


        private void BubbleDown(int parent)
        {
            var left = GetLeftChildIndex(parent);
            var right = GetRightChildIndex(parent);

            while ((IsValidChildren(left) && DoesBreakHeapProperty(parent, left))
                   || (IsValidChildren(right) && DoesBreakHeapProperty(parent, right)))
            {
                if (!IsValidChildren(right)) //as right is after left, current left is the latest
                {
                    Swap(left, parent); //last possible move
                    continue;
                }

                var furthestChild = GetFurthestChild(left, right);

                Swap(parent, furthestChild);

                parent = furthestChild;
                left = GetLeftChildIndex(parent);
                right = GetRightChildIndex(parent);
            }
        }
        
        private bool IsValidChildren(int children) => children < _storage.Count;
        
        private void Swap(int parent, int current) =>
        (_storage[parent], _storage[current]) = (_storage[current], _storage[parent]);
    }

    public class MinHeap<T> : Heap<T> where T : IComparable<T>
    {
        public MinHeap(int capacity = 2) : base(capacity) { }

        public static MinHeap<T> Heapify(IEnumerable<T> items)
        {
            var heap = new MinHeap<T>();
            return heap.BulkInsert(items) as MinHeap<T>;
        }

        protected override int GetFurthestChild(int left, int right) =>
            _storage[left].CompareTo(_storage[right]) < 0
                ? left
                : right;

        protected override bool DoesBreakHeapProperty(int parent, int current) =>
            _storage[parent].CompareTo(_storage[current]) > 0;
    }

    public class MaxHeap<T> : Heap<T> where T : IComparable<T>
    {
        public MaxHeap(int capacity = 2) : base(capacity) { }

        public static MaxHeap<T> Heapify(IEnumerable<T> items)
        {
            var heap = new MaxHeap<T>();
            return heap.BulkInsert(items) as MaxHeap<T>;
        }


        protected override int GetFurthestChild(int left, int right) =>
            _storage[left].CompareTo(_storage[right]) > 0
                ? left
                : right;

        protected override bool DoesBreakHeapProperty(int parent, int current) =>
            _storage[parent].CompareTo(_storage[current]) < 0;
    }
}