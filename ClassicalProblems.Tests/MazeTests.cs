using FluentAssertions;
using Xunit;
using Xunit.Abstractions;

namespace ClassicalProblems.Tests;

public class MazeTests
{
    private readonly ITestOutputHelper _output;
    private static readonly Maze _maze =new Maze(10, 10
        , new Maze.MazeLocation(0, 0), new Maze.MazeLocation(9, 9)
        , 0.2);

    public MazeTests(ITestOutputHelper output)
    {
        _output = output;
        //_maze = new Maze(10, 10
        //    , new Maze.MazeLocation(0, 0), new Maze.MazeLocation(9, 9)
        //    , 0.2);
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
            var (lastNode, stateCount) = SearchNode<Maze.MazeLocation>
                .DepthFirstSearch(initial, _maze.IsInGoal, _maze.GetPossibleSuccessors);
        
            lastNode.Should().NotBeNull();

            var path = SearchNode<Maze.MazeLocation>.AsPath(lastNode);
            _maze.MarkPath(path);
            _output.WriteLine($"When path is found\n\n{_maze}\n\n");
            _output.WriteLine($"{stateCount} states has been checked");
        }
        finally
        {
            _maze.CleanPath();
        }
    }

    [Fact]
    public void Bfs_FindSolution_Print()
    {
        try
        {
            _output.WriteLine($"Initial state\n\n{_maze}\n\n");

            var initial = new Maze.MazeLocation(0, 0);
            var (lastNode, stateCount) = SearchNode<Maze.MazeLocation>
                .BreadthFirstSearch(initial, _maze.IsInGoal, _maze.GetPossibleSuccessors);
        
            lastNode.Should().NotBeNull();

            var path = SearchNode<Maze.MazeLocation>.AsPath(lastNode);
            _maze.MarkPath(path);
            _output.WriteLine($"When path is found\n\n{_maze}\n\n");
            _output.WriteLine($"{stateCount} states has been checked");
        }
        finally
        {
            _maze.CleanPath();
        }
    }

    [Fact]
    public void AStar_FindSolution_Print()
    {
        try
        {
            _output.WriteLine($"Initial state\n\n{_maze}\n\n");

            var initial = new Maze.MazeLocation(0, 0);
            var (lastNode, stateCount) = SearchNode<Maze.MazeLocation>
                .AStarSearch(initial, _maze.IsInGoal, _maze.GetPossibleSuccessors, _maze.GetManhattanDistanceToGoal);
        
            lastNode.Should().NotBeNull();

            var path = SearchNode<Maze.MazeLocation>.AsPath(lastNode);
            _maze.MarkPath(path);
            _output.WriteLine($"When path is found\n\n{_maze}\n\n");
            _output.WriteLine($"{stateCount} states has been checked");
        }
        finally
        {
            _maze.CleanPath();
        }
    }
}