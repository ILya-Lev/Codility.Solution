using Codility.Solvers;
using FluentAssertions;
using System;
using System.Linq;
using Xunit;
using Xunit.Sdk;

namespace Codility.Tests
{
    public class MinAbsSumOfTwoTests : IClassFixture<TestOutputHelper>
    {
        private readonly TestOutputHelper _outputHelper;

        public MinAbsSumOfTwoTests(TestOutputHelper outputHelper)
        {
            _outputHelper = outputHelper;
        }

        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        [InlineData(4)]
        [InlineData(5)]
        [InlineData(19)]
        [Theory]
        public void FindNextSmallerIndex_NoValueLessThanGiven_NotFound(int length)
        {
            var values = Enumerable.Range(1, length).ToArray();
            var index = MinAbsSumOfTwo.FindNextSmallerIndex(values, 0, 0, length);
            index.Should().Be(-1);
        }

        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        [InlineData(4)]
        [InlineData(5)]
        [InlineData(19)]
        [Theory]
        public void FindNextSmallerIndex_GivenIsTheSmallest_NotFound(int length)
        {
            var values = Enumerable.Range(1, length).ToArray();
            var index = MinAbsSumOfTwo.FindNextSmallerIndex(values, 1, 0, length);
            index.Should().Be(-1);
        }

        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        [InlineData(4)]
        [InlineData(5)]
        [InlineData(19)]
        [Theory]
        public void FindNextSmallerIndex_GivenIsTheLargest_LastButOne(int length)
        {
            var values = Enumerable.Range(1, length).ToArray();
            var index = MinAbsSumOfTwo.FindNextSmallerIndex(values, values.Last(), 0, length);
            index.Should().Be(length - 2);
        }

        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        [InlineData(4)]
        [InlineData(5)]
        [InlineData(19)]
        [Theory]
        public void FindNextSmallerIndex_AllSmallerThanGiven_Last(int length)
        {
            var values = Enumerable.Range(1, length).ToArray();
            var index = MinAbsSumOfTwo.FindNextSmallerIndex(values, values.Last() + 1, 0, length);
            index.Should().Be(length - 1);
        }

        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        [InlineData(4)]
        [InlineData(5)]
        [InlineData(19)]
        [Theory]
        public void FindNextSmallerIndex_GivenIsInTheMiddle_JustBeforeMiddle(int length)
        {
            var values = Enumerable.Range(1, length).ToArray();
            var target = values[length / 2];
            var index = MinAbsSumOfTwo.FindNextSmallerIndex(values, target, 0, length);
            index.Should().Be(length / 2 - 1);
        }

        private readonly Random _randomGenerator = new Random(DateTime.UtcNow.Millisecond);
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        [InlineData(4)]
        [InlineData(5)]
        [InlineData(19)]
        [InlineData(50)]
        [Theory]
        public void FindNextSmallerIndex_GivenIsRandom_MatchLinearApproach(int length)
        {
            var values = Enumerable.Range(1, length)
                .Select(n => _randomGenerator.Next(-n, n))
                .Distinct()
                .OrderBy(n => n)
                .ToArray();

            var targetIndex = _randomGenerator.Next(0, values.Length);
            var target = values[targetIndex];
            _outputHelper.WriteLine(string.Join(", ", values.Select(n => $"{n}")));
            _outputHelper.WriteLine($"target item {target}, index {targetIndex}");


            var index = DoFind(values, target);


            var expectedIndex = targetIndex;
            while (expectedIndex >= 0 && values[expectedIndex] >= target)
            {
                expectedIndex--;
            }

            index.Should().Be(expectedIndex);
        }

        [InlineData(1000005)]
        [InlineData(2000009)]
        [InlineData(3000007)]
        [InlineData(4000003)]
        [InlineData(5000001)]
        [InlineData(1900000)]
        [InlineData(5000000)]
        //[Theory]
        [Theory(Skip = "performance test - takes about 30seconds to execute")]
        public void FindNextSmallerIndex_GivenIsRandomInBigRange_MatchLinearApproach(int length)
        {
            var values = Enumerable.Range(1, length)
                .Select(n => _randomGenerator.Next(-n, n))
                .Distinct()
                .OrderBy(n => n)
                .ToArray();

            var targetIndex = _randomGenerator.Next(0, values.Length);
            var target = values[targetIndex];
            //_outputHelper.WriteLine(string.Join(", ", values.Select(n => $"{n}")));
            _outputHelper.WriteLine($"target item {target}, index {targetIndex}");


            var index = DoFind(values, target);


            var expectedIndex = targetIndex;
            while (expectedIndex >= 0 && values[expectedIndex] >= target)
            {
                expectedIndex--;
            }

            index.Should().Be(expectedIndex);
        }

        private int? DoFind(int[] values, int target)
        {
            try
            {
                return MinAbsSumOfTwo.FindNextSmallerIndex(values, target, 0, values.Length);
            }
            catch (Exception exc)
            {
                _outputHelper.WriteLine(exc.Message);
                return null;
            }
        }

        [Fact]
        public void GetMinAbsSumOfTwo_Sample1_1()
        {
            var values = new[] { 1, 4, -3 };
            var minAbsSum = new MinAbsSumOfTwo().GetMinAbsSumOfTwo(values);
            minAbsSum.Should().Be(1);
        }

        [Fact]
        public void GetMinAbsSumOfTwo_Sample2_3()
        {
            var values = new[] { -8, 4, 5, -10, 3 };
            var minAbsSum = new MinAbsSumOfTwo().GetMinAbsSumOfTwo(values);
            minAbsSum.Should().Be(3);
        }

        [Fact]
        public void GetMinAbsSumOfTwo_ContainsZero_0()
        {
            var values = new[] { -8, 4, 5, -10, 0, 3 };
            var minAbsSum = new MinAbsSumOfTwo().GetMinAbsSumOfTwo(values);
            minAbsSum.Should().Be(0);
        }

        [Fact]
        public void GetMinAbsSumOfTwo_AllPositive_MinPositiveDoubled()
        {
            var values = new[] { 8, 4, 546546448, 10, 3 };
            var minAbsSum = new MinAbsSumOfTwo().GetMinAbsSumOfTwo(values);
            minAbsSum.Should().Be(6);
        }

        [Fact]
        public void GetMinAbsSumOfTwo_AllNegative_MaxNegativeDoubledAbs()
        {
            var values = new[] { -8, -4123221, -5, -10, -3 };
            var minAbsSum = new MinAbsSumOfTwo().GetMinAbsSumOfTwo(values);
            minAbsSum.Should().Be(6);
        }

    }
}
