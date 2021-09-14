namespace Facebook.Problems
{
    public class ContiguousSubarray
    {
        //is it possible to optimize given solution with a Trie or a Heap data structure?
        public static int[] CountSubarrays(int[] arr)
        {
            // Write your code here

            var subarrayAmount = new int[arr.Length]; //time: O(N^2); memory: O(N)
            for (int i = 0; i < arr.Length; i++)
            {
                subarrayAmount[i] = 1
                                    + LeftSubarrayAmount(arr, i)  //to make the overall algorithm faster
                                    + RightSubarrayAmount(arr, i);//make L/R sub array methods faster than O(N)
            }

            return subarrayAmount;
        }

        private static int LeftSubarrayAmount(int[] arr, int currentPosition)
        {
            for (int i = currentPosition - 1; i >= 0; i--)
            {
                if (arr[i] > arr[currentPosition])
                    return currentPosition - i - 1;
            }
            return currentPosition;
        }

        private static int RightSubarrayAmount(int[] arr, int currentPosition)
        {
            for (int i = currentPosition + 1; i < arr.Length; i++)
            {
                if (arr[i] > arr[currentPosition])
                    return i - currentPosition - 1;
            }
            return arr.Length - currentPosition - 1;
        }
    }
}