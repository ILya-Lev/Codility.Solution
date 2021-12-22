using FluentAssertions;
using Xunit;
using Xunit.Abstractions;

namespace ClassicalProblems.Tests;

public class MazeTests
{
    private readonly ITestOutputHelper _output;
    private readonly Maze _maze;

    public MazeTests(ITestOutputHelper output)
    {
        _output = output;
        _maze = new Maze(10, 10
            , new Maze.MazeLocation(0, 0), new Maze.MazeLocation(9, 9)
            , 0.2);
    }

    [Fact]
    public void Ctor_ToString_LooksGood()
    {
        _output.WriteLine(_maze.ToString());
    }

    [Fact]
    public void Dfs_FindSolution_Print()
    {
        try
        {
            _output.WriteLine($"Initial state\n\n{_maze}\n\n");

            var initial = new Maze.MazeLocation(0, 0);
            var lastNode = Maze.Node<Maze.MazeLocation>.DepthFirstSearch(initial, _maze.IsInGoal, _maze.GetPossibleSuccessors);
        
            lastNode.Should().NotBeNull();

            var path = Maze.Node<Maze.MazeLocation>.AsPath(lastNode);
            _maze.MarkPath(path);
            _output.WriteLine($"When path is found\n\n{_maze}\n\n");
        }
        finally
        {
            _maze.CleanPath();
        }
    }
}