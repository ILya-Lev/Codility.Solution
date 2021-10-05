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

        public static Subarray[] FindAboveAverageSubarrays(int[] A)
        {
            var total = A.Sum();  //O(N)
            var subarrays = new List<Subarray>();

            //O() <--- T(N) = 2*T(N-1) or const
            DoFind(A, total, 0, subarrays, 0, A.Length - 1);

            //O(N*LogN)
            return subarrays.OrderBy(s => s.L).ThenBy(s => s.R).ToArray();
        }

        private static void DoFind(int[] A, int total, int extracted, List<Subarray> subarrays, int l, int r)
        {
            if (l > r) return;

            var currentLen = r - l + 1;
            var currentAver = total * 1.0 / currentLen;

            var remLen = A.Length - currentLen;
            var extractedAver = remLen == 0 ? 0 : extracted * 1.0 / remLen;

            if (currentAver > extractedAver)
            {
                subarrays.Add(new Subarray(l, r));
                DoFind(A, total - A[l], extracted + A[l], subarrays, l + 1, r);
                DoFind(A, total - A[r], extracted + A[r], subarrays, l, r - 1);
            }
        }
    }
}
