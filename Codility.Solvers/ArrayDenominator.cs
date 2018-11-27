using MoreLinq;
using System.Collections.Generic;
using System.Linq;

namespace Codility.Solvers
{
    public class ArrayDenominator
    {
        public int GetDenominatorIndex(int[] values)
        {
            if (values.Length == 0)
            {
                return -1;
            }

            var frequencies = CalculateFrequencies(values);
            var mostFrequentValue = GetMostFrequentValue(frequencies);

            if (mostFrequentValue.Value * 2 > values.Length)
                return IndexOfValue(values, mostFrequentValue.Key);
            return -1;
        }

        private static KeyValuePair<int,int> GetMostFrequentValue(Dictionary<int, int> frequencies)
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
