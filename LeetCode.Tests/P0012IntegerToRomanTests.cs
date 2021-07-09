using FluentAssertions;
using LeetCode.Tasks;
using Xunit;

namespace LeetCode.Tests
{
    public class P0012IntegerToRomanTests
    {
        private readonly P0012IntegerToRoman _sut = new();

        [Theory]
        [InlineData(3, "III")]
        [InlineData(4, "IV")]
        [InlineData(9, "IX")]
        [InlineData(58, "LVIII")]
        [InlineData(1994, "MCMXCIV")]
        public void IntToRoman_Sample_AsExpected(int integer, string roman)
        {
            _sut.IntToRoman(integer).Should().Be(roman);
        }
    }
}
