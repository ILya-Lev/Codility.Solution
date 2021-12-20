using System.Collections.Generic;
using FluentAssertions;
using Xunit;

namespace ClassicalProblems.Tests
{
    public class FibonacciTests
    {
        public static IEnumerable<object[]> Expectations() => new[]
        {
            new object[] { 0, 0 },
            new object[] { 1, 1 },
            new object[] { 2, 1 },
            new object[] { 3, 2 },
            new object[] { 4, 3 },
            new object[] { 5, 5 },
            new object[] { 6, 8 },
            new object[] { 7, 13 },
            new object[] { 20, 6765 },
            new object[] { 92, 7_540_113_804_746_346_429L },//long.Max, 93rd is overflow
        };

        [Theory]
        [MemberData(nameof(Expectations))]
        public void GetRecursion_MatchExpectations(int n, long f) => Fibonacci.GetRecursion(n).Should().Be(f);

        [Theory]
        [MemberData(nameof(Expectations))]
        public void GetDynamic_MatchExpectations(int n, long f) => Fibonacci.GetDynamic(n).Should().Be(f);
        
        [Theory]
        [MemberData(nameof(Expectations))]
        public void GetViaSequence_MatchExpectations(int n, long f) => Fibonacci.GetViaSequence(n).Should().Be(f);
    }
}