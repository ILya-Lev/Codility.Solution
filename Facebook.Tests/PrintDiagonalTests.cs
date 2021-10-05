using Facebook.Problems;
using FluentAssertions;
using System.Collections.Generic;
using Xunit;

namespace Facebook.Tests
{
    public class PrintDiagonalTests
    {
        [Fact]
        public void Print_Sample_MatchExpectations()
        {
            var matrix = new[,]
            {
                { 1, 2, 3, 4 },
                { 5, 6, 7, 8 },
                { 9, 10, 11, 12 }
            };

            var expected = new List<string>()
            {
                "1 ",
                "2 5 ",
                "3 6 9 ",
                "4 7 10 ",
                "8 11 ",
                "12 "
            };

            PrintDiagonal.Print(matrix).Should().Equal(expected);
        }
    }
}
