using System.Collections.Generic;
using System.Linq;

namespace LeetCode.Tasks
{
    public class P0010RegularExpressionMatching_002
    {
        public bool IsMatch(string s, string p)
        {
            var tokens = ParsePattern(p, s.Length);
            if (tokens is null)
                return false;//s is too short
            return IsCovered(tokens, 0, s, 0);
        }

        private bool IsCovered(IReadOnlyList<Token> tokens, int firstToken, string s, int sStart)
        {
            if (firstToken >= tokens.Count)//all tokens are processed and non result in failure
                return sStart >= s.Length;//is s covered completely, or it's too long?

            if (sStart >= s.Length)//whole string is processed
                return tokens.Skip(firstToken).All(t => t.AtLeast == 0);//are there any mandatory tokens left?

            var currentToken = tokens[firstToken];
            if (currentToken.AtLeast == 0)
            {
                if (IsCovered(tokens, firstToken + 1, s, sStart))//try to skip current token
                    return true;
            }

            for (int j = 1; j <= currentToken.AtMost; j++)
            {
                if (currentToken.Symbol is null)
                {
                    if (IsCovered(tokens, firstToken + 1, s, sStart + j))//skip j symbols
                        return true;
                }
                else
                {
                    if (sStart + j - 1 >= s.Length)//s is too short
                        return false;
                    if (s[sStart + j - 1] != currentToken.Symbol)//s does not contain required symbol
                        return false;

                    if (IsCovered(tokens, firstToken + 1, s, sStart + j))//j symbols are covered
                        return true;
                }
            }

            return false;
        }

        private IReadOnlyList<Token> ParsePattern(string p, int inputLength)
        {
            var tokens = new List<Token>();
            var inputRemainder = inputLength;
            for (int i = 0; i < p.Length; i++)
            {
                var token = new Token() { Symbol = p[i] == Token.AnyValue ? default(char?) : p[i] };
                tokens.Add(token);

                if (i + 1 < p.Length && p[i + 1] == Token.AnyAmount)
                {
                    token.AtLeast = 0;
                    token.AtMost = inputRemainder;
                    i++;
                    continue;
                }

                token.AtLeast = 1;
                token.AtMost = 1;
                inputRemainder--;
                if (inputRemainder < 0)
                    return null;//instead of throwing exceptions
            }

            return tokens;
        }

        public class Token
        {
            public const char AnyValue = '.';
            public const char AnyAmount = '*';

            public char? Symbol { get; set; }
            public int AtLeast { get; set; }
            public int AtMost { get; set; }
        }
    }
}
