using Codility.Solvers;
using FluentAssertions;
using Xunit;

namespace Codility.Tests
{
    public class MaxProfitTests
    {
        [Fact]
        public void GetMaxProfit_Sample_356()
        {
            var prices = new int[] { 23171, 21011, 21123, 21366, 21013, 21367 };
            var solver = new MaxProfit();

            var maxProfit = solver.GetMaxProfit(prices);

            maxProfit.Should().Be(356);
        }
    }
}