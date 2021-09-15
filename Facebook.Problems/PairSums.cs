using System.Collections.Generic;
using System.Linq;

namespace Facebook.Problems
{
    public class PairSums
    {
        public static int NumberOfWays(int[] arr, int k)
        {
            var totalWays = 0;

            //O(N) time; O(N) space
            var frequencyByNumber = arr.GroupBy(n => n, n => n).ToDictionary(g => g.Key, g => g.Count());
            var alreadySeen = new HashSet<int>();

            foreach (var item in frequencyByNumber)    //O(N)
            {
                var counterparty = k - item.Key;

                if (counterparty == item.Key)
                // number of pairs in a set of n identical numbers => n!/2!/(n-2)! => n*(n-1)/2
                {
                    totalWays += item.Value * (item.Value - 1) / 2;
                    continue;
                }

                if (alreadySeen.Contains(counterparty))
                    continue;

                //this search is O(1)
                if (!frequencyByNumber.TryGetValue(counterparty, out var counterpartyPopularity))
                    continue;

                totalWays += item.Value * counterpartyPopularity;
                alreadySeen.Add(counterparty);
                alreadySeen.Add(item.Key);
            }

            return totalWays;
        }
    }
}