using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace Codility.Solvers
{
    public class PyconProblems
    {
        public static string GetFrequency(string input)
        {
            var freq = new Dictionary<char, int>();
            foreach (var symbol in input.ToCharArray().Where(ch => char.IsLetter(ch) && char.IsLower(ch)))
            {
                if (!freq.ContainsKey(symbol))
                    freq.Add(symbol, 0);
                freq[symbol]++;
            }

            var frequencyStrings = freq.Select(p => $"{p.Key}{p.Value}").OrderBy(item => item);
            return string.Join("", frequencyStrings);
        }

        public static string Zip(string input)
        {
            var sb = new StringBuilder();
            for (int i = 0; i < input.Length; i++)
            {
                var current = input[i];
                var counter = 0;
                for (int j = i; j < input.Length && current == input[j]; j++)
                {
                    counter++;
                }

                var freq = counter == 1 ? $"{current}" : $"{counter}{current}";
                sb.Append(freq);
                i += counter - 1;
            }

            return sb.ToString();
        }

        public static IEnumerable<int> GenerateKollatz(int seed)
        {
            var current = seed;
            while (current != 1)
            {
                yield return current;
                current = current % 2 == 0 ? current / 2 : (current * 3 + 1);
            }

            yield return current;
        }

        private const string alphabet = " abcdefghijklmnopqrstuvwxyz";
        public static int Cesar(string raw, string cypheredPart)
        {
            for (int shift = 1; shift <= 27; shift++)
            {
                var shiftedMask = GenerateShiftedMask(shift);

                var cyphred = Cypher(raw, shiftedMask);

                if (cyphred.Contains(cypheredPart))
                    return shift;
            }

            return -1;
        }

        private static string Cypher(string raw, Dictionary<char, char> shiftedMask)
        {
            var cypheredArray = raw.ToCharArray().Select(ch => shiftedMask[ch]).ToArray();
            return new string(cypheredArray);
        }

        private static Dictionary<char, char> GenerateShiftedMask(int shift)
        {
            return alphabet.ToCharArray()
                .Select((ch, idx) => new { ch, nch = alphabet[(idx + shift) % alphabet.Length] })
                .ToDictionary(item => item.ch, item => item.nch);
        }

        public static string Hanoi(int amount)
        {
            var steps  = SolveHanoi(amount, 1, 3, 2);

            return string.Join(",", steps);
        }

        private static IEnumerable<string> SolveHanoi(int amount, int start, int end, int middle)
        {
            if (amount == 1)
                return new[] { $"{start}-{end}" };

            return SolveHanoi(amount - 1, start, middle, end)
                .Concat(SolveHanoi(1, start, end, middle))
                .Concat(SolveHanoi(amount - 1, middle, end, start));
        }
    }
}
