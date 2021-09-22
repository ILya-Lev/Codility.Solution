using System;
using System.Collections.Generic;
using System.Linq;

namespace Algorithms.Solutions
{
    /// <summary>
    /// min path (by length of each edge) between the two given nodes
    /// </summary>
    public class DijkstraShortestPath
    {
        public class Vertex
        {
            public int Data { get; }

            public List<Edge> Edges { get; } = new List<Edge>();

            public Vertex(int data) => Data = data;
        }

        public class Edge : IComparable<Edge>
        {
            public int Length { get; }
            public Vertex End { get; set; }

            public int EffectiveLength => Length + (Preceding?.EffectiveLength ?? 0);
            public Edge Preceding { get; set; }

            public Edge(int length) => Length = length;

            public int CompareTo(Edge other) => EffectiveLength.CompareTo(other.EffectiveLength);
        }

        /// <summary>
        /// not correct so far... think it over!
        /// </summary>
        /// <param name="start">vertex to start the path from</param>
        /// <param name="end">vertex to finish the path at</param>
        /// <returns>vertex and a length of an edge pointing to it which is in the path</returns>
        public static List<(Vertex, int)> FindShortestPath(Vertex start, Vertex end)
        {
            var lastPathEdge = FindLastPathEdge(start, end) ?? throw new Exception($"Path cannot be found");

            return RestorePath(start, lastPathEdge);
        }

        private static Edge? FindLastPathEdge(Vertex start, Vertex end)
        {
            var superVertex = new HashSet<Vertex> { start };
            var boundaryEdges = MinHeap<Edge>.Heapify(start.Edges);

            while (!superVertex.Contains(end))
            {
                var shortestEdge = boundaryEdges.Extract();//effectively shortest
                if (shortestEdge.End == end)
                    return shortestEdge;
                superVertex.Add(shortestEdge.End);

                foreach (var edge in shortestEdge.End.Edges.Where(e => !superVertex.Contains(e.End)))
                {
                    edge.Preceding = shortestEdge;
                    boundaryEdges.Insert(edge);
                }
            }

            return null;
        }

        private static List<(Vertex, int)> RestorePath(Vertex start, Edge? lastPathEdge)
        {
            var path = new List<(Vertex, int)>();
            for (var edge = lastPathEdge; edge != null; edge = edge.Preceding)
            {
                path.Add((edge.End, edge.Length));
            }

            path.Add((start, 0));
            path.Reverse();
            return path;
        }
    }
}