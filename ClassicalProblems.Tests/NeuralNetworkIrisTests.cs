using FluentAssertions;
using System.IO;
using System.Linq;
using Xunit;
using Xunit.Abstractions;

namespace ClassicalProblems.Tests;

public class NeuralNetworkIrisTests
{
    private readonly ITestOutputHelper _output;
    public NeuralNetworkIrisTests(ITestOutputHelper output) => _output = output;

    [Fact]
    public void ReadCsv_Iris_150Records()
    {
        var path = Path.Combine(Directory.GetCurrentDirectory(), @"data\iris.csv");
        var data = NeuralNetwork.Utils.LoadCSV<NeuralNetwork.Irises, NeuralNetwork.IrisesMap>(path);

        data.Should().HaveCount(150);
        
        #region print out
        _output.WriteLine("first group");
        foreach (var row in data.Skip(30).Take(3))
            _output.WriteLine(row.ToString());
        
        _output.WriteLine("second group");
        foreach (var row in data.Skip(70).Take(3))
            _output.WriteLine(row.ToString());
        
        _output.WriteLine("third group");
        foreach (var row in data.Skip(130).Take(3))
            _output.WriteLine(row.ToString());
        #endregion print out
    }

    [Fact]
    public void IrisesClassification_Classify_90PercentPrecision()
    {
        //typical results: 11-13 out of 15 are correct (i.e. 0.72 - 0.87 correctness rate) - compare with kMeans

        var path = Path.Combine(Directory.GetCurrentDirectory(), @"data\iris.csv");
        var irises = NeuralNetwork.Utils.LoadCSV<NeuralNetwork.Irises, NeuralNetwork.IrisesMap>(path);

        var result = new NeuralNetwork.IrisesClassification().Classify(irises);

        result.Should().NotBeNull();
        _output.WriteLine($"{result.Correct} correct out of {result.Trials} = {result.Percentage:N2}");
    }

    [Fact]
    public void WineClassification_Classify_90PercentPrecision()
    {
        //typical results: 4-10 out of 18 are correct (i.e. 0.22 - 0.55 correctness rate) - compare with kMeans

        var path = Path.Combine(Directory.GetCurrentDirectory(), @"data\wine.csv");
        var wine = NeuralNetwork.Utils.LoadCSV<NeuralNetwork.Wine, NeuralNetwork.WineMap>(path);

        var result = new NeuralNetwork.WineClassification().Classify(wine);

        result.Should().NotBeNull();
        _output.WriteLine($"{result.Correct} correct out of {result.Trials} = {result.Percentage:N2}");
    }
}