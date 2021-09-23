using Facebook.Problems;
using FluentAssertions;
using Microsoft.VisualStudio.TestPlatform.ObjectModel.DataCollection;
using Xunit;

namespace Facebook.Tests
{
    public class ElementSwappingTests
    {
        [Theory]
        [InlineData(new[]{5,3,1}, 2, new[]{1,5, 3})]
        [InlineData(new[]{5,3,1}, 3, new[]{1,3, 5})]        //k = n*(n-1)/2 => in ascending order
        [InlineData(new[]{8,9,11,2,1}, 3, new[]{2,8,9,11,1})]
        [InlineData(new[]{11,9,8,2,1}, 10, new[]{1,2,8,9,11})]//k is enough to do ascending order sorting
        [InlineData(new[]{11,9,8,2,1}, 9, new[]{1,2,8,11,9})]
        public void FindMinArray_Example_Match(int[] input, int k, int[] output)
        {
            ElementSwapping.FindMinArray(input, k).Should().Equal(output);
        }
    }
}