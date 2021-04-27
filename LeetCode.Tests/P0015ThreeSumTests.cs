using FluentAssertions;
using LeetCode.Tasks;
using System;
using System.Linq;
using Xunit;
using Xunit.Abstractions;

namespace LeetCode.Tests
{
    [Trait("Category", "Unit")]
    public class P0015ThreeSumTests
    {
        private readonly P0015ThreeSum _sut = new P0015ThreeSum();

        private readonly ITestOutputHelper _output;
        public P0015ThreeSumTests(ITestOutputHelper output) => _output = output;

        [Fact]
        public void ThreeSum_SimpleValid_MatchExpectations()
        {
            var nums = new[] { -1, 0, 1, 2, -1, -4 };
            var expected = new[] { new[] { -1, -1, 2 }, new[] { -1, 0, 1 } };

            _sut.ThreeSum(nums).Should().BeEquivalentTo(expected, config => config.WithoutStrictOrdering());
        }

        [Fact]
        public void ThreeSum_Empty_Empty()
        {
            _sut.ThreeSum(new int[0]).Should().BeEmpty();
        }

        [Fact]
        public void ThreeSum_NotEnoughtNumbers_Empty()
        {
            var nums = new int[] { 1, 2 };
            _sut.ThreeSum(nums).Should().BeEmpty();
        }

        [Fact]
        public void ThreeSum_NoSolutionIsPresent_Empty()
        {
            var random = new Random(DateTime.UtcNow.Millisecond);
            var nums = Enumerable.Range(1, 3_000).Select(n => random.Next(1, 100_000)).ToArray();
            _sut.ThreeSum(nums).Should().BeEmpty();
        }

        [Fact]
        public void ThreeSum_SolutionIsPresent_NotEmpty()
        {
            var random = new Random(DateTime.UtcNow.Millisecond);
            var nums = Enumerable.Range(1, 3_000).Select(n => random.Next(-100_000, 100_000)).ToArray();

            var sums = _sut.ThreeSum(nums);

            sums.Should().NotBeEmpty();
            _output.WriteLine($"{sums.Count} triplets has been found");
        }


    }
}
