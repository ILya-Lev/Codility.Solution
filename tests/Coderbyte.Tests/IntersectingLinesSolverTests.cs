using FluentAssertions;

namespace Coderbyte.Tests;

[Trait("Category", "Unit")]
public class IntersectingLinesSolverTests
{
    public static object[][] GetTestCases() => new object[][]
    {
        new object[] { new string[] { "(9,-2)", "(-2,9)", "(3,4)", "(10,11)" }, "(3,4)" },//a,b & a,b
        new object[] { new string[] {"(1,2)","(3,4)","(-5,-6)","(-7,-8)"}, "no intersection" },
        new object[] { new string[] {"(1,2)","(1,4)","(3,-6)","(3,-8)"}, "no intersection" },//x1!=x2
        new object[] { new string[] {"(1,2)","(3,2)","(-5,4)","(-7,4)"}, "no intersection" },//y1!=y2
        new object[] { new string[] { "(9,-2)", "(9,9)", "(3,4)", "(10,11)" }, "(9,10)" },//x & a,b
        new object[] { new string[] { "(2,9)", "(3,9)", "(3,4)", "(10,11)" }, "(8,9)" },//y & a,b
        new object[] { new string[] { "(3,4)", "(10,11)", "(9,-2)", "(9,9)" }, "(9,10)" },//x & a,b
        new object[] { new string[] { "(3,4)", "(10,11)", "(2,9)", "(3,9)"}, "(8,9)" },//y & a,b
        new object[] { new string[] { "(3,4)", "(3,11)", "(2,9)", "(3,9)"}, "(3,9)" },//x & y
        new object[] { new string[] { "(3,11)", "(10,11)", "(3,7)", "(3,9)"}, "(3,11)" },//y & x
    };

    [Theory, MemberData(nameof(GetTestCases))]
    public void IntersectingLines_Sample_MatchExpectation(string[] points, string result)
    {
        IntersectingLinesSolver.IntersectingLines(points).Should().Be(result);
    }
}