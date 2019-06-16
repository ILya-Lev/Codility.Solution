using Codility.Solvers;
using FluentAssertions;
using Xunit;

namespace Codility.Tests
{
    public class CommonPrimeDivisorsTests
    {
        [Fact]
        public void Amount_Sample_1()
        {
            var lhs = new[] { 15, 10, 3 };
            var rhs = new[] { 75, 30, 5 };

            var solver = new CommonPrimeDivisors();

            var amount = solver.Amount(lhs, rhs);

            amount.Should().Be(1);
        }

        [Fact]
        public void Amount_DifferentPrimes_0()
        {
            var lhs = new[] { 149, 10, 72, 9*25*121, 64*625 };
            var rhs = new[] { 75, 30, 77, 7*3*5*11, 2*3 };

            var solver = new CommonPrimeDivisors();

            var amount = solver.Amount(lhs, rhs);

            amount.Should().Be(0);
        }

        [Fact]
        public void Amount_DifferentPowersOfTheSamePrimes_All()
        {
            var lhs = new[] {48, 4*9*49*25, 11*49};
            var rhs = new[] {54, 8*3*7*125, 121*7};

            var solver = new CommonPrimeDivisors();

            var amount = solver.Amount(lhs, rhs);

            amount.Should().Be(lhs.Length);
        }
    }
}
