using System;
using System.Collections.Generic;
using System.Linq;

namespace Algorithms.Solutions
{
    public class RodProblem
    {
        private readonly IReadOnlyDictionary<int, int> _priceByLength;
        private readonly Dictionary<int, Cut> _cutCache = new Dictionary<int, Cut>();

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
                .Concat(new[] { new KeyValuePair<int, int>(0, 0) })
                .ToDictionary(p => p.Key, p => p.Value);
        }


        public (int L, int amount, int price)[] CutGreedy(int length)
        {
            int initialLength = length;
            var pieces = new List<(int L, int amount, int price)>();

            foreach (var (L, price) in _priceByLength)
            {
                if (length < L || L == 0) continue;

                pieces.Add((L, length / L, length / L * price));
                length %= L;
            }

            if (length > 0)
            {
                throw new InvalidOperationException(
                    $"Cannot cut rod of length {initialLength} without remainder {length} using lengths {string.Join(", ", _priceByLength.Select(e => $"{e.Key}"))}");
            }

            return pieces.ToArray();
        }

        public int CalculateMaxPrice(int length)
        {
            var maxPrices = new Dictionary<int, int>() { { 0, 0 } };
            for (int i = 1; i <= length; i++)
            {
                var maxPrice = -1;
                for (int j = 1; j <= i; j++)
                {
                    var currentPrice = GetPriceForChop(_priceByLength, j);
                    var remainingPrice = GetPriceForChop(maxPrices, i - j);
                    maxPrice = Math.Max(maxPrice, currentPrice + remainingPrice);
                }
                maxPrices.Add(i, maxPrice);
            }

            return maxPrices[length];

            int GetPriceForChop(IReadOnlyDictionary<int, int> map, int len) => map.TryGetValue(len, out var p)
                ? p
                : 0;
        }

        public Cut CutDynamic(int length)
        {
            var bestCut = new Cut();
            if (_priceByLength.TryGetValue(length, out var p))
            {
                bestCut.TotalPrice = p;
                bestCut.AmountByLength.Add(length, 1);
            }

            for (int i = 1; i <= length / 2; i++)
            {
                var lhsCut = DoCut(i);
                var rhsCut = DoCut(length - i);

                if (lhsCut.TotalPrice + rhsCut.TotalPrice > bestCut.TotalPrice)
                {
                    bestCut = lhsCut + rhsCut;
                }
            }

            return bestCut;
        }

        private Cut DoCut(int length)
        {
            if (_cutCache.TryGetValue(length, out var cut))
                return cut;

            cut = CutDynamic(length);
            _cutCache.Add(length, cut);
            return cut;
        }

        public class Cut
        {
            public Dictionary<int, int> AmountByLength { get; } = new Dictionary<int, int>();
            public int TotalPrice { get; set; }

            public static Cut operator +(Cut lhs, Cut rhs)
            {
                var united = new Cut { TotalPrice = lhs.TotalPrice + rhs.TotalPrice };

                foreach (var p in lhs.AmountByLength) AccumulateAmount(p);
                foreach (var p in rhs.AmountByLength) AccumulateAmount(p);

                return united;

                void AccumulateAmount(KeyValuePair<int, int> p)
                {
                    if (united.AmountByLength.ContainsKey(p.Key))
                        united.AmountByLength[p.Key] += p.Value;
                    else
                        united.AmountByLength.Add(p.Key, p.Value);
                }
            }
        }
    }
}
