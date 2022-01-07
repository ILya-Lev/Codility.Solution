using System.Collections.Generic;
using FluentAssertions;
using Xunit;

namespace ClassicalProblems.Tests;

public class AnimalPuzzleTests
{
    [Fact]
    public void GetFinalResult_Condition_Observe()
    {
        AnimalPuzzle.GetFinalResult().Should().Be(32);
    }

    [Fact]
    public void ExpressionParser_ParseAsPredicate_1stEquation()
    {
        var predicate = ExpressionParser.ParseAsPredicate("a+a+a=60");
        predicate(new Dictionary<char, double>()
        {
            ['a'] = 20
        }).Should().BeTrue();
    }
    
    [Fact]
    public void ExpressionParser_ParseAsPredicate_2ndEquation()
    {
        var predicate = ExpressionParser.ParseAsPredicate("b+b+a=28");
        predicate(new Dictionary<char, double>()
        {
            ['a'] = 20,
            ['b'] = 4,
        }).Should().BeTrue();
    }

    [Fact]
    public void ExpressionParser_ParseAsPredicate_3rdEquation()
    {
        var predicate = ExpressionParser.ParseAsPredicate("c+c+b=10");
        predicate(new Dictionary<char, double>()
        {
            ['b'] = 4,
            ['c'] = 3,
        }).Should().BeTrue();
    }

    [Fact] public void ExpressionParser_Calculate_FinalExpression()
    {
        var assignment = new Dictionary<char, double>()
        {
            ['a'] = 20,
            ['b'] = 4,
            ['c'] = 3,
        };
        var value = ExpressionParser.Calculate(assignment , "a+c*b");
        value.Should().Be(32);
    }
}