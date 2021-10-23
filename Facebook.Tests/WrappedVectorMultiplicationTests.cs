using Facebook.Problems;
using FluentAssertions;
using Xunit;

namespace Facebook.Tests
{
    public class WrappedVectorMultiplicationTests
    {
        [Fact]
        public void CalculateProduct_AllChunksAligned_SameChunks()
        {
            var lhs = new[] { (3, 1), (4, 2), (5, 6) };
            var rhs = new[] { (3, 5), (4, 0), (5, 8) };

            WrappedVectorMultiplication.CalculateProduct(lhs, rhs)
                .Should().Equal(new[] { (3, 5), (4, 0), (5, 48) });
        }

        [Fact]
        public void CalculateProduct_ShiftedChunks_Correct()
        {
            var lhs = new[] { (3, 1), (2, 2), (7, 6) };
            var rhs = new[] { (3, 5), (4, 0), (3, 8) };

            WrappedVectorMultiplication.CalculateProduct(lhs, rhs)
                .Should().Equal(new[] { (3, 5), (2, 0), (2, 0), (3, 48), (2, 6) });
            //ends with (2,6) as padding is 1, i.e. 6*1=6
        }
    }
}
