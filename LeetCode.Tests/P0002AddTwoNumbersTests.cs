using FluentAssertions;
using LeetCode.Tasks;
using Xunit;

namespace LeetCode.Tests
{
    [Trait("Category", "Unit")]
    public class P0002AddTwoNumbersTests
    {
        private readonly P0002AddTwoNumbers _sut = new();

        [Fact]
        public void AddTwoNumbers_TheSameLength_SimpleAddition()
        {
            var first = new[] { 3, 4, 2 }.FromDirectOrder();
            var second = new[] { 4, 6, 5 }.FromDirectOrder();
            var expected = new[] { 8, 0, 7 }.FromDirectOrder();

            var total = _sut.AddTwoNumbers(first, second);

            total.Should().Be(expected);
        }

        [Fact]
        public void AddTwoNumbers_TwoZeros_Zero()
        {
            var first = new[] { 0 }.FromDirectOrder();
            var second = new[] { 0 }.FromDirectOrder();
            var expected = new[] { 0 }.FromDirectOrder();

            var total = _sut.AddTwoNumbers(first, second);

            total.Should().Be(expected);
        }

        [Fact]
        public void AddTwoNumbers_5And5_Ten()
        {
            var first = new[] { 5 }.FromDirectOrder();
            var second = new[] { 5 }.FromDirectOrder();
            var expected = new[] { 1, 0 }.FromDirectOrder();

            var total = _sut.AddTwoNumbers(first, second);

            total.Should().Be(expected);
        }

        [Fact]
        public void AddTwoNumbers_Many9s_PropagateOne()
        {
            var first = new[] { 9, 9, 9, 9, 9, 9, 9 }.FromDirectOrder();
            var second = new[] { 9, 9, 9, 9 }.FromDirectOrder();
            var expected = new[] { 1, 0, 0, 0, 9, 9, 9, 8 }.FromDirectOrder();

            var total = _sut.AddTwoNumbers(first, second);

            total.Equals(expected).Should().BeTrue();
        }

        [Fact]
        public void AddTwoNumbers_Simple002_PropagateOne()
        {
            var first = new[] { 9, 4, 2 }.FromDirectOrder();
            var second = new[] { 9, 4, 6, 5 }.FromDirectOrder();
            var expected = new[] { 1, 0, 4, 0, 7 }.FromDirectOrder();

            var total = _sut.AddTwoNumbers(first, second);

            total.Equals(expected).Should().BeTrue();
        }
    }
}
