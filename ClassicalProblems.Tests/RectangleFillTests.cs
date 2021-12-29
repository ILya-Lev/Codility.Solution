using System;
using FluentAssertions;
using Xunit;
using Xunit.Abstractions;

namespace ClassicalProblems.Tests;

public class RectangleFillTests
{
    private readonly ITestOutputHelper _output;

    public RectangleFillTests(ITestOutputHelper output)
    {
        _output = output;
    }

    [Fact]
    public void RectangleFill_9x9_5Rectangles()
    {
        var rectangles = new[]
        {
            new RectangleFill.Rectangle(4, 4),
            new RectangleFill.Rectangle(6, 1),
            new RectangleFill.Rectangle(3, 3),
            new RectangleFill.Rectangle(2, 2),
            new RectangleFill.Rectangle(2, 5),
        };
        var filler = new RectangleFill(9, 9);

        var solution = filler.CoverWithRectangles(rectangles);
        solution.Should().NotBeNullOrEmpty();
        //still have some overlap!
        _output.WriteLine(filler.ToString());
    }
}