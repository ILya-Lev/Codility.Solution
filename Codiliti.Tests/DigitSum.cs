using FluentAssertions;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Codility.Tests
{
    public class DigitSum
    {
        [Fact]
        public void ByPlace_UpTo1M_27M()
        {
            const int limit = 1_000_000;
            var total = 0;
            for (int i = 0; i < 6; i++)
            {
                total += 45 * limit / 10;
            }

            total.Should().Be(27 * limit);
        }

        [Fact]
        public void BruteForce_UpTo1M_27M()
        {
            var total = 0;
            for (int i = 0; i < 1_000_000; i++)
            {
                total += SplitIntoDigits(i).Sum();
            }

            total.Should().Be(27_000_000);
        }

        private IEnumerable<int> SplitIntoDigits(int number)
        {
            while (number > 0)
            {
                yield return number % 10;
                number /= 10;
            }
        }
    }
}
