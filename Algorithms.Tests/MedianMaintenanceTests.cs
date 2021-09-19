using System;
using System.Linq;
using Algorithms.Solutions;
using FluentAssertions;
using Xunit;

namespace Algorithms.Tests
{
    [Trait("Category", "Unit")]
    public class MedianMaintenanceTests
    {
        private readonly MedianMaintenance<int> _sut = new();

        [Fact]
        public void GetMedians_1To10_DoubleItems()
        {
            var sequence = Enumerable.Range(1, 10).ToArray();
            var medians = _sut.GetMedians(sequence);
            medians.Should().Equal(new[] { 1, 1, 2, 2, 3, 3, 4, 4, 5, 5 });
        }

        [Fact]
        public void GetMedians_10To1_DoubleItems()
        {
            var sequence = Enumerable.Range(1, 10).Reverse().ToArray();
            var medians = _sut.GetMedians(sequence);
            medians.Should().Equal(new[] { 10, 9, 9, 8, 8, 7, 7, 6, 6, 5 });
        }

        [Fact]
        public void GetMedians_Strings_FunnyMiddle()
        {
            var medianCalculator = new MedianMaintenance<string>();
            var input = new[] { "a", "b", "c" };
            Action getMedians = () => medianCalculator.GetMedians(input).ToArray();
            //medians.Should().Equal(new[] { "a", "ab", "c" });
            getMedians.Should().Throw<Exception>();
        }
    }
}