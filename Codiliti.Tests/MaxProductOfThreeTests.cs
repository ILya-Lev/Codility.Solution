using Codility.Solvers;
using FluentAssertions;
using Xunit;

namespace Codility.Tests
{
    public class MaxProductOfThreeTests
    {
        [Fact]
        public void GetMaxProduct_Sample_60()
        {
            int[] values = new[] { -3, 1, 2, -2, 5, 6 };
            var solver = new MaxProductOfThree(values);
            var product = solver.GetMaxProduct();
            product.Should().Be(60);
        }

        [Fact]
        public void GetMaxProduct_AllNegativeAnd0_0()
        {
            int[] values = new[] { -3, -1, -2, -2, -5, 0 };
            var solver = new MaxProductOfThree(values);
            var product = solver.GetMaxProduct();
            product.Should().Be(0);
        }

        [Fact]
        public void GetMaxProduct_BigNegativeNumbersAndSmallPositive_TwoNegativeAndOnePositive()
        {
            int[] values = new[] { -100, -50, 0, 1, 2, 3 };
            var solver = new MaxProductOfThree(values);
            var product = solver.GetMaxProduct();
            product.Should().Be(100 * 50 * 3);
        }

        [Fact]
        public void GetMaxProduct_ThreeBiggestGiveNegative_ComposePositiveProduct()
        {
            int[] values = new[] { -5, -4, 2, 3 };
            var solver = new MaxProductOfThree(values);
            var product = solver.GetMaxProduct();
            product.Should().Be(60);
        }
    }
}
