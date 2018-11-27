using Codility.Solvers;
using FluentAssertions;
using Xunit;

namespace Codility.Tests
{
    public class ArrayDenominatorTests
    {
        [Fact]
        public void GetDenominatorIndex_Sample_0()
        {
            var values = new[] { 3, 4, 3, 2, 3, -1, 3, 3 };
            var solver = new ArrayDenominator();

            var index = solver.GetDenominatorIndex(values);

            index.Should().Be(0);
        }

        [Fact]
        public void GetDenominatorIndex_AllUnique_MinusOne()
        {
            var values = new[] { 7, 8, 9, 4, 5, 6, 1, 2, 3 };
            var solver = new ArrayDenominator();

            var index = solver.GetDenominatorIndex(values);

            index.Should().Be(-1);
        }

        [Fact]
        public void GetDenominatorIndex_Empty_MinusOne()
        {
            var values = new int[0];
            var solver = new ArrayDenominator();

            var index = solver.GetDenominatorIndex(values);

            index.Should().Be(-1);
        }
    }
}
