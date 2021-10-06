using Facebook.Problems;
using FluentAssertions;
using Xunit;

namespace Facebook.Tests
{
    public class FibonacciTests
    {
        [Fact]
        public void GetMemoization_45_Find()
        {
            Fibonacci.GetMemoization(45).Should().Be(1_134_903_170L);
        }

        [Fact]
        public void GetRecursive_45_Find()
        {
            Fibonacci.GetRecursive(45).Should().Be(1_134_903_170L);
        }

        [Fact]
        public void Get_45_Find()
        {
            Fibonacci.Get(45).Should().Be(1_134_903_170L);
        }
    }
}
