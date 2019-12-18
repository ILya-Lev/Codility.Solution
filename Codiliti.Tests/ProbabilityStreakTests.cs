using System.Collections.Generic;
using System.Linq;
using Codility.Solvers;
using FluentAssertions;
using MoreLinq;
using Xunit;
using Xunit.Sdk;

namespace Codility.Tests
{
    public class ProbabilityStreakTests : IClassFixture<TestOutputHelper>
    {
        private readonly TestOutputHelper _output;
        public ProbabilityStreakTests(TestOutputHelper output)  { _output = output; }

        [InlineData(10)]
        [InlineData(100)]
        [InlineData(1_000)]
        [InlineData(10_000)]
        [Theory]
        public void DoStep_NSteps_ObserveResult(int n)
        {
            var streak = new ProbabilityStreak(0.5, 0.4);
            var seed = 100;
            var sequence = new List<double>(n);

            double current = seed;
            for (int i = 0; i < n; i++)
            {
                sequence.Add(current);
                if (i % 1_000 == 0)
                    _output.WriteLine($"i = {i} => current = {current}");

                current = streak.DoStep(current);
            }

            _output.WriteLine($"{n}th value {sequence.Last()}");
            sequence.Last().Should().BeGreaterOrEqualTo(0);
        }

        [InlineData(10)]
        [InlineData(100)]
        [InlineData(1_000)]
        [InlineData(10_000)]
        [Theory]
        public void DoStep_Ensemble_ObserveResult(int n)
        {
            var streakGenerators = new List<ProbabilityStreak>(n);
            for (int i = 0; i < n; i++)
            {
                streakGenerators.Add(new ProbabilityStreak(0.5, 0.4));
            }

            var current = Enumerable.Repeat(100.0, n).ToArray();
            var lenght = 100;

            for (int i = 0; i < lenght; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    current[j] = streakGenerators[j].DoStep(current[j]);
                    //if (i != 0 && i % 1_000 == 0)
                    //    _output.WriteLine($"i={i}, j={j}, current = {current[j]}");
                }
            }

            //foreach (var c in current) _output.WriteLine($"current {c}");

            _output.WriteLine($"average ensemble {current.Average()}");
            _output.WriteLine($"max ensemble {current.Max()}");
            _output.WriteLine($"min ensemble {current.Min()}");
        }
    }
}