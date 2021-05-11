using FluentAssertions;
using LeetCode.Tasks;
using System.Linq;
using Xunit;

namespace LeetCode.Tests
{
    public class P0992SubArrayTests
    {
        [Fact]
        public void SubArraysWithKDistinct_AllTheSame_Kis2_Zero()
        {
            var sut = new P0992SubArray();
            var source = Enumerable.Repeat(1, 5).ToArray();

            sut.SubarraysWithKDistinct(source, 2)
                .Should()
                .Be(0, "source contains the same number repeated several times," +
                       " while sub arrays should have exactly 2 different integers");
        }

        [Fact]
        public void SubArraysWithKDistinct_AllTheSame_Kis1_15()
        {
            var sut = new P0992SubArray();
            var source = Enumerable.Repeat(1, 5).ToArray();

            sut.SubarraysWithKDistinct(source, 1)
                .Should()
                .Be(15, "all possible consequent sub array fits in");
        }

        [Theory]
        [InlineData(1, 0)]
        [InlineData(2, 1)]
        [InlineData(1_000, 999)]
        [InlineData(10_000, 9_999)]
        [InlineData(20_000, 19_999)]
        public void SubArraysWithKDistinct_AllUnique_Kis1_SizeOfSource(int k, int delta)
        {
            var sut = new P0992SubArray();
            var size = 20_000;
            var source = Enumerable.Range(1, size).ToArray();

            sut.SubarraysWithKDistinct(source, k).Should().Be(size - delta);
        }

        [Theory]
        [InlineData(new[] { 1, 2, 1, 2, 3 }, 2, 7)]
        [InlineData(new[] { 1, 2, 1, 3, 4 }, 3, 3)]
        public void SubArraysWithKDistinct_Sample001_Kis2_7(int[] source, int k, int expectedAmount)
        {
            new P0992SubArray().SubarraysWithKDistinct(source, k).Should().Be(expectedAmount);
        }
    }
}
