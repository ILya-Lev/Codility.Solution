using System.Linq;

namespace Codility.Solvers
{
    public class NumberOfDiscIntersections
    {
        public int GetNumber(int[] radiuses)
        {
            var orderedRanges = ToOrderedRanges(radiuses);

            var intersectionCounter = 0;
            for (int i = 0; i < orderedRanges.Length; i++)
            {
                for (int j = i + 1; j < orderedRanges.Length && intersectionCounter <= 10_000_000; j++)
                {
                    if (orderedRanges[i].Intersects(orderedRanges[j]))
                        intersectionCounter++;
                    else
                        break;
                }
            }

            return intersectionCounter <= 10_000_000 ? intersectionCounter : -1;
        }

        private Range[] ToOrderedRanges(int[] radiuses)
        {
            return radiuses
                .Select((r, n) => new Range
                {
                    Start = (long)n - r,
                    End = (long)n + r
                })
                .OrderBy(r => r.Start)
                .ThenByDescending(r => r.End)
                .ToArray();
        }
    }

    internal class Range
    {
        public long Start { get; set; }
        public long End { get; set; }

        public bool Intersects(Range other) //works only if other is smaller!
        {
            return Start <= other.Start && other.Start <= End
                   || Start <= other.End && other.End <= End;
        }
    }
}
