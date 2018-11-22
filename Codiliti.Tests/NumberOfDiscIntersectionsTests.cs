using Codility.Solvers;
using FluentAssertions;
using Xunit;

namespace Codility.Tests
{
    public class NumberOfDiscIntersectionsTests
    {
        [Fact]
        public void GetNumber_Sample_11()
        {
            var solver = new NumberOfDiscIntersections();
            var radiuses = new[] { 1, 5, 2, 1, 4, 0 };
            var intersections = solver.GetNumber(radiuses);
            intersections.Should().Be(11);
        }

        [Fact]
        public void GetNumber_SmallExtreme_2()
        {
            var solver = new NumberOfDiscIntersections();
            var radiuses = new[] { 1, 2147483647, 0 };
            var intersections = solver.GetNumber(radiuses);
            intersections.Should().Be(2);
        }
    }
}
