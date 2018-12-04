using System;
using System.Collections.Generic;
using System.Linq;

namespace Codility.Solvers
{
    public class Peaks
    {
        public int GetBlockNumber(int[] array)      // estimated to have complexity N * log(log(N)) ... why?
        {
            if (array.Length == 0)
                return 0;
            if (array.Length == 1)
                return 0;

            var peakIndices = FindAllPeaks(array);
            if (peakIndices.Count == 0)             //there is no peaks - even one block solution does not fit
                return 0;

            var arrayLengthDividers = FindDividers(array.Length).Distinct().OrderBy(d => d).ToArray();

            foreach (var blockLength in arrayLengthDividers)
            {
                var blockAmount = array.Length / blockLength;
                if (EachBlockHasPeak(peakIndices, blockLength, blockAmount))
                {
                    return blockAmount;
                }
            }

            return 0;
        }

        private HashSet<int> FindAllPeaks(int[] array)
        {
            var peakIndices = new HashSet<int>();
            //if (array[0] > array[1])
            //    peakIndices.Add(0);
            //if (array[array.Length - 1] > array[array.Length - 2])
            //    peakIndices.Add(array.Length - 1);

            for (int i = 1; i < array.Length - 1; i++)
            {
                if (array[i] > array[i - 1] && array[i] > array[i + 1])
                {
                    peakIndices.Add(i);
                }
            }

            return peakIndices;
        }

        private IEnumerable<int> FindDividers(int n)
        {
            yield return 1;
            yield return n;

            var maxDivider = (int)Math.Ceiling(Math.Sqrt(n));
            for (int divider = 2; divider <= maxDivider; divider++)
            {
                if (n % divider == 0)
                {
                    yield return divider;
                    yield return n / divider;
                }
            }
        }

        private bool EachBlockHasPeak(HashSet<int> peakIndices, int blockLength, int blockAmount)
        {
            for (int i = 0; i < blockAmount; i++)
            {
                var block = Enumerable.Range(i * blockLength, blockLength).ToArray();
                if (!BlockHasPeak(peakIndices, block))
                    return false;
            }

            return true;
        }

        private bool BlockHasPeak(HashSet<int> peakIndices, int[] block)
        {
            return block.Any(peakIndices.Contains);
        }

    }
}
