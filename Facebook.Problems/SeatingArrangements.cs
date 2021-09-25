using System;
using System.Linq;

namespace Facebook.Problems
{
    public class SeatingArrangements
    {
        public static int MinOverallAwkwardness(int[] arr)
        {
            if (arr is null || arr.Length < 3)
            {
                throw new Exception($"Array must contain at least 3 items." +
                                    $" {arr?.Length ?? 0} is provided");
            }

            var desc = arr.OrderByDescending(n => n).ToArray();

            var maxAwkwardness = Math.Max(desc[0] - desc[1], desc[^2] - desc[^1]);

            for (var i = 0; i + 2 < desc.Length; i++)
            {
                maxAwkwardness = Math.Max(maxAwkwardness, desc[i] - desc[i + 2]);
            }

            return maxAwkwardness;
        }
    }
}