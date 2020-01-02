using Codility.Solvers.Knight;
using FluentAssertions;
using System.IO;
using System.Text;
using Xunit;
using Xunit.Sdk;

namespace Codility.Tests
{
    public class KnightPathTests : IClassFixture<TestOutputHelper>
    {
        private readonly TestOutputHelper _output;

        public KnightPathTests(TestOutputHelper output)
        {
            _output = output;
        }

        [InlineData(0,0)]
        [InlineData(3,5)]
        [Theory]
        public void Traverse_TopLeft_FindPath(in int startRow, in int startColumn)
        {
            var sb = new StringBuilder();
            var writer = new StringWriter(sb);

            var board = new Board(writer);
            var solver = new KnightPath(board, FigureStep.KnightSteps);

            var path = solver.Traverse(startRow, startColumn);

            for (int i = 0; i < path.Count; ++i)
            {
                _output.WriteLine($"{i}: {path[i].r} {path[i].c}");
            }

            _output.WriteLine(sb.ToString());

            board.ContainUnvisitedCells().Should().BeFalse();
        }

        [Fact]
        public void Traverse_AllStartPositions_FindPathEachTime()
        {
            for (int i = 0; i < Board.Size; i++)
            {
                for (int j = 0; j < Board.Size; j++)
                {
                    if (i == 3 && j == 5)
                        continue;

                    var board = new Board(null);
                    var solver = new KnightPath(board, FigureStep.KnightSteps);
                    var path = solver.Traverse(i, j);

                    _output.WriteLine($"start position {i}, {j}");
                    board.ContainUnvisitedCells().Should().BeFalse();
                    _output.WriteLine("is ok");
                }
            }
        }
    }
}