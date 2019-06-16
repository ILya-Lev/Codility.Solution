using Codility.Solvers;
using FluentAssertions;
using Xunit;

namespace Codility.Tests
{
    public class AbsDistinctTests
    {
        [Fact]
        public void Amount_Sample_5()
        {
            var values = new[] { -5, -3, 0, 1, 3, 6 };
            var amount = new AbsDistinct().Amount(values);
            amount.Should().Be(5);
        }

        [Fact]
        public void Amount_Empty_0()
        {
            var values = new int[0];
            var amount = new AbsDistinct().Amount(values);
            amount.Should().Be(0);
        }

        [Fact]
        public void Amount_TheSameAbsolute_1()
        {
            var values = new[] { -5, -5, 5, 5, 5, 5 };
            var amount = new AbsDistinct().Amount(values);
            amount.Should().Be(1);
        }

        [Fact]
        public void Amount_TheSame_1()
        {
            var values = new[] { 5, 5, 5, 5, 5, 5 };
            var amount = new AbsDistinct().Amount(values);
            amount.Should().Be(1);
        }

        [Fact]
        public void Amount_OneItem_1()
        {
            var values = new[] { -5 };
            var amount = new AbsDistinct().Amount(values);
            amount.Should().Be(1);
        }
        [Fact]
        public void Amount_OneItemNegativeInt_1()
        {
            var values = new[] { int.MinValue };
            var amount = new AbsDistinct().Amount(values);
            amount.Should().Be(1);
        }

        [Fact]
        public void Amount_TwoItems_2()
        {
            var values = new[] { -5, 3 };
            var amount = new AbsDistinct().Amount(values);
            amount.Should().Be(2);
        }

    }
}
