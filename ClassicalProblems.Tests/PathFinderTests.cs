using FluentAssertions;
using LuxoftRiddles;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Xunit.Abstractions;

namespace ClassicalProblems.Tests;

public class PathFinderTests
{
    private readonly ITestOutputHelper _output;
    public PathFinderTests(ITestOutputHelper output) => _output = output;

    [Theory]
    [InlineData(0,0)]
    [InlineData(2,0)]
    [InlineData(0,2)]
    public void FindPath_BottomRight_Find(int startX, int startY)
    {
        var pathFinder = new PathFinder();

        int[][] board = new []
        {
            new []{0,1,0,0,0},
            new []{0,0,0,1,0},
            new []{0,1,1,1,0},
            new []{0,0,0,0,0},
            new []{1,1,0,0,0},
        };

        Func<int, int, string> getEmptyDisplay = (r,c) => board[r][c] == LuxoftRiddles.Maze.Occupied ? "1|" : " |";
        Print(board, getEmptyDisplay);

        var path = pathFinder.FindPath(board, startX, startY, 4, 4);
        path.Should().NotBeNull();

        var lookupPath = new HashSet<Point>(path!);
        Func<int, int, string> getTraversedDisplay = (r,c) =>
        {
            var location = new Point(c, r);
            return lookupPath.Contains(location) ? "x|" : getEmptyDisplay(r,c);
        };
        Print(board, getTraversedDisplay);
    }

    [Fact]
    public void FindPath_Impossible_Null()
    {
        var pathFinder = new PathFinder();

        int[][] board = new []
        {
            new []{0,1,0,1,0},
            new []{0,1,0,1,0},
            new []{0,1,1,1,0},
            new []{0,0,0,0,0},
            new []{1,1,0,0,0},
        };

        Func<int, int, string> getEmptyDisplay = (r,c) => board[r][c] == LuxoftRiddles.Maze.Occupied ? "1|" : " |";
        Print(board, getEmptyDisplay);

        var path = pathFinder.FindPath(board, 2, 1, 4, 4);
        path.Should().BeNull();
    }

    private void Print(int[][] board, Func<int, int, string> getDisplaySymbol)
    {
        var sb = new StringBuilder();
        for (int row = 0; row < board.Length; row++)
        {
            for (int col = 0; col < board[row].Length; col++)
            {
                sb.Append(getDisplaySymbol(row, col));
            }

            sb.AppendLine();
        }
        _output.WriteLine(sb.ToString());
    }
}