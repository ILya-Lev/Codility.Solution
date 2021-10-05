using System.Collections.Generic;

namespace Facebook.Problems
{
    /// <summary>
    /// preparation to the screening; 2021-10-05 before the actual tasks
    /// this is problem 1
    ///
    /// initially array contains only false values (N of them)
    /// you're given a set of queries (either 1 - SET, or 2 - GET)
    /// with an index
    /// SET turns false value into the true one
    /// GET returns next (on the right) true value, or at the same position, or -1 if no true on the right is found
    ///
    /// ideally we need a search tree
    ///
    /// to model the required behavior:
    /// 1. store in list indexes of SET operation
    /// 2. sort the list
    /// 3. binary search for index in GET operation
    /// 4. if found, return, if above the list, return -1
    /// </summary>
    public class PerformQueries
    {
        public static int[] ExecuteQueries((int q, int i)[] queries, int n)
        {
            var results = new List<int>();
            var trueValues = new List<int>(n);
            foreach (var query in queries)
            {
                if (query.q == 1) //SET
                {
                    trueValues.Add(query.i);
                    trueValues.Sort();              //due to this time complexity is O(q*n * log n)
                }
                else
                {
                    var found = trueValues.BinarySearch(query.i);       //due to this O(q * log n)

                    if (found >= 0 && found < trueValues.Count)
                    {
                        results.Add(trueValues[found]);
                    }
                    else
                    {
                        found = ~found;
                        if (found < trueValues.Count)
                            results.Add(trueValues[found]);
                        else
                            results.Add(-1);
                    }
                }
            }

            return results.ToArray();
        }
    }
}
