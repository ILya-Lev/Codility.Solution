using System.Collections.Generic;
using System.Linq;

namespace Facebook.Problems
{
    /// <summary>
    /// preparation to the screening; 2021-10-05 before the actual tasks
    /// this is problem 2
    /// </summary>
    public class AboveAverageSubarrays
    {
        public class Subarray
        {
            public int L { get; }
            public int R { get; }

            public Subarray(int l, int r) => (L, R) = (l, r);
        }

        //public static Subarray[] FindAboveAverageSubarrays(int[] A)
        //{
        //    var total = A.Sum();  //O(N)
        //    var subarrays = new List<Subarray>();

        //    //O() <--- T(N) = 2*T(N-1) or const
        //    DoFind(A, total, 0, subarrays, 0, A.Length - 1);

        //    //O(N*LogN)
        //    return subarrays.OrderBy(s => s.L).ThenBy(s => s.R).ToArray();
        //}

        //private static readonly HashSet<(int, int)> _seenSubarrays = new HashSet<(int, int)>();

        //private static void DoFind(int[] A, int total, int extracted, List<Subarray> subarrays, int l, int r)
        //{
        //    if (l > r) return;

        //    if (_seenSubarrays.Contains((l, r))) return;

        //    var currentLen = r - l + 1;
        //    var currentAver = total * 1.0 / currentLen;

        //    var remLen = A.Length - currentLen;
        //    var extractedAver = remLen == 0 ? 0 : extracted * 1.0 / remLen;

        //    if (currentAver > extractedAver)
        //    {
        //        subarrays.Add(new Subarray(l, r));
        //        _seenSubarrays.Add((l, r));

        //        DoFind(A, total - A[l], extracted + A[l], subarrays, l + 1, r);
        //        DoFind(A, total - A[r], extracted + A[r], subarrays, l, r - 1);
        //    }
        //}


        public static (int L, int R)[] FindAboveAverageSubarrays(int[] A)
        {
            var total = A.Sum();  //O(N)
            var subarrays = new HashSet<(int L, int R)>();

            //O() <--- T(N) = 2*T(N-1) or const
            var stack = new Stack<(int L, int R, int inside, int outside)>();
            stack.Push((0, A.Length - 1, total, 0));
            while (stack.Any())
            {
                var (l, r, inside, outside) = stack.Pop();
                if (l > r) continue;

                if (subarrays.Contains((l, r))) continue;

                var currentLen = r - l + 1;
                var currentAver = inside * 1.0 / currentLen;

                var remLen = A.Length - currentLen;
                var extractedAver = remLen == 0 ? 0 : outside * 1.0 / remLen;

                if (currentAver > extractedAver)
                {
                    subarrays.Add((l, r));

                    stack.Push((l + 1, r, inside - A[l], outside + A[l]));
                    stack.Push((l, r - 1, inside - A[r], outside + A[r]));
                }
            }

            //O(N*LogN)
            return subarrays.OrderBy(s => s.L).ThenBy(s => s.R).ToArray();
        }
    }
}
