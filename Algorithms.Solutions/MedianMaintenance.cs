using System;
using System.Collections.Generic;

namespace Algorithms.Solutions
{
    /// <summary>
    /// use heap low and heap high, half of items in low, another half in high one
    /// </summary>
    public class MedianMaintenance<T> where T : IComparable<T>
    {
        private readonly MaxHeap<T> _left = new MaxHeap<T>();
        private readonly MinHeap<T> _right = new MinHeap<T>();

        public IEnumerable<T> GetMedians(IEnumerable<T> sequence)
        {
            foreach (var item in sequence)
            {
                InsertIntoHeap(item);
                NormalizeHeaps();
                var median = FindMedian();

                yield return median;
            }
        }

        private void InsertIntoHeap(T item)
        {
            if (_left.Count == 0 && _right.Count == 0)
            {
                _left.Insert(item);
            }
            else
            {
                if (_left.Head.CompareTo(item) >= 0)
                    _left.Insert(item);
                else
                    _right.Insert(item);
            }
        }

        private void NormalizeHeaps()
        {
            while (_left.Count > _right.Count + 1)
            {
                _right.Insert(_left.Extract());
            }

            while (_left.Count +1 < _right.Count)
            {
                _left.Insert(_right.Extract());
            }
        }

        private T FindMedian()
        {
            if (_left.Count > _right.Count)
                return _left.Head;
            if (_left.Count < _right.Count)
                return _right.Head;

            //to workaround absence of operator+/ constraint for type parameters in C#
            dynamic maxLeft = _left.Head;
            dynamic minRight = _right.Head;

            return (maxLeft + minRight) / 2;
        }
    }
}