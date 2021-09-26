using System.Linq;

namespace Facebook.Problems
{
    public class BalancedSplit
    {
        public static bool BalancedSplitExists(int[] arr)
        {
            long total = arr.Select(n => (long)n).Sum();
            if (total % 2 == 1) return false;

            var ascending = arr.OrderBy(n => n).ToArray();

            var upToSplit = 0L;
            var splitPosition = 0;

            while (splitPosition < ascending.Length)
            {
                upToSplit += ascending[splitPosition++];
                if (upToSplit == total - upToSplit)
                    break;
            }

            return splitPosition < ascending.Length
                && ascending[splitPosition - 1] != ascending[splitPosition];
        }
    }
}