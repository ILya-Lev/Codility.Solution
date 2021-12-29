using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;
using Xunit.Abstractions;

namespace ClassicalProblems.Tests;

public class WordGridTests
{
    private readonly ITestOutputHelper _output;

    public WordGridTests(ITestOutputHelper output)
    {
        _output = output;
    }

    [Fact]
    public void LookUpWords_9x9_AFewWords()
    {
        var grid = new WordGrid(9, 9);
        var words = new[] { "MATTHEW", "JOE", "MARY", "SARAH", "SALLLY" };
        var domains = words.ToDictionary(w => w, w => (IReadOnlyCollection<List<WordGrid.GridLocation>>)grid.GenerateDomain(w));

        var csp = new ConstraintSatisfactoryProblem<string, List<WordGrid.GridLocation>>(words, domains);
        csp.AddConstraint(new WordGrid.WordSearchConstraint(words));
        var solution = csp.BacktrackingSearch();

        foreach (var pair in solution)
        {
            _output.WriteLine($"{pair.Key} {string.Join(", ", pair.Value.Select(v => (v.Row, v.Column).ToString()))}");
        }
        
        _output.WriteLine(grid.ToString());

        foreach (var pair in solution)
        {
            grid.InitializeWithWord(pair.Key, pair.Value);
        }

        _output.WriteLine(grid.ToString());
    }
}