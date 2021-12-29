using Xunit;
using Xunit.Abstractions;

namespace ClassicalProblems.Tests;

public class QueensProblemTests
{
    private readonly ITestOutputHelper _output;

    public QueensProblemTests(ITestOutputHelper output)
    {
        _output = output;
    }

    [Fact]
    public void PlaceFigures_8_FindSolution()
    {
        var solution = QueensProblem.PlaceFigures();

        foreach (var pair in solution)
        {
            _output.WriteLine($"queen {pair.Key} is in position {pair.Value}");
        }
    }
}