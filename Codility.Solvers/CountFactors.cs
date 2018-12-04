using System;

namespace Codility.Solvers
{
    public class CountFactors
    {
        public int GetFactorsAmount(int n)
        {
            if (n < 2) return 1;

            var amount = 2;                                         //for 1 and n
            
            var maxDivisor = (int)Math.Ceiling(Math.Sqrt(n));       //lookup up to sqrt of n
            if (maxDivisor == 1) return amount;

            for (int i = 2; i < maxDivisor; i++)
            {
                if (n % i == 0)                              // if n divides by m, it divides by (n/m) as well
                    amount += 2;
            }

            if (n == maxDivisor * maxDivisor)                // when n = k^2
                amount++;

            return amount;
        }
    }
}
