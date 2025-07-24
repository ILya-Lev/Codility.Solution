using FluentAssertions;
using Xunit;
using Xunit.Abstractions;

namespace LuxoftPolandContest.Tests;

public class SudokuTests(ITestOutputHelper output)
{
    public static IEnumerable<object[]> Initials() => new []
    {
        new object[]{"easy01", new int[][]
        {
            [ 0, 0, 0, 0, 0, 5, 4, 0, 9 ],
            [ 4, 5, 1, 0, 0, 2, 3, 0, 0 ],
            [ 9, 8, 2, 0, 0, 0, 5, 6, 1 ],
            [ 6, 0, 7, 0, 0, 0, 9, 8, 0 ],
            [ 0, 0, 3, 4, 6, 0, 0, 0, 0 ],
            [ 5, 0, 0, 2, 8, 7, 0, 1, 0 ],
            [ 0, 4, 0, 0, 7, 0, 0, 9, 6 ],
            [ 3, 0, 0, 0, 0, 0, 7, 0, 0 ],
            [ 0, 0, 5, 9, 4, 6, 8, 0, 2 ],
        }},
        new object[]{"easy02", new int[][]
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
        }},
        new object[]{"hard", new int[][]
        {
            [ 0, 0, 7, 0, 0, 0, 3, 0, 2 ],
            [ 2, 0, 0, 0, 0, 5, 0, 1, 0 ],
            [ 0, 0, 0, 8, 0, 1, 4, 0, 0 ],
            [ 0, 1, 0, 0, 9, 6, 0, 0, 8 ],
            [ 7, 6, 0, 0, 0, 0, 0, 4, 9 ],
            [ 0, 0, 0, 0, 0, 0, 0, 0, 0 ],
            [ 0, 0, 0, 1, 0, 3, 0, 0, 0 ],
            [ 8, 0, 1, 0, 6, 0, 0, 0, 0 ],
            [ 0, 0, 0, 7, 0, 0, 0, 6, 3 ],
        }},
        new object[]{"expert01", new int[][]
        {
            [ 0, 0, 0, 0, 5, 0, 0, 0, 4 ],
            [ 0, 4, 0, 2, 0, 0, 3, 0, 6 ],
            [ 0, 0, 2, 0, 0, 0, 0, 0, 0 ],
            [ 3, 0, 0, 0, 6, 0, 4, 0, 0 ],
            [ 0, 0, 9, 0, 8, 0, 0, 0, 0 ],
            [ 0, 0, 1, 0, 0, 4, 0, 2, 0 ],
            [ 0, 0, 0, 5, 0, 1, 0, 7, 0 ],
            [ 0, 0, 0, 0, 4, 0, 0, 0, 1 ],
            [ 6, 0, 0, 0, 0, 0, 0, 0, 9 ],
        }},
        new object[]{"expert02", new int[][]
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
        }},
        new object[]{"expert03", new int[][]
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
        }},
    };


    [Theory, MemberData(nameof(Initials))]
    public void Solve_Field_Solved(string name, int[][] digits)
    {
        var field = Sudoku.Create(digits);

        var solvedField = Sudoku.Solve(field);
        
        Print(name, solvedField);
        solvedField.Cells.Should().NotContain(c => c.Value.IsEmpty);
        Sudoku.IsConsistent(solvedField).Should().BeTrue();
    }

    private void Print(string name, Sudoku.Field field)
    {
        output.WriteLine($"Sudoku {name} solved:");
        foreach (var g in field.Cells.Values.GroupBy(c => c.Position.Row))
        {
            var line = string.Join(" ", g.Select(c => c.Digit.ToString()));
            output.WriteLine(line);
        }
    }
}