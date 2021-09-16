using System.Collections.Generic;
using System.Linq;

namespace Facebook.Problems
{
    public class BalanceBrackets
    {
        private static readonly HashSet<char> _openingBrackets = new HashSet<char>()
        {
            '(', '[', '{'
        };

        private static readonly IReadOnlyDictionary<char, char> _bracketPairs = new Dictionary<char, char>()
        {
            [')'] = '(',
            ['}'] = '{',
            [']'] = '[',
        };

        public static bool IsBalanced(string s)
        {
            // Write your code here
            var notMatched = new Stack<char>();
            foreach (var c in s)
            {
                if (_openingBrackets.Contains(c))
                {
                    notMatched.Push(c);
                    continue;
                }

                if (!notMatched.Any()) return false;

                var mostRecentOpeningBracket = notMatched.Pop();
                if (_bracketPairs[c] != mostRecentOpeningBracket) return false;
            }

            return notMatched.Count == 0;
        }
    }
}