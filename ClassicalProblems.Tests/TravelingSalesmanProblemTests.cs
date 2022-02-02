using FluentAssertions;
using FluentAssertions.Execution;
using System.Collections.Generic;
using System.Linq;
using Xunit;
using Xunit.Abstractions;

namespace ClassicalProblems.Tests;

public class TravelingSalesmanProblemTests
{
    private readonly ITestOutputHelper _output;
    public TravelingSalesmanProblemTests(ITestOutputHelper output) => _output = output;

    [Theory]
    [InlineData(3,6)]
    [InlineData(4,24)]
    [InlineData(5,120)]
    public void GeneratePermutations_Items_Sequences(int size, int count)
    {
        var sequence = Enumerable.Range(1,size).ToArray();
        var permutations = TravelingSalesmanProblem.GenerateAllPermutations(sequence);

        using var scope = new AssertionScope();
        new HashSet<int[]>(permutations).Should().HaveCount(count);
        foreach (var permutation in permutations)
        {
            _output.WriteLine(string.Join(", ", permutation.Select(n => $"{n}")));
        }
    }
}