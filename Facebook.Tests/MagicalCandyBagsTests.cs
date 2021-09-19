using Facebook.Problems;
using FluentAssertions;
using Xunit;

namespace Facebook.Tests
{
    [Trait("Category", "Unit")]
    public class MagicalCandyBagsTests
    {
        [Fact]
        public void MaxCandies_Sample1_14()
        {
            MagicalCandyBags.MaxCandies(new[] { 2, 1, 7, 4, 2 }, 3).Should().Be(14);
        }

        [Fact]
        public void MaxCandies_SoMuchTimeThatBagsAreNotRefilledAnyMore_25()
        {
            MagicalCandyBags.MaxCandies(new[] { 2, 1, 7, 4, 2 }, 11).Should().Be(25);
        }
    }
}