using FluentAssertions;
using LeetCode.Tasks;
using Xunit;

namespace LeetCode.Tests
{
    [Trait("Category", "Unit")]
    public class P0234PalindromeLinkedListTests
    {
        private readonly P0234PalindromeLinkedList _sut = new();

        [Theory]
        [InlineData(new[] { 1, 2, 2, 1 })]
        [InlineData(new[] { 1, 2, 3, 2, 1 })]
        public void IsPalindrome_1221_True(int[] sequence)
        {
            var head = sequence.FromSameOrder();
            _sut.IsPalindrome(head).Should().BeTrue();
        }

        [Theory]
        [InlineData(new[] { 1, 2 })]
        [InlineData(new[] { 1, 2, 1, 2 })]
        [InlineData(new[] { 1, 2, 3, 1, 2 })]
        [InlineData(new[] { 1, 0, 0, 0, 1, 0 })]
        [InlineData(new[] { 1, 0, 0, 1, 0, 0 })]
        public void IsPalindrome_1212_False(int[] sequence)
        {
            var head = sequence.FromSameOrder();
            _sut.IsPalindrome(head).Should().BeFalse();
        }
    }
}
