using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MoreLinq;

namespace Codility.Solvers
{
    public class MaxNonOverlappingSegments
    {
        public int GetLargestSize(int[] heads, int[] tails)
        {
            ValidateInput(heads, tails);
            var segments = ParseToSegments(heads, tails)
                .ToArray();

            var segmentWithIntersected = CalculateIntersections(segments);
            var biggestNoIntersectSubset = LeaveLessIntersectedItems(segmentWithIntersected).ToArray();

            return biggestNoIntersectSubset.Length;
        }

        private IEnumerable<Segment> LeaveLessIntersectedItems(Dictionary<Segment, HashSet<Segment>> segmentWithIntersected)
        {
            while (segmentWithIntersected.Count > 0)
            {
                var lessIntersected = GetLessIntersectedSegment(segmentWithIntersected);
                yield return lessIntersected;

                var itsIntersectings = segmentWithIntersected[lessIntersected];
                foreach (var itsIntersecting in itsIntersectings)
                {
                    //var subIntersectings = segmentWithIntersected[itsIntersecting];
                    //foreach (var subIntersecting in subIntersectings.Where(i => i != itsIntersecting))
                    //{
                    //    segmentWithIntersected[subIntersecting].Remove(itsIntersecting);
                    //}
                    segmentWithIntersected.Remove(itsIntersecting);
                }
                segmentWithIntersected.Remove(lessIntersected);
            }
        }

        private Segment GetLessIntersectedSegment(Dictionary<Segment, HashSet<Segment>> segmentWithIntersected)
        {
            var min = int.MaxValue;
            Segment lessIntersectedSegment = null;
            foreach (var item in segmentWithIntersected.Select(p => new{p.Key, p.Value.Count}))
            {
                if (min > item.Count)
                {
                    min = item.Count;
                    lessIntersectedSegment = item.Key;
                }
            }
            return lessIntersectedSegment;
        }

        private Dictionary<Segment, HashSet<Segment>> CalculateIntersections(Segment[] segments)
        {
            var intersections = new Dictionary<Segment, HashSet<Segment>>();
            for (int i = 0; i < segments.Length; i++)
            {
                var current = segments[i];
                intersections.Add(current, new HashSet<Segment>());
                //as segments are sorted by tail_i <= tail_i+1
                for (int j = i + 1; j < segments.Length && current.Tail >= segments[j].Head; j++)
                {
                    intersections[current].Add(segments[j]);
                }
            }

            foreach (var segmentWithIntersections in intersections)
            {
                var containing = segmentWithIntersections.Key;
                foreach (var segment in segmentWithIntersections.Value)
                {
                    if (!intersections[segment].Contains(containing))
                        intersections[segment].Add(containing);
                }
            }

            return intersections;
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

    [DebuggerDisplay("{Id}")]
    internal class Segment
    {
        public int Head { get; }
        public int Length { get; }
        public int Tail => Head + Length;
        public int Id { get; }

        private static int _instance = 0;
        public Segment(int head, int tail)
        {
            Head = head;
            Length = tail - head;
            Id = ++_instance;
        }

        public bool Intersects(Segment other) => Contains(other.Head) || Contains(other.Tail) || other.Contains(Head);

        private bool Contains(int point) => Head <= point && point <= Tail;
    }
}
