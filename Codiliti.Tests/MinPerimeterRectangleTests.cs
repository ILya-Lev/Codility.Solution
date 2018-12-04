using Codility.Solvers;
using FluentAssertions;
using Xunit;

namespace Codility.Tests
{
    public class MinPerimeterRectangleTests
    {
        [InlineData(1, 4)]
        [InlineData(4, 8)]
        [InlineData(9, 12)]
        [Theory]
        public void GetMinPerimeter_Square_4TimesRoot(int area, int perimeter)
        {
            var solver = new MinPerimeterRectangle();
            var p = solver.GetMinPerimeter(area);
            p.Should().Be(perimeter);
        }

        [InlineData(2)]
        [InlineData(3)]
        [InlineData(5)]
        [InlineData(7)]
        [InlineData(11)]
        [Theory]
        public void GetMinPerimeter_Prime_NPlus1Twice(int area)
        {
            var solver = new MinPerimeterRectangle();
            var p = solver.GetMinPerimeter(area);
            p.Should().Be(2 * (area + 1));
        }

        [InlineData(6, 10)]
        [InlineData(8, 12)]
        [InlineData(10, 14)]
        [InlineData(12, 14)]
        [InlineData(24, 20)]
        [Theory]
        public void GetMinPerimeter_Composite_ClosestToRoot(int area, int perimeter)
        {
            var solver = new MinPerimeterRectangle();
            var p = solver.GetMinPerimeter(area);
            p.Should().Be(perimeter);
        }
    }
}
