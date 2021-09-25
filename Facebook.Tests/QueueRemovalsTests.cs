using Facebook.Problems;
using FluentAssertions;
using Xunit;

namespace Facebook.Tests
{
    public class QueueRemovalsTests
    {
        [Fact]
        public void FindPositions_Sample_Match()
        {
            QueueRemovals.FindPositions(new[] { 1, 2, 2, 3, 4, 5 }, 5)
                .Should().Equal(new[] { 5, 6, 4, 1, 2 });
        }
    }
}