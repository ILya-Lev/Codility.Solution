using Facebook.Problems;
using FluentAssertions;
using Xunit;

namespace Facebook.Tests
{
    public class BalancedSplitTests
    {
        [Theory]
        [InlineData(new[] { 1, 5, 7, 1 }, true)]
        [InlineData(new[] { 12, 7, 6, 7, 6 }, false)]
        public void BalancedSplitExists_Samples_MatchExpectations(int[] numbers, bool expected)
        {
            BalancedSplit.BalancedSplitExists(numbers).Should().Be(expected);
        }
    }
}