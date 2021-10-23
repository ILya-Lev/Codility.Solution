using System.Collections.Generic;
using System.Linq;

namespace Facebook.Problems
{
    public class WrappedVectorMultiplication
    {
        public static IReadOnlyCollection<(int amount, int number)> CalculateProduct(
            IList<(int amount, int number)> lhs,
            IList<(int amount, int number)> rhs,
            int paddingNumber = 1)
        {
            var product = new List<(int amount, int number)>();
            int lhsIndex = 0, rhsIndex = 0;

            while (lhsIndex < lhs.Count && rhsIndex < rhs.Count)
            {
                var a = lhs[lhsIndex];
                var b = rhs[rhsIndex];

                if (a.amount == b.amount)
                {
                    product.Add((a.amount, a.number * b.number));
                    lhsIndex++;
                    rhsIndex++;
                }
                else if (a.amount < b.amount)
                {
                    product.Add((a.amount, a.number * b.number));
                    lhsIndex++;
                    rhs[rhsIndex] = (b.amount - a.amount, b.number);
                }
                else if (a.amount > b.amount)
                {
                    product.Add((b.amount, a.number * b.number));
                    rhsIndex++;
                    lhs[lhsIndex] = (a.amount - b.amount, a.number);
                }
            }

            while (lhsIndex < lhs.Count)
            {
                var a = lhs[lhsIndex++];
                product.Add((a.amount, a.number * paddingNumber));
            }

            while (rhsIndex < rhs.Count)
            {
                var a = rhs[rhsIndex++];
                product.Add((a.amount, a.number * paddingNumber));
            }

            return product;
        }

        public static IReadOnlyCollection<int> CalculateProduct(IList<int> lhs, IList<int> rhs, int paddingNumber = 1)
        {
            var wrappedLhs = Wrap(lhs).ToArray();
            var wrappedRhs = Wrap(rhs).ToArray();

            var wrappedProduct = CalculateProduct(wrappedLhs, wrappedRhs, paddingNumber);

            return Unwrap(wrappedProduct).ToArray();
        }

        private static IEnumerable<(int amount, int number)> Wrap(IList<int> raw)
        {
            var count = 1;
            int i = 0;
            for (; i + 1 < raw.Count; i++)
            {
                if (raw[i] == raw[i + 1])
                {
                    count++;
                    continue;
                }

                yield return (count, raw[i]);
                count = 1;
            }

            if (raw.Count != 0)
                yield return (count, raw[i]);
        }

        private static IEnumerable<int> Unwrap(IReadOnlyCollection<(int amount, int number)> wrapped)
        {
            foreach (var tuple in wrapped)
            {
                for (int i = 0; i < tuple.amount; i++)
                {
                    yield return tuple.number;
                }
            }
        }

    }
}
