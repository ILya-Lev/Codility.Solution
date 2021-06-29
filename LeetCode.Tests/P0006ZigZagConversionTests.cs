using FluentAssertions;
using LeetCode.Tasks;
using Xunit;

namespace LeetCode.Tests
{
    public class P0006ZigZagConversionTests
    {
        private readonly P0006ZigZagConversion _sut = new();

        [Fact]
        public void Convert_StringLengthEqualNumberOfStrings_TheSame()
        {
            var seed = "PaypalIsHiring";
            _sut.Convert(seed, seed.Length).Should().Be(seed);
        }

        [Fact]
        public void Convert_OneSymbol_TheSame()
        {
            var seed = ".";
            _sut.Convert(seed, seed.Length).Should().Be(seed);
        }

        [Theory]
        [InlineData("PaypalIsHiring", 3, "PaHnaplsiigyIr")]
        [InlineData("PaypalIsHiring", 4, "PInalsigyaHrpi")]
        [InlineData("ab", 1, "ab")]
        [InlineData("abcdefg", 2, "acegbdf")]
        public void Convert_Sample_MatchExpectations(string seed, int rowNumber, string expected)
        {
            _sut.Convert(seed, rowNumber).Should().Be(expected);
        }

    }
}
