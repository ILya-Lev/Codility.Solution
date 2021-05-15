using FluentAssertions;
using LeetCode.Tasks;
using System;
using Xunit;

namespace LeetCode.Tests
{
    [Trait("Category", "Unit")]
    public class P0004MedianOfTwoSortedArraysTests
    {
        private readonly P0004MedianOfTwoSortedArrays _sut = new();

        [Theory]
        [InlineData(new[] { 1, 2, 3, 4 }, new[] { 5, 6, 7 }, 4)]
        [InlineData(new[] { 1, 2, 3 }, new[] { 5, 6, 7 }, 4)]
        [InlineData(new[] { 1, 2, 4 }, new[] { 5, 6, 7 }, 4.5)]
        [InlineData(new[] { 1, 2, 5 }, new[] { 5, 6, 7 }, 5)]
        [InlineData(new[] { 0, 0 }, new[] { 0, 0 }, 0)]
        public void FindMedianSortedArrays_NoOverlap_ConstantTime(int[] lhs, int[] rhs, double median)
        {
            _sut.FindMedianSortedArrays(lhs, rhs).Should().Be(median);
        }

        [Fact]
        public void FindMedianSortedArrays_FirstIsEmpty_MedianFromSecond()
        {
            _sut.FindMedianSortedArrays(Array.Empty<int>(), new[] { 5, 6, 7 }).Should().Be(6);
        }

        [Fact]
        public void FindMedianSortedArrays_SecondIsEmpty_MedianFromFirst()
        {
            _sut.FindMedianSortedArrays(new[] { 5, 6, 7, 8 }, Array.Empty<int>()).Should().Be(6.5);
        }

        [Fact]
        public void FindMedianSortedArrays_WithOverlap_Even_LogTime()
        {
            var lhs = new[] { 1, 2, 5, 6 };
            var rhs = new[] { 3, 4, 7, 8 };
            _sut.FindMedianSortedArrays(lhs, rhs).Should().Be(4.5);
        }

        [Fact]
        public void FindMedianSortedArrays_WithOverlap_Odd_LogTime()
        {
            var lhs = new[] { 1, 2, 5, 6 };
            var rhs = new[] { 3, 4, 7 };
            _sut.FindMedianSortedArrays(lhs, rhs).Should().Be(4);
        }
    }
}
