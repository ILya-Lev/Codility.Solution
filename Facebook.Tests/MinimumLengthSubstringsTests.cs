using Facebook.Problems;
using FluentAssertions;
using Microsoft.VisualStudio.TestPlatform.ObjectModel.DataCollection;
using Xunit;

namespace Facebook.Tests
{
    public class MinimumLengthSubstringsTests
    {
        [Fact]
        public void MinLengthSubstring_Sample1_5()
        {
            MinimumLengthSubstrings.MinLengthSubstring("dcbefebce", "fd").Should().Be(5);
        }

        [Fact]
        public void MinLengthSubstring_Sample2_5()
        {
            MinimumLengthSubstrings.MinLengthSubstring("dcbeffffebce", "fd").Should().Be(5);
        }
    }
}