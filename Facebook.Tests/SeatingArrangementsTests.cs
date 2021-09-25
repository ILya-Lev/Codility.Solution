using System.Linq;
using Facebook.Problems;
using FluentAssertions;
using Xunit;

namespace Facebook.Tests
{
    public class SeatingArrangementsTests
    {
        [Fact]
        public void MinOverallAwkwardness_Sample_4()
        {
            var heights = new[] { 10, 5, 6, 8 };
            SeatingArrangements.MinOverallAwkwardness(heights).Should().Be(4);
        }

        [Fact]
        public void MinOverallAwkwardness_NaturalSequence_2()
        {
            var heights = Enumerable.Range(1, 10).ToArray();
            SeatingArrangements.MinOverallAwkwardness(heights).Should().Be(2);
        }

        [Fact]
        public void MinOverallAwkwardness_SameHeight_0()
        {
            var heights = Enumerable.Repeat(1, 10).ToArray();
            SeatingArrangements.MinOverallAwkwardness(heights).Should().Be(0);
        }

        [Fact]
        public void MinOverallAwkwardness_SameHeightButOne_AnyVsOne()
        {
            var heights = new[] { 1, 5, 5, 5, 5, 5 };
            SeatingArrangements.MinOverallAwkwardness(heights).Should().Be(4);
        }
    }
}