using FluentAssertions;

namespace Coderbyte.Tests;

public class CityTrafficTests
{
    public static object[][] GetData() => new[]
    {
        new object []{new[] 
        {
            "1:[5]", "4:[5]", "3:[5]", "5:[1,4,3,2]", "2:[5,15,7]", "7:[2,8]", "8:[7,38]", "15:[2]", "38:[8]"
        }, "1:82,2:53,3:80,4:79,5:70,7:46,8:38,15:68,38:45"},
        new object []{ new[] { "1:[5]", "2:[5]", "3:[5]", "4:[5]", "5:[1,2,3,4]" }, "1:14,2:13,3:12,4:11,5:4"},
        new object []
        {
            new[] { "1:[5]", "2:[5,18]", "3:[5,12]", "4:[5]", "5:[1,2,3,4]", "18:[2]", "12:[3]" },
            "1:44,2:25,3:30,4:41,5:20,12:33,18:27"
        },
    };

    [Theory, MemberData(nameof(GetData))]
    public void CityTraffic_Sample_MatchExpectations(string[] input, string expected)
    {
        var result = CityTrafficSolver.CityTraffic(input);
        result.Should().Be(expected);
    }
}