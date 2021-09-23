using System.Linq;

namespace Facebook.Problems
{
    public class ElementSwapping_Wrong
    {
        private static int[] FindMinArray(int[] arr, int k)
        {
            var swapsRemaining = k;
            var ascending = arr.OrderBy(n => n).ToArray();

            for (int finalPosition = 0; finalPosition < ascending.Length && swapsRemaining > 0; finalPosition++)
            {

                var currentPosition = FindNextSmallestItemPosition(arr, ascending[finalPosition]);


                while (currentPosition > finalPosition && swapsRemaining > 0)
                {
                    Swap(arr, currentPosition, currentPosition - 1);
                    swapsRemaining--;
                }
            }

            return arr;
        }

        private static int FindNextSmallestItemPosition(int[] arr, int item)
        {
            int i = 0;
            while (arr[i] != item)
                i++;
            return i;
        }

        private static void Swap(int[] arr, int lhs, int rhs) => (arr[lhs], arr[rhs]) = (arr[rhs], arr[lhs]);
    }

    /*
     * Element Swapping
Given a sequence of n integers arr, determine the lexicographically smallest sequence which may be obtained from it after performing at most k element swaps, each involving a pair of consecutive elements in the sequence.
Note: A list x is lexicographically smaller than a different equal-length list y if and only if, for the earliest index at which the two lists differ, x's element at that index is smaller than y's element at that index.
Signature
int[] findMinArray(int[] arr, int k)
Input
n is in the range [1, 1000].
Each element of arr is in the range [1, 1,000,000].
k is in the range [1, 1000].
Output
Return an array of n integers output, the lexicographically smallest sequence achievable after at most k swaps.


Example 1
n = 3
k = 2
arr = [5, 3, 1]
output = [1, 5, 3]
We can swap the 2nd and 3rd elements, followed by the 1st and 2nd elements, to end up with the sequence [1, 5, 3]. This is the lexicographically smallest sequence achievable after at most 2 swaps.


Example 2
n = 5
k = 3
arr = [8, 9, 11, 2, 1]
output = [2, 8, 9, 11, 1]
We can swap [11, 2], followed by [9, 2], then [8, 2].
     */

    public class ElementSwapping
    {
        public static int[] FindMinArray(int[] arr, int k)
        {
            var swapsRemaining = k;
            var ascending = arr.OrderBy(n => n).ToArray();

            var start = 0;
            while (swapsRemaining > 0 && start < arr.Length)
            {

                var positionByValue = arr
                    .Select((n, idx) => (n, idx))
                    .ToDictionary(item => item.Item1, item => item.Item2);

                for (int smallestNumberShift = 0; start + smallestNumberShift < arr.Length; smallestNumberShift++)
                {

                    var unmovedSmallestNumber = ascending[start + smallestNumberShift];
                    var swapsToPutIntoStart = positionByValue[unmovedSmallestNumber] - start;

                    if (swapsRemaining >= swapsToPutIntoStart)
                    {
                        swapsRemaining -= swapsToPutIntoStart;
                        MoveToStart(arr, positionByValue[unmovedSmallestNumber], start);
                        start++;
                        break;
                    }
                }

            }

            return arr;
        }

        private static void MoveToStart(int[] arr, int current, int start)
        {
            for (int i = current; i > start; i--)
                Swap(arr, i, i-1);
        }

        private static void Swap(int[] arr, int lhs, int rhs) => (arr[lhs], arr[rhs]) = (arr[rhs], arr[lhs]);
    }
}