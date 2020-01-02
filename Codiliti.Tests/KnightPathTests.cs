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

        [Fact]
        public void Traverse_TopLeft_OneStepStrategy_FindPath()
        {
            var sb = new StringBuilder();
            var writer = new StringWriter(sb);

            var board = new Board(writer);
            var strategy = new BestPositionStrategy(board, FigureStep.KnightSteps);
            var solver = new KnightPath(board, FigureStep.KnightSteps, strategy);

            var path = solver.Traverse(0,0);

            for (int i = 0; i < path.Count; ++i)
            {
                _output.WriteLine($"{i}: {path[i].r} {path[i].c}");
            }

            _output.WriteLine(sb.ToString());

            board.ContainUnvisitedCells().Should().BeFalse();
        }

        [Fact]
        public void Traverse_35_TwoStepStrategy_FindPath()
        {
            var sb = new StringBuilder();
            var writer = new StringWriter(sb);

            var board = new Board(writer);
            var strategy = new BestPosition2StepStrategy(board, FigureStep.KnightSteps);
            var solver = new KnightPath(board, FigureStep.KnightSteps, strategy);

            var path = solver.Traverse(3,5);

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
                    var strategy = new BestPositionStrategy(board, FigureStep.KnightSteps);
                    var solver = new KnightPath(board, FigureStep.KnightSteps, strategy);
                    var path = solver.Traverse(i, j);

                    _output.WriteLine($"start position {i}, {j}");
                    board.ContainUnvisitedCells().Should().BeFalse();
                    _output.WriteLine("is ok");
                }
            }
        }
    }
}