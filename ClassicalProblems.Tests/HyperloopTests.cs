using FluentAssertions;
using Xunit;
using Xunit.Abstractions;

namespace ClassicalProblems.Tests;

public class HyperloopTests
{
    private readonly ITestOutputHelper _output;
    private readonly UnweightedGraph<string> _cityGraph;
    private readonly WeightedGraph<string> _weightedCityGraph;

    public HyperloopTests(ITestOutputHelper output)
    {
        _output = output;

        var cities = new[]
        {
            "Boston", "NewYork", "Philadelphia", "Washington", "Miami", "Atlanta", "Detroit", "Chicago",
            "Dallas", "Huston", "Phoenix", "Riverside", "LosAngeles", "SanFrancisco", "Seattle"
        };
        
        _cityGraph = new UnweightedGraph<string>(cities);

        _cityGraph.AddEdge("Boston", "NewYork");
        _cityGraph.AddEdge("Boston", "Detroit");
        _cityGraph.AddEdge("NewYork", "Detroit");
        _cityGraph.AddEdge("NewYork", "Philadelphia");
        _cityGraph.AddEdge("Washington", "Philadelphia");
        _cityGraph.AddEdge("Washington", "Detroit");
        _cityGraph.AddEdge("Washington", "Miami");
        _cityGraph.AddEdge("Washington", "Atlanta");
        _cityGraph.AddEdge("Miami", "Atlanta");
        _cityGraph.AddEdge("Miami", "Huston");
        _cityGraph.AddEdge("Atlanta", "Chicago");
        _cityGraph.AddEdge("Atlanta", "Huston");
        _cityGraph.AddEdge("Atlanta", "Dallas");
        _cityGraph.AddEdge("Huston", "Dallas");
        _cityGraph.AddEdge("Huston", "Phoenix");
        _cityGraph.AddEdge("Dallas", "Phoenix");
        _cityGraph.AddEdge("Dallas", "Chicago");
        _cityGraph.AddEdge("Detroit", "Chicago");
        _cityGraph.AddEdge("Riverside", "Chicago");
        _cityGraph.AddEdge("Riverside", "Phoenix");
        _cityGraph.AddEdge("Riverside", "LosAngeles");
        _cityGraph.AddEdge("Riverside", "SanFrancisco");
        _cityGraph.AddEdge("Phoenix", "LosAngeles");
        _cityGraph.AddEdge("SanFrancisco", "LosAngeles");
        _cityGraph.AddEdge("SanFrancisco", "Seattle");
        _cityGraph.AddEdge("Chicago", "Seattle");
        
        _weightedCityGraph = new WeightedGraph<string>(cities);
        _weightedCityGraph.AddEdge("Boston", "NewYork", 190);
        _weightedCityGraph.AddEdge("Boston", "Detroit", 613);
        _weightedCityGraph.AddEdge("NewYork", "Detroit", 482);
        _weightedCityGraph.AddEdge("NewYork", "Philadelphia", 81);
        _weightedCityGraph.AddEdge("Washington", "Philadelphia", 123);
        _weightedCityGraph.AddEdge("Washington", "Detroit", 396);
        _weightedCityGraph.AddEdge("Washington", "Miami", 923);
        _weightedCityGraph.AddEdge("Washington", "Atlanta", 543);
        _weightedCityGraph.AddEdge("Miami", "Atlanta", 604);
        _weightedCityGraph.AddEdge("Miami", "Huston", 968);
        _weightedCityGraph.AddEdge("Atlanta", "Chicago", 588);
        _weightedCityGraph.AddEdge("Atlanta", "Huston", 702);
        _weightedCityGraph.AddEdge("Atlanta", "Dallas", 721);
        _weightedCityGraph.AddEdge("Huston", "Dallas", 225);
        _weightedCityGraph.AddEdge("Huston", "Phoenix", 1015);
        _weightedCityGraph.AddEdge("Dallas", "Phoenix", 887);
        _weightedCityGraph.AddEdge("Dallas", "Chicago", 805);
        _weightedCityGraph.AddEdge("Detroit", "Chicago", 238);
        _weightedCityGraph.AddEdge("Riverside", "Chicago", 1704);
        _weightedCityGraph.AddEdge("Riverside", "Phoenix", 307);
        _weightedCityGraph.AddEdge("Riverside", "LosAngeles", 50);
        _weightedCityGraph.AddEdge("Riverside", "SanFrancisco", 386);
        _weightedCityGraph.AddEdge("Phoenix", "LosAngeles", 357);
        _weightedCityGraph.AddEdge("SanFrancisco", "LosAngeles", 348);
        _weightedCityGraph.AddEdge("SanFrancisco", "Seattle", 678);
        _weightedCityGraph.AddEdge("Chicago", "Seattle", 1737);
    }

    [Fact]
    public void ToString_15Cities_Observe() => _output.WriteLine(_cityGraph.ToString());

    [Fact]
    public void FindShortestPath_BostonToMiami_ThroughDetroit()
    {
        var path = _cityGraph.FindShortestPath("Boston", "Miami");
        path.Should().HaveCount(4);
        path.Should().Contain("Detroit");
        _output.WriteLine(string.Join(" -> ", path));
    }
    
    [Fact]
    public void ToString_15WeightedCities_Observe() => _output.WriteLine(_weightedCityGraph.ToString());

    [Fact]
    public void Mst_Build_Print()
    {
        var tree = _weightedCityGraph.BuildMinimumSpanningTree();
        tree.Should().HaveCount(14);
        var report = _weightedCityGraph.ConvertMstToString(tree);
        _output.WriteLine(report);
    }

    [Theory]
    [InlineData("LosAngeles", "Boston")]
    public void Dijkstra_FromLosAngeles_FindShortest(string origin, string target)
    {
        var markResult = _weightedCityGraph.FindAllPaths(origin);

        _output.WriteLine($"Distances from {origin}");
        var distanceByName = _weightedCityGraph.GetDistanceByVertex(markResult.Distances);
        foreach (var pair in distanceByName)
            _output.WriteLine($"{pair.Key}: {pair.Value}");

        var originIndex = _weightedCityGraph.GetIndexOf(origin);
        var targetIndex = _weightedCityGraph.GetIndexOf(target);
        var path = _weightedCityGraph.GetPath(originIndex, targetIndex, markResult.PointingEdgeByVertex);
        var pathAsString = _weightedCityGraph.ConvertMstToString(path);
        _output.WriteLine(pathAsString);
    }
}