﻿using FluentAssertions;
using LeetCode.Tasks;
using Xunit;

namespace LeetCode.Tests
{
    public class P0044WildcardMatchingTests
    {
        private readonly P0044WildcardMatching _sut = new();

        [Fact]
        public void IsMatch_WildcardsOnly_True()
        {
            _sut.IsMatch("lvjnlvnljf", "?*??*?*").Should().BeTrue();
        }

        [Theory]
        [InlineData("aa", "a*")]
        [InlineData("aa", "a*a")]
        [InlineData("aaa", "a*a")]
        [InlineData("aaa", "a?a")]
        [InlineData("ab", "?*")]
        [InlineData("mississippi", "mis*is*p*")]
        public void IsMatch_Simple_True(string s, string p)
        {
            _sut.IsMatch(s, p).Should().BeTrue();
        }

        [Theory]
        [InlineData("aa", "a")]
        [InlineData("aa", "a*aa")]
        [InlineData("aa", "a*aaa")]
        [InlineData("ab", "?*c")]
        [InlineData("aab", "c*a*b")]
        [InlineData("aaa", "ab*a*c*a")]
        [InlineData("aaabbbaabaaaaababaabaaabbabbbbbbbbaabababbabbbaaaaba", "a*******b")]
        public void IsMatch_Simple_False(string s, string p)
        {
            _sut.IsMatch(s, p).Should().BeFalse();
        }
    }
}
