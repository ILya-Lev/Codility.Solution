using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Codility.Solvers
{
    public class MaxNonOverlappingSegments
    {
        public int GetLargestSize(int[] heads, int[] tails)
        {
            ValidateInput(heads, tails);
            var segments = ParseToSegments(heads, tails)
                //.OrderBy(s => s.Length)
                //.ThenBy(s => s.Head)
                //.ThenBy(s => s.Tail)
                .ToArray();

            var shorterNonOverlappingSet = GetNonOverlappingSet(segments.OrderBy(s => s.Length));
            var longerNonOverlappingSet = GetNonOverlappingSet(segments.OrderByDescending(s => s.Length));
            //var maxWithReplacement = ReplaceLongWithShortSequential(longerNonOverlappingSet, shorterNonOverlappingSet);
            //var maxWithReplacement = ReplaceLongWithShort(longerNonOverlappingSet, shorterNonOverlappingSet);
            //var extremeMax = Math.Max(shorterNonOverlappingSet.Count, longerNonOverlappingSet.Count);
            return Math.Max(shorterNonOverlappingSet.Count, longerNonOverlappingSet.Count);
            //return Math.Max(extremeMax, maxWithReplacement);
        }

        private int ReplaceLongWithShort(List<Segment> longer, List<Segment> shorter)
        {
            var sizes = new ConcurrentBag<int>();
            Parallel.For(1, longer.Count, (i) =>
            {
                var size = shorter.Count(s => longer.Skip(i).All(ls => !ls.Intersects(s))) + longer.Count - i;
                sizes.Add(size);
            });
            
            return sizes.Count > 0 ? sizes.Max() : 0;
        }
        
        private int ReplaceLongWithShortSequential(List<Segment> longer, List<Segment> shorter)
        {
            var sizes = new ConcurrentBag<int>();
            for(int i =1; i < longer.Count; i++)
            {
                var size = shorter.Count(s => longer.Skip(i).All(ls => !ls.Intersects(s))) + longer.Count - i;
                sizes.Add(size);
            };
            
            return sizes.Count > 0 ? sizes.Max() : 0;
        }

        private List<Segment> GetNonOverlappingSet(IOrderedEnumerable<Segment> segments)
        {
            var set = new List<Segment>();
            foreach (var segment in segments)
            {
                if (set.Any(s => s.Intersects(segment)))
                    continue;
                set.Add(segment);
            }

            return set;
        }

        private void ValidateInput(int[] heads, int[] tails)
        {
            if (heads == null || tails == null)
                throw new Exception("Either heads or tails is null!");

            if (heads.Length != tails.Length)
            {
                throw new Exception($"Heads should contain the same amount of item as tails, but provided {heads.Length} != {tails.Length}.");
            }

            var containNegativeLength = heads.Zip(tails, (h, t) => t - h).Any(length => length < 0);
            if (containNegativeLength)
                throw new Exception("Some segment has negative length!");

            var containDecreasingTail = tails.Take(tails.Length - 1)
                .Zip(tails.Skip(1), (t1, t2) => t2 - t1)
                .Any(distance => distance < 0);
            if (containDecreasingTail)
                throw new Exception("There is a pair of segments with decreasing tail value.");
        }

        private IEnumerable<Segment> ParseToSegments(int[] heads, int[] tails)
        {
            for (int i = 0; i < heads.Length; i++)
            {
                yield return new Segment(heads[i], tails[i]);
            }
        }
    }

    //public class MaxNonoverlappingSegments
    //{
    //    public int GetLargestSize(int[] heads, int[] tails)
    //    {
    //        var segments = ConvertToSegments(heads, tails).ToArray();
    //        var notCoveredArea = FindNotCoveredArea(segments);
    //    }

    //    private IEnumerable<Segment> ConvertToSegments(int[] heads, int[] tails)
    //    {
    //        if (heads.Length != tails.Length)
    //        {
    //            throw new Exception($"Heads should contain the same amount of elements as tails, but {heads.Length} is not equal to {tails.Length}.");
    //        }

    //        for (int i = 0; i < heads.Length; i++)
    //        {
    //            yield return new Segment(heads[i], tails[i]);
    //        }
    //    }

    //    private IEnumerable<int> FindNotCoveredArea(Segment[] segments)
    //    {
    //        var wholeRange = Enumerable.Range(segments.First().Tail + 1, segments.Last().Head - segments.First().Tail).ToArray();
    //        for (int i = 0; i < segments.Length; i++)
    //        {
    //            yield break ;
    //        }
    //    }
    //}

    internal class Segment
    {
        public int Head { get; }
        public int Length { get; }
        public int Tail => Head + Length;

        public Segment(int head, int tail)
        {
            Head = head;
            Length = tail - head;
        }

        public bool Intersects(Segment other) => Contains(other.Head) || Contains(other.Tail) || other.Contains(Head);

        private bool Contains(int point) => Head <= point && point <= Tail;
    }
}
