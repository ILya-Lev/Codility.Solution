using System;
using System.Collections.Generic;
using System.Linq;

namespace Codility.Solvers
{
    public class CommonPrimeDivisors
    {
        public int Amount(int[] lhs, int[] rhs)
        {
            var amount = 0;
            for (int i = 0; i < lhs.Length; i++)
            {
                if (HasOnlyCommonPrimeDivisors(lhs[i], rhs[i]))
                    amount++;
            }

            return amount;
        }

        private bool HasOnlyCommonPrimeDivisors(int a, int b)
        {
            //if (a == 1 || b == 1) return false;
            if (a == b) return true;

            var clf = CalculateLCF(a, b);

            if (clf == 1) return false;

            var aFactors = GetPrimeFactors(a / clf).ToArray();
            if (aFactors.Length == 0)
                aFactors = new[] {a / clf};

            var bFactors = GetPrimeFactors(b / clf).ToArray();
            if (bFactors.Length == 0)
                bFactors = new[] {b / clf};

            return aFactors.All(f => clf % f == 0) && bFactors.All(f => clf % f == 0);
        }

        private int CalculateLCF(int a, int b)
        {
            var bigger = Math.Max(a, b);
            var smaller = Math.Min(a, b);
            if (bigger % smaller == 0)
                return smaller;
            return CalculateLCF(bigger % smaller, smaller);
        }

        /// <summary>
        /// the factors are prime by def - I mean we do not need to check if they are prime
        /// as we start from primes (3,5,7,...) and divide by it as many times as possible
        /// </summary>
        private IEnumerable<int> GetPrimeFactors(int number)
        {
            if (number < 2) yield break;
            if (number % 2 == 0)
            {
                yield return 2;
                while (number %2 == 0)
                {
                    number /= 2;
                }
            }

            var middle = (int)Math.Ceiling(Math.Sqrt(number));
            for (int i = 3; i <= middle && number > 1; i += 2)
            {
                if (number % i == 0)
                {
                    yield return i;
                    while (number%i==0)
                    {
                        number /= i;
                    }
                    middle = (int)Math.Ceiling(Math.Sqrt(number));
                }
            }
        }
    }
}
