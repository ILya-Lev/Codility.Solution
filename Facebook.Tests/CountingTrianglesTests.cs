using Facebook.Problems;
using FluentAssertions;
using Xunit;

namespace Facebook.Tests
{
    public class CountingTrianglesTests
    {
        [Fact]
        public void CountDistinctTriangles_Sample1_2()
        {
            var sides = new[,]
            {
                {2,2,3}, {3,2,2}, {2,5,6}
            };

            CountingTriangles.CountDistinctTriangles(sides).Should().Be(2);
        }

        [Fact]
        public void CountDistinctTriangles_Sample2_3()
        {
            var sides = new[,]
            {
                {8,4,6}, {100,101,102}, {84,93,173}
            };

            CountingTriangles.CountDistinctTriangles(sides).Should().Be(3);
        }

        [Fact]
        public void CountDistinctTriangles_Sample3_1()
        {
            var sides = new[,]
            {
                {5,8,9}, {5,9,8}, {9,5,8}, {9,8,5}, {8,9,5}, {8,5,9}
            };

            CountingTriangles.CountDistinctTriangles(sides).Should().Be(1);
        }
    }
}