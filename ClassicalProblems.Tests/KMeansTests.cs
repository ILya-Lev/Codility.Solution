using System;
using FluentAssertions;
using Xunit;
using Xunit.Abstractions;

namespace ClassicalProblems.Tests;

public class KMeansTests
{
    private readonly ITestOutputHelper _output;
    public KMeansTests(ITestOutputHelper output) => _output = output;

    [Fact]
    public void Run_3DataPoints_2Clusters()
    {
        var sut = new KMeans<DataPoint>(2, new[]
        {
            new DataPoint(new[] { 2.0, 1.0, 1.0 }),
            new DataPoint(new[] { 2.0, 2.0, 5.0 }),
            new DataPoint(new[] { 3.0, 1.5, 2.5 }),
        });

        var clusters = sut.Run(100);

        clusters.Should().HaveCount(2);
        foreach (var cluster in clusters)
        {
            _output.WriteLine($"Cluster in {cluster.Centroid} contains {cluster.Points.Count} points: ");
            foreach (var point in cluster.Points)
            {
                _output.WriteLine($"{point}");
            }
        }
    }
}