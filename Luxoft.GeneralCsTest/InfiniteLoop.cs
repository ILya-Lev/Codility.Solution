using System;
using FluentAssertions;
using Xunit;

namespace Luxoft.GeneralCsTest
{
    public class InfiniteLoop
    { 
        [Fact]
        public void MaxIntOverflow_NeverEndingLoop()
        {
            var end = int.MaxValue;
            var begin = end - 100;
            var result = 0;
            for (var i = begin; i <= end; i++)
            {
                result++;
                result.Should().BeLessThan(1_000, $"i is {i}, begin is {begin}, end is {end}");
            }

        }

        [Fact]
        public void Postfix_LoopedIncrement_NoChange()
        {
            var j = 0;
            for (int i = 0; i < 10; i++)
            {
                j = j++;        //compiler knows it's an error
            }

            j.Should().Be(0);
        }

        [InlineData(1, 1)]
        [InlineData(10, 10)]
        [InlineData(-1, 0)]
        [Theory]
        public void HalfSequence_MatchExpectations_(int seed, int expected)
        {
            HalfSequence(seed).Should().Be(expected);
        }

        private static double HalfSequence(double n)
        {
            if (n < 1e-20) return 0;
            return HalfSequence(n / 2) + n / 2;
        }

        [Fact] 
        public void Precision_AddSubtract_NeglectTooSmall()
        {
            var a = 1e-5;
            var squaredA = a * a;
            var b = 1e+5;
            var squaredB = b * b;

            var closeToZero = squaredB + squaredA - squaredB;
            closeToZero.Should().Be(0);

            var big = squaredA + squaredB - squaredA;
            big.Should().Be(squaredB);
        }
    }
}
