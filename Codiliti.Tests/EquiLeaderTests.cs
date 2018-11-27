using Codility.Solvers;
using FluentAssertions;
using System.Linq;
using Xunit;

namespace Codility.Tests
{
    public class EquiLeaderTests
    {
        [Fact]
        public void GetNumber_Sample_2()
        {
            var values = new[] { 4, 3, 4, 4, 4, 2 };
            var equiLeader = new EquiLeader(values);

            var number = equiLeader.GetNumberOfEquiLeaders();

            number.Should().Be(2);
        }

        [Fact]
        public void GetNumber_TwoEqualItems_1()
        {
            var values = new[] { 4, 4 };
            var equiLeader = new EquiLeader(values);

            var number = equiLeader.GetNumberOfEquiLeaders();

            number.Should().Be(1);
        }

        [Fact]
        public void GetNumber_ManyEqualItems_NumberOfItemsMinusOne()
        {
            var size = 100_000;
            var values = Enumerable.Repeat(1_000_000_000, size).ToArray();
            var equiLeader = new EquiLeader(values);

            var number = equiLeader.GetNumberOfEquiLeaders();

            number.Should().Be(size - 1);
        }

        [Fact]
        public void GetNumber_NaturalSequence_0()
        {
            var size = 100_000;
            var values = Enumerable.Range(1, size).ToArray();
            var equiLeader = new EquiLeader(values);

            var number = equiLeader.GetNumberOfEquiLeaders();

            number.Should().Be(0);
        }
    }
}
