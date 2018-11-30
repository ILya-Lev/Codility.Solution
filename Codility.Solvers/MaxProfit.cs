using System;
using System.Collections.Generic;

namespace Codility.Solvers
{
    // problem: https://app.codility.com/programmers/lessons/9-maximum_slice_problem/max_profit/
    /*
    go through the array from left to right (first day to last day)
    for each item find min of slice from index 0 until current item
    max profit for this day is current item - min item
    if the result is positive (it's a profit) put it into max_heap_data structure

    to find min item - create a min_heap_data structure and fill it occasionally (while go over input array)
    */

    public class MaxProfit
    {
        public int GetMaxProfit(int[] prices)
        {
            var minHeap = new Heap<int>(isMin: true);
            var profitHeap = new Heap<int>(isMin: false);
            foreach (var price in prices)
            {
                if (minHeap.NotEmpty)
                {
                    var result = price - minHeap.Peek();
                    if (result > 0)
                        profitHeap.Push(result);
                }

                minHeap.Push(price);
            }

            return profitHeap.NotEmpty ? profitHeap.Peek() : 0;
        }
    }

    public class Heap<T> where T : IComparable<T>
    {
        public readonly List<T> _storage = new List<T>(); //for test reasons only
        private readonly bool _isMin;

        public Heap(bool isMin)
        {
            _isMin = isMin;
        }


        public bool NotEmpty => _storage.Count > 0;

        public T Peek()
        {
            if (_storage.Count == 0)
                throw new Exception($"Heap is empty. Do not call {nameof(Peek)} method at such circumstances");
            return _storage[0];
        }

        public int Push(T value)
        {
            _storage.Add(value);
            if (_storage.Count > 1)
                return BubbleUp();
            return 0;
        }

        /*
         *0
         *12
         *3456
         *7 8 9 10 11 12 13
         */
        private int BubbleUp()
        {
            var operationCount = 0;
            var lastIndex = _storage.Count - 1;
            for (int i = 0; i < lastIndex; i++)
            {
                operationCount++;
                if (ShouldReplace(_storage[lastIndex], _storage[i]))
                {
                    Swap(i, lastIndex);
                    i = (i + 1) * 2 - 2;//additional -1 because of ++ in for header
                }
                else
                {
                    break;
                }
            }

            return operationCount;
        }

        private bool ShouldReplace(T newValue, T oldValue)
        {
            var comparison = newValue.CompareTo(oldValue);
            return (comparison < 0 && _isMin) || (comparison > 0 && !_isMin);
        }

        private void Swap(int lhs, int rhs)
        {
            var temp = _storage[lhs];
            _storage[lhs] = _storage[rhs];
            _storage[rhs] = temp;
        }
    }

}
