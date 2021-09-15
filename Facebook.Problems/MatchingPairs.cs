using System;
using System.Collections.Generic;

namespace Facebook.Problems
{
    /*
        cases
        1 - 0 diff => cab -> cbc => when all symbols unique return N-2, if at least one symbol is duplicated return N
        2 - 1 diff => if there is a duplicate return N-1, if not N-2
        3 - 2 diff => if swapping the 2 diffs fixes everything, return N, if there are duplicates return N-2
        4 - 2+diff => try to find a pair (among diff) swapping which leads to matching both items
                      otherwise try to find a pair (among diff) swapping which leads to matching 1 item
        */

    public class MatchingPairs
    {
        public static int GetNumberOfMatchingPairs(string s, string t)
        {
            var differences = FindDifferences(s, t);                          //O(N)

            switch (differences.Count)
            {
                case 0: return HasDuplicates(s) ? s.Length : s.Length - 2;    //O(N)
                case 1: return HasDuplicates(s) ? s.Length - 1 : s.Length - 2;//O(N)
                default:                                                      // 
                    var matchingInitially = s.Length - differences.Count;
                    var matchingAfterSwap = TheBestSwapEffect(s, t, differences);//in {0,1,2};
                    return matchingInitially + matchingAfterSwap;
            }
        }

        private static IReadOnlyList<int> FindDifferences(string s, string t)
        {
            var differences = new List<int>();
            for (int i = 0; i < s.Length; i++)
            {
                if (s[i] != t[i])
                {
                    differences.Add(i);
                }
            }
            return differences;
        }

        private static bool HasDuplicates(string s)
        {
            var set = new HashSet<char>();
            for (int i = 0; i < s.Length; i++)
            {
                if (set.Contains(s[i])) return true;
                set.Add(s[i]);
            }
            return false;
        }

        //O(N) time and O(N) space
        private static int TheBestSwapEffect(string s, string t, IReadOnlyList<int> differences)
        {
            var tDiffPopulation = new Dictionary<char, int>();
            foreach (var diff in differences)
            {
                if (!tDiffPopulation.ContainsKey(t[diff]))
                    tDiffPopulation.Add(t[diff], 0);
                tDiffPopulation[t[diff]]++;
            }

            var positiveEffect = 0;
            foreach (var diff in differences)
            {
                if (tDiffPopulation.TryGetValue(s[diff], out var population))
                {
                    if (population > 0)
                        positiveEffect++;
                    tDiffPopulation[s[diff]]--;
                }

                if (positiveEffect == 2) return 2;
            }

            return positiveEffect;
        }

        //O(N^2) time; O(1) space
        private static int TheBestSwapEffectNaive(string s, string t, IReadOnlyList<int> differences)
        {
            var matching = 0;

            for (int i = 0; i < differences.Count; i++)
                for (int j = i + 1; j < differences.Count; j++)
                {
                    var currentMatching =
                      (s[differences[i]] == t[differences[j]] ? 1 : 0)
                    +
                      (s[differences[j]] == t[differences[i]] ? 1 : 0);
                    
                    matching = Math.Max(matching, currentMatching);
                    
                    if (matching == 2)
                        return 2;
                }

            return matching;
        }
    }
}