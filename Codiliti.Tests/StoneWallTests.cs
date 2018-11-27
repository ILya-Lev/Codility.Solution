using Codility.Solvers;
using FluentAssertions;
using System.Linq;
using Xunit;

namespace Codility.Tests
{
    public class StoneWallTests
    {
        [Fact]
        public void BricksNumber_SimpleExample_7()
        {
            var heights = new int[] { 8, 8, 5, 7, 9, 8, 7, 4, 8 };
            var solver = new StoneWall();
            var bricksNumber = solver.BricksNumber(heights);
            bricksNumber.Should().Be(7);
        }

        [Fact]
        public void BricksNumber_MonotonicIncrease_AmountOfHeights()
        {
            var heightsNumber = 10;
            var heights = Enumerable.Range(1, heightsNumber).ToArray();
            var solver = new StoneWall();

            var bricksNumber = solver.BricksNumber(heights);

            bricksNumber.Should().Be(heightsNumber);
        }

        [Fact]
        public void BricksNumber_MonotonicDecrease_AmountOfHeights()
        {
            var heightsNumber = 10;
            var heights = Enumerable.Range(1, heightsNumber).Select(n => heightsNumber - n).ToArray();
            var solver = new StoneWall();

            var bricksNumber = solver.BricksNumber(heights);

            bricksNumber.Should().Be(heightsNumber);
        }

        [Fact]
        public void BricksNumber_AllTheSame_1()
        {
            var heightsNumber = 10;
            var heights = Enumerable.Range(1, heightsNumber).Select(n => heightsNumber).ToArray();
            var solver = new StoneWall();

            var bricksNumber = solver.BricksNumber(heights);

            bricksNumber.Should().Be(1);
        }

        [Fact]
        public void BricksNumber_StepsDown_3()
        {
            var heights = new[] { 9, 9, 9, 8, 8, 8, 7, 7, 7 };
            var solver = new StoneWall();

            var bricksNumber = solver.BricksNumber(heights);

            bricksNumber.Should().Be(3);
        }

        [Fact]
        public void BricksNumber_StepsUp_3()
        {
            var heights = new[] { 7, 7, 7, 8, 8, 8, 9, 9, 9 };
            var solver = new StoneWall();

            var bricksNumber = solver.BricksNumber(heights);

            bricksNumber.Should().Be(3);
        }

        [Fact]
        public void BricksNumber_Hill_3()
        {
            var heights = new[] { 7, 9, 9, 8, 8, 7 };
            var solver = new StoneWall();

            var bricksNumber = solver.BricksNumber(heights);

            bricksNumber.Should().Be(3);
        }

        [Fact]
        public void BricksNumber_DoubleHill_5()
        {
            var heights = new[] { 7, 9, 9, 8, 8, 7, 7, 9, 9, 8, 8, 7 };
            var solver = new StoneWall();

            var bricksNumber = solver.BricksNumber(heights);

            bricksNumber.Should().Be(5);
        }


    }
}
