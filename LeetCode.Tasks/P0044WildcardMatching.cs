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

            return IsMatch(s, 0, pattern, 0);
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

        //private bool IsMatch(string s, int sHead, int sTail, string p, int pHead, int pTail)
        private bool IsMatch(string s, int sHead, string p, int pHead)
        {
            #region recursion root
            if (sHead >= s.Length)
            {
                return pHead >= p.Length || p.ToCharArray().Skip(pHead).All(c => c == AnySequence);
            }

            if (pHead >= p.Length)
            {
                return sHead >= s.Length || p[^1] == AnySequence;
            }
            #endregion recursion root

            if (p[pHead] == AnySingle)
                return IsMatch(s, sHead + 1, p, pHead + 1);

            if (p[pHead] == AnySequence)
            {
                for (int i = 0; i < s.Length - sHead; i++)//or < s.length-sHead-non-*-p after pHead
                {
                    if (IsMatch(s, sHead + i, p, pHead + 1))
                        return true;
                }

                return false;
            }

            if (p[pHead] != s[sHead])
                return false;

            return IsMatch(s, sHead + 1, p, pHead + 1);
        }
    }
}
