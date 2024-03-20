using FluentAssertions;

namespace Coderbyte.Tests;

[Trait("Category", "Unit")]
public class CalculatorSolverTests
{
    [Fact]
    public void Calculator_OneParenthesis_MatchExpectation()
    {
        var input = "6*(4/2)+3*1";
        var result = CalculatorSolver.Calculator(input);
        result.Should().Be("15");
    }

    [Fact]
    public void Calculator_TwoParenthesis_MatchExpectation()
    {
        var input = "(12/2)(4/2)+3*1";
        var result = CalculatorSolver.Calculator(input);
        result.Should().Be("15");
    }

    [Fact]
    public void Calculator_NestedParenthesis_MatchExpectation()
    {
        var input = "((2*5+7-5)/2)(4/2)+3*1";
        var result = CalculatorSolver.Calculator(input);
        result.Should().Be("15");
    }

    [Fact]
    public void Calculator_NegativeSubEvaluation_MatchExpectation()
    {
        var input = "1+((2-3)(5+1))";
        var result = CalculatorSolver.Calculator(input);
        result.Should().Be("-5");
    }

    [Fact]
    public void Calculator_Negatives_MatchExpectation()
    {
        var input = "5-(3-4)";
        var result = CalculatorSolver.Calculator(input);
        result.Should().Be("6");
    }

    [Fact]
    public void Calculator_NegativeAndPositive_MatchExpectation()
    {
        var input = "5(3-4+1)";
        var result = CalculatorSolver.Calculator(input);
        result.Should().Be("0");
    }
}