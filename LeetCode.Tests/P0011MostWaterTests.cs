using System;
using System.Diagnostics;
using System.Linq;
using FluentAssertions;
using LeetCode.Tasks;
using Newtonsoft.Json;
using Xunit;
using Xunit.Abstractions;

namespace LeetCode.Tests
{
    [Trait("Category", "Unit")]
    public class P0011MostWaterTests
    {
        private readonly ITestOutputHelper _output;
        private readonly P0011MostWater _sut = new();

        public P0011MostWaterTests(ITestOutputHelper output)
        {
            _output = output;
        }

        [Fact]
        public void MaxArea_Small1_MatchNaive()
        {
            var heights = new[] { 1, 8, 4, 3, 5, 8, 7 };//8x4 or 7x5
            
            _sut.MaxArea(heights).Should().Be(35);
            
            var naiveMaxArea = _sut.NaiveMaxArea(heights);
            naiveMaxArea.Area.Should().Be(35);

            _output.WriteLine(JsonConvert.SerializeObject(naiveMaxArea, Formatting.Indented));

        }

        [Fact]
        public void MaxArea_10Pow4Straight_MatchNaive()
        {
            var heights = Enumerable.Range(1, 10_000).ToArray();

            var stopwatchNaive = Stopwatch.StartNew();
            var naiveMaxArea = _sut.NaiveMaxArea(heights);
            stopwatchNaive.Stop();

            var stopwatch = Stopwatch.StartNew();
            var maxArea = _sut.MaxArea(heights);
            stopwatch.Stop();

            maxArea.Should().Be(naiveMaxArea.Area);
            stopwatch.ElapsedMilliseconds.Should().BeLessThan(stopwatchNaive.ElapsedMilliseconds);
            _output.WriteLine($"max area = {maxArea:### ### ###};" +
                              $" naive approach took {stopwatchNaive.ElapsedMilliseconds} ms;" +
                              $" smarter approach took {stopwatch.ElapsedMilliseconds}");
            
            _output.WriteLine(JsonConvert.SerializeObject(naiveMaxArea, Formatting.Indented));
        }

        [Fact]
        public void MaxArea_10Pow4Hill_MatchNaive()
        {
            var m = 10_000;//middle
            var heights = Enumerable.Range(1, m).Concat(Enumerable.Range(1, m).Reverse()).ToArray();

            var stopwatchNaive = Stopwatch.StartNew();
            var naiveMaxArea = _sut.NaiveMaxArea(heights);
            stopwatchNaive.Stop();

            var stopwatch = Stopwatch.StartNew();
            var maxArea = _sut.MaxArea(heights);
            stopwatch.Stop();

            maxArea.Should().Be(naiveMaxArea.Area);
            stopwatch.ElapsedMilliseconds.Should().BeLessThan(stopwatchNaive.ElapsedMilliseconds);
            _output.WriteLine($"max area = {maxArea:### ### ###};" +
                              $" naive approach took {stopwatchNaive.ElapsedMilliseconds} ms;" +
                              $" smarter approach took {stopwatch.ElapsedMilliseconds}");

            _output.WriteLine(JsonConvert.SerializeObject(naiveMaxArea, Formatting.Indented));
        }
    }
}