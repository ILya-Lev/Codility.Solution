using System.Linq;
using Algorithms.Solutions;
using FluentAssertions;
using FluentAssertions.Execution;
using Xunit;

namespace Algorithms.Tests
{
    [Trait("Category", "Unit")]
    public class DijkstraShortestPathTests
    {
        [Fact]
        public void FindShortestPath_Straightforward_MatchExpectations()
        {
            #region setup
            var start = new DijkstraShortestPath.Vertex(1);
            var l11 = new DijkstraShortestPath.Vertex(2);
            var l12 = new DijkstraShortestPath.Vertex(3);
            var end = new DijkstraShortestPath.Vertex(4);

            start.Edges.AddRange(new []
            {
                new DijkstraShortestPath.Edge(10){End = l11},
                new DijkstraShortestPath.Edge(20){End = l12},
            });

            l11.Edges.Add(new DijkstraShortestPath.Edge(30){End = end});
            l12.Edges.Add(new DijkstraShortestPath.Edge(40){End = end});
            #endregion setup

            var path = DijkstraShortestPath.FindShortestPath(start, end);

            using var scope = new AssertionScope();
            path.Select(p => p.Item2).Sum().Should().Be(40);
            path[0].Item1.Data.Should().Be(1);
            path[1].Item1.Data.Should().Be(2);
            path[2].Item1.Data.Should().Be(4);
        }

        [Fact]
        public void FindShortestPath_FirstShortestNotTheBest_MatchExpectations()
        {
            #region setup
            var start = new DijkstraShortestPath.Vertex(1);
            var l11 = new DijkstraShortestPath.Vertex(2);
            var l12 = new DijkstraShortestPath.Vertex(3);
            var end = new DijkstraShortestPath.Vertex(4);

            start.Edges.AddRange(new []
            {
                new DijkstraShortestPath.Edge(30){End = l11},
                new DijkstraShortestPath.Edge(20){End = l12},
            });

            l11.Edges.Add(new DijkstraShortestPath.Edge(10){End = end});
            l12.Edges.Add(new DijkstraShortestPath.Edge(40){End = end});
            #endregion setup

            var path = DijkstraShortestPath.FindShortestPath(start, end);

            using var scope = new AssertionScope();
            path.Select(p => p.Item2).Sum().Should().Be(40);
            path[0].Item1.Data.Should().Be(1);
            path[1].Item1.Data.Should().Be(2);
            path[2].Item1.Data.Should().Be(4);
        }

        [Fact]
        public void FindShortestPath_FirstShortestNotTheBest_2levels_MatchExpectations()
        {
            #region setup
            var start = new DijkstraShortestPath.Vertex(1);
            var l11 = new DijkstraShortestPath.Vertex(2);
            var l12 = new DijkstraShortestPath.Vertex(3);
            var l21 = new DijkstraShortestPath.Vertex(4);
            var l22 = new DijkstraShortestPath.Vertex(5);
            var end = new DijkstraShortestPath.Vertex(6);

            start.Edges.AddRange(new []
            {
                new DijkstraShortestPath.Edge(30){End = l11},
                new DijkstraShortestPath.Edge(20){End = l12},
            });

            l11.Edges.Add(new DijkstraShortestPath.Edge(60){End = l21});
            l12.Edges.Add(new DijkstraShortestPath.Edge(50){End = l22});
            
            l21.Edges.Add(new DijkstraShortestPath.Edge(10){End = end});
            l22.Edges.Add(new DijkstraShortestPath.Edge(40){End = end});
            #endregion setup

            var path = DijkstraShortestPath.FindShortestPath(start, end);

            using var scope = new AssertionScope();
            path.Select(p => p.Item2).Sum().Should().Be(100);
            path[0].Item1.Data.Should().Be(1);
            path[1].Item1.Data.Should().Be(2);
            path[2].Item1.Data.Should().Be(4);
            path[3].Item1.Data.Should().Be(6);
        }
    }
}