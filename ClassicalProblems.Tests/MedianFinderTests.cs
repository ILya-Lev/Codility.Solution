using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using FluentAssertions.Execution;
using Xunit;
using Xunit.Abstractions;

namespace ClassicalProblems.Tests;

public class MedianFinderTests
{
    private readonly ITestOutputHelper _output;
    public MedianFinderTests(ITestOutputHelper output) => _output = output;

    [Fact]
    public async Task GetMedianPrices_Repeat_SameValue()
    {
        var numbers = Enumerable.Repeat(5m, 1000).ToArray();
        var asyncSequence = GenerateSequence(numbers);
        var medians = MedianFinder.GetMedianPrices(asyncSequence);

        using var scope = new AssertionScope();
        await foreach (var m in medians)
            m.Should().Be(5m);
    }

    [Fact]
    public async Task GetMedianPrices_ArithmeticalSeq_Observe()
    {
        var numbers = Enumerable.Range(1, 10).Select(n => (decimal)n).ToArray();
        var asyncSequence = GenerateSequence(numbers);
        var medians = MedianFinder.GetMedianPrices(asyncSequence);

        var medianList = new List<decimal>();
        await foreach (var m in medians)
            medianList.Add(m);

        medianList.Should().Equal(new decimal[] { 1, 1.5m, 2, 2.5m, 3, 3.5m, 4, 4.5m, 5, 5.5m });
    }

    private async IAsyncEnumerable<decimal> GenerateSequence(IEnumerable<decimal> numbers)
    {
        await Task.Yield();

        foreach (var n in numbers)
        {
            yield return (decimal)n;
        }
    }
}