using System.Collections.Generic;
using Facebook.Problems;
using FluentAssertions;
using Xunit;

namespace Facebook.Tests
{
    public class NodesInSubtreeTests
    {
        [Fact]
        public void CountOfNodes_Sample1_2()
        {
            var root = new NodesInSubtree.Node()
            {
                U = 1,
                Left = new NodesInSubtree.Node() { U = 2 },
                Right = new NodesInSubtree.Node() { U = 3 },
            };

            var queries = new List<NodesInSubtree.Query>()
            {
                new NodesInSubtree.Query() { U = 1, C = 'a' }
            };

            NodesInSubtree.CountOfNodes(root, queries, null).Should().Equal(new[] { 2 });
            
            var constructed = NodesInSubtree.ConstructTree("aba");
            constructed.Should().BeEquivalentTo(root);
        }


    }
}