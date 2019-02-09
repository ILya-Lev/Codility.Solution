using MoreLinq;
using System.Linq;

namespace Codility.Solvers
{
    public class CountTriangles
    {
        public int CountTriplets(int[] values)
        {
            var sortedValues = values.OrderBy(v => v).ToArray();
            return CountFirstValuesNumber(sortedValues);
        }

        private int CountFirstValuesNumber(int[] values)
        {
            var counter = 0;

            for (int i = 0; i < values.Length - 2; i++)
            {
                var first = values[i];
                var secondValuesNumber = CountSecondValuesNumber(values, i + 1, first);
                counter += secondValuesNumber;
            }

            return counter;
        }

        private int CountSecondValuesNumber(int[] values, int start, int first)
        {
            var secondValuesNumber = 0;
            for (int j = start; j < values.Length - 1; j++)
            {
                var second = values[j];
                var thirdValuesNumber = CountThirdValues(values, j + 1, first, second);
                if (thirdValuesNumber == 0)
                {
                    break;
                }

                secondValuesNumber += thirdValuesNumber;
            }

            return secondValuesNumber;
        }

        private int CountThirdValues(int[] values, int start, int first, int second)
        {
            var thirdValuesNumber = 0;
            for (int k = start; k < values.Length; k++)
            {
                var third = values[k];
                if (first + second <= third)
                {
                    break;
                }
                thirdValuesNumber++;
            }

            return thirdValuesNumber;
        }
    }
}
