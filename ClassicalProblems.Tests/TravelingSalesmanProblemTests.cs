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

    [Theory]
    [InlineData("Brattlboro", 318)]
    [InlineData("Benningtone", 318)]
    [InlineData("White-Riwer", 318)]
    [InlineData("Berlingtone", 318)]
    [InlineData("Ratland", 318)]
    public void GetShortestPathByPermutations_5Towns_MatchExpectations(string origin, int expected)
    {
        var tsp = new TravelingSalesmanProblem();
        var path = tsp.GetShortestPathByPermutations(origin);

        using var scope = new AssertionScope();
        path.TotalDistance.Should().Be(expected);
        path.Towns.First().Should().BeEquivalentTo(origin);
        path.Towns.Should().HaveCount(5); //i.e. visit all towns, we have 5 of them so far
        _output.WriteLine(path.ToString());
    }
}