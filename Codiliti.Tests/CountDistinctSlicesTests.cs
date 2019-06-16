using Codility.Solvers;
using FluentAssertions;
using System;
using System.Linq;
using Xunit;

namespace Codility.Tests
{
    public class CountDistinctSlicesTests
    {
        [Fact]
        public void CalculateDistinctSlices_Sample_9()
        {
            var input = new int[] { 3, 4, 5, 5, 2 };
            var number = new CountDistinctSlices().CalculateDistinctSlices(input);
            number.Should().Be(9);
        }

        [Fact]
        public void CalculateDistinctSlices_Empty_0()
        {
            var input = new int[0];
            var number = new CountDistinctSlices().CalculateDistinctSlices(input);
            number.Should().Be(0);
        }

        [Fact]
        public void CalculateDistinctSlices_AllTheSame_LengthOfTheInput()
        {
            var input = new int[] { 1, 1, 1, 1 };
            var number = new CountDistinctSlices().CalculateDistinctSlices(input);
            number.Should().Be(input.Length);
        }

        [Fact]
        public void CalculateDistinctSlices_AllDifferent_ArithmeticSum()
        {
            var input = new int[] { 1, 2, 3, 4, 5 };
            var number = new CountDistinctSlices().CalculateDistinctSlices(input);
            number.Should().Be(input.Length * (input.Length + 1) / 2);
        }

        [Fact]
        public void CalculateDistinctSlices_One_One()
        {
            var input = new int[] { 1 };
            var number = new CountDistinctSlices().CalculateDistinctSlices(input);
            number.Should().Be(input.Length * (input.Length + 1) / 2);
        }

        [Fact]
        public void CalculateDistinctSlices_TwoIdentical_2()
        {
            var input = new int[] { 1, 1 };
            var number = new CountDistinctSlices().CalculateDistinctSlices(input);
            number.Should().Be(2);
        }

        [Fact]
        public void CalculateDistinctSlices_101_5()
        {
            var input = new int[] { 1, 0, 1 };
            var number = new CountDistinctSlices().CalculateDistinctSlices(input);
            number.Should().Be(5);
        }

        [Fact]
        public void CalculateDistinctSlices_Overlap_Corect()
        {
            var input = new int[] { 3, 4, 1, 2, 0, 1, 7, 8, 9 };
            var number = new CountDistinctSlices().CalculateDistinctSlices(input);
            number.Should().Be(15 + 21 - 3);
        }

        [Fact]
        public void CalculateDistinctSlices_WithMaxMin_ArithmeticSum()
        {
            var input = new int[] { int.MaxValue, int.MinValue, 0, 3 };
            var number = new CountDistinctSlices().CalculateDistinctSlices(input);
            number.Should().Be(input.Length * (input.Length + 1) / 2);
        }

        [Fact]
        public void CalculateDistinctSlices_Random_SomeResult()
        {
            var random = new Random(DateTime.Now.Millisecond);
            var input = Enumerable.Range(1, 20).Select(n => random.Next(1, 10)).ToArray();
            var number = new CountDistinctSlices().CalculateDistinctSlices(input);
            number.Should().BeGreaterThan(30);
        }

    }
}
