using System;

namespace LeetCode.Tasks
{
    /// <summary>
    /// complexity is O(2^n) - too much!
    /// </summary>
    public class P0005LongestPalindromicSubstring
    {
        private int _longestSoFar = 0;
        public int OperationsCount { get; private set; }

        public string LongestPalindrome(string s)
        {
            if (string.IsNullOrEmpty(s)) return string.Empty;

            _longestSoFar = 0;
            OperationsCount = 0;
            return DoGetLongestPalindrome(s);
        }

        private string DoGetLongestPalindrome(string s)
        {
            OperationsCount++;
            if (s.Length < _longestSoFar)
                return String.Empty;

            if (IsPalindrome(s))
            {
                _longestSoFar = Math.Max(_longestSoFar, s.Length);
                return s;
            }

            var headPalindrome = DoGetLongestPalindrome(s.Substring(0, s.Length - 1));
            var tailPalindrome = DoGetLongestPalindrome(s.Substring(1));

            return headPalindrome.Length > tailPalindrome.Length ? headPalindrome : tailPalindrome;
        }

        private bool IsPalindrome(string s)
        {
            for (int i = 0; i < s.Length / 2; i++)
            {
                OperationsCount++;
                if (s[i] != s[s.Length - i - 1])
                    return false;
            }

            return true;
        }
    }
}
