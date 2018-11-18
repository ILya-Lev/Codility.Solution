using Codility.Solvers;
using FluentAssertions;
using System.Linq;
using Xunit;

namespace Codility.Tests
{
    public class GenomicRangeQueryTests
    {
        [Fact]
        public void MinImpactFactor_Sample_3()
        {
            var sequence = "CAGCCTA";
            var starts = new[] { 2, 5, 0 };
            var ends = new[] { 4, 5, 6 };

            var solver = new GenomicRangeQuery();
            var minImpactFactors = solver.MinImpactFactors(sequence, starts, ends).ToArray();

            minImpactFactors.Should().Equal(new[] { 2, 4, 1 });
        }
    }
}
