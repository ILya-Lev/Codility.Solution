using Algorithms.Solutions;
using FluentAssertions;
using System.Linq;
using Xunit;
using Xunit.Abstractions;

namespace Algorithms.Tests
{
    public class RodProblemTests
    {
        private readonly ITestOutputHelper _output;
        public RodProblemTests(ITestOutputHelper output) => _output = output;

        [Theory]
        [InlineData(01, 1)]
        [InlineData(02, 5)]
        [InlineData(03, 8)]
        [InlineData(04, 10)]//this is the only case, when greedy algorithm gives wrong answer, 3+1 -> 9 instead of 2+2 -> 10
        [InlineData(05, 13)]
        [InlineData(06, 17)]
        [InlineData(07, 18)]
        [InlineData(08, 22)]
        [InlineData(09, 25)]
        [InlineData(10, 30)]
        public void CutGreedy_Sample_Observe(int length, int total)
        {
            var pieces = new RodProblem(null).CutGreedy(length);
            foreach (var piece in pieces)
            {
                _output.WriteLine($"length {piece.L}; amount {piece.amount}; total price {piece.price}");
            }

            pieces.Sum(p => p.price).Should().Be(total);
        }

        [Theory]
        [InlineData(01, 1)]
        [InlineData(02, 5)]
        [InlineData(03, 8)]
        [InlineData(04, 10)]
        [InlineData(05, 13)]
        [InlineData(06, 17)]
        [InlineData(07, 18)]
        [InlineData(08, 22)]
        [InlineData(09, 25)]
        [InlineData(10, 30)]
        [InlineData(40, 120)]
        [InlineData(41, 121)]
        [InlineData(100, 300)]
        public void CutDynamic_Sample_Observe(int length, int total)
        {
            var cut = new RodProblem(null).CutDynamic(length);
            foreach (var piece in cut.AmountByLength)
            {
                _output.WriteLine($"length {piece.Key}; amount {piece.Value}");
            }

            cut.TotalPrice.Should().Be(total);
        }

        [Theory]
        [InlineData(01, 1)]
        [InlineData(02, 5)]
        [InlineData(03, 8)]
        [InlineData(04, 10)]
        [InlineData(05, 13)]
        [InlineData(06, 17)]
        [InlineData(07, 18)]
        [InlineData(08, 22)]
        [InlineData(09, 25)]
        [InlineData(10, 30)]
        [InlineData(40, 120)]
        [InlineData(41, 121)]
        [InlineData(100, 300)]
        public void CalculateMaxPrice_Sample_Observe(int length, int total)
        {
            var price = new RodProblem(null).CalculateMaxPrice(length);
            price.Should().Be(total);
        }
    }
}
