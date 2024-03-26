using FluentAssertions;

namespace Coderbyte.Tests;

[Trait("Category", "Unit")]
public class PolynomialExpansionSolverTests
{
    [Theory]
    [InlineData("(1x)(2x^-2+1)", "x+2x^-1")]
    [InlineData("(-1x^3)(3x^3+2)", "-3x^6-2x^3")]
    [InlineData("(2x^2+4)(6x^3+3)", "12x^5+24x^3+6x^2+12")]
    public void PolynomialExpansion_Sample_MatchExpectations(string expression, string result)
    {
        PolynomialExpansionSolver.PolynomialExpansion(expression).Should().Be(result);
    }
}