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
}
