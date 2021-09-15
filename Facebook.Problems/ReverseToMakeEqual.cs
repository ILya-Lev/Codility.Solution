using System.Linq;

namespace Facebook.Problems
{
    public class ReverseToMakeEqual
    {
        public static bool AreTheyEqual(int[] arr_a, int[] arr_b)
        {
            //reversing any subarray any number of times
            //means one can swap 2 consecutive items of a given array as many times as he wants
            //therefore both A and B could be converted into sorted order (from the smallest to the largest)
            //threfore the function just have to check if A and B containt the same values the same amount of times

            var aFrequency = arr_a.GroupBy(n => n).ToDictionary(g => g.Key, g => g.Count());
            foreach (var n in arr_b)
            {
                if (!aFrequency.ContainsKey(n)) return false;

                aFrequency[n]--;

                if (aFrequency[n] < 0) return false;
            }

            return true;
        }
    }
}