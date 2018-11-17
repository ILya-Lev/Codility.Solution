using Codility.Solvers;
using FluentAssertions;
using System.Linq;
using Xunit;

namespace Codility.Tests
{
    public class PassingCarsTests
    {
        [Fact]
        public void Amount_OnePair_1()
        {
            var cars = new[] { 0, 1 };
            var solver = new PassingCars(cars);

            var passingTotal = solver.Amount();

            passingTotal.Should().Be(1);
        }

        [InlineData(1, 1)]
        [InlineData(5, 6)]
        [InlineData(10, 0)]
        [InlineData(0, 10)]
        [Theory]
        public void Amount_AllWestBeforeAllEast_0(int amountWest, int amountEast)
        {
            var cars = Enumerable.Repeat(1, amountWest)
                .Concat(Enumerable.Repeat(0, amountEast))
                .ToArray();
            var solver = new PassingCars(cars);

            var passingTotal = solver.Amount();

            passingTotal.Should().Be(0);
        }

        [InlineData(1, 1)]
        [InlineData(5, 6)]
        [InlineData(10, 0)]
        [InlineData(0, 10)]
        [Theory]
        public void Amount_AllEastBeforeAllWest_Multiplication(int amountWest, int amountEast)
        {
            var cars = Enumerable.Repeat(0, amountEast)
                .Concat(Enumerable.Repeat(1, amountWest))
                .ToArray();
            var solver = new PassingCars(cars);

            var passingTotal = solver.Amount();

            passingTotal.Should().Be(amountEast * amountWest);
        }

        [Fact]
        public void Amount_SampleExample_5()
        {
            var cars = new[] { 0, 1, 0, 1, 1 };
            var solver = new PassingCars(cars);

            var passingTotal = solver.Amount();

            passingTotal.Should().Be(5);
        }

        [Fact]
        public void Amount_MoreThanBillion_MinusOne()
        {
            var cars = Enumerable.Repeat(0, 1_000_000)
                .Concat(Enumerable.Repeat(1, 1_000_000))
                .ToArray();
            var solver = new PassingCars(cars);

            var passingTotal = solver.Amount();

            passingTotal.Should().Be(-1);
        }
    }
}
