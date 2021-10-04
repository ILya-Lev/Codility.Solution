using Facebook.Problems;
using FluentAssertions;
using System;
using System.Diagnostics;
using System.Linq;
using Xunit;
using Xunit.Abstractions;

namespace Facebook.Tests
{
    public class BirthDayPopulationTests
    {
        private readonly ITestOutputHelper _output;

        public BirthDayPopulationTests(ITestOutputHelper output)
        {
            _output = output;
        }

        [Fact]
        public void FindMostPopulatedYear_Sample_2000()
        {
            var lifetimes = Enumerable.Range(1, 50).Select(n => (1950 + n, 2000 + n)).ToArray();

            BirthDayPopulation.FindMostPopulatedYear(lifetimes).Should().Be((2000, 50));
        }

        [Fact]
        public void FindMostPopulatedYearSorted_Sample_2000()
        {
            var lifetimes = Enumerable.Range(1, 50).Select(n => (1950 + n, 2000 + n)).ToArray();

            BirthDayPopulation.FindMostPopulatedYear_Sorted(lifetimes).Should().Be((2000, 50));
        }

        [Fact(Skip = "takes quite a lot of time")]
        public void FindMostPopularYear_Stress_TwoSolutionsMatch()
        {
            const int size = 1_000_000_000, minLifeExpectancy = 20, maxLifeExpectancy = 110;

            var generator = new Random(DateTime.UtcNow.Millisecond);
            var lifetimes = Enumerable.Range(1, size).Select(n =>
            {
                var birth = generator.Next(1750, 2150);
                var death = generator.Next(minLifeExpectancy, maxLifeExpectancy) + birth;
                return (birth, death);
            }).ToArray();

            var sw = Stopwatch.StartNew();
            var f = BirthDayPopulation.FindMostPopulatedYear(lifetimes);
            sw.Stop();

            _output.WriteLine($"via frequency table result is {f} and time is {sw.ElapsedMilliseconds} ms");

            sw = Stopwatch.StartNew();
            var s = BirthDayPopulation.FindMostPopulatedYear_Sorted(lifetimes);
            sw.Stop();

            _output.WriteLine($"via action tracking result is {s} and time is {sw.ElapsedMilliseconds} ms");

            f.Should().Be(s);
        }
    }
}
