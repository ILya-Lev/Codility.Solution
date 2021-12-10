using System;
using System.Collections.Generic;
using System.Linq;

namespace Algorithms.Solutions
{
    public class PrimeNumberSearch
    {
        private static readonly List<long> _primes = new List<long>();

        public IReadOnlyCollection<long> GetAllPrimesUpTo(long threshold) => GetAllPrimes()
            .TakeWhile(p => p <= threshold)
            .ToArray();

        private IEnumerable<long> GetAllPrimes()
        {
            foreach (var prime in _primes)
            {
                yield return prime;
            }

            if (!_primes.Any())
            {
                _primes.Add(2);
                _primes.Add(3);
                yield return 2;
                yield return 3;
            }

            for (var current = _primes.Last(); current > 0; current += 2)
            {
                if (IsPrime(current))
                {
                    _primes.Add(current);
                    yield return current;
                }
            }
        }

        /// <summary> even numbers are not handled as it's not expected they will ever be passed inhere </summary>
        private bool IsPrime(long current)
        {
            var limit = (long)Math.Sqrt(current) + 1;
            foreach (var prime in _primes.TakeWhile(p => p <= limit))
            {
                if (current % prime == 0)
                    return false;
            }

            return true;
        }
    }
}
