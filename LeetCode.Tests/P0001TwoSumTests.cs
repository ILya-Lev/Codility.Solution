using FluentAssertions;
using LeetCode.Tasks;
using System;
using System.Collections.Generic;
using Xunit;

namespace LeetCode.Tests
{
    [Trait("Category", "Unit")]
    public class P0001TwoSumTests
    {
        private readonly P0001TwoSum _sut = new P0001TwoSum();

        [Fact]
        public void TwoSum_NoDuplicates_ContainSolution()
        {
            var numbers = new[] { 9, 7, 3, 1, 6, 4, 2 };
            _sut.TwoSum(numbers, 13).Should().Equal(new[] { 0, 5 });        //values 9 and 4 instead of 7 and 6
        }

        [Fact]
        public void TwoSum_WithDuplicates_ContainSolution()
        {
            var numbers = new[] { 3, 3, 3 };
            _sut.TwoSum(numbers, 6).Should().Equal(new[] { 0, 1 });
        }

        [Fact]
        public void TwoSum_DoesNotContainSolution_Exception()
        {
            var numbers = new[] { 9, 7, 3, 1, 6, 4, 2 };
            Action getTwoSum = () => _sut.TwoSum(numbers, 500);
            getTwoSum.Should().Throw<KeyNotFoundException>();
        }
    }
}
