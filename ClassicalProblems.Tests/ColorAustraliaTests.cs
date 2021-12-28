using System;
using Xunit;
using Xunit.Abstractions;

namespace ClassicalProblems.Tests;

public class ColorAustraliaTests
{
    private readonly ITestOutputHelper _output;

    public ColorAustraliaTests(ITestOutputHelper output)
    {
        _output = output;
    }

    [Fact]
    public void ColorTheMap_3Colors_FindsSolution()
    {
        var coloredMap = ColorAustralia.ColorTheMap();

        foreach (var map in coloredMap)
        {
            _output.WriteLine($"{map.Key} {map.Value}");
        }
    }
}