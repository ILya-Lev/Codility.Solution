using Algorithms.Solutions;
using FluentAssertions;
using Xunit;

namespace Algorithms.Tests
{
    public class JosephusPermutationTests
    {
        [Fact]
        public void Construct_7And3_MatchExpectations()
        {
            JosephusPermutation.Construct(7, 3).Should().Equal(new[] { 3, 6, 2, 7, 5, 1, 4 });
        }

        [Fact]
        public void ConstructQueue_7And3_MatchExpectations()
        {
            JosephusPermutation.ConstructQueue(7, 3).Should().Equal(new[] { 3, 6, 2, 7, 5, 1, 4 });
        }

        [Theory]
        [InlineData(7,3)]
        [InlineData(7,7)]
        [InlineData(7,1)]
        [InlineData(10,3)]
        [InlineData(10,5)]
        public void Construct_QueueVsMath_Match(int n, int m)
        {
            JosephusPermutation.Construct(n, m).Should().Equal(JosephusPermutation.ConstructQueue(n, m));
        }
    }
}