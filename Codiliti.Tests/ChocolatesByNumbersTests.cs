using Codility.Solvers;
using FluentAssertions;
using Xunit;

namespace Codility.Tests
{
    public class ChocolatesByNumbersTests
    {
        [Fact]
        public void OverallEaten_Sample_5()
        {
            var solver = new ChocolatesByNumbers();
            var amount = solver.OverallEaten(10, 4);
            amount.Should().Be(5);
        }

        [Fact]
        public void OverallEaten_AllDividableByStep_DivisionTime()
        {
            var solver = new ChocolatesByNumbers();
            var amount = solver.OverallEaten(10, 5);
            amount.Should().Be(2);
        }

        [Fact]
        public void OverallEaten_AllEqualToStep_1()
        {
            var solver = new ChocolatesByNumbers();
            var amount = solver.OverallEaten(10, 10);
            amount.Should().Be(1);
        }

        [Fact]
        public void OverallEaten_AllSemiprimeToStep_All()
        {
            var solver = new ChocolatesByNumbers();
            var amount = solver.OverallEaten(10, 3);
            amount.Should().Be(10);
        }

        [Fact]
        public void OverallEaten_AllSmallerThanStep_UntilValueRepeats()
        {
            var solver = new ChocolatesByNumbers();
            var amount = solver.OverallEaten(4, 10);
            amount.Should().Be(2);
        }

        [Fact]
        public void OverallEaten_AllSemiprimeStep_UntilValueRepeats()
        {
            var solver = new ChocolatesByNumbers();
            var amount = solver.OverallEaten(947853, 4453);
            amount.Should().Be(947853);
        }
    }
}
