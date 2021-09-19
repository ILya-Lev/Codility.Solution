using System;
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

        [Fact]
        public void FindMaxProduct_Empty_Empty()
        {
            LargestTripleProducts.FindMaxProduct(Array.Empty<int>()).Should().BeEmpty();
        }

        [Fact]
        public void FindMaxProduct_OneItem_minus1()
        {
            LargestTripleProducts.FindMaxProduct(new[] { 91_276 })
                .Should().Equal(new[] { -1 });
        }

        [Fact]
        public void FindMaxProduct_TwoItems_minus1()
        {
            LargestTripleProducts.FindMaxProduct(new[] { 91_276, 876_213 })
                .Should().Equal(new[] { -1, -1 });
        }

        [Fact]
        public void FindMaxProduct_TreeItems_MultiplyAll()
        {
            LargestTripleProducts.FindMaxProduct(new[] { 3, 5, 7 })
                .Should().Equal(new[] { -1, -1, 105 });
        }
    }
}