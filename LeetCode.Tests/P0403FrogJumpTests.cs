using FluentAssertions;
using LeetCode.Tasks;
using System.Linq;
using Xunit;

namespace LeetCode.Tests
{
    [Trait("Category", "Unit")]
    public class P0403FrogJumpTests
    {
        private readonly P0403FrogJump _sut = new();

        [Fact]
        public void CanCross_SkipOneStoneInTheMiddle_True()
        {
            var stones = new[] { 0, 1, 3, 5, 6, 8, 12, 17 };
            _sut.CanCross(stones).Should().BeTrue();
        }

        [Fact]
        public void CanCross_TooBigGapInTheMiddle_False()
        {
            var stones = new[] { 0, 1, 2, 3, 4, 8, 9, 11 };
            _sut.CanCross(stones).Should().BeFalse();
        }

        [Fact]
        public void CanCross_Empty_False()
        {
            var stones = new int[0];
            var stones1 = new[] { 0 };
            var stones2 = new[] { 1 };
            _sut.CanCross(stones).Should().BeFalse();
            _sut.CanCross(stones1).Should().BeFalse();
            _sut.CanCross(stones2).Should().BeFalse();
        }

        [Fact]
        public void CanCross_TooBigFirstJump_False()
        {
            var stones = new[] { 0, 2 };
            _sut.CanCross(stones).Should().BeFalse();
        }

        [Fact(Timeout = 10)]
        public void CanCross_ManyStones_False()
        {
            //var a = 998;//last consecutive item
            var a = 30;//last consecutive item
            //var d = 37;//delta between the very last and the last but one items
            var d = 37;//delta between the very last and the last but one items
            var stones = Enumerable.Range(0, a + 1).Concat(new[] { a + d }).ToArray();
            stones[^2].Should().Be(a);
            (stones[^1] - stones[^2]).Should().Be(d);

            //var deltaNotOne = new List<int>();
            //for (int i = 0; i + 1 < stones.Length; i++)
            //{
            //    if (stones[i] + 1 != stones[i + 1])
            //        deltaNotOne.Add(stones[i + 1]);
            //}

            //deltaNotOne.Should().HaveCount(1);
            //deltaNotOne[0].Should().Be(1035);

            _sut.CanCross(stones).Should().BeFalse();
            _sut.TryJumpCounter.Should().BeLessThan(a * 5);
        }
    }
}
