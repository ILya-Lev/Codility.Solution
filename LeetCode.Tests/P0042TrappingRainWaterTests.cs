using System.Linq;
using FluentAssertions;
using LeetCode.Tasks;
using Xunit;

namespace LeetCode.Tests
{
    [Trait("Category", "Unit")]
    public class P0042TrappingRainWaterTests
    {
        private readonly P0042TrappingRainWater _sut = new();

        [Fact]
        public void Trap_Sample1_6()
        {
            var height = new[] { 0, 1, 0, 2, 1, 0, 1, 3, 2, 1, 2, 1 };
            _sut.Trap(height).Should().Be(6);
        }

        [Fact]
        public void Trap_Hill_Zero()
        {
            int m = 100_000;
            var height = Enumerable.Range(1, m).Concat(Enumerable.Range(1, m).Reverse()).ToArray();
            _sut.Trap(height).Should().Be(0);
        }

        [Fact]
        public void Trap_Valley_Zero()
        {
            int m = 10_000;
            var height = Enumerable.Range(1, m).Reverse().Concat(Enumerable.Range(1, m)).ToArray();
            _sut.Trap(height).Should().Be(height.Sum(n => m-n));
        }
    }
}