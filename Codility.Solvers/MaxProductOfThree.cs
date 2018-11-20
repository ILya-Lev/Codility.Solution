using System;
using System.Collections.Generic;
using System.Linq;

namespace Codility.Solvers
{
    public class MaxProductOfThree
    {
        private readonly int[] _values;

        public MaxProductOfThree(int[] values)
        {
            if (values.Length < 3)
            {
                throw new Exception($"Input array should contain at least 3 items instead of {values.Length}");
            }

            _values = values;
        }


        public int GetMaxProduct()
        {
            var biggestNumbers = FindExtremalTriplet(_values, (a, b) => a < b);
            var biggestNumbersProduct = biggestNumbers.Aggregate(1, (current, next) => current * next);

            var smallestNumbers = FindExtremalTriplet(_values, (a, b) => a > b);
            var combinedProduct = smallestNumbers[0] * smallestNumbers[1] * biggestNumbers[0];

            return Math.Max(biggestNumbersProduct, combinedProduct);
        }

        private int[] FindExtremalTriplet(int[] values, Func<int, int, bool> relation)
        {
            var triplet = new int[3];
            var storedIndexes = new HashSet<int>();
            for (int tripletIndex = 0; tripletIndex < 3; tripletIndex++)
            {
                var maxIndex = 0;
                for (int i = 0; i < values.Length; i++)
                {
                    if (!storedIndexes.Contains(i))
                    {
                        triplet[tripletIndex] = values[i];
                        break;
                    }
                }

                for (int i = 0; i < values.Length; i++)
                {
                    if (relation(triplet[tripletIndex], values[i]) && !storedIndexes.Contains(i))
                    {
                        triplet[tripletIndex] = values[i];
                        maxIndex = i;
                    }
                }

                storedIndexes.Add(maxIndex);
            }

            return triplet;
        }

    }
}
