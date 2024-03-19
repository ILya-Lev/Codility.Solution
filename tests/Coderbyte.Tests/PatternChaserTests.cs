using FluentAssertions;

namespace Coderbyte.Tests;

[Trait("Category", "Unit")]
public class PatternChaserTests
{
    [Fact]
    public void FindPattern_HalfString_Yes()
    {
        PatternChaser.FindPattern("abcdabcd").Should().Be("yes abcd");
    }

    [Fact]
    public void FindPattern_NoPattern_No()
    {
        PatternChaser.FindPattern("abcdefgh").Should().Be("no null");
    }

    [Fact]
    public void FindPattern_TwoPatterns_Longer()
    {
        PatternChaser.FindPattern("aacbbbeaadbbbf").Should().Be("yes bbb");
    }

    [Fact]
    public void FindPattern_MinimalPattern_Yes()
    {
        PatternChaser.FindPattern("aabebaacdb").Should().Be("yes aa");
    }
}