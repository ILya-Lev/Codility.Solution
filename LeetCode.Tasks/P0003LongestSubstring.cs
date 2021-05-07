using System;
using System.Collections.Generic;

namespace LeetCode.Tasks
{
    public class P0003LongestSubstring
    {
        public int FindLongestUniqueLength(string s)
        {
            var maxLength = 0;
            for (int start = 0; start < s.Length; start++)
            {
                var currentLength = GetUniqueSubstringLength(s, start);
                maxLength = Math.Max(maxLength, currentLength);
            }
            return maxLength;
        }

        private int GetUniqueSubstringLength(string s, int start)
        {
            var seenCharacters = new HashSet<char>();
            for (int i = start; i < s.Length; i++)
            {
                if (!seenCharacters.Add(s[i]))  //returns false if already present
                    return i - start;
            }
            return s.Length - start;
        }
    }
}
