using FluentAssertions;
using LeetCode.Tasks;
using Xunit;

namespace LeetCode.Tests
{
    public class P0003LongestSubstringTests
    {
        private readonly P0003LongestSubstring _sut = new P0003LongestSubstring();

        [Theory]
        [InlineData("abcabcbb", 3)]
        [InlineData("bbbbb", 1)]
        [InlineData("pwwkew", 3)]
        public void FindLongestUniqueLength_ValidInput_MatchExpectations(string s, int expectedLength)
        {
            _sut.FindLongestUniqueLength(s).Should().Be(expectedLength);
        }
    }
}
