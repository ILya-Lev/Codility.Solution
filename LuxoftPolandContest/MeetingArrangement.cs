using System;
using System.Collections.Generic;
using System.Linq;

namespace LuxoftPolandContest
{
    public class MeetingArrangement
    {
        /// <remarks>
        /// assumption - all meetings are held the same date and with precision HH:mm
        /// </remarks>
        public int MinRoomsNumber(IReadOnlyCollection<Meeting> meetings)
        {
            var rankedMeetings = meetings.OrderBy(m => m.StartTime).ThenBy(m => m.EndTime).ToArray();
            var overlappingMeetingsCounts = OverlappingMeetingsCounts(rankedMeetings).ToArray();
            return overlappingMeetingsCounts.Max();
        }

        private static IEnumerable<int> OverlappingMeetingsCounts(Meeting[] rankedMeetings)
        {
            for (var currentTime = rankedMeetings.First().StartTime;
                currentTime <= rankedMeetings.Last().EndTime;
                currentTime = currentTime.AddMinutes(1))
            {
                yield return rankedMeetings.Count(m => ContainsTime(m, currentTime));
            }
        }

        private static bool ContainsTime(Meeting meeting, DateTime currentTime)
        {
            return currentTime >= meeting.StartTime && currentTime <= meeting.EndTime;
        }
    }

    public class Meeting
    {
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
    }
}