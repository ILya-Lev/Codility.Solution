using System.Collections.Generic;
using FluentAssertions;
using Xunit;

namespace ClassicalProblems.Tests;

public class AnimalPuzzleTests
{
    [Fact]
    public void GetFinalResult_OriginalSystem_32()
    {
        AnimalPuzzle.GetFinalResult().Should().Be(32);
    }

    [Fact]
    public void GetFinalResultFlexible_OriginalSystem_32()
    {
        var equations = new[]
        {
            "a+a+a=60",
            "b+b+a=28",
            "c+c+b=10"
        };
        var expression = "a+b*c";

        AnimalPuzzle.GetFinalResultFlexible(equations, expression).Should().Be(32);
    }

    [Fact]
    public void GetFinalResultFlexible_System1_Observe()
    {
        var equations = new[]
        {
            "a+b+c=12",
            "b*b-a*c=1",
            "c*b+1=a*7"
        };
        var expression = "a*b*c";

        AnimalPuzzle.GetFinalResultFlexible(equations, expression).Should().Be(60);
    }

    [Fact]
    public void ExpressionParser_AreAllSatisfied_1stEquation()
    {
        var parser = new EqualityParser(new []{"a+a+a=60"});
        parser.AreAllSatisfied(new Dictionary<char, double>()
        {
            ['a'] = 20
        }).Should().BeTrue();
    }
    
    [Fact]
    public void ExpressionParser_ParseAsPredicate_2ndEquation()
    {
        var parser = new EqualityParser(new []{"b+b+a=28"});
        parser.AreAllSatisfied(new Dictionary<char, double>()
        {
            ['a'] = 20,
            ['b'] = 4,
        }).Should().BeTrue();
    }

    [Fact]
    public void ExpressionParser_ParseAsPredicate_3rdEquation()
    {
        var parser = new EqualityParser(new []{"c+c+b=10"});
        parser.AreAllSatisfied(new Dictionary<char, double>()
        {
            ['b'] = 4,
            ['c'] = 3,
        }).Should().BeTrue();
    }

    [Fact] public void EqualityParser_Calculate_FinalExpression()
    {
        var assignment = new Dictionary<char, double>()
        {
            ['a'] = 20,
            ['b'] = 4,
            ['c'] = 3,
        };
        var value = new EqualityParser.ExpressionParser("a+c*b").Calculate(assignment);
        value.Should().Be(32);
    }
    [Fact] public void EqualityParser_AreAllSatisfied_AllEquations()
    {
        var assignment = new Dictionary<char, double>()
        {
            ['a'] = 20,
            ['b'] = 4,
            ['c'] = 3,
        };
        var parser = new EqualityParser(new []
        {
            "a+a+a=60",
            "b+b+a=28",
            "c+c+b=10"
        });
        parser.AreAllSatisfied(assignment).Should().BeTrue();
    }
}