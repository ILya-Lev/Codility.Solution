using FluentAssertions;
using LeetCode.Tasks;
using Xunit;

namespace LeetCode.Tests
{
    public class P0273IntegerToEnglishWordsTests
    {
        private readonly P0273IntegerToEnglishWords _sut = new();

        [Theory]
        [InlineData(1234567891, "One Billion Two Hundred Thirty Four Million Five Hundred Sixty Seven Thousand Eight Hundred Ninety One")]
        [InlineData(1234567, "One Million Two Hundred Thirty Four Thousand Five Hundred Sixty Seven")]
        [InlineData(12345, "Twelve Thousand Three Hundred Forty Five")]
        [InlineData(123, "One Hundred Twenty Three")]
        [InlineData(12, "Twelve")]
        [InlineData(10, "Ten")]
        [InlineData(1, "One")]
        [InlineData(0, "Zero")]
        public void NumberToWords_Sample_MatchExpectations(int n, string s)
        {
            _sut.NumberToWords(n).Should().Be(s);
        }
    }
}
