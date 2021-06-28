using FluentAssertions;
using LeetCode.Tasks;
using System;
using System.Linq;
using Xunit;
using Xunit.Abstractions;

namespace LeetCode.Tests
{
    [Trait("Category", "Unit")]
    public class P005LongestPalindromicSubstringTests
    {
        private readonly ITestOutputHelper _output;
        private readonly P005LongestPalindrome _sut = new();
        public P005LongestPalindromicSubstringTests(ITestOutputHelper output) => _output = output;

        [Fact]
        public void LongestPalindrome_Empty_Empty()
        {
            _sut.LongestPalindrome(null).Should().Be(String.Empty);
            _sut.LongestPalindrome(String.Empty).Should().Be(String.Empty);
        }

        [Theory]
        [InlineData("babab", "babab")]//whole
        [InlineData("ababad", "ababa")]//head
        [InlineData("dababa", "ababa")]//tail
        [InlineData("cbbd", "bb")]//middle
        [InlineData("bb", "bb")]//middle
        [InlineData("bbb", "bbb")]//middle
        [InlineData("bbbb", "bbbb")]//middle
        [InlineData("ccd", "cc")]//middle
        [InlineData("ac", "a")]//middle
        [InlineData("a", "a")]//middle
        public void LongestPalindrome_Sample_MatchExpectations(string input, string output)
        {
            _sut.LongestPalindrome(input).Should().Be(output);
        }

        [Theory]
        [InlineData(1000)]
        [InlineData(1001)]
        public void LongestPalindrome_LongRepeat_WholeString(int length)
        {
            var symbols = Enumerable.Repeat(9, length).Select(n => (char)n).ToArray();
            var source = new string(symbols);
            _sut.LongestPalindrome(source).Should().Be(source);
        }

        [Fact]
        public void LongestPalindrome_SomeInput1_Fast()
        {
            var source = "babaddtattarrattatddetartrateedredividerb";
            var result = _sut.LongestPalindrome(source);

            _output.WriteLine($"{result}; initial length {source.Length}, result length {result.Length}");
        }
    }
}
