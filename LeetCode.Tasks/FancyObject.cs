using System;

namespace LeetCode.Tasks
{
    public class FancyObject
    {
        public int I { get; }
        public string S { get; }
        public DateTime Dt { get; set; }
        public DayOfWeek DayOfWeek { get; }

        public FancyObject(int i, string s, DayOfWeek dayOfWeek)
        {
            I = i;
            S = s;
            DayOfWeek = dayOfWeek;
        }
    }
}