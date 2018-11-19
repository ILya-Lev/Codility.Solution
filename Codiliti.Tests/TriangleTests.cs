using Codility.Solvers;
using FluentAssertions;
using System.Linq;
using Xunit;

namespace Codility.Tests
{
    public class TriangleTests
    {
        [Fact]
        public void ContainsTriangle_ContainsSample_True()
        {
            int[] sides = new[] { 10, 2, 5, 1, 8, 20 };
            var solver = new Triangle();

            var hasTriangle = solver.ContainsTriangle(sides);

            hasTriangle.Should().BeTrue();
        }
        [Fact]
        public void ContainsTriangle_AbsentSample_False()
        {
            int[] sides = new[] { 10, 50, 5, 1 };
            var solver = new Triangle();

            var hasTriangle = solver.ContainsTriangle(sides);

            hasTriangle.Should().BeFalse();
        }

        [Fact]
        public void ContainsTriangle_AllTheSame_False()
        {
            int[] sides = new[] { 1, 1, 1, 1, 1, 1, 1 };
            var solver = new Triangle();

            var hasTriangle = solver.ContainsTriangle(sides);

            hasTriangle.Should().BeFalse();
        }

        [Fact]
        public void ContainsTriangle_AllNegativeContains_True()
        {
            int[] sides = new[] { -10, -2, -5, -1, -8, -20 };
            var solver = new Triangle();

            var hasTriangle = solver.ContainsTriangle(sides);

            hasTriangle.Should().BeTrue();
        }

        [Fact]
        public void ContainsTriangle_ArithmeticalProgression_True()
        {
            int[] sides = Enumerable.Range(-10, 20).ToArray();
            var solver = new Triangle();

            var hasTriangle = solver.ContainsTriangle(sides);

            hasTriangle.Should().BeTrue();
        }
    }
}
