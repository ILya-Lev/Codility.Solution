using Facebook.Problems;
using FluentAssertions;
using Xunit;

namespace Facebook.Tests
{
    public class MatchingPairsTests
    {
        [Fact]
        public void GetNumberOfMatchingPairs_SameNoDuplicates_LenMin2()
        {
            var s = "abcdefgh1234567890";
            MatchingPairs.GetNumberOfMatchingPairs(s, s).Should().Be(s.Length - 2);
        }

        [Fact]
        public void GetNumberOfMatchingPairs_SameWithDuplicates_Len()
        {
            var s = "abcdefgh123456789a";
            MatchingPairs.GetNumberOfMatchingPairs(s, s).Should().Be(s.Length);
        }

        [Fact]
        public void GetNumberOfMatchingPairs_SameWithTriplicates_Len()
        {
            var s = "abcdefgha23456789a";
            MatchingPairs.GetNumberOfMatchingPairs(s, s).Should().Be(s.Length);
        }

        [Fact]
        public void GetNumberOfMatchingPairs_OneDifferentNoDuplicates_LenMin2()
        {
            var s = "abcdefghAi34567890";
            var t = "abcdefgh1i34567890";
            MatchingPairs.GetNumberOfMatchingPairs(s, t).Should().Be(s.Length-2);
        }

        [Fact]
        public void GetNumberOfMatchingPairs_OneDifferentWithDuplicates_LenMin1()
        {
            var s = "abcdefghAe34567890";
            var t = "abcdefgh1e34567890";
            MatchingPairs.GetNumberOfMatchingPairs(s, t).Should().Be(s.Length-1);
        }

        [Fact]
        public void GetNumberOfMatchingPairs_TwoDifferentSwappingNoEffect_LenMin2()
        {
            var s = "abcBefghAe34567890";
            var t = "abc2efgh1e34567890";
            MatchingPairs.GetNumberOfMatchingPairs(s, t).Should().Be(s.Length-2);
        }

        [Fact]
        public void GetNumberOfMatchingPairs_TwoDifferentSwappingOneMatch_LenMin1()
        {
            var s = "abcBefghAe34567890";
            var t = "abcAefgh1e34567890";
            MatchingPairs.GetNumberOfMatchingPairs(s, t).Should().Be(s.Length-1);
        }

        [Fact]
        public void GetNumberOfMatchingPairs_TwoDifferentSwappingBothMatch_Len()
        {
            var s = "abcBefghAe34567890";
            var t = "abcAefghBe34567890";
            MatchingPairs.GetNumberOfMatchingPairs(s, t).Should().Be(s.Length);
        }

        [Fact]
        public void GetNumberOfMatchingPairs_ManyDifferentSwappingTwoMatchExist_SimilarityPlus2()
        {
            var s = "abcdabcBefghAe34567890qwertyqwertyqwerty";
            var t = "CDCDabcAefghBe34567890QWERTYQWERTYQWERTY";
            MatchingPairs.GetNumberOfMatchingPairs(s, t).Should().Be(16+2);
        }
    }
}