using System;
using System.Collections.Generic;
using System.Linq;

namespace Algorithms.Solutions
{
    public class PrimMinimumSpanningTree
    {
        public class Vertex<T>
        {
            public T Data { get; }
            public List<Edge<T>> Edges { get; } = new List<Edge<T>>();

            public Vertex(T data) => Data = data;
        }

        public class Edge<T> : IComparable<Edge<T>>
        {
            public int Length { get; }
            public Vertex<T> End { get; set; }

            public Edge(int length) => Length = length;
            
            public int CompareTo(Edge<T> other) => Length.CompareTo(other.Length);
        }

        public class SpanningTree<T>
        {
            private readonly HashSet<Vertex<T>> _vertices = new HashSet<Vertex<T>>();
            private readonly List<Edge<T>> _edges = new List<Edge<T>>();

            public IReadOnlyCollection<Vertex<T>> Vertices => _vertices;
            public IReadOnlyCollection<Edge<T>> Edges => _edges;

            public bool ContainsVertex(Vertex<T> vertex) => _vertices.Contains(vertex);
            public void AddVertex(Vertex<T> vertex) => _vertices.Add(vertex);

            public void AddEdge(Edge<T> edge) => _edges.Add(edge);
        }

        public static SpanningTree<T> DiscoverMst<T>(Vertex<T> seed)
        {
            var tree = new SpanningTree<T>();
            tree.AddVertex(seed);

            var externalEdges = MinHeap<Edge<T>>.Heapify(seed.Edges);

            while (externalEdges.Count > 0)
            {
                var shortestEdge = externalEdges.Extract();
                
                var nextMember = shortestEdge.End;
                if (tree.ContainsVertex(nextMember))
                    continue;

                tree.AddVertex(nextMember);
                tree.AddEdge(shortestEdge);

                foreach (var edge in nextMember.Edges.Where(e => !tree.ContainsVertex(e.End)))
                {
                    externalEdges.Insert(edge);
                }
            }

            return tree;
        }
    }
}