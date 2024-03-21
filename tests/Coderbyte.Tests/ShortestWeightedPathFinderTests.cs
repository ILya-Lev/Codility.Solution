using FluentAssertions;

namespace Coderbyte.Tests;

[Trait("Category", "Unit")]
public class ShortestWeightedPathFinderTests
{
    public static object[][] TestData() => new []
    {
        new object[]
        {
            new []{"4","A","B","C","D","A|B|2","C|B|11","C|D|3","B|D|2"},
            "A-B-D"
        },
        new object[]
        {
            new []{"4","A","B","C","D","A|B|2","C|B|11","C|A|3"},
            "-1"
        },
        new object[]
        {
            new []{"4","X","Y","Z","W","X|Y|2","Y|Z|14","X|W|20", "Z|W|1"},
            "X-Y-Z-W"
        },
        new object[]
        {
            new []{"4","X","Y","Z","W","X|Y|2","Y|Z|14","Z|Y|1"},
            "-1"
        },
        new object[]
        {
            new []{"7","A","B","C","D","E","F","G","A|B|1","A|E|9","B|C|2","C|D|1","D|F|2","E|D|6","F|G|2"},
            "A-B-C-D-F-G"
        },
        new object[]
        {
            new []{"7","A","B","C","D","E","F","G","A|B|1","A|E|9","B|C|2","C|D|1","E|D|6","F|G|2"},
            "-1"
        },
    };

    [Theory, MemberData(nameof(TestData))]
    public void FindPath_Sample_MatchExpectations(string[] input, string expectedPath)
    {
        var path = ShortestWeightedPathFinder.WeightedPath(input);
        path.Should().Be(expectedPath);
    }
}