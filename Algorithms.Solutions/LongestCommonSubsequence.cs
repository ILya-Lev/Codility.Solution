using System.Collections.Generic;
using System.Linq;

namespace Algorithms.Solutions
{
    public class LongestCommonSubsequence
    {
        private readonly char[] _lhs;
        private readonly char[] _rhs;
        private readonly Dictionary<(int, int), string> _knownLcs = new Dictionary<(int, int), string>();

        public LongestCommonSubsequence(string lhs, string rhs)
        {
            _lhs = lhs.ToArray();
            _rhs = rhs.ToArray();
        }
        public string Get()
        {
            var lcs = DoFind(_lhs.Length - 1, _rhs.Length - 1);
            return lcs;
        }

        private string DoFind(int lhsEnd, int rhsEnd)
        {
            if (_knownLcs.TryGetValue((lhsEnd, rhsEnd), out var lcs))
                return lcs;

            if (lhsEnd < 0 || rhsEnd < 0)
                return "";

            if (_lhs[lhsEnd] == _rhs[rhsEnd])
            {
                lcs = DoFind(lhsEnd - 1, rhsEnd - 1) + _lhs[lhsEnd];

                _knownLcs.Add((lhsEnd, rhsEnd), lcs);
                return lcs;
            }

            var first = DoFind(lhsEnd - 1, rhsEnd);
            var second = DoFind(lhsEnd, rhsEnd - 1);

            lcs = first.Length > second.Length ? first : second;

            _knownLcs.Add((lhsEnd, rhsEnd), lcs);
            return lcs;
        }
    }
}