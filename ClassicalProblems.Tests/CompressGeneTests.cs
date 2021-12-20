using System;
using FluentAssertions;
using System.Linq;
using Xunit;

namespace ClassicalProblems.Tests
{
    public class CompressGeneTests
    {
        enum Nucleotide {A=1,C=2,G=3,T=0}

        [Fact]
        public void Compress_Decompress_Coincide()
        {
            const int size = 100_000_000;
            var gene = new string(Enumerable.Range(1, size).Select(n => ((Nucleotide)(n % 4)).ToString()[0]).ToArray());

            var compressed = CompressGene.Compress(gene);
            var decompressed = CompressGene.Decompress(compressed);

            decompressed.Should().BeEquivalentTo(gene);
        }
    }
}
