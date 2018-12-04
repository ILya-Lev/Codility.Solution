using Codility.Solvers;
using FluentAssertions;
using System.Linq;
using Xunit;

namespace Codility.Tests
{
    public class PeaksTests
    {
        [Fact]
        public void GetBlockNumber_Sample_3()
        {
            var input = new[] { 1, 2, 3, 4, 3, 4, 1, 2, 3, 4, 6, 2 };
            var solver = new Peaks();
            var peaksNumber = solver.GetBlockNumber(input);
            peaksNumber.Should().Be(3);
        }

        [Fact]
        public void GetBlockNumber_PrimeLength_1()
        {
            var input = new[] { 1, 2, 3, 4, 3, 4, 1, 2, 3, 4, 6 };
            var solver = new Peaks();
            var peaksNumber = solver.GetBlockNumber(input);
            peaksNumber.Should().Be(1);
        }

        [Fact]
        public void GetBlockNumber_Empty_0()
        {
            var input = new int[0];
            var solver = new Peaks();
            var peaksNumber = solver.GetBlockNumber(input);
            peaksNumber.Should().Be(0);
        }
        
        //they say item could be peak if has 2 neighbors
        [Fact]
        public void GetBlockNumber_OneItem_0()
        {
            var input = new[] { 1};
            var solver = new Peaks();
            var peaksNumber = solver.GetBlockNumber(input);
            peaksNumber.Should().Be(0);
        }

        [InlineData(2)]
        [InlineData(10)]
        [InlineData(11)]
        [InlineData(12)]
        [Theory]
        public void GetBlockNumber_AllItemsTheSame_0(int arrayLength)
        {
            var input = Enumerable.Repeat(1, arrayLength).ToArray();
            var solver = new Peaks();
            var peaksNumber = solver.GetBlockNumber(input);
            peaksNumber.Should().Be(0);
        }

        [Fact]
        public void GetBlockNumber_BigDecreaseIncrease_2()
        {
            int size = 55_000;
            var input = Enumerable.Range(1, size).Reverse().Concat(Enumerable.Range(1,size)).ToArray();
            Swap(input, 0, 1);
            Swap(input, input.Length - 1, input.Length - 2);
            var solver = new Peaks();

            var peaksNumber = solver.GetBlockNumber(input);

            peaksNumber.Should().Be(2);
        }

        private void Swap(int[] input, int lhs, int rhs)
        {
            var tmp = input[lhs];
            input[lhs] = input[rhs];
            input[rhs] = tmp;
        }
    }
}
