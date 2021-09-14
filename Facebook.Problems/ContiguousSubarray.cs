using System;
using System.Linq;

namespace Facebook.Problems
{
    public class ContiguousSubarray1
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

    //an attempt to make a solution a little bit faster
    //this solution is O(N*f(N))
    //where f(N) most probably ~log(N), but need to prove - some sort of random algo... not sure
    //took 40 more minutes to figure out and write down in VS2022
    public class ContiguousSubarray
    {
        private class ItemRange
        {
            public int Item { get; set; }
            public int Index { get; set; }
            public int Start { get; set; }
            public int End { get; set; }
            
            public int SubArrayCount => End - Start + 1;
        }

        public static int[] CountSubarrays(int[] arr)
        {
            var subarrayRanges = new ItemRange[arr.Length]; //time: O(N*f(N)); memory: O(N)
            for (int i = 0; i < arr.Length; i++)            //actual f(N) is on average....?
            {
                subarrayRanges[i] = new ItemRange()
                {
                    Index = i,
                    Item = arr[i],
                    Start = GetSubarrayStart(arr, i, subarrayRanges)
                };
            }

            for (int i = arr.Length - 1; i >= 0; i--)
            {
                subarrayRanges[i].End = GetSubarrayEnd(arr, i, subarrayRanges);
            }

            return subarrayRanges.Select(range => range.SubArrayCount).ToArray();
        }

        private static int GetSubarrayStart(int[] arr, int currentPosition, ItemRange[] subarrayAmount)
        {
            var start = currentPosition - 1;
            while (start >= 0)
            {
                if (subarrayAmount[start].Item > arr[currentPosition])
                    return start + 1;
                //7,4,5,6,3,2 <- numbers
                //0,1,2,3,4,5 <- indexes

                start = subarrayAmount[start].Start - 1;
            }
            return Math.Max(start, 0);
        }

        private static int GetSubarrayEnd(int[] arr, int currentPosition, ItemRange[] subarrayAmount)
        {
            var end = currentPosition + 1;
            while (end < arr.Length)
            {
                if (subarrayAmount[end].Item > arr[currentPosition])
                    return end - 1;
                //7,4,5,3,6,2 <- numbers
                //0,1,2,3,4,5 <- indexes

                end = subarrayAmount[end].End + 1;
            }
            return Math.Min(end, arr.Length-1);
        }
    }
}