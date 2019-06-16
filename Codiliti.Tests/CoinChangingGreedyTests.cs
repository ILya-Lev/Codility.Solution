using Codility.Solvers;
using FluentAssertions;
using Xunit;

namespace Codility.Tests
{
    public class CoinChangingGreedyTests
    {
        [Fact]
        public void SplitScheme_BigNumber_Ok()
        {
            var denominals = new[] { 1, 2, 5, 10, 25, 50 };
            var scheme = new CoinChangingGreedy().SplitScheme(denominals, 47);
            scheme.Should().HaveCount(3);

            scheme[25].Should().Be(1);
            scheme[10].Should().Be(2);
            scheme[2].Should().Be(1);
        }
    }
}
