using Codility.Solvers;
using FluentAssertions;
using System.Linq;
using Xunit;

namespace Codility.Tests
{
    public class CountNonDivisibleTests
    {
        [Fact]
        public void GetNonDivisibleAmount_Sample_Sample()
        {
            var values = new[] { 3, 1, 2, 3, 6 };
            var solver = new CountNonDivisible();
            var sequence = solver.GetNonDivisibleAmount(values);
            sequence.Should().Equal(new[] {2, 4, 3, 2, 0});
        }

        [Fact]
        public void GetNonDivisibleAmount_OneToTen_Fast()
        {
            var values = Enumerable.Range(1, 10).ToArray();
            var solver = new CountNonDivisible();
            var sequence = solver.GetNonDivisibleAmount(values);
            sequence.Should().Equal(new[] { 9, 8, 8, 7, 8, 6, 8, 6, 7, 6 });
        }

        [Fact]
        public void GetNonDivisibleAmount_TenToOne_Fast()
        {
            var values = Enumerable.Range(1, 10).Reverse().ToArray();
            var solver = new CountNonDivisible();
            var sequence = solver.GetNonDivisibleAmount(values);
            sequence.Should().Equal(new[] { 9, 8, 8, 7, 8, 6, 8, 6, 7, 6 }.Reverse());
        }

        [Fact]
        public void GetNonDivisibleAmount_AllTheSame_Zeros()
        {
            var size = 10_000;
            var values = Enumerable.Repeat(1,size).ToArray();
            var solver = new CountNonDivisible();
            var sequence = solver.GetNonDivisibleAmount(values);
            sequence.Should().Equal(Enumerable.Repeat(0, size));
        }
    }
}
