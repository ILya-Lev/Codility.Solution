using System.Collections.Generic;
using System.Linq;

namespace Codility.Solvers
{
    public class CountDistinctSlices
    {
        public int CalculateDistinctSlices(int[] sequence)
        {
            if (sequence.Length == 0)
                return 0;

            var totalSlices = 0L;
            var slice = new Slice();
            slice.Add(sequence[0], 0);
            for (int i = 1; i < sequence.Length; i++)
            {
                if (slice.ContainsValue(sequence[i]))
                {
                    totalSlices += GetNumberOfSubSlices(slice.Count);
                    slice.CleanUpToValue(sequence[i]);
                    totalSlices -= GetNumberOfSubSlices(slice.Count);
                }
                slice.Add(sequence[i], i);
            }
            totalSlices += GetNumberOfSubSlices(slice.Count);

            return (int)totalSlices;
        }

        private Dictionary<int, int> ClearUpToKey(Dictionary<int, int> slice, int key)
        {
            return slice.Where(pair => pair.Value > slice[key])
                .ToDictionary(pair => pair.Key, pair => pair.Value);
        }

        private long GetNumberOfSubSlices(int sequenceLength)
        {
            return (long)sequenceLength * (sequenceLength + 1) / 2;
        }

        private class Slice
        {
            private readonly Dictionary<int, int> _valueByIndex = new Dictionary<int, int>();
            private readonly Dictionary<int, int> _indexByValue = new Dictionary<int, int>();
            private int _minIndex = 0;

            public int Count => _valueByIndex.Count;

            public bool ContainsValue(int value) => _valueByIndex.ContainsKey(value);

            public void Add(int value, int index)
            {
                _valueByIndex.Add(value, index);
                _indexByValue.Add(index, value);
            }

            public void CleanUpToValue(int value)
            {
                var targetIndex = _valueByIndex[value];
                for (int i = targetIndex; i >= _minIndex; i--)
                {
                    _valueByIndex.Remove(_indexByValue[i]);
                    _indexByValue.Remove(i);
                }

                _minIndex = targetIndex + 1;
            }
        }
    }
}
