using FluentAssertions;
using System.Collections.Generic;
using System.Linq;
using Xunit;
using Xunit.Abstractions;

namespace ClassicalProblems.Tests;

public class SendMoreMoneyTests
{
    private readonly ITestOutputHelper _output;
    public SendMoreMoneyTests(ITestOutputHelper output) => _output = output;

    [Fact]
    public void SendMoreMoney_Sum_FindAllDigits()
    {
        var letters = "SENDMOREMONEY".ToCharArray().Distinct().ToArray();
        var range = Enumerable.Range(0, 10).ToArray();
        var domain = letters.ToDictionary(l => l, l => (IReadOnlyCollection<int>)range);

        var csp = new ConstraintSatisfactoryProblem<char, int>(letters, domain);
        csp.AddConstraint(new SendMoreMoneyConstraint(letters));

        var s = csp.BacktrackingSearch();
        s.Should().NotBeNull();

        foreach (var pair in s)
        {
            _output.WriteLine($"{pair.Key} = {pair.Value}");
        }

        _output.WriteLine($"  {s['S']}{s['E']}{s['N']}{s['D']}");
        _output.WriteLine($"+");
        _output.WriteLine($"  {s['M']}{s['O']}{s['R']}{s['E']}");
        _output.WriteLine($"______");
        _output.WriteLine($" {s['M']}{s['O']}{s['N']}{s['E']}{s['Y']}");
    }
}