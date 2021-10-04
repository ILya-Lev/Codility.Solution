using Algorithms.Solutions;
using FluentAssertions;
using FluentAssertions.Execution;
using Xunit;

namespace Algorithms.Tests
{
    public class LongestCommonSubsequenceTests
    {
        [Fact]
        public void Get_Exists_Find()
        {
            var lhs = "abcbdab";
            var rhs = "bdcaba";
            var solver = new LongestCommonSubsequence(lhs, rhs);

            var lcs = solver.Get();

            using var scope = new AssertionScope();
            lcs.Length.Should().Be(4);
            lcs.Should().Be("bdab");
        }
    }
}
