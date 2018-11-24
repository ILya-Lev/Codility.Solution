using System.Collections.Generic;

namespace Codility.Solvers
{
    public class Brackets
    {
        private static readonly Dictionary<char, char> _bracketMapping = new Dictionary<char, char>
        {
            ['('] = ')',
            ['{'] = '}',
            ['['] = ']',
        };

        private static readonly Dictionary<char, char> _revertedBracketMapping = new Dictionary<char, char>
        {
            [')'] = '(',
            ['}'] = '{',
            [']'] = '[',
        };

        public bool IsProperlyNested(string input)
        {
            var seenOpeningBrackets = new Stack<char>();
            foreach (var symbol in input.ToCharArray())
            {
                if (IsOpening(symbol))
                {
                    seenOpeningBrackets.Push(symbol);
                    continue;
                }

                if (IsClosing(symbol))
                {
                    if (!MatchExpectedClosingBracket(seenOpeningBrackets, symbol))
                        return false;
                }
            }

            return seenOpeningBrackets.Count == 0;
        }

        private static bool IsOpening(char symbol) => _bracketMapping.ContainsKey(symbol);
        private static bool IsClosing(char symbol) => _revertedBracketMapping.ContainsKey(symbol);

        private static bool MatchExpectedClosingBracket(Stack<char> seenOpeningBrackets, char currentClosing)
        {
            if (seenOpeningBrackets.Count == 0)
                return false;
            var previousOpening = seenOpeningBrackets.Pop();
            var expectedClosing = _bracketMapping[previousOpening];

            return expectedClosing == currentClosing;
        }
    }
}
