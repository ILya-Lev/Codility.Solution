using FluentAssertions;
using Xunit;

namespace ClassicalProblems.Tests;

public class AnimalPuzzleTests
{
    [Fact]
    public void GetFinalResult_Condition_Observe()
    {
        AnimalPuzzle.GetFinalResult().Should().Be(32);
    }
}