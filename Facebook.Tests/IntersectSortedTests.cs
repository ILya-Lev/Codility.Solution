using Facebook.Problems;
using FluentAssertions;
using Xunit;

namespace Facebook.Tests
{
    public class IntersectSortedTests
    {
        [Fact]
        public void IntersectSorted_Sample1_MatchExpectation()
        {
            var lhs = new[] { 1, 12, 15, 19 };
            var rhs = new[] { 2, 12, 13, 20 };

            IntersectSorted.Intersection(lhs, rhs).Should().Equal(new[] { 12 });
        }

        [Fact]
        public void IntersectSorted_Sample2_MatchExpectation()
        {
            var lhs = new[] { 1, 12, 15, 19, 20, 21 };
            var rhs = new[] { 2, 15, 17, 19, 21, 25, 27 };

            IntersectSorted.Intersection(lhs, rhs).Should().Equal(new[] { 15, 19, 21 });
        }
    }
}