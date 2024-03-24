using FluentAssertions;

namespace Coderbyte.Tests;

[Trait("Category", "Unit")]
public class RationalExtensionsTests
{
    [Fact]
    public void FindGreatestCommonDivisor_Coprime_One()
    {
        RationalExtensions.FindGreatestCommonDivisor(25, 24).Should().Be(1);
    }

    [Fact]
    public void FindGreatestCommonDivisor_Multipliers_Smaller()
    {
        RationalExtensions.FindGreatestCommonDivisor(25, 5).Should().Be(5);
    }

    [Fact]
    public void FindGreatestCommonDivisor_StandardFlow_Find()
    {
        RationalExtensions.FindGreatestCommonDivisor(25, 15).Should().Be(5);
    }

    [Fact]
    public void FindGreatestCommonDivisor_One_One()
    {
        RationalExtensions.FindGreatestCommonDivisor(25, 1).Should().Be(1);
    }

    [Fact]
    public void FindGreatestCommonDivisor_Zero_NonZeroSelf()//edge case...
    {
        RationalExtensions.FindGreatestCommonDivisor(25, 0).Should().Be(25);
    }
}