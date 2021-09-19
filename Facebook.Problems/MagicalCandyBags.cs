using Algorithms.Solutions;

namespace Facebook.Problems
{
    /*
     You have N bags of candy. The ith bag contains arr[i] pieces of candy, and each of the bags is magical!
It takes you 1 minute to eat all of the pieces of candy in a bag (irrespective of how many pieces of candy are inside), and as soon as you finish, the bag mysteriously refills. If there were x pieces of candy in the bag at the beginning of the minute, then after you've finished you'll find that floor(x/2) pieces are now inside.
You have k minutes to eat as much candy as possible. How many pieces of candy can you eat?
     */
    public class MagicalCandyBags
    {
        /// <summary>
        /// heapify => O(N), where N is arr.Length
        /// loop => [k or sum(ceiling log by 2 of arr[i])] * log N => O(N*logN)
        /// overall time complexity is O(N*logN)
        /// space O(N)
        /// </summary>
        public static int MaxCandies(int[] arr, int k)
        {
            var maxHeap = MaxHeap<int>.Heapify(arr);
            var remainingTime = k;
            var total = 0;
            while (remainingTime-- > 0 && maxHeap.Count > 0)
            {
                total += maxHeap.Head;

                var refilled = maxHeap.Extract() / 2;
                if (refilled > 0)
                    maxHeap.Insert(refilled);
            }

            return total;
        }
    }
}