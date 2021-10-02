using System;
using System.Collections.Generic;
using System.Linq;

namespace Algorithms.Solutions
{
    public class RodProblem
    {
        private readonly IReadOnlyList<(int length, int price)> _priceByLength;

        public RodProblem(IReadOnlyDictionary<int, int> priceByLength)
        {
            var prices = priceByLength ?? new Dictionary<int, int>()
            {
                [1] = 1,
                [2] = 5,
                [3] = 8,
                [4] = 9,
                [5] = 10,
                [6] = 17,
                [7] = 17,
                [8] = 20,
                [9] = 24,
                [10] = 30,
            };

            _priceByLength = prices
                .OrderByDescending(p => p.Value * 1.0 / p.Key)
                .Select(p => (p.Key, p.Value))
                .ToArray();
        }


        public (int L, int amount, int price)[] CutGreedy(int length)
        {
            int initialLength = length;
            var pieces = new List<(int L, int amount, int price)>();

            foreach (var (L, price) in _priceByLength)
            {
                if (length < L) continue;

                pieces.Add((L, length / L, length / L * price));
                length %= L;
            }

            if (length > 0)
            {
                throw new InvalidOperationException(
                    $"Cannot cut rod of length {initialLength} without remainder {length} using lengths {string.Join(", ", _priceByLength.Select(e => $"{e.length}"))}");
            }

            return pieces.ToArray();
        }
    }
}
