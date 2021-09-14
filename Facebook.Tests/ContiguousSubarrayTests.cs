using System.Linq;
using Facebook.Problems;
using FluentAssertions;
using Xunit;

namespace Facebook.Tests
{
    public class ContiguousSubarrayTests
    {
        [Fact]
        public void CountSubarrays_Sample1_ExpectedResult()
        {
            ContiguousSubarray.CountSubarrays(new[] { 3, 4, 1, 6, 2 })
                .Should().Equal(new []{1,3,1,5,1});
        }

        [Fact]
        public void CountSubarrays_NaturalNumbers_TheSame()
        {
            var numbers = Enumerable.Range(1, 10).ToArray();
            
            ContiguousSubarray.CountSubarrays(numbers)
                .Should().Equal(numbers);
        }

        [Fact]
        public void CountSubarrays_ReverseNaturalNumbers_TheSame()
        {
            var numbers = Enumerable.Range(1, 10_000).Reverse().ToArray();
            
            ContiguousSubarray.CountSubarrays(numbers)
                .Should().Equal(numbers);
        }
    }
}