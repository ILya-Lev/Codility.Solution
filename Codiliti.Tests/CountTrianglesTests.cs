using Codility.Solvers;
using FluentAssertions;
using System.Linq;
using Xunit;

namespace Codility.Tests
{
    public class CountTrianglesTests
    {
        [Fact]
        public void CountTriplets_Sample_4()
        {
            var values = new[] { 10, 2, 5, 1, 8, 12 };
            var tripletsNumber = new CountTriangles().CountTriplets(values);
            tripletsNumber.Should().Be(4);
        }

        [Fact]
        public void CountTriplets_Empty_0()
        {
            var values = new int[0];
            var tripletsNumber = new CountTriangles().CountTriplets(values);
            tripletsNumber.Should().Be(0);
        }

        [Fact]
        public void CountTriplets_TheSame_Cn3()
        {
            var values = new[] { 1, 1, 1, 1, 1, 1 };
            var tripletsNumber = new CountTriangles().CountTriplets(values);
            tripletsNumber.Should().Be(20); //as C from 6 by 3 = 6!/3!/(6-3)!
        }

        [Fact]
        public void CountTriplets_1To9_34()
        {
            var values = Enumerable.Range(1, 9).Reverse().ToArray();
            var tripletsNumber = new CountTriangles().CountTriplets(values);
            tripletsNumber.Should().Be(34);
        }

    }
}
