using Facebook.Problems;
using FluentAssertions;
using Xunit;

namespace Facebook.Tests
{
    public class PerformQueriesTests
    {
        [Fact]
        public void ExecuteQueries_Sample_MatchExpectations()
        {
            var queries = new[]
            {
                (2, 1), (1, 1), (1, 2), (2, 4), (2, 2)
            };

            var output = PerformQueries.ExecuteQueries(queries, 5);

            output.Should().Equal(new[] { -1, -1, 2 });
        }
    }
}
