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

            var solver = new GenomicRangeQuery(sequence);
            var minImpactFactors = solver.MinImpactFactors(starts, ends).ToArray();

            minImpactFactors.Should().Equal(new[] { 2, 4, 1 });
        }

        [Fact]
        public void MinImpactFactor_DoubleCharacterString_TwoPossibleValues()
        {
            var sequence = "TGTTGGGG";
            var starts = new[] { 4, 5, 2 };
            var ends = new[] { 7, 5, 3 };

            var solver = new GenomicRangeQuery(sequence);
            var minImpactFactors = solver.MinImpactFactors(starts, ends).ToArray();

            minImpactFactors.Should().Equal(new[] { 3, 3, 4 });
        }

        [Fact]
        public void MinImpactFactor_AllChars_Correct()
        {
                          //0123456789012345678901
            var sequence = "ACTGTGCAACTGAGAGCCAATT";
            var starts = new[] { 0, 5, 2, 7, 15, 20, 1 };
            var ends = new[] { 21, 5, 3, 10, 17, 21, 6 };
            var expectation = new[] { 1, 3, 3, 1, 2, 4, 2 };

            var solver = new GenomicRangeQuery(sequence);
            var minImpactFactors = solver.MinImpactFactors(starts, ends).ToArray();

            minImpactFactors.Should().Equal(expectation);
        }

        [Fact]
        public void MinImpactFactor_AlmostAllTheSame_Correct()
        {
                          //0123456789012345678901
            var sequence = "GGGGGGGGGAGGGGGGGGGGGG";
            var starts = new[] { 0, 0, 0, 9, 10, 20, 9 };
            var ends = new[] { 21, 8, 9, 21, 21, 20, 9 };
            var expectation = new[] { 1, 3, 1, 1, 3, 3, 1 };

            var solver = new GenomicRangeQuery(sequence);
            var minImpactFactors = solver.MinImpactFactors(starts, ends).ToArray();

            minImpactFactors.Should().Equal(expectation);
        }
    }
}
