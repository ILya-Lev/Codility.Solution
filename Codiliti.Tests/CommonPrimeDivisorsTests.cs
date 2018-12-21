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
            var lhs = new[] {15, 10, 3};
            var rhs = new[] {75, 30, 5};

            var solver = new CommonPrimeDivisors();

            var amount = solver.Amount(lhs, rhs);

            amount.Should().Be(1);
        }
    }
}
