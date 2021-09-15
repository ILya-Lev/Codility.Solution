using Facebook.Problems;
using FluentAssertions;
using Xunit;

namespace Facebook.Tests
{
    public class PairSumsTests
    {
        [Fact]
        public void NumberOfWays_Example1_2()
        {
            PairSums.NumberOfWays(new[] { 1, 2, 3, 4, 3 }, 6).Should().Be(2);
        }

        [Fact]
        public void NumberOfWays_Example2_4()
        {
            PairSums.NumberOfWays(new[] { 1, 5, 3, 3, 3 }, 6).Should().Be(4);
        }
    }
}