using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Algorithms.Solutions;
using FluentAssertions;
using Xunit;
using Xunit.Abstractions;

namespace Algorithms.Tests
{
    public class PrimeNumberSearchTests
    {
        private readonly ITestOutputHelper _output;
        public PrimeNumberSearchTests(ITestOutputHelper output) => _output = output;

        [Fact]
        public void GetAllPrimesUpTo_Bln_Profile()
        {
            var searcher = new PrimeNumberSearch();
            var primes = searcher.GetAllPrimesUpTo(100_000_000L);

            primes.Should().NotBeEmpty();
            _output.WriteLine($"{primes.Count} item is {primes.Last()}");
        }
    }
}
