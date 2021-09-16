using System;
using System.Linq;
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

        [Fact]
        public void IsBalanced_OpeningOnly_False()
        {
            var indexGenerator = new Random(DateTime.UtcNow.Millisecond);
            var openingBrackets = new[] { '(', '[', '{' };
            var characters = Enumerable.Range(1, 1_000_000)
                .Select(n => openingBrackets[indexGenerator.Next(0, 3)])
                .ToArray();

            var s = new string(characters);

            BalanceBrackets.IsBalanced(s).Should().BeFalse();
        }
    }
}