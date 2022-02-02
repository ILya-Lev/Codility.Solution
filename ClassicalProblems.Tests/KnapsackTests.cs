using FluentAssertions;
using FluentAssertions.Execution;
using System.Collections.Generic;
using System.Linq;
using Xunit;
using Xunit.Abstractions;

namespace ClassicalProblems.Tests;

public class KnapsackTests
{
    private readonly ITestOutputHelper _output;
    public KnapsackTests(ITestOutputHelper output) => _output = output;


    [Theory, MemberData(nameof(GetTestData))]
    public void FillIn_Stuff_MatchExpectations(IEnumerable<(decimal cost, decimal weight)> stuff, decimal expectedCost)
    {
        const decimal capacity = 75;
        
        var items = stuff.Select(t => new Knapsack.Item { Cost = t.cost, Weight = t.weight });
        var content = new Knapsack().FillIn(items, capacity);

        using var scope = new AssertionScope();
        content.Sum(i => i.Cost).Should().Be(expectedCost);
        content.Sum(i => i.Weight).Should().BeLessOrEqualTo(capacity);
        foreach (var item in content)
            _output.WriteLine($"cost: {item.Cost}, weight: {item.Weight}, ratio: {item.Cost/item.Weight:N2}");
    }

    public static IEnumerable<object[]> GetTestData() => new[]
    {
        new object[] { new (decimal cost, decimal weight)[] { (500, 50), (300, 2), (700, 200), (4_000, 1), (300, 100), (1_000, 3) }, 5_800 },
        new object[] { new (decimal cost, decimal weight)[] { (500, 5), (300, 25), (400, 50)}, 900 },
    };
}