using System;
using System.Text;

namespace Facebook.Problems
{
    public class EncryptedWords
    {
        public static string FindEncryptedWord(string s)
        {
            var encrypted = new StringBuilder(s.Length);

            Encrypt(s, 0, s.Length, encrypted);

            return encrypted.ToString();
        }

        //T(n) = 2*T(n/2) + const => O(n)
        private static void Encrypt(string s, int start, int endExclusive, StringBuilder encrypted)
        {
            if (start >= endExclusive) return;

            var middle = (endExclusive + start) / 2;
            encrypted.Append(s[middle]);
            Encrypt(s, start, middle, encrypted);
            Encrypt(s, middle + 1, endExclusive, encrypted);
        }
    }
}