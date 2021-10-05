using Facebook.Problems;
using FluentAssertions;
using FluentAssertions.Execution;
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
    }
}
