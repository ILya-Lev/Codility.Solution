using Facebook.Problems;
using FluentAssertions;
using Xunit;

namespace Facebook.Tests
{
    public class ChangeForeignCurrenciesTests
    {
        [Theory]
        [InlineData(new[]{5,10,25,100,200}, 94, false)]
        [InlineData(new[]{4,17,29}, 75, true)]
        public void CanGetExactChange_Sample_MatchExpectations(int[] denominations, int targetMoney, bool result)
        {
            ChangeForeignCurrencies.CanGetExactChange(targetMoney, denominations).Should().Be(result);
        }

        [Theory]
        [InlineData(new[]{5,10,25,100,200}, 94, false)]
        [InlineData(new[]{4,17,29}, 75, true)]
        public void CanGetExactChangeRecursive_Sample_MatchExpectations(int[] d, int m, bool r)
        {
            ChangeForeignCurrencies.CanGetExactChange_Recursive(m, d).Should().Be(r);
        }
    }
}