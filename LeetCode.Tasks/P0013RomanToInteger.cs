using System.Collections.Generic;
using System.Linq;

namespace LeetCode.Tasks
{
    public class P0013RomanToInteger
    {
        private static readonly Dictionary<char, int> _map = new Dictionary<char, int>()
        {
            ['I'] = 1,
            ['V'] = 5,
            ['X'] = 10,
            ['L'] = 50,
            ['C'] = 100,
            ['D'] = 500,
            ['M'] = 1000,
        };

        public int RomanToInt(string s)
        {
            if (string.IsNullOrWhiteSpace(s)) return 0;

            var roman = s.ToUpperInvariant();
            var numbers = roman.ToCharArray()
                .Select(c => _map.TryGetValue(c, out var n) ? n : 0)
                .Where(n => n > 0)
                .ToArray();

            var result = 0;
            for (int i = 0; i + 1 < numbers.Length; i++)
            {
                if (numbers[i + 1] > numbers[i])
                    result -= numbers[i];
                else
                    result += numbers[i];
            }

            return result + numbers.Last();
        }
    }
}
