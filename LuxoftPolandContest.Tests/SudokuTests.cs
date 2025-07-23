using FluentAssertions;
using Xunit;
using Xunit.Abstractions;

namespace LuxoftPolandContest.Tests;

public class SudokuTests(ITestOutputHelper output)
{
    [Fact]
    public void Solve_Easy_Solved()
    {
        var digits = new int[][]
        {
            [9,1,5,3,0,0,6,7,0],
            [6,8,0,1,0,7,0,4,0],
            [4,2,7,5,6,0,0,0,0],
            [8,0,1,0,2,6,3,5,4],
            [0,4,0,0,0,1,0,0,0],
            [0,0,0,0,0,0,0,1,0],
            [0,6,0,2,0,0,0,0,0],
            [5,0,8,7,3,0,1,0,6],
            [0,0,0,6,8,0,0,9,7]
        };

        var field = Sudoku.Create(digits);

        var solvedField = Sudoku.Solve(field);

        solvedField.Cells.Should().NotContain(c => c.Value.IsEmpty);

        Print(solvedField);
    }

    [Fact]
    public void Solve_Expert_Solved()
    {
        var digits = new int[][]
        {
            [0,0,0,0,8,7,0,0,0],
            [0,6,3,4,0,0,1,0,0],
            [9,0,0,0,1,0,0,0,0],
            [0,0,0,0,0,0,0,0,8],
            [0,5,0,0,0,0,0,9,0],
            [0,8,4,0,0,0,0,0,3],
            [6,7,0,9,0,8,5,0,0],
            [0,0,1,0,0,0,6,0,0],
            [0,0,0,0,3,0,0,0,0]
        };

        var field = Sudoku.Create(digits);

        var solvedField = Sudoku.Solve(field);

        Print(solvedField);
        solvedField.Cells.Should().NotContain(c => c.Value.IsEmpty);
        Sudoku.IsConsistent(solvedField).Should().BeTrue();
    }

    [Fact]
    public void Solve_Expert002_Solved()
    {
        var digits = new int[][]
        {
            [0,6,0,0,0,7,0,0,0],
            [1,0,0,0,8,0,0,0,4],
            [0,0,0,9,1,0,0,0,0],
            [0,0,0,0,0,0,0,0,0],
            [0,0,0,3,0,0,0,2,6],
            [4,7,0,0,0,6,8,0,0],
            [6,0,5,0,0,2,4,7,0],
            [0,0,0,0,0,8,1,0,0],
            [0,0,9,0,0,0,0,3,0]
        };

        var field = Sudoku.Create(digits);

        var solvedField = Sudoku.Solve(field);

        Print(solvedField);
        solvedField.Cells.Should().NotContain(c => c.Value.IsEmpty);
        Sudoku.IsConsistent(solvedField).Should().BeTrue();
    }

    private void Print(Sudoku.Field field)
    {
        foreach(var g in field.Cells.Values.GroupBy(c => c.Position.Row))
        {
            var line = string.Join(" ", g.Select(c => c.Digit.ToString()));
            output.WriteLine(line);
        }
    }
}