using FluentAssertions;

namespace Coderbyte.Tests;

[Trait("Category", "Unit")]
public class ShortestPathFinderTests
{
    public static object[][] TestData() => new []
    {
        new object[]
        {
            new []{"5","A","B","C","D","F","A-B","A-C","B-C","C-D","D-F"},
            "A-C-D-F"
        },
        new object[]
        {
            new []{"5","A","B","C","D","F","A-B","A-C","B-C","C-D"},
            "-1"
        },
        new object[]
        {
            new []{"4","X","Y","Z","W","X-Y","Y-Z","X-W"},
            "X-W"
        },
        new object[]
        {
            new []{"4","X","Y","Z","W","X-Y","Y-Z"},
            "-1"
        },
        new object[]
        {
            new []{"7","A","B","C","D","E","F","G","A-B","A-E","B-C","C-D","D-F","E-D","F-G"},
            "A-E-D-F-G"
        },
        new object[]
        {
            new []{"7","A","B","C","D","E","F","G","A-B","A-E","B-C","C-D","D-F","E-D"},
            "-1"
        },
    };

    [Theory, MemberData(nameof(TestData))]
    public void FindPath_Sample_MatchExpectations(string[] input, string expectedPath)
    {
        var path = ShortestPathFinder.FindPath(input);
        path.Should().Be(expectedPath);
    }
}