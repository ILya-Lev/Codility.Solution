using Codility.Solvers;
using FluentAssertions;
using Xunit;
using Xunit.Sdk;

namespace Codility.Tests
{
    public class CountFactorsTests : IClassFixture<TestOutputHelper>
    {
        private readonly TestOutputHelper _outputHelper;

        public CountFactorsTests(TestOutputHelper outputHelper)
        {
            _outputHelper = outputHelper;
        }

        [InlineData(0)]
        [InlineData(-1)]
        [InlineData(1)]
        [Theory]
        public void GetFactorsAmount_Small_1(int n)
        {
            var solver = new CountFactors();
            var amount = solver.GetFactorsAmount(n);
            amount.Should().Be(1);
        }

        [InlineData(2)]
        [InlineData(3)]
        [InlineData(5)]
        [InlineData(7)]
        [InlineData(11)]
        [InlineData(13)]
        [InlineData(17)]
        [Theory]
        public void GetFactorsAmount_Prime_2(int n)
        {
            var solver = new CountFactors();
            var amount = solver.GetFactorsAmount(n);
            amount.Should().Be(2);
        }

        [InlineData(4, 3)]
        [InlineData(6, 4)]
        [InlineData(8, 4)]
        [InlineData(9, 3)]
        [InlineData(10, 4)]
        [InlineData(12, 6)]
        [InlineData(14, 4)]
        [Theory]
        public void GetFactorsAmount_Composite_Many(int n, int expected)
        {
            var solver = new CountFactors();
            var amount = solver.GetFactorsAmount(n);
            amount.Should().Be(expected);
        }

        [Fact]
        public void GetFactorsAmount_MaxInt_IsPrime()           //!!! 2^n-1 sometimes is prime; 2^32-1 indeed !
        {
            var solver = new CountFactors();
            var amount = solver.GetFactorsAmount(int.MaxValue);
            _outputHelper.WriteLine($"max int has {amount} divisors");
        }

        [Fact]
        public void GetFactorsAmount_BigInt_Many()
        {
            var bigNumber = 2_147_395_600;
            var solver = new CountFactors();
            var amount = solver.GetFactorsAmount(bigNumber);
            
            _outputHelper.WriteLine($"{bigNumber} has {amount} divisors");
        }
    }
}
