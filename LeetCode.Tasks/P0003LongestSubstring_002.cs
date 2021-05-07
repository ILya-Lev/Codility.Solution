using System;
using System.Collections.Generic;

namespace LeetCode.Tasks
{
    public class P0003LongestSubstring_002
    {
        /// <summary>
        /// O(n) solution
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public int FindLongestUniqueLength(string s)
        {
            int start = 0, maxLength = 0;
            var lastOccurrence = new Dictionary<char, int>();
            for (int i = 0; i < s.Length; i++)
            {
                //if the char has already been met treat as a new start max of next index for previous time the char
                //was met or current start - i.e. make sure your substring is without any gaps
                if (lastOccurrence.ContainsKey(s[i]))
                {
                    start = Math.Max(lastOccurrence[s[i]] + 1, start);
                }

                //here i is the end of the substring, +1 to take into account both ends of the substring
                maxLength = Math.Max(maxLength, i - start + 1);

                lastOccurrence[s[i]] = i;
            }

            return maxLength;
        }
    }
}