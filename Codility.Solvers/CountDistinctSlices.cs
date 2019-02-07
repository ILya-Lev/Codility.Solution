using System.Collections.Generic;

namespace Codility.Solvers
{
    public class CountDistinctSlices
    {
        public int CalculateDistinctSlices(int[] sequence)
        {
            if (sequence.Length == 0)
                return 0;

            var totalSlices = 0;
            var slice = new HashSet<int>();
            slice.Add(sequence[0]);
            for (int i = 1; i < sequence.Length; i++)
            {
                if (slice.Contains(sequence[i]))
                {
                    totalSlices += GetNumberOfSubSlices(slice.Count);
                    slice.Clear();
                    slice.Add(sequence[i - 1]);
                }
                slice.Add(sequence[i]);
            }
            totalSlices += GetNumberOfSubSlices(slice.Count);

            return totalSlices;
        }

        private int GetNumberOfSubSlices(int sequenceLength)
        {
            return sequenceLength * (sequenceLength + 1) / 2;
        }
    }
}
