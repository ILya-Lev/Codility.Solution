using FluentAssertions;
using System;
using Xunit;

namespace LuxoftPolandContest.Tests
{
    public class MeetingArrangementTests
    {
        [Fact]
        public void MinRoomsNumber_Sample_4()
        {
            var s = new DateTime(2019,06,16,00,00,00, DateTimeKind.Local);
            var meetings = new[]
            {
                new Meeting(){StartTime = s.AddHours(8), EndTime = s.AddHours(9).AddMinutes(15)},
                new Meeting(){StartTime = s.AddHours(13).AddMinutes(20), EndTime = s.AddHours(15).AddMinutes(20)},
                new Meeting(){StartTime = s.AddHours(10), EndTime = s.AddHours(14)},
                new Meeting(){StartTime = s.AddHours(13).AddMinutes(55), EndTime = s.AddHours(16).AddMinutes(25)},
                new Meeting(){StartTime = s.AddHours(14), EndTime = s.AddHours(17).AddMinutes(45)},
                new Meeting(){StartTime = s.AddHours(14).AddMinutes(05), EndTime = s.AddHours(17).AddMinutes(45)},
            };

            var arrangement = new MeetingArrangement();
            arrangement.MinRoomsNumber(meetings).Should().Be(4);
        }
    }
}