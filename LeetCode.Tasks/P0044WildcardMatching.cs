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

            return IsMatch(s, 0, p, 0);
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

            if (p[^2] == p[^1] && p[^1] == AnySequence)
                yield break;

            yield return p[^1];
        }

        private bool IsMatch(string s, int sIdx, string p, int pIdx)
        {
            #region recursion root
            if (sIdx >= s.Length)
            {
                return pIdx >= p.Length || p.ToCharArray().Skip(pIdx).All(c => c == AnySequence);
            }

            if (pIdx >= p.Length)
            {
                return sIdx >= s.Length || p[^1] == AnySequence;
            }
            #endregion recursion root

            if (p[pIdx] == AnySingle)
                return IsMatch(s, sIdx + 1, p, pIdx + 1);

            if (p[pIdx] == AnySequence)
            {
                for (int i = 0; i < s.Length - sIdx; i++)//or < s.length-sIdx-non-*-p after pIdx
                {
                    if (IsMatch(s, sIdx + i, p, pIdx + 1))
                        return true;
                }

                return false;
            }

            if (p[pIdx] != s[sIdx])
                return false;

            return IsMatch(s, sIdx + 1, p, pIdx + 1);
        }
    }
}
