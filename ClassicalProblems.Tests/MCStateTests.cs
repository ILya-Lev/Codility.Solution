using System;
using FluentAssertions;
using Xunit;
using Xunit.Abstractions;

namespace ClassicalProblems.Tests;

public class MCStateTests
{
    private readonly ITestOutputHelper _output;
    public MCStateTests(ITestOutputHelper output) => _output = output;

    [Theory]
    [InlineData(1)]
    [InlineData(2)]
    [InlineData(3)]
    public void Solve_Size_CouldBeSolved(int size)
    {
        MCState.MaxCount = size;
        var state = new MCState(size, size, true);
        var (lastState, count) = SearchNode<MCState>.BreadthFirstSearch(state, MCState.IsSolved, MCState.GetNextStates);
        lastState.Should().NotBeNull();

        var path = SearchNode<MCState>.AsPath(lastState);
        var solution = MCState.CreateReportForPath(path);

        _output.WriteLine($"the solution is found in {count} iterations; contains {path.Count} steps");
        _output.WriteLine(solution);
    }

    [Fact]//~ 6 seconds to run
    public void Solve_Size_CannotBeSolved()
    {
        int successCounter = 0;
        for (int size = 4; size < 2_000; size++)
        {
            try
            {
                MCState.MaxCount = size;
                var state = new MCState(size, size, true);
                var (lastState, count) = SearchNode<MCState>.BreadthFirstSearch(state, MCState.IsSolved, MCState.GetNextStates);
                lastState.Should().NotBeNull();

                var solution = MCState.CreateReportForPath(SearchNode<MCState>.AsPath(lastState));

                successCounter++;
                _output.WriteLine(solution);
            }
            catch (Exception exc)
            {
                //_output.WriteLine($"for size = {size} solution is not found");
            }    
        }
        _output.WriteLine($"Successfully solved {successCounter} cases");
    }
}