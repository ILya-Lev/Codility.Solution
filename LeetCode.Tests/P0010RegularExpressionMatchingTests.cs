using FluentAssertions;
using LeetCode.Tasks;
using Xunit;

namespace LeetCode.Tests
{
    public class P0010RegularExpressionMatchingTests
    {
        private readonly P0010RegularExpressionMatching_002 _sut = new();

        [Fact]
        public void IsMatch_WildcardsOnly_True()
        {
            _sut.IsMatch("lvjnlvnljf", ".*..*.*").Should().BeTrue();
        }

        [Theory]
        [InlineData("aa", "a*")]
        [InlineData("aa", "a*a")]
        [InlineData("aa", "a*aa")]
        [InlineData("aaa", "a*a")]
        [InlineData("aaa", "a.a")]
        [InlineData("ab", ".*")]
        [InlineData("aab", "c*a*b")]
        [InlineData("aaa", "ab*a*c*a")]
        public void IsMatch_Simple_True(string s, string p)
        {
            _sut.IsMatch(s, p).Should().BeTrue();
        }

        [Theory]
        [InlineData("aa", "a")]
        [InlineData("aa", "a*aaa")]
        [InlineData("mississippi", "mis*is*p*.")]
        [InlineData("ab", ".*c")]
        public void IsMatch_Simple_False(string s, string p)
        {
            _sut.IsMatch(s, p).Should().BeFalse();
        }
    }
}
