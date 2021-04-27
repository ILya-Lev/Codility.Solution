using FluentAssertions;
using LeetCode.Tasks;
using System;
using System.Linq;
using Xunit;
using Xunit.Abstractions;

namespace LeetCode.Tests
{
    [Trait("Category", "Unit")]
    public class P0016ThreeSumClosestTests
    {
        private readonly ITestOutputHelper _output;
        private readonly P0016ThreeSumClosest _sut = new P0016ThreeSumClosest();

        public P0016ThreeSumClosestTests(ITestOutputHelper output)
        {
            _output = output;
        }

        [Fact]
        public void ThreeSumClosest_SimpleValid_Find()
        {
            var nums = new[] { -1, 2, 1, -4 };
            var sum = _sut.ThreeSumClosest(nums, 1);
            sum.Should().Be(2);
        }

        [Fact]
        public void ThreeSumClosest_SimpleValid01_Find()
        {
            var nums = new[] { -1, 0, 1, 1, 55 };
            var sum = _sut.ThreeSumClosest(nums, 3);
            sum.Should().Be(2);
        }

        [Fact]
        public void ThreeSumClosest_BigRandom_Find()
        {
            var random = new Random(DateTime.UtcNow.Millisecond);
            var nums = Enumerable.Range(3, 1_000).Select(n => random.Next(-1000, 1000)).ToArray();
            var target = random.Next(-10_000, 10_000);
            var sum = _sut.ThreeSumClosest(nums, target);
            _output.WriteLine($"for target {target} the closest sum is {sum}");
        }
    }
}
