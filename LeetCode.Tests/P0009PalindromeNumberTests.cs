using FluentAssertions;
using LeetCode.Tasks;
using Xunit;

namespace LeetCode.Tests
{
    [Trait("Category", "Unit")]
    public class P0009PalindromeNumberTests
    {
        private readonly P0009PalindromeNumber _sut = new();

        [Fact]
        public void IsPalindrome_Negative_No()
        {
            _sut.IsPalindrome(-12321).Should().BeFalse();
        }

        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(22)]
        [InlineData(333)]
        [InlineData(4114)]
        [InlineData(51215)]
        [InlineData(651156)]
        [InlineData(7651567)]
        [InlineData(86511568)]
        [InlineData(906505609)]
        [InlineData(1000000001)]
        public void IsPalindrome_Yes(int x)
        {
            _sut.IsPalindrome(x).Should().BeTrue();
        }

        [Theory]
        [InlineData(20)]
        [InlineData(133)]
        [InlineData(4214)]
        [InlineData(53215)]
        [InlineData(601156)]
        [InlineData(7621567)]
        [InlineData(86511528)]
        [InlineData(906505209)]
        [InlineData(1090000001)]
        public void IsPalindrome_No(int x)
        {
            _sut.IsPalindrome(x).Should().BeFalse();
        }
    }
}
