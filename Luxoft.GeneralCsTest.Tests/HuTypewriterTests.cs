namespace Luxoft.GeneralCsTest.Tests;

[Trait("Category", "Unit")]
public class HuTypewriterTests
{
    [Theory]
    [InlineData("bat, tab, tad", "tad")]
    [InlineData("bat, bate, tad", "bate")]
    [InlineData("bat", "bat")]
    [InlineData("tab, hate", "hate")]
    [InlineData("cat, bat", "bat")]
    public void FindTheLongestWord_Samples_Expectations(string input, string expected)
    {
        var typewriter = new HuTypewriter("ertabdfyh".ToCharArray());

        var result = typewriter.FindTheLongestWord(input);

        result.Should().Be(expected);
    }
}