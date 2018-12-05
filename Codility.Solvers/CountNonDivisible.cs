using MoreLinq;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Codility.Solvers
{
    public class CountNonDivisible
    {
        public int[] GetNonDivisibleAmount(int[] values)
        {
            //O(N)
            var frequencyTable = FillFrequencyTable(values);
            //O(N*log(N))
            var uniqueIncreasingValues = frequencyTable.Keys.Cast<int>().OrderBy(k => k).ToArray();
            //O(N^2) -> O(N) with big coefficient - as each number is factorized
            var counterByUniqueValue = GetAmountByUniqueSortedValue(values, uniqueIncreasingValues, frequencyTable);
            //O(N)
            return values.Select(v => counterByUniqueValue[v]).ToArray();
        }

        private Dictionary<int,int> FillFrequencyTable(int[] input)
        {
            var frequency = new Dictionary<int, int>();

            foreach (var number in input)
            {
                if (!frequency.ContainsKey(number))
                    frequency.Add(number, 1);
                else
                    frequency[number]++;
            }

            return frequency;
        }

        private Dictionary<int, int> GetAmountByUniqueSortedValue(int[] values, int[] uniqueIncreasingValues, Dictionary<int, int> frequencyTable)
        {
            var counterByUniqueValue = new Dictionary<int, int>();
            for (int i = 0; i < uniqueIncreasingValues.Length; ++i)
            {
                var factors = GetFactors(uniqueIncreasingValues[i]).Distinct().ToArray();
                
                var totalDividers = factors
                    .Where(frequencyTable.ContainsKey)
                    .Select(f => frequencyTable[f])
                    .Sum();


                var nonDividableCounter = values.Length - totalDividers;
                counterByUniqueValue.Add(uniqueIncreasingValues[i], nonDividableCounter);
            }

            return counterByUniqueValue;
        }

        private IEnumerable<int> GetFactors(int n)
        {
            int maxDivider = (int)Math.Ceiling(Math.Sqrt(n));
            for (int i = 1; i <= maxDivider; i++)
            {
                if (n % i == 0)
                {
                    yield return i;
                    yield return n / i;
                }
            }
        }

        private Dictionary<int, int> _GetAmountByUniqueSortedValue(int[] values, int[] uniqueIncreasingValues, Dictionary<int, int> frequencyTable)
        {
            var counterByUniqueValue = new Dictionary<int, int>();
            for (int i = 0; i < uniqueIncreasingValues.Length; ++i)
            {
                var nonDividableCounter = values.Length - CalculateDividable(uniqueIncreasingValues, i, frequencyTable);
                counterByUniqueValue.Add(uniqueIncreasingValues[i], nonDividableCounter);
            }

            return counterByUniqueValue;
        }

        private int CalculateDividable(int[] sortedUnique, int currentIndex, Dictionary<int, int> frequencyTable)
        {
            var dividable = 0;
            for (int i = 0; i <= currentIndex; i++)         //the number is dividable by itself
            {
                if (sortedUnique[currentIndex] % sortedUnique[i] == 0)
                {
                    dividable += frequencyTable[sortedUnique[i]];
                }
            }

            return dividable;
        }
    }
}
