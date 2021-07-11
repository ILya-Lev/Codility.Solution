using System;
using System.Collections.Generic;
using System.Linq;

namespace LeetCode.Tasks
{
    public class P0044WildcardMatching
    {
        private const char AnySingle = '?';
        private const char AnySequence = '*';

        public bool IsMatch(string s, string p)
        {
            if (string.IsNullOrWhiteSpace(s))
                return p?.ToCharArray().All(c => c == AnySequence) ?? false;
            if (string.IsNullOrWhiteSpace(p))
                return string.IsNullOrWhiteSpace(s);

            var pattern = new string(SimplifyPattern(p).ToArray());

            return IsMatch(s, 0, s.Length - 1, pattern, 0, pattern.Length - 1);
        }

        private IEnumerable<char> SimplifyPattern(string p)
        {
            if (p.Length == 1)
            {
                yield return p[0];
                yield break;
            }

            for (int i = 0; i + 1 < p.Length; i++)
            {
                if (p[i] == p[i + 1] && p[i] == AnySequence)
                    continue;
                yield return p[i];
            }

            yield return p[^1];
        }

        private bool IsMatch(string s, int sHead, int sTail, string p, int pHead, int pTail)
        {
            #region recursion root
            if (sHead > sTail)
            {
                return (pHead > pTail && LengthWithoutAnySequenceMatch(s, p))
                    || (pHead <= pTail && p.ToCharArray().Skip(pHead).Take(pTail - pHead + 1).All(c => c == AnySequence));
            }

            if (pHead > pTail)
            {
                return sHead > sTail || p[Math.Max(0, pTail)] == AnySequence;
            }
            #endregion recursion root

            if (p[pHead] == AnySingle)
                return IsMatch(s, sHead + 1, sTail, p, pHead + 1, pTail);

            if (p[pTail] == AnySingle)
                return IsMatch(s, sHead, sTail - 1, p, pHead, pTail - 1);

            if (p[pHead] == AnySequence)
            {
                for (int i = 0; i <= sTail - sHead; i++)//or < s.length-sHead-non-*-p after pHead
                {
                    if (IsMatch(s, sHead + i, sTail, p, pHead + 1, pTail))
                        return true;
                }

                return false;
            }

            if (p[pTail] == AnySequence)
            {
                for (int i = 0; i <= sTail - sHead; i++)
                {
                    if (IsMatch(s, sHead, sTail - i, p, pHead, pTail - 1))
                        return true;
                }

                return false;
            }

            if (p[pHead] != s[sHead] || p[pTail] != s[sTail])
                return false;

            return IsMatch(s, sHead + 1, sTail - 1, p, pHead + 1, pTail - 1);
        }

        private bool LengthWithoutAnySequenceMatch(string s, string p)
        {
            if (p.Contains(AnySequence))
                return true;
            return p.Length == s.Length;
        }
    }
}
