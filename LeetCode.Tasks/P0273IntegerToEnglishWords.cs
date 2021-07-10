using System.Collections.Generic;

namespace LeetCode.Tasks
{
    public class P0273IntegerToEnglishWords
    {
        private static readonly IReadOnlyDictionary<int, string> _map = new Dictionary<int, string>()
        {
            [1_000_000_000] = "Billion",
            [1_000_000] = "Million",
            [1_000] = "Thousand",
            [100] = "Hundred",

            [90] = "Ninety",
            [80] = "Eighty",
            [70] = "Seventy",
            [60] = "Sixty",
            [50] = "Fifty",
            [40] = "Forty",
            [30] = "Thirty",
            [20] = "Twenty",

            [19] = "Nineteen",
            [18] = "Eighteen",
            [17] = "Seventeen",
            [16] = "Sixteen",
            [15] = "Fifteen",
            [14] = "Fourteen",
            [13] = "Thirteen",
            [12] = "Twelve",
            [11] = "Eleven",
            [10] = "Ten",

            [9] = "Nine",
            [8] = "Eight",
            [7] = "Seven",
            [6] = "Six",
            [5] = "Five",
            [4] = "Four",
            [3] = "Three",
            [2] = "Two",
            [1] = "One",
            [0] = "Zero",
        };

        public string NumberToWords(int num)
        {
            if (num == 0) return _map[num];

            var parts = GetParts(num);

            return string.Join(" ", parts);
        }

        private static List<string> GetParts(int number)
        {
            if (number < 10) return new List<string>() { _map[number] };

            var parts = new List<string>();
            foreach (var p in _map)
            {
                if (number == 0) break;

                var current = number / p.Key;
                if (current != 0)
                {
                    if (p.Key >= 100)
                        parts.AddRange(GetParts(current));

                    parts.Add(p.Value);
                }

                number %= p.Key;
            }

            return parts;
        }
    }
}
