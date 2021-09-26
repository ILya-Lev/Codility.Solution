using System.Linq;
using Facebook.Problems;
using FluentAssertions;
using Xunit;

namespace Facebook.Tests
{
    [Trait("Category", "Unit")]
    public class ReverseOperationsTests
    {
        [Theory]
        [InlineData(new[] { 1, 2, 8, 9, 12, 16 }, new[] { 1, 8, 2, 9, 16, 12 })]
        [InlineData(new[] { 2, 8, 9, 12, 16 }, new[] { 8, 2, 9, 16, 12 })]
        [InlineData(new[] { 2, 8, 12, 16 }, new[] { 16, 12, 8, 2 })]
        [InlineData(new[] { 2, 8, 12, 16, 11 }, new[] { 16, 12, 8, 2, 11 })]
        public void Reverse_Sample_MatchExpectations(int[] input, int[] expected)
        {
            var direct = ReverseOperations.FromArray(input);

            var reversed = ReverseOperations.ReverseNew(direct);

            var final = ReverseOperations.ToSequence(reversed).ToArray();
            final.Should().Equal(expected);
        }
    }
}