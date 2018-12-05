using Codility.Solvers;
using FluentAssertions;
using System.Linq;
using Xunit;

namespace Codility.Tests
{
    public class CountSemiprimesTests
    {
        [Fact]
        public void GetSemiprimesPopulation_sample_1040()
        {
            var lower = new[] { 1, 4, 16 };
            var upper = new[] { 26, 10, 20 };
            var solver = new CountSemiprimes();

            var populationSequence = solver.GetSemiprimesPopulation(26, lower, upper).ToArray();

            populationSequence.Should().Equal(new[] { 10, 4, 0 });
        }
    }
}
