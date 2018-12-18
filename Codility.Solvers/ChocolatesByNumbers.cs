using System;

namespace Codility.Solvers
{
    public class ChocolatesByNumbers
    {
        public int OverallEaten(int all, int step)
        {
            //the answer is smallest common divisable (scd) divided by smaller number
            // scd is a*b / largest common factor (lcf)
            // lcf we will find by euclidian algorithm

            var lcf = CalculateLCF(all, step);

            var scd = (long)all * step / lcf;
            return (int)(scd / step);

        }

        private int CalculateLCF(int a, int b)
        {
            var bigger = Math.Max(a, b);
            var smaller = Math.Min(a, b);
            if (bigger % smaller == 0)
                return smaller;
            return CalculateLCF(bigger % smaller, smaller);
        }
    }
}
