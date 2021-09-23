using Facebook.Problems;
using FluentAssertions;
using Microsoft.VisualStudio.TestPlatform.ObjectModel.DataCollection;
using Xunit;

namespace Facebook.Tests
{
    [Trait("Category", "Unit")]
    public class SlowSumsTests
    {
        [Theory]
        [InlineData(new[]{1,2,3,4,5}, 50)]
        [InlineData(new[]{4,2,3,1}, 26)]
        public void GetTotalTime_Sample_MatchExpectations(int[] numbers, int expected)
        {
            SlowSums.GetTotalTime(numbers).Should().Be(expected);
        }
    }
}