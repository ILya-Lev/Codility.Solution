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
    }
}