using Facebook.Problems;
using FluentAssertions;
using Xunit;

namespace Facebook.Tests
{
    public class BalanceBracketsTests
    {
        [Theory]
        [InlineData("{[()]}", true)]
        [InlineData("{}[]", true)]
        [InlineData("{[}]", false)]
        [InlineData(")", false)]
        public void IsBalanced_Example_MatchExpectations(string input, bool expectations)
        {
            BalanceBrackets.IsBalanced(input).Should().Be(expectations);
        }
    }
}