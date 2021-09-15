using Facebook.Problems;
using FluentAssertions;
using Xunit;

namespace Facebook.Tests
{
    public class PassingYearBooksTests
    {
        [Fact]
        public void FindSignatureCounts_Sample1_All2()
        {
            PassingYearBooks.FindSignatureCounts(new[] { 2, 1 }).Should().Equal(new[] { 2, 2 });
        }

        [Fact]
        public void FindSignatureCounts_Sample2_All1()
        {
            PassingYearBooks.FindSignatureCounts(new[] { 1, 2 }).Should().Equal(new[] { 1, 1 });
        }

        [Fact]
        public void FindSignatureCounts_Sample3_Ok()
        {
            PassingYearBooks.FindSignatureCounts(new[] {3, 2, 1 })
                .Should().Equal(new[] { 2, 1, 2 });
        }
    }
}