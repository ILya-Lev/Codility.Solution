using System.Collections.Generic;

namespace LeetCode.Tasks
{
    public class P0010RegularExpressionMatching_001
    {
        public bool IsMatch(string s, string p)
        {
            var tokens = ParseTokens(p);

            var sIdx = 0;
            int i = 0;

            for (; i < tokens.Count && sIdx < s.Length; i++)
            {
                var canSkipSome = false;
                var j = 0;
                while (i + j < tokens.Count && tokens[i + j].Value == default)
                {
                    canSkipSome = true;//instead of bool use += at least per token
                    j++;
                }

                if (i + j >= tokens.Count)
                    return true;//pattern is equivalent to .*
                else
                    i = i + j;

                var currentMatch = s.IndexOf(tokens[i].Value.Value, sIdx);
                if (currentMatch < 0 && tokens[i].AtLeast > 0) return false;//mismatch
                if (!canSkipSome && currentMatch > sIdx) return false;//there is a gap and it's forbidden

                if (currentMatch < 0 && tokens[i].AtLeast == 0)
                    continue;

                sIdx = currentMatch;//if > it's skipped and is ok; otherwise no change

                var shift = 0;
                while (shift < tokens[i].AtLeast && sIdx + shift < s.Length)
                {
                    if (s[sIdx + shift] != tokens[i].Value)
                        return false;
                    shift++;
                }

                if (shift < tokens[i].AtLeast)
                    return false;//s is less then needed, e.g. s=aa, p=aaa

                //cover AtMost
                while (sIdx + shift < s.Length && s[sIdx + shift] == tokens[i].Value && shift < tokens[i].AtMost)
                {
                    shift++;
                }

                //if (shift > tokens[i].AtMost)
                //    return false;// having more items then expected, e.g. s=aa vs p=a

                sIdx += shift;
            }

            return i >= tokens.Count && sIdx >= s.Length;
        }

        public static IReadOnlyList<Token> ParseTokens(string pattern)
        {
            var tokens = new List<Token>();
            for (int i = 0; i < pattern.Length; i++)
            {
                if (pattern[i] == Token.AnyValue)
                {
                    var token = new Token() { Value = default, AtLeast = 1, AtMost = 1 };
                    if (i + 1 < pattern.Length && pattern[i + 1] == Token.AnyAmount)
                    {
                        token.AtLeast = 0;
                        token.AtMost = int.MaxValue;
                        i++;
                    }
                    tokens.Add(token);
                    continue;
                }

                var t = new Token() { Value = pattern[i], AtLeast = 1, AtMost = 1 };
                if (i + 1 < pattern.Length && pattern[i + 1] == Token.AnyAmount)
                {
                    t.AtLeast = 0;
                    t.AtMost = int.MaxValue;
                    i++;
                }
                while (i + 1 < pattern.Length
                       && (pattern[i + 1] == t.Value || pattern[i + 1] == Token.AnyAmount))
                {
                    t.AtLeast++;
                    t.AtMost = t.AtMost == int.MaxValue ? int.MaxValue : (t.AtMost + 1);
                    i++;
                }
                tokens.Add(t);
            }

            return tokens;
        }

        public class Token
        {
            public const char AnyValue = '.';
            public const char AnyAmount = '*';

            public char? Value { get; set; }//null stands for '.', means any
            public int AtLeast { get; set; }//e.g. a* => 0; a => 1; a*aa => 2
            public int AtMost { get; set; }//e.g. a => 1; a* => int.MaxInt
        }
    }
}
