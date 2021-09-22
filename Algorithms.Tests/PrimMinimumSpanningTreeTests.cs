using System.Formats.Asn1;
using System.Linq;
using Algorithms.Solutions;
using FluentAssertions;
using FluentAssertions.Execution;
using Xunit;

namespace Algorithms.Tests
{
    public class PrimMinimumSpanningTreeTests
    {
        [Fact]
        public void DiscoverMst_Sample1_Shortest()
        {
            #region setup
            var a = new PrimMinimumSpanningTree.Vertex<char>('a');
            var b = new PrimMinimumSpanningTree.Vertex<char>('b');
            var c = new PrimMinimumSpanningTree.Vertex<char>('c');
            var d = new PrimMinimumSpanningTree.Vertex<char>('d');

            var ab = new PrimMinimumSpanningTree.Edge<char>(1) { End = b };
            var bc = new PrimMinimumSpanningTree.Edge<char>(2) { End = c };
            var ac = new PrimMinimumSpanningTree.Edge<char>(3) { End = c };
            var cd = new PrimMinimumSpanningTree.Edge<char>(4) { End = d };
            var ad = new PrimMinimumSpanningTree.Edge<char>(5) { End = d };

            a.Edges.AddRange(new[] { ab, ac, ad });
            b.Edges.Add(bc);
            c.Edges.Add(cd);
            #endregion setup

            var tree = PrimMinimumSpanningTree.DiscoverMst(a);

            using var scope = new AssertionScope();
            tree.Edges.Sum(e => e.Length).Should().Be(7);
            tree.Vertices.Should().HaveCount(4);
        }
    }
}