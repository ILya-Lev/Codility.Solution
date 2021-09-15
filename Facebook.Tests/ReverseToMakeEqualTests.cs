using Facebook.Problems;
using FluentAssertions;
using Xunit;

namespace Facebook.Tests
{
    public class ReverseToMakeEqualTests
    {
        [Fact]
        public void AreTheyEqual_Permutation_True()
        {
            var a = new[] { 1, 2, 3, 4 };
            var b = new[] { 1, 4, 3, 2 };

            ReverseToMakeEqual.AreTheyEqual(a, b).Should().BeTrue();
        }

        [Fact]
        public void AreTheyEqual_DifferentDuplicated_False()
        {
            var a = new[] { 1, 2, 3, 3, 4 };
            var b = new[] { 1, 4, 3, 2, 2 };

            ReverseToMakeEqual.AreTheyEqual(a, b).Should().BeFalse();
        }

        [Fact]
        public void AreTheyEqual_DifferentElement_False()
        {
            var a = new[] { 1, 2, 3, 5, 4 };
            var b = new[] { 1, 4, 3, 6, 2 };

            ReverseToMakeEqual.AreTheyEqual(a, b).Should().BeFalse();
        }
    }
}