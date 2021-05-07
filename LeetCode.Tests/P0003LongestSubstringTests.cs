using FluentAssertions;
using LeetCode.Tasks;
using Xunit;

namespace LeetCode.Tests
{
    public class P0003LongestSubstringTests
    {
        private readonly P0003LongestSubstring_002 _sut = new();

        [Theory]
        [InlineData("abcabcbb", 3)]
        [InlineData("bbbbb", 1)]
        [InlineData("pwwkew", 3)]
        [InlineData("abba", 2)]
        public void FindLongestUniqueLength_ValidInput_MatchExpectations(string s, int expectedLength)
        {
            _sut.FindLongestUniqueLength(s).Should().Be(expectedLength);
        }
    }
}
