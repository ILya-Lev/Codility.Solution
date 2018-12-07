using System;
using System.Collections.Generic;
using System.Linq;

namespace Codility.Solvers
{
    public class CountSemiprimes
    {
        public IEnumerable<int> GetSemiprimesPopulation(int limit, int[] lowerBounds, int[] upperBounds)
        {
            var maxUpper = upperBounds.Max();
            var allSemiprimes = GenerateSemiprimesSequence(maxUpper);

            for (int n = 0; n < lowerBounds.Length; n++)
            {
                var population = 0;
                for (int m = lowerBounds[n]; m <= upperBounds[n]; m++)
                {
                    if (allSemiprimes.Contains(m))
                        population++;
                }
                yield return population;
            }
        }

        private HashSet<int> GenerateSemiprimesSequence(int to)
        {
            var semiprimes = new HashSet<int>();
            var primes = Enumerable.Range(1, to / 2 + 1).Where(IsPrime).ToArray();
            for (int i = 0; i < primes.Length && primes[i] * primes[i] < to; i++)
            {
                for (int j = i; j < primes.Length; j++)
                {
                    var candidate = primes[i] * primes[j];
                    if (candidate <= to)
                        semiprimes.Add(candidate);
                    else
                        break;
                }
            }

            return semiprimes;
        }

        private bool IsPrime(int n)
        {
            if (n < 2) return false;
            if (n == 2) return true;
            if (n % 2 == 0) return false;

            var maxDivisor = (int)Math.Ceiling(Math.Sqrt(n));
            for (int i = 3; i <= maxDivisor; i += 2)
            {
                if (n % i == 0)
                    return false;
            }

            return true;

        }

        private bool IsSemiprime(int n)
        {
            if (n < 4) return false;
            var dividerAmount = 0;  //except 1 and n
            var maxDivisor = (int)Math.Ceiling(Math.Sqrt(n));
            for (int i = 2; i < maxDivisor; i++)
            {
                if (n % 2 == 0)
                {
                    dividerAmount += 2;
                    if (dividerAmount > 2)
                        return false;
                }
            }

            if (n == maxDivisor * maxDivisor)
                dividerAmount += 2;

            return dividerAmount == 2;
        }
    }
}
