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

            var totalSlices = 0;
            var slice = new Dictionary<int, int>();
            slice.Add(sequence[0], 0);
            for (int i = 1; i < sequence.Length; i++)
            {
                if (slice.ContainsKey(sequence[i]))
                {
                    totalSlices += GetNumberOfSubSlices(slice.Count);
                    slice = ClearUpToKey(slice, sequence[i]);
                    totalSlices -= GetNumberOfSubSlices(slice.Count);
                }
                slice.Add(sequence[i], i);
            }
            totalSlices += GetNumberOfSubSlices(slice.Count);

            return totalSlices;
        }

        private Dictionary<int, int> ClearUpToKey(Dictionary<int, int> slice, int key)
        {
            return slice.Where(pair => pair.Value > slice[key])
                .ToDictionary(pair => pair.Key, pair => pair.Value);
        }

        private int GetNumberOfSubSlices(int sequenceLength)
        {
            return sequenceLength * (sequenceLength + 1) / 2;
        }
    }
}
