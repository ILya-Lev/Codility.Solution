using Facebook.Problems;
using FluentAssertions;
using Xunit;

namespace Facebook.Tests
{
    public class RotationStringTests
    {
        [Theory]
        [InlineData("Zebra-493?", 3, "Cheud-726?")]
        [InlineData("abcdefghijklmNOPQRSTUVWXYZ0123456789", 39, "nopqrstuvwxyzABCDEFGHIJKLM9012345678")]
        public void RotationalCipher_SampleInputs_MatchExpectations(string input, int factor, string expected)
        {
            RotationString.RotationalCipher(input, factor).Should().Be(expected);
        }
    }
}