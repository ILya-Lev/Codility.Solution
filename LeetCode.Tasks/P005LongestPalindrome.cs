using System;

namespace LeetCode.Tasks
{
    /// <summary>
    /// complexity is O(n^2) more or less affordable
    ///
    /// could be even better for middle loop not 0-length, but length/2, length/2+-1, ...
    /// taking into account input data
    /// </summary>
    public class P005LongestPalindrome
    {
        public string LongestPalindrome(string s)
        {
            if (string.IsNullOrEmpty(s)) return String.Empty;

            return DoGetLongestPalindrome(s);
        }

        private string DoGetLongestPalindrome(string s)
        {
            var odd = GetLongestOddPalindrome(s);
            var even = GetLongestEvenPalindrome(s);

            var longest = odd.length > even.length ? odd : even;
            return s.Substring(longest.strat, longest.length);
        }

        private (int strat, int length) GetLongestOddPalindrome(string s)
        {
            (int strat, int length) longest = (0, 0);
            for (int middle = 0; middle < s.Length; middle++)
            {
                var palindrome = GetOddPalindrome(s, middle);

                if (longest.length < palindrome.length)
                    longest = palindrome;
            }

            return longest;
        }

        private (int start, int length) GetOddPalindrome(string s, int middle)
        {
            var shift = 0;
            var lowerBound = GetLowerBound(middle, shift);
            var upperBound = GetUpperBound(middle, shift);

            while (lowerBound >= 0 && upperBound < s.Length)
            {
                if (s[lowerBound] != s[upperBound])
                    break;

                shift++;
                lowerBound = GetLowerBound(middle, shift);
                upperBound = GetUpperBound(middle, shift);
            }

            //shift-1 + middle + shift-1
            return (middle - shift + 1, 2 * shift - 1);
        }

        private (int strat, int length) GetLongestEvenPalindrome(string s)
        {
            (int strat, int length) longest = (0, 0);
            for (int middle = 0; middle + 1 < s.Length; middle++)
            {
                if (s[middle] != s[middle + 1])
                    continue;

                var palindrome = GetEvenPalindrome(s, middle);

                if (longest.length < palindrome.length)
                    longest = palindrome;
            }

            return longest;
        }

        private (int start, int length) GetEvenPalindrome(string s, int middle)
        {
            var shift = 0;
            var lowerBound = GetLowerBound(middle, shift);
            var upperBound = GetUpperBound(middle + 1, shift);

            while (lowerBound >= 0 && upperBound < s.Length)
            {
                if (s[lowerBound] != s[upperBound])
                    break;

                shift++;
                lowerBound = GetLowerBound(middle, shift);
                upperBound = GetUpperBound(middle + 1, shift);
            }

            //shift-1 + middle1 + middle2 + shift-1
            return (middle - shift + 1, 2 * shift);
        }

        private int GetLowerBound(int middle, int shift) => middle - shift;
        private int GetUpperBound(int middle, int shift) => middle + shift;
    }
}
