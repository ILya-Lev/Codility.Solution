using System.Collections.Generic;
using System.Linq;

namespace Codility.Solvers
{
    public class CoinChangingGreedy
    {
        public Dictionary<int, int> SplitScheme(int[] coins, int value)
        {
            var scheme = new Dictionary<int, int>();
            var sortedCoins = coins.OrderByDescending(c => c).ToArray();

            for (var i = 0; i < sortedCoins.Length && value > 0; ++i)
            {
                var coin = sortedCoins[i];
                if (value < coin)
                    continue;
                scheme.Add(coin, value / coin);
                value %= coin;
            }

            return scheme;
        }
    }
}
