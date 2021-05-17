using FluentAssertions;
using LeetCode.Tasks;
using Xunit;

namespace LeetCode.Tests
{
    [Trait("Category", "Unit")]
    public class P0065ValidNumberTests
    {
        private readonly P0065ValidNumber _sut = new();

        [Theory]
        [InlineData("2")]
        [InlineData("0089")]
        [InlineData("-0.1")]
        [InlineData("+3.14")]
        [InlineData("4.")]
        [InlineData("-.9")]
        [InlineData("2e10")]
        [InlineData("-90E3")]
        [InlineData("3e+7")]
        [InlineData("+6e-1")]
        [InlineData("53.5e93")]
        [InlineData("-123.456e-789")]
        public void IsNumber_Valid_True(string s)
        {
            _sut.IsNumber(s).Should().BeTrue();
        }

        [Theory]
        [InlineData("abc")]
        [InlineData("..")]
        [InlineData(".e1")]
        [InlineData("e")]
        [InlineData(".")]
        [InlineData("1a")]
        [InlineData("1e")]
        [InlineData("e3")]
        [InlineData("99e2.5")]
        [InlineData("--6")]
        [InlineData("-+3")]
        [InlineData("2+3")]
        [InlineData(".+3")]
        [InlineData("987e654e132")]
        [InlineData("")]
        public void IsNumber_Invalid_False(string s)
        {
            _sut.IsNumber(s).Should().BeFalse();
        }
    }
}
