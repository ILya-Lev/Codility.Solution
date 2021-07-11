using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LeetCode.Tasks
{
    public class P0044WildcardMatch_002
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

                var tokens = ConstructTokens(p);
                var tokensByPriority = tokens
                    .GroupBy(t => t.Priority, t => t)
                    .OrderByDescending(g => g.Key)
                    .ToArray();

                var solidTokenGroups = tokensByPriority
                    .Where(g => g.Key != 0)
                    .Select(g => g.OrderBy(t => t.FromStart))
                    .ToArray();

                foreach (var group in solidTokenGroups)
                {
                    foreach (var token in group)//handle * case
                    {
                        for (int i = token.FromStart; i < s.Length - token.FromEnd - token.Priority; i++)
                        {
                            if (IsMatchSolidPattern(s.Substring(i, token.Priority), token.Pattern))
                                token.AddMatching(i);
                        }
                    }
                }

                if (tokens.Any(t => t.Priority != 0 && t.MatchingPositions.Count == 0))
                    return false;

                //check if there is a combination of matching positions + pattern length, so that it does not overlap

                return false;
            }

            private bool IsMatchSolidPattern(string s, string p)
            {
                for (int i = 0; i < p.Length; i++)
                {
                    if (p[i] == AnySingle)
                        continue;
                    if (p[i] != s[i])
                        return false;
                }

                return true;
            }

            private IReadOnlyList<Token> ConstructTokens(string p)
            {
                var tokens = new List<Token>();
                for (var i = 0; i < p.Length; i++)
                {
                    var currentToken = new Token();

                    while (i < p.Length && p[i] != AnySequence)
                    {
                        currentToken.AddChar(p[i++]);
                    }

                    if (currentToken.Pattern.Length != 0)
                    {
                        tokens.Add(currentToken);
                    }

                    if (i < p.Length && p[i] == AnySequence)
                    {
                        currentToken = new Token();
                        currentToken.AddChar(AnySequence);
                        tokens.Add(currentToken);
                    }
                }

                for (int i = 0; i + 1 < tokens.Count; i++)
                {
                    //i.e. consequent *
                    //impossible case is like aa = aa, as it should be one token aaaa
                    if (tokens[i].Pattern == tokens[i + 1].Pattern)
                        tokens.RemoveAt(i--);
                }

                var distanceToStart = 0;
                for (int i = 0; i < tokens.Count; i++)
                {
                    tokens[i].FromStart = distanceToStart;
                    distanceToStart += tokens[i].Priority;
                }

                var distanceToEnd = 0;
                for (int i = tokens.Count - 1; i >= 0; i--)
                {
                    tokens[i].FromEnd = distanceToEnd;
                    distanceToEnd += tokens[i].Priority;
                }

                return tokens;
            }

            private class Token
            {
                public int FromStart { get; set; } //how many non-'*' symbols are in the pattern before current start
                public int FromEnd { get; set; } //how many non-'*' symbols are in the pattern after current end
                public string Pattern => _storage.ToString();
                public int Priority => Pattern[0] == AnySequence ? 0 : Pattern.Length;

                private readonly StringBuilder _storage = new StringBuilder();
                public void AddChar(char c) => _storage.Append(c);

                public List<int> MatchingPositions { get; } = new List<int>();
                public void AddMatching(int matchingPosition) => MatchingPositions.Add(matchingPosition);
            }
        }
    }
}
