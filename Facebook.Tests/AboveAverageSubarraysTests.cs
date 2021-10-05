using Facebook.Problems;
using FluentAssertions;
using FluentAssertions.Execution;
using System.Linq;
using Xunit;

namespace Facebook.Tests
{
    public class AboveAverageSubarraysTests
    {
        [Fact]
        public void FindAboveAverageSubarrays_Sample_MatchExpectations()
        {
            var subarrays = AboveAverageSubarrays.FindAboveAverageSubarrays(new[] { 3, 4, 2 });

            using var scope = new AssertionScope();
            subarrays.Should().HaveCount(3);
            subarrays[0].L.Should().Be(0);
            subarrays[0].R.Should().Be(1);

            subarrays[1].L.Should().Be(0);
            subarrays[1].R.Should().Be(2);

            subarrays[2].L.Should().Be(1);
            subarrays[2].R.Should().Be(1);
        }

        [Fact]//when hash set is used N^2 otherwise 2^N !!!!!
        public void FindAboveAverageSubarrays_Stress_Fast()
        {
            var numbers = Enumerable.Range(1, 1_000).ToArray();
            var subarrays = AboveAverageSubarrays.FindAboveAverageSubarrays(numbers);

            subarrays.Length.Should().BeGreaterOrEqualTo(100);
        }
    }
}
