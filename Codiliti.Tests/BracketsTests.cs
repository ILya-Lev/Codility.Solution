using Codility.Solvers;
using FluentAssertions;
using Xunit;

namespace Codility.Tests
{
    public class BracketsTests
    {
        [Fact]
        public void IsProperlyNested_Sample_True()
        {
            var input = "[{()()}]";
            var solver = new Brackets();

            var isProperlyNested = solver.IsProperlyNested(input);

            isProperlyNested.Should().BeTrue();
        }

        [Fact]
        public void IsProperlyNested_IncorrectOrder_False()
        {
            var input = "[{())}]";
            var solver = new Brackets();

            var isProperlyNested = solver.IsProperlyNested(input);

            isProperlyNested.Should().BeFalse();
        }

        [Fact]
        public void IsProperlyNested_OnlyOpening_False()
        {
            var input = "[[[[[";
            var solver = new Brackets();

            var isProperlyNested = solver.IsProperlyNested(input);

            isProperlyNested.Should().BeFalse();
        }

        [Fact]
        public void IsProperlyNested_OnlyClosing_False()
        {
            var input = "]]]]";
            var solver = new Brackets();

            var isProperlyNested = solver.IsProperlyNested(input);

            isProperlyNested.Should().BeFalse();
        }
    }
}