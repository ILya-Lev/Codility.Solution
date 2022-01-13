using System;
using System.Linq;
using FluentAssertions;
using Xunit;
using Xunit.Abstractions;

namespace ClassicalProblems.Tests;

public class SimpleEquationTests
{
    private readonly ITestOutputHelper _output;
    public SimpleEquationTests(ITestOutputHelper output) => _output = output;

    [Theory]
    [InlineData(20, 100, 13.0, 0.1, 0.7)]
    [InlineData(20, 100, 50.0, 0.1, 0.7)]
    public void Run_Tournament_FindSolution(int populationSize
        , int generations, double threshold
        , double mutationChance, double crossoverChance)
    {
        var initialPopulation = Enumerable.Range(1, populationSize)
            .Select(n => SimpleEquation.GetRandomInstance())
            .ToArray();
        
        var parentSelectionStrategy = new TournamentParentSelectionStrategy<SimpleEquation>();

        var geneticAlgorithm = new GeneticAlgorithm<SimpleEquation>(initialPopulation
            , mutationChance, crossoverChance, parentSelectionStrategy);

        var (equation, report) = geneticAlgorithm.Run(generations, threshold);

        _output.WriteLine(report);
        _output.WriteLine($"Solution {equation}");

        equation.GetFitness().Should().BeGreaterOrEqualTo(10);//max is 13 for max(6x-x^2 + 4y - y^2)
    }

    [Theory]
    [InlineData(20, 100, 13.0, 0.1, 0.7)]
    [InlineData(20, 100, 50.0, 0.1, 0.7)]
    public void Run_Roulette_FindSolution(int populationSize
        , int generations, double threshold
        , double mutationChance, double crossoverChance)
    {
        var initialPopulation = Enumerable.Range(1, populationSize)
            .Select(n => SimpleEquation.GetRandomInstance())
            .ToArray();
        
        var parentSelectionStrategy = new RouletteParentSelectionStrategy<SimpleEquation>();

        var geneticAlgorithm = new GeneticAlgorithm<SimpleEquation>(initialPopulation
            , mutationChance, crossoverChance, parentSelectionStrategy);

        var (equation, report) = geneticAlgorithm.Run(generations, threshold);

        _output.WriteLine(report);
        _output.WriteLine($"Solution {equation}");
    }


    [Theory]
    [InlineData(1_000, 1_000, 1.0, 0.2, 0.7)]
    public void Run_SendMoreMoney_Roulette_FindSolution(int populationSize
        , int generations, double threshold//=1.0 as proper solution gives (send+more) == money => (send+more)/money = 1.0
        , double mutationChance, double crossoverChance)
    {
        var initialPopulation = Enumerable.Range(1, populationSize)
            .Select(_ => SendMoreMoney.GetRandomInstance())
            .ToArray();
        
        var parentSelectionStrategy = new RouletteParentSelectionStrategy<SendMoreMoney>();

        var geneticAlgorithm = new GeneticAlgorithm<SendMoreMoney>(initialPopulation
            , mutationChance, crossoverChance, parentSelectionStrategy);

        var (setting, report) = geneticAlgorithm.Run(generations, threshold);

        _output.WriteLine(report);
        _output.WriteLine($"Solution {setting}");
    }

    [Theory]
    [InlineData(1_000, 1_000, 1.0, 0.2, 0.7)]
    public void Run_SendMoreMoney_Tournament_FindSolution(int populationSize
        , int generations, double threshold
        , double mutationChance, double crossoverChance)
    {
        var initialPopulation = Enumerable.Range(1, populationSize)
            .Select(_ => SendMoreMoney.GetRandomInstance())
            .ToArray();
        
        var parentSelectionStrategy = new TournamentParentSelectionStrategy<SendMoreMoney>();

        var geneticAlgorithm = new GeneticAlgorithm<SendMoreMoney>(initialPopulation
            , mutationChance, crossoverChance, parentSelectionStrategy);

        var (setting, report) = geneticAlgorithm.Run(generations, threshold);

        _output.WriteLine(report);
        _output.WriteLine($"Solution {setting}");
    }
}