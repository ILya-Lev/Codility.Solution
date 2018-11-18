using Codility.Solvers;
using FluentAssertions;
using System;
using System.Diagnostics;
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

            var solver = new GenomicRangeQuery(sequence);
            var minImpactFactors = solver.MinImpactFactors(starts, ends).ToArray();

            minImpactFactors.Should().Equal(new[] { 2, 4, 1 });
        }

        [Fact]
        public void MinImpactFactor_Big_LessThan100Mls()
        {
            var random = new Random(DateTime.UtcNow.Millisecond);
            var code = new[] { 'A', 'C', 'G', 'T' };
            var sequence = Enumerable.Range(1, 100_000).Select(n => code[n % 4]).ToArray();
            var starts = Enumerable.Range(1, 50_000).Select(n => random.Next(0, 80_000)).ToArray();
            var ends = Enumerable.Range(1, 50_000).Select(n => random.Next(20_000, 100_000)).ToArray();

            var stopWatch = Stopwatch.StartNew();
            var solver = new GenomicRangeQuery(new string(sequence));
            var minImpactFactors = solver.MinImpactFactors(starts, ends).ToArray();
            stopWatch.Stop();

            stopWatch.ElapsedMilliseconds.Should().BeLessOrEqualTo(100);
        }
    }
}
