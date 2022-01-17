using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
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

    [Fact]
    public void Run_Governors_Observe()
    {
        const int k = 2;
        var governors = new List<GovernorPoint>
        {
            new(-86.79113, 72, "Alabama"),
            new(-152.404419, 66, "Alaska"),
            new(-111.431221, 53, "Arizona"),
            new(-92.373123, 66, "Arkansas"),
            new(-119.681564, 79, "California"),
            new(-105.311104, 65, "Colorado"),
            new(-72.755371, 61, "Connecticut"),
            new(-75.507141, 61, "Delaware"),
            new(-81.686783, 64, "Florida"),
            new(-83.643074, 74, "Georgia"),
            new(-157.498337, 60, "Hawaii"),
            new(-114.478828, 75, "Idaho"),
            new(-88.986137, 60, "Illinois"),
            new(-86.258278, 49, "Indiana"),
            new(-93.210526, 57, "Iowa"),
            new(-96.726486, 60, "Kansas"),
            new(-84.670067, 50, "Kentucky"),
            new(-91.867805, 50, "Louisiana"),
            new(-69.381927, 68, "Maine"),
            new(-76.802101, 61, "Maryland"),
            new(-71.530106, 60, "Massachusetts"),
            new(-84.536095, 58, "Michigan"),
            new(-93.900192, 70, "Minnesota"),
            new(-89.678696, 62, "Mississippi"),
            new(-92.288368, 43, "Missouri"),
            new(-110.454353, 51, "Montana"),
            new(-98.268082, 52, "Nebraska"),
            new(-117.055374, 53, "Nevada"),
            new(-71.563896, 42, "New Hampshire"),
            new(-74.521011, 54, "New Jersey"),
            new(-106.248482, 57, "New Mexico"),
            new(-74.948051, 59, "New York"),
            new(-79.806419, 60, "North Carolina"),
            new(-99.784012, 60, "North Dakota"),
            new(-82.764915, 65, "Ohio"),
            new(-96.928917, 62, "Oklahoma"),
            new(-122.070938, 56, "Oregon"),
            new(-77.209755, 68, "Pennsylvania"),
            new(-71.51178, 46, "Rhode Island"),
            new(-80.945007, 70, "South Carolina"),
            new(-99.438828, 64, "South Dakota"),
            new(-86.692345, 58, "Tennessee"),
            new(-97.563461, 59, "Texas"),
            new(-111.862434, 70, "Utah"),
            new(-72.710686, 58, "Vermont"),
            new(-78.169968, 60, "Virginia"),
            new(-121.490494, 66, "Washington"),
            new(-80.954453, 66, "West Virginia"),
            new(-89.616508, 49, "Wisconsin"),
            new(-107.30249, 55, "Wyoming")
        };
        var sut = new KMeans<GovernorPoint>(k, governors);
        var clusters = sut.Run(100);

        clusters.Count(c => c.Points.Any()).Should().BeGreaterThan(1);

        for (int i = 0; i < clusters.Count; i++)
        {
            _output.WriteLine($"Cluster {i} in {clusters[i].Centroid} contains {clusters[i].Points.Count} points");
        }
        
        for (int i = 0; i < clusters.Count; i++)
        {
            _output.WriteLine($"Cluster {i}: {string.Join(Environment.NewLine, clusters[i].Points.Select(p => p.ToString()))}");
        }
    }

    [Fact]
    public void Run_Albums_Observe()
    {
        const int k = 3;//as there is expected an eruption (history album is too long)
        var albums = new List<Album>
        {
            new("Got to Be There", 1972, 35.45, 10),
            new("Ben", 1972, 31.31, 10),
            new("Music & Me", 1973, 32.09, 10),
            new("Forever, Michael", 1975, 33.36, 10),
            new("Off the Wall", 1979, 42.28, 10),
            new("Thriller", 1982, 42.19, 9),
            new("Bad", 1987, 48.16, 10),
            new("Dangerous", 1991, 77.03, 14),
            new("HIStory: Past, Present and Future, Book I", 1995, 148.58, 30),
            new("Invincible", 2001, 77.05, 16)
        };

        var sut = new KMeans<Album>(k, albums);
        var clusters = sut.Run(100);

        clusters.Count(c => c.Points.Any()).Should().BeGreaterThan(1);

        for (int i = 0; i < clusters.Count; i++)
        {
            _output.WriteLine($"Cluster {i} in {clusters[i].Centroid} contains {clusters[i].Points.Count} points");
        }
        
        for (int i = 0; i < clusters.Count; i++)
        {
            _output.WriteLine($"Cluster {i}: {string.Join(Environment.NewLine, clusters[i].Points.Select(p => p.ToString()))}");
        }
    }
}