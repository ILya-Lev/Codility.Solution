using System;
using System.Collections.Generic;

namespace Algorithms.Solutions
{
    /// <summary>
    /// min path (by length of each edge) between the two given nodes
    /// </summary>
    public class DijkstraShortestPath
    {
        public class Vertex
        {
            public int Data { get; set; }
            
            public List<Edge> Edges { get; } = new List<Edge>();
        }

        public class Edge : IComparable<Edge>
        {
            public int Length { get; set; }
            public Vertex End { get; set; }

            public int CompareTo(Edge other) => Length.CompareTo(other.Length);
        }

        /// <summary>
        /// not correct so far... think it over!
        /// </summary>
        /// <param name="start">vertex to start the path from</param>
        /// <param name="end">vertex to finish the path at</param>
        /// <returns>vertex and a length of an edge pointing to it which is in the path</returns>
        public Dictionary<Vertex, int> FindShortestPath(Vertex start, Vertex end)
        {
            var path = new Dictionary<Vertex, int> { { start, 0 } };

            var superVertex = new HashSet<Vertex> { start };
            var boundaryEdges = MinHeap<Edge>.Heapify(start.Edges);

            while (!path.ContainsKey(end))
            {
                var shortestExternal = boundaryEdges.Extract();
                if (!superVertex.Contains(shortestExternal.End))
                {
                    path.Add(shortestExternal.End, shortestExternal.Length);
                    superVertex.Add(shortestExternal.End);
                    foreach (var edge in shortestExternal.End.Edges)
                    {
                        if (!superVertex.Contains(edge.End))
                            boundaryEdges.Insert(edge);
                    }
                }
            }
            return path;
        }
    }
}