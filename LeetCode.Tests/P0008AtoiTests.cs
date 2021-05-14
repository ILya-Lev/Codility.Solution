using FluentAssertions;
using LeetCode.Tasks;
using System.Linq;
using Xunit;

namespace LeetCode.Tests
{
    [Trait("Category", "Unit")]
    public class P0008AtoiTests
    {
        private readonly P0008Atoi _sut = new P0008Atoi();

        [Theory]
        [InlineData("string and 918261")]
        [InlineData("\\t918261")]// \t instead of \s
        [InlineData("--918261")]
        [InlineData("-+918261")]
        [InlineData("00000-654654")]
        [InlineData("00000+654654")]
        [InlineData("   +0 321")]
        public void MyAtoi_Invalid_Zero(string input)
        {
            _sut.MyAtoi(input).Should().Be(0);
        }

        [Theory]
        [InlineData("918261 and string", 918261)]
        [InlineData("918261", 918261)]
        [InlineData(" 918261", 918261)]
        [InlineData("   -918261", -918261)]
        [InlineData("   +918261", 918261)]
        [InlineData("-5-", -5)]
        public void MyAtoi_Valid_MatchExpectations(string input, int expected)
        {
            _sut.MyAtoi(input).Should().Be(expected);
        }

        [Fact]
        public void MyAtoi_TooBig_StickToInt32Max()
        {
            var input = Enumerable.Repeat(4, 200)
                .Select(n => n.ToString())
                .Aggregate((current, next) => current + next);

            _sut.MyAtoi(input).Should().Be(int.MaxValue);
        }

        [Fact]
        public void MyAtoi_TooSmall_StickToInt32Min()
        {
            var input = Enumerable.Repeat(4, 200)
                .Select(n => n.ToString())
                .Aggregate((current, next) => current + next);

            _sut.MyAtoi($"-{input}").Should().Be(int.MinValue);
        }

        [Fact]
        public void MyAtoi_TooSmallWith0AtEdge_StickToInt32Min()
        {
            //2147483647 == int.max, next digit is 0, so int.max + next digit <= int.max; but the number itself is out of reach
            _sut.MyAtoi("-21474836470").Should().Be(int.MinValue);
        }
    }
}
