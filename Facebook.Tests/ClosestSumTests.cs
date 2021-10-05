using Facebook.Problems;
using FluentAssertions;
using Xunit;

namespace Facebook.Tests
{
    public class ClosestSumTests
    {
        [Fact]
        public void Find_Sample_ExpectedResult()
        {
            var numbers = new[] { 10, 22, 28, 30, 40 };
            ClosestSum.Find(numbers, 54).Should().Be((22, 30));
        }
    }
}
