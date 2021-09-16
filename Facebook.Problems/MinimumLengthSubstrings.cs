using System;
using System.Collections.Generic;
using System.Linq;

namespace Facebook.Problems
{
    public class MinimumLengthSubstrings
    {
        public static int MinLengthSubstring(string s, string t)
        {
            // Write your code here
            var frequencyTable = ComposeFrequencyTable(s, t).ToArray();
            if (frequencyTable.Length != t.Length) return -1;

            var rangeWidth = FindTheMostNarrowRange(frequencyTable);

            return rangeWidth;
        }

        private static IEnumerable<List<int>> ComposeFrequencyTable(string s, string t)
        {
            var sFrequencies = s
              .Select((c, idx) => (c, idx))
              .GroupBy(e => e.c, e => e.idx)
              .ToDictionary(g => g.Key, g => g.ToList());

            foreach (var c in t)
            {
                if (!sFrequencies.ContainsKey(c))
                    yield break;
                yield return sFrequencies[c];
            }
        }

        private static int FindTheMostNarrowRange(List<int>[] ft)
        {
            var minWidth = int.MaxValue;
            var allIndexes = ft.SelectMany(c => c).OrderBy(idx => idx).ToArray();//O(N*lgN)

            foreach (var firstIndex in allIndexes)
            {//O(N^3/2*lgN*lgN)
                int min = firstIndex, max = firstIndex;

                var columnToIgnore = FindColumnToIgnore(ft, firstIndex);

                for (int i = 0; i < ft.Length; i++)
                {
                    if (i == columnToIgnore)
                        continue;

                    var closest = FindTheClosest(firstIndex, ft[i]);
                    min = Math.Min(min, closest);
                    max = Math.Max(max, closest);
                }

                minWidth = Math.Min(minWidth, max - min + 1);
            }

            return minWidth;
        }

        //O(N^1/2*lgN)
        private static int FindColumnToIgnore(List<int>[] ft, int firstIndex)
        {
            for (int i = 0; i < ft.Length; i++)
            {
                if (ft[i].BinarySearch(firstIndex) >= 0)
                    return i;
            }
            throw new Exception($"Cannot find a column with {firstIndex} in it.");
        }

        private static int FindTheClosest(int seed, List<int> indexes)
        {
            var minDistance = int.MaxValue;
            foreach (var idx in indexes)
            {
                var distance = idx - seed;
                if (distance == 0)
                    continue;
                minDistance = Math.Min(minDistance, distance);
            }
            return minDistance + seed;
        }
    }
}