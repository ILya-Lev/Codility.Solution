using Facebook.Problems;
using FluentAssertions;
using Xunit;

namespace Facebook.Tests
{
    [Trait("Category", "Unit")]
    public class EncryptedWordsTests
    {
        [Theory]
        [InlineData("abc", "bac")]
        [InlineData("abcxcba", "xbacbca")]
        [InlineData("a", "a")]
        public void FindEncryptedWord_Sample_MatchExpectation(string s, string r)
        {
            EncryptedWords.FindEncryptedWord(s).Should().Be(r);
        }
    }
}