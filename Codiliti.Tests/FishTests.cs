using Codility.Solvers;
using FluentAssertions;
using Xunit;

namespace Codility.Tests
{
    public class FishTests
    {
        [Fact]
        public void SurvivalAmount_Sample_2()
        {
            var size = new[] { 4, 3, 2, 1, 5 };
            var direction = new[] { 0, 1, 0, 0, 0 };
            var solver = new Fish();
            var survived = solver.SurvivalAmount(size, direction);
            survived.Should().Be(2);
        }
        [Fact]
        public void SurvivalAmount_AllUp_All()
        {
            var size = new[] { 4, 3, 2, 1, 5 };
            var direction = new[] { 0, 0, 0, 0, 0 };
            var solver = new Fish();
            var survived = solver.SurvivalAmount(size, direction);
            survived.Should().Be(5);
        }
        [Fact]
        public void SurvivalAmount_AllDown_All()
        {
            var size = new[] { 4, 3, 2, 1, 5 };
            var direction = new[] { 1, 1, 1, 1, 1 };
            var solver = new Fish();
            var survived = solver.SurvivalAmount(size, direction);
            survived.Should().Be(5);
        }
    }
}
