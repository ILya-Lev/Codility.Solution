using FluentAssertions;

namespace Coderbyte.Tests;

[Trait("Category", "Unit")]
public class GasStationSolverTests
{
    public static object[][] GetTestData() => new object[][]
    {
        new object[]{new string[] {"4","1:1","2:2","1:2","0:1"}, "impossible"},
        new object[]{new string[] {"4","3:1","2:2","1:2","0:1"}, "1"},
        new object[]{new string[] {"4","0:1","2:2","1:2","3:1"}, "4"},
    };

    [Theory, MemberData(nameof(GetTestData))]
    public void GasStation_Samples_Expectations(string[] gasStations, string expected)
    {
        GasStationSolver.GasStation(gasStations).Should().Be(expected);
    }
}