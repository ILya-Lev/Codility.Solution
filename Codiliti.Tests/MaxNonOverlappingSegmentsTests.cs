using Codility.Solvers;

using FluentAssertions;

using System.Linq;

using Xunit;

namespace Codility.Tests
{
    public class MaxNonOverlappingSegmentsTests
    {
        [Fact]
        public void GetLargestSize_Sample1_3()
        {
            var heads = new[] { 1, 3, 7, 9, 9 };
            var tails = new[] { 5, 6, 8, 9, 10 };

            var size = new MaxNonOverlappingSegments().GetLargestSize(heads, tails);

            size.Should().Be(3);
        }

        [Fact]
        public void GetLargestSize_Empty_0()
        {
            var heads = new int[0];
            var tails = new int[0];

            var size = new MaxNonOverlappingSegments().GetLargestSize(heads, tails);

            size.Should().Be(0);
        }

        [Fact]
        public void GetLargestSize_One_1()
        {
            var heads = new[] { 1 };
            var tails = new[] { 5 };

            var size = new MaxNonOverlappingSegments().GetLargestSize(heads, tails);

            size.Should().Be(1);
        }

        [Fact]
        public void GetLargestSize_NoOverlapping_All()
        {
            var limit = 30_000;
            var heads = Enumerable.Range(1, limit).Where(n => n % 2 == 1).ToArray();
            var tails = Enumerable.Range(1, limit).Where(n => n % 2 == 0).ToArray();

            var size = new MaxNonOverlappingSegments().GetLargestSize(heads, tails);

            size.Should().Be(limit / 2);
        }

    }
}
