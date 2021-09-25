using Facebook.Problems;
using FluentAssertions;
using Xunit;

namespace Facebook.Tests
{
    public class RevenueMilestonesTests
    {
        [Fact]
        public void GetMilestoneDays_Sample1_MatchExpectations()
        {
            var revenues = new[] { 10, 20, 30, 40, 50, 60, 70, 80, 90, 100 };
            var milestones = new[] { 100, 200, 500 };
           
            RevenueMilestones.GetMilestoneDays(revenues, milestones)
                .Should().Equal(new[] { 4, 6, 10 });
        }

        [Fact]
        public void GetMilestoneDays_CannotMeetMilestones_TailIsNegative()
        {
            var revenues = new[] { 10, 20, 30, 40, 50, 60, 70, 80, 90, 100 };
            var milestones = new[] { 100, 500, 1000 };
           
            RevenueMilestones.GetMilestoneDays(revenues, milestones)
                .Should().Equal(new[] { 4, 10, -1 });
        }

        [Fact]
        public void GetMilestoneDays_MeetMilestonesBeforeTheEnd_TailIsQuiteEarly()
        {
            var revenues = new[] { 10, 20, 30, 40, 50, 60, 70, 80, 90, 100 };
            var milestones = new[] { 100, 100, 100, 100, 200 };
           
            RevenueMilestones.GetMilestoneDays(revenues, milestones)
                .Should().Equal(new[] { 4, 4, 4, 4, 6 });
        }
    }
}