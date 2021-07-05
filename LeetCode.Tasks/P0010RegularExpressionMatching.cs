using System.Collections.Generic;

namespace LeetCode.Tasks
{
    public class P0010RegularExpressionMatching
    {
        public bool IsMatch(string s, string p)
        {
            var tokens = ParsePattern(p);

            var currentIndex = 0;
            var i = 0;
            for (; i < tokens.Count && currentIndex < s.Length; i++)
            {
                //'.' cases
                if (!tokens[i].Value.HasValue)
                {
                    if (tokens[i].Amount.HasValue)
                    {
                        currentIndex++; //move by 1 symbol in s; should be index += amount
                        continue;//use next token
                    }

                    //.* case - move by 0 or more of any symbols
                    var j = i + 1;//find next specific token
                    while (j < tokens.Count && !tokens[j].Value.HasValue)
                    {
                        j++;
                    }

                    i = j - 1;
                    if (j == tokens.Count) //there is no other specific token
                    {
                        return true;
                    }

                    while (currentIndex < s.Length && s[currentIndex] != tokens[j].Value)
                        currentIndex++;

                    continue;//use next token
                }

                //non '.' cases
                //x - case
                if (tokens[i].Amount.HasValue)
                {
                    if (tokens[i].Value != s[currentIndex])
                        return false;//not matched

                    currentIndex++; //move by 1 symbol in s; index += amount
                    continue; //use next token
                }

                //x* case - 0 or more times the same
                while (currentIndex < s.Length && s[currentIndex] == tokens[i].Value)
                {
                    currentIndex++;
                }
            }

            if (i == tokens.Count && currentIndex < s.Length) return false;//not whole s is covered

            return currentIndex == s.Length && i == tokens.Count;
        }

        private static List<Token> ParsePattern(string p)
        {
            var tokens = new List<Token>();
            var pattern = p.ToCharArray();
            for (int i = 0; i + 1 < pattern.Length; i++)
            {
                if (pattern[i + 1] == Token.AnyAmount)
                {
                    AddToken(pattern[i], null);
                    i++;
                    continue;
                }

                AddToken(pattern[i], 1);
            }

            if (pattern[^1] != Token.AnyAmount)
                AddToken(pattern[^1], 1);

            return tokens;

            void AddToken(char symbol, int? amount)
            {
                tokens.Add(new Token()
                {
                    Value = symbol == Token.AnyValue ? default(char?) : symbol,
                    Amount = amount
                });
            }
        }

        /// <summary>
        /// x -> value == x; amount == 1
        /// . -> value == null; amount == 1
        /// a* -> value == a; amount == null
        /// .* -> value == null; amount == null
        /// </summary>
        /// <remarks>
        /// a*aa case is missing => value == a; amount = 2 or more => need to introduce amount range
        /// </remarks>
        private class Token
        {
            public const char AnyValue = '.';
            public const char AnyAmount = '*';

            public char? Value { get; set; }//null means any, i.e. for '.'
            public int? Amount { get; set; }//null means any, i.e. for '*'
        }
    }
}
