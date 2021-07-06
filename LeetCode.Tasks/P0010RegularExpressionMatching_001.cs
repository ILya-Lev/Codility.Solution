using System.Collections.Generic;

namespace LeetCode.Tasks
{
    public class P0010RegularExpressionMatching_001
    {
        public bool IsMatch(string s, string p)
        {
            var tokens = ParseTokens(p);

            var sIdx = 0;
            var tokenIdx = 0;
            char? tailChar = null;

            for (; tokenIdx < tokens.Count; tokenIdx++)
            {
                var currentToken = tokens[tokenIdx];
                if (currentToken.Value.HasValue)
                {
                    if (tailChar.HasValue)//finish tail
                    {
                        while (sIdx < s.Length && s[sIdx] == tailChar)
                        {
                            sIdx++;
                        }
                    }

                    for (int i = 0; i < currentToken.AtLeast; i++)//cover at least amount
                    {
                        if (sIdx + i >= s.Length || s[sIdx + i] != currentToken.Value)
                            return false;
                    }

                    sIdx += currentToken.AtLeast;

                    if (currentToken.AtMost > currentToken.AtLeast)//if there might be a tail
                    {
                        if (tokenIdx + 1 >= tokens.Count)//if it's already latest token
                        {
                            while (sIdx < s.Length)//but there is only a tail
                            {
                                if (s[sIdx++] != currentToken.Value)
                                    return false;
                            }

                            return true;
                        }

                        //---- a*aa. won't be matched
                        tailChar = currentToken.Value;
                    }
                    else
                    {
                        tailChar = null;
                    }

                    continue;
                }

                // case .
                //1. tail char is none
                if (tailChar is null)
                {
                    for (int i = 0; i < currentToken.AtLeast; i++)//cover at least amount
                    {
                        if (sIdx + i >= s.Length)
                            return false;
                    }

                    sIdx += currentToken.AtLeast;

                    if (currentToken.AtMost > currentToken.AtLeast)//if there might be a tail
                    {
                        if (tokenIdx + 1 >= tokens.Count)//if it's already latest token
                        {
                            return true;//as current token is .*
                        }

                        var nextToken = tokens[tokenIdx + 1];
                        if (nextToken.Value.HasValue)
                            while (sIdx < s.Length && s[sIdx] != nextToken.Value)
                            {
                                sIdx++;
                            }
                        else
                        {
                            var newSIdx = s.Length - tokens.Count + tokenIdx;
                            if (newSIdx > sIdx)
                                sIdx = newSIdx;

                            continue;
                        }
                    }
                }
                //2. tail char is not none
                var tailLength = 0;
                while (sIdx + tailLength < s.Length && s[sIdx + tailLength] == tailChar)
                {
                    tailLength++;
                }
                if (tokenIdx + 1 >= tokens.Count)//if it's already latest token
                {
                    return currentToken.AtLeast <= tailLength;
                }
                else
                {//convoluted
                    var newSIdx = s.Length - tokens.Count + tokenIdx;
                    if (newSIdx > sIdx)
                        sIdx = newSIdx;
                }
            }


            return tokenIdx >= tokens.Count && sIdx >= s.Length;
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
