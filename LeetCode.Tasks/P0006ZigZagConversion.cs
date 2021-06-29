using System.Collections.Generic;
using System.Linq;

namespace LeetCode.Tasks
{
    public class P0006ZigZagConversion
    {
        public string ConvertFast(string s, int numRows)
        {
            if (string.IsNullOrWhiteSpace(s) || numRows == 1) return s;

            var patternSize = 2 * (numRows - 1);

            var chars = new Dictionary<int, List<char>>();
            for (int i = 0; i < s.Length; i++)
            {
                var remainder = i % patternSize;
                var rowNumber = remainder <= numRows - 1 ? remainder : patternSize - remainder;

                if (!chars.ContainsKey(rowNumber))
                    chars.Add(rowNumber, new List<char>());
                chars[rowNumber].Add(s[i]);
            }

            var result = new List<char>(s.Length);
            foreach (var pair in chars)
            {
                result.AddRange(pair.Value);
            }

            return new string(result.ToArray());
        }

        public string Convert(string s, int numRows)
        {
            if (string.IsNullOrWhiteSpace(s) || numRows == 1) return s;

            var charByNumber = GetCharByNumber(s, numRows);
            var chars = new List<char>(s.Length);
            for (int rowNumber = 1; rowNumber <= numRows; rowNumber++)
            {
                chars.AddRange(charByNumber.Where(item => item.r == rowNumber).Select(item => item.c));
            }

            return new string(chars.ToArray());
        }

        private static List<(char c, int r)> GetCharByNumber(string s, int numRows)
        {
            var charByNumber = new List<(char, int)>(s.Length);
            bool isDown = true;
            var rowNumber = 1;
            foreach (var c in s)
            {
                charByNumber.Add((c, rowNumber));

                if (isDown)
                {
                    if (rowNumber == numRows)
                    {
                        isDown = false;
                        rowNumber--;
                    }
                    else
                    {
                        rowNumber++;
                    }
                }
                else
                {
                    if (rowNumber == 1)
                    {
                        isDown = true;
                        rowNumber++;
                    }
                    else
                    {
                        rowNumber--;
                    }
                }
            }

            return charByNumber;
        }
    }
}
