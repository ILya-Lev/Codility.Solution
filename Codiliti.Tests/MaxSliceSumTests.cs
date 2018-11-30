using Codility.Solvers;
using FluentAssertions;
using System.Linq;
using Xunit;

namespace Codility.Tests
{
    public class MaxSliceSumTests
    {
        [Fact]
        public void GetMaxSliceSum_Sample_5()
        {
            var values = new[] { 3, 2, -6, 4, 0 };
            var solver = new MaxSliceSum();
            var maxSum = solver.GetMaxSliceSum(values);
            maxSum.Should().Be(5);
        }

        [Fact]
        public void GetMaxSliceSum_RepeatBig_SumAll()
        {
            var values = Enumerable.Repeat(1, 100_000).ToArray();
            var solver = new MaxSliceSum();
            var maxSum = solver.GetMaxSliceSum(values);
            maxSum.Should().Be(100_000);
        }
    }
}