using Facebook.Problems;
using FluentAssertions;
using Xunit;

namespace Facebook.Tests
{
    [Trait("Category", "Unit")]
    public class LargestTripleProductsTests
    {
        [Fact]
        public void FindMaxProduct_Sample1_Expected()
        {
            LargestTripleProducts.FindMaxProduct(new[] { 1, 2, 3, 4, 5 })
                .Should().Equal(new[] { -1, -1, 6, 24, 60 });
        }
    }
}