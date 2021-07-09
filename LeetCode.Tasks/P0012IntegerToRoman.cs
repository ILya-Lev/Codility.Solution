using System.Collections.Generic;
using System.Linq;

namespace LeetCode.Tasks
{
    public class P0012IntegerToRoman
    {
        private static readonly Dictionary<char, int> _mapR = new Dictionary<char, int>()
        {
            ['I'] = 1,
            ['V'] = 5,
            ['X'] = 10,
            ['L'] = 50,
            ['C'] = 100,
            ['D'] = 500,
            ['M'] = 1000,
        };

        private static readonly Dictionary<int, char> _map = _mapR
            .OrderByDescending(p => p.Value)
            .ToDictionary(p => p.Value, p => p.Key);

        private static readonly IReadOnlyDictionary<int, int> _subtraction = new Dictionary<int, int>()
        {
            [1000] = 100,
            [500] = 100,
            [100] = 10,
            [50] = 10,
            [10] = 1,
            [5] = 1,
        };

        public string IntToRoman(int num)
        {
            var roman = new List<char>();
            foreach (var p in _map)
            {
                if (num <= 0) break;

                for (int i = 0; i < num / p.Key; i++)
                {
                    roman.Add(p.Value);
                }

                num %= p.Key;
                if (num <= 0) break;

                var subtractingNumber = p.Key - _subtraction[p.Key];
                if (num >= subtractingNumber)               //e.g. 92
                {
                    roman.Add(_map[_subtraction[p.Key]]);   //e.g. add X
                    roman.Add(p.Value);                     //e.g. add C
                    num %= subtractingNumber;               //e.g. 92 -> 92 % 90 -> 2
                }
            }

            return new string(roman.ToArray());
        }
    }
}