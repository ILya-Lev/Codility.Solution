using Facebook.Problems;
using FluentAssertions;
using Xunit;

namespace Facebook.Tests
{
    public class OneBillionUsersTests
    {
        [Theory]
        [InlineData(new double[]{1.5}, 52)]
        [InlineData(new double[]{1.1,1.2,1.3}, 79)]
        [InlineData(new double[]{1.01, 1.02}, 1047)]
        public void GetBillionUsersDay_Sample_ExpectedAmountOfDays(double[] rates, int days)
        {
            OneBillionUsers.GetBillionUsersDay(rates).Should().Be(days);
        }
    }
}