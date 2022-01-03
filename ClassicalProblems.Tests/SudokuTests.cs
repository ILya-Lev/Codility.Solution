using System;
using System.Collections.Generic;
using System.Linq;
using ApprovalTests;
using ApprovalTests.Namers;
using ApprovalTests.Reporters;
using ApprovalTests.Reporters.TestFrameworks;
using FluentAssertions;
using Xunit;
using Xunit.Abstractions;

namespace ClassicalProblems.Tests;

[UseReporter(typeof(VisualStudioReporter), typeof(XUnit2Reporter))]
public class SudokuTests
{
    private readonly ITestOutputHelper _output;

    public SudokuTests(ITestOutputHelper output)
    {
        _output = output;
    }

    public static IEnumerable<object[]> Initials() => new []
    {
        new object[]{"easy", new[,]
        {
            { 0, 0, 0, 0, 0, 5, 4, 0, 9 },
            { 4, 5, 1, 0, 0, 2, 3, 0, 0 },
            { 9, 8, 2, 0, 0, 0, 5, 6, 1 },
            { 6, 0, 7, 0, 0, 0, 9, 8, 0 },
            { 0, 0, 3, 4, 6, 0, 0, 0, 0 },
            { 5, 0, 0, 2, 8, 7, 0, 1, 0 },
            { 0, 4, 0, 0, 7, 0, 0, 9, 6 },
            { 3, 0, 0, 0, 0, 0, 7, 0, 0 },
            { 0, 0, 5, 9, 4, 6, 8, 0, 2 },
        }},
        new object[]{"hard", new[,]
        {
            { 0, 0, 7, 0, 0, 0, 3, 0, 2 },
            { 2, 0, 0, 0, 0, 5, 0, 1, 0 },
            { 0, 0, 0, 8, 0, 1, 4, 0, 0 },
            { 0, 1, 0, 0, 9, 6, 0, 0, 8 },
            { 7, 6, 0, 0, 0, 0, 0, 4, 9 },
            { 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 1, 0, 3, 0, 0, 0 },
            { 8, 0, 1, 0, 6, 0, 0, 0, 0 },
            { 0, 0, 0, 7, 0, 0, 0, 6, 3 },
        }},
    };

    [Theory, MemberData(nameof(Initials))]
    public void Sudoku_EasyLevel_Solve(string level, int[,] initial)
    {
        NamerFactory.AdditionalInformation = level;
        
        var sudoku = new Sudoku(initial);

        var variables = Enumerable.Range(0, Sudoku.Size)
            .SelectMany(r => Enumerable.Range(0, Sudoku.Size).Select(c => (r, c)))
            .ToArray();

        var range = Enumerable.Range(1, Sudoku.Size).ToArray();

        var ranges = variables
            .ToDictionary(v => v, v => initial[v.r, v.c] != 0 ? new[] { initial[v.r, v.c] } : range)
            .ToDictionary(p => p.Key, p => (IReadOnlyCollection<int>)p.Value);

        var solver = new ConstraintSatisfactoryProblem<(int, int), int>(variables, ranges);
        solver.AddConstraint(new Sudoku.SudokuConstraint(variables));
        //for (int row = 0; row < Sudoku.Size; row++)
        //    solver.AddConstraint(new Sudoku.RowConstraint(variables.Where(p => p.r == row).ToArray(), row));
        //for (int col = 0; col < Sudoku.Size; col++)
        //    solver.AddConstraint(new Sudoku.ColConstraint(variables.Where(p => p.c == col).ToArray(), col));
        //for (int id = 0; id < Sudoku.Size; id++)
        //    solver.AddConstraint(new Sudoku.ColConstraint(variables.Where(p => p.r / 3 + p.c % 3 == id).ToArray(), id));


        var solution = solver.BacktrackingSearch();
        solution.Should().NotBeNull();

        _output.WriteLine("initial");
        var unsolved = sudoku.ToString();
        _output.WriteLine(unsolved);
        sudoku.FillIn(solution);
        _output.WriteLine("solution");
        var solved = sudoku.ToString();
        _output.WriteLine(solved);

        Approvals.Verify(solved);
    }
}