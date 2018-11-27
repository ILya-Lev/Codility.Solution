using MoreLinq;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Codility.Solvers
{
    public class ArrayDenominator
    {
        private readonly KeyValuePair<int, int>? _mostFrequentValue;
        private readonly int[] _values;

        public ArrayDenominator(int[] values)
        {
            if (values == null || values.Length == 0)
            {
                _mostFrequentValue = null;
            }
            else
            {
                _values = values;

                var frequencies = CalculateFrequencies(values);
                _mostFrequentValue = GetMostFrequentValue(frequencies);
            }
        }

        public bool HasDenominator() => _mostFrequentValue?.Value * 2 > _values?.Length;

        public int? GetDenominator() => HasDenominator() ? _mostFrequentValue?.Key : throw new Exception("There is no denominator!");

        public int GetDenominatorIndex()
        {
            return HasDenominator() ? IndexOfValue(_values, _mostFrequentValue.Value.Key) : -1;
        }

        private static KeyValuePair<int, int> GetMostFrequentValue(Dictionary<int, int> frequencies)
        {
            var maxFrequence = frequencies.First();
            foreach (var pair in frequencies.Skip(1))
            {
                if (maxFrequence.Value < pair.Value)
                {
                    maxFrequence = pair;
                }
            }

            return maxFrequence;
        }

        private static Dictionary<int, int> CalculateFrequencies(int[] values)
        {
            var frequencies = new Dictionary<int, int>();
            foreach (var value in values)
            {
                if (!frequencies.ContainsKey(value))
                    frequencies[value] = 1;
                else
                    frequencies[value]++;
            }

            return frequencies;
        }

        private int IndexOfValue(int[] values, int value)
        {
            for (int i = 0; i < values.Length; i++)
            {
                if (values[i] == value)
                {
                    return i;
                }
            }

            return -1;
        }
    }
}
