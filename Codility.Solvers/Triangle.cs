using System.Linq;

namespace Codility.Solvers
{
    public class Triangle
    {
        public bool ContainsTriangle(int[] sides)
        {
            if (sides.Length < 3)
                return false;

            var sortedSides = sides.OrderBy(n => n).ToArray();
            if (sortedSides.First() == sortedSides.Last())
                return false;

            var mean = sortedSides.Average();
            var meanIndex = DefineIndex(sortedSides, mean);

            var forCurrentMean = meanIndex - 1 >= 0
                && meanIndex + 1 < sortedSides.Length
                && sortedSides[meanIndex - 1] + sortedSides[meanIndex] > sortedSides[meanIndex + 1];

            if (forCurrentMean) return true;

            var forBiggerMean = meanIndex + 2 < sortedSides.Length
                && sortedSides[meanIndex] + sortedSides[meanIndex + 1] > sortedSides[meanIndex + 2];

            return forBiggerMean;
        }

        private int DefineIndex(int[] sortedSides, double mean)
        {
            var start = 0;
            var end = sortedSides.Length;
            while (start + 1 < end)
            {
                var middle = (start + end) / 2;
                if ((int)mean == sortedSides[middle])
                    return middle;

                if (mean > sortedSides[middle])
                {
                    start = middle;
                }
                else
                {
                    end = middle;
                }
            }

            return start;
        }
    }
}
