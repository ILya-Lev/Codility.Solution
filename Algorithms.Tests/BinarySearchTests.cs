using Algorithms.Solutions;
using FluentAssertions;
using Xunit;

namespace Algorithms.Tests
{
    [Trait("Category", "Unit")]
    public class BinarySearchTests
    {
        [Theory]
        [InlineData(new[] { 1, 2, 3, 4, 5 }, 0, -1)]
        [InlineData(new[] { 1, 2, 3, 4, 5 }, 1, 0)]
        [InlineData(new[] { 1, 2, 3, 4, 5 }, 2, 1)]
        [InlineData(new[] { 1, 2, 3, 4, 5 }, 3, 2)]
        [InlineData(new[] { 1, 2, 3, 4, 5 }, 4, 3)]
        [InlineData(new[] { 1, 2, 3, 4, 5 }, 5, 4)]
        [InlineData(new[] { 1, 2, 3, 4, 5 }, 6, -5)]
        [InlineData(new[] { 1, 2, 3, 4, 5 }, 7, -5)]
        [InlineData(new[] { 1, 2, 3, 5, 6 }, 4, -4)]//~expected is where key should be inserted
        public void Find_Sample_ExpectedIndex(int[] source, int key, int expected)
        {
            BinarySearch<int>.Find(source, key).Should().Be(expected);
        }
    }
}