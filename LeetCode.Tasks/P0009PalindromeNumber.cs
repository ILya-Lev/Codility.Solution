using System.Collections.Generic;

namespace LeetCode.Tasks
{
    public class P0009PalindromeNumber
    {
        public bool IsPalindrome(int x)
        {
            if (x < 0) return false;

            var digits = new List<int>(11);

            var remainder = x;
            while (remainder > 0)
            {
                digits.Add(remainder % 10);
                remainder /= 10;
            }

            for (int i = 0; i < digits.Count / 2; i++)
            {
                if (digits[i] != digits[^(i + 1)])
                    return false;
            }

            return true;
        }
    }

    public class P0009PalindromeNumber_InPlace
    {
        //as input should be within Int32.MaxValue, do it in place
        public bool IsPalindrome(int x)
        {
            if (x < 0) return false;

            var remainder = x;
            var reversed = 0;
            while (remainder > 0)
            {
                var currentDigit = remainder % 10;

                if (reversed > (int.MaxValue - currentDigit) / 10)
                    return false;

                reversed = reversed * 10 + currentDigit;
                remainder /= 10;
            }

            return reversed == x;
        }
    }
}
