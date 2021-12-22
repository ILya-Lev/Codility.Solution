using FluentAssertions;
using Xunit;

namespace ClassicalProblems.Tests
{
    public class CalculatePiTests
    {
        [Fact]
        public void Get_1M_HighPrecision()
        {
            //CalculatePi.Get(200_000_000).Should().BeApproximately(3.14159265, 1e-8);
            CalculatePi.Get(1_000_000).Should().Be(3.14159_2_6535897932387126435740m);
        }
    }
}
