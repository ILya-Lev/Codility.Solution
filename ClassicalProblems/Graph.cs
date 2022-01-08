using System.Text;

namespace ClassicalProblems;

public class Edge
{
    public int From { get; }
    public int To { get; }
    public Edge(int from, int to) => (From, To) = (from, to);
    public virtual Edge CreateReversed() => new(To, From);
    public override string ToString() => $"{From:D3} -> {To:D3}";
}

public class WeightedEdge : Edge, IComparable<WeightedEdge>
{
    public double Weight { get; }
    public WeightedEdge(int from, int to, double weight) : base(from, to) => Weight = weight;
    public int CompareTo(WeightedEdge other) => Weight.CompareTo(other.Weight);
    public override Edge CreateReversed() => new WeightedEdge(To, From, Weight);
    public override string ToString() => $"{From:D3} {Weight:N}> {To:D3}";
}

/// <summary>
/// 
/// </summary>
/// <typeparam name="V">type of Vertex</typeparam>
/// <typeparam name="E">type of Edge, should inherit class Edge</typeparam>
public abstract class Graph<V, E>
    where V : notnull
    where E : Edge
{
    private readonly List<V> _vertices;
    private readonly Dictionary<V, int> _indexByVertex;
    protected readonly List<List<E>> _edges;

    public int VertexCount => _vertices.Count;
    public int EdgeCount => _edges.Sum(e => e.Count);
    public V this[int index] => _vertices[index];

    protected Graph(IEnumerable<V> vertices)
    {
        _vertices = new List<V>(vertices);
        _indexByVertex = _vertices.Select((v, i) => (v, i)).ToDictionary(p => p.v, p => p.i);
        _edges = Enumerable.Repeat(1, _vertices.Count).Select(n => new List<E>()).ToList();
    }

    /// <summary> add new vertex and return its index in the list of vertices </summary>
    public int AddVertex(V vertex)
    {
        _vertices.Add(vertex);
        _indexByVertex.Add(vertex, _vertices.Count - 1);
        _edges.Add(new List<E>());
        return _vertices.Count - 1;
    }

    public int GetIndexOf(V vertex) => _indexByVertex[vertex];//O(1)

    public IReadOnlyList<V> GetNeighborsOf(int index) => _edges[index].Select(e => this[e.To]).ToArray();
    public IReadOnlyList<V> GetNeighborsOf(V vertex) => GetNeighborsOf(GetIndexOf(vertex));

    public IReadOnlyList<E> GetEdgesOf(int index) => _edges[index];
    public IReadOnlyList<E> GetEdgesOf(V vertex) => _edges[GetIndexOf(vertex)];

    public abstract void AddEdge(E edge);

    public override string ToString()
    {
        var sb = new StringBuilder();
        for (int i = 0; i < VertexCount; i++)
        {
            sb.Append(this[i]);
            sb.Append(" -> ");
            sb.Append(string.Join(", ", GetNeighborsOf(i).Select(n => n.ToString())));
            sb.AppendLine();
        }

        return sb.ToString();
    }
}

public class UnweightedGraph<V> : Graph<V, Edge>
{
    public UnweightedGraph(IEnumerable<V> vertices) : base(vertices) { }

    //as the graph is not oriented
    public override void AddEdge(Edge edge)
    {
        _edges[edge.From].Add(edge);
        _edges[edge.To].Add(edge.CreateReversed());
    }

    public void AddEdge(int from, int to) => AddEdge(new Edge(@from, to));
    public void AddEdge(V from, V to) => AddEdge(new Edge(GetIndexOf(@from), GetIndexOf(to)));

    public IReadOnlyCollection<V> FindShortestPath(V from, V to)
    {
        Func<V, bool> isFound = v => v.Equals(to);

        var (lastNode, _) = SearchNode<V>.BreadthFirstSearch(from, isFound, GetNeighborsOf);

        var path = SearchNode<V>.AsPath(lastNode);

        return path;
    }
}

public class WeightedGraph<V> : Graph<V, WeightedEdge> where V : notnull
{
    public WeightedGraph(IEnumerable<V> vertices) : base(vertices) { }

    public override void AddEdge(WeightedEdge edge)
    {
        _edges[edge.From].Add(edge);
        _edges[edge.To].Add(edge.CreateReversed() as WeightedEdge);
    }

    public void AddEdge(int from, int to, double weight) => AddEdge(new WeightedEdge(from, to, weight));

    public void AddEdge(V from, V to, double weight) => AddEdge(GetIndexOf(from), GetIndexOf(to), weight);

    public override string ToString()
    {
        var sb = new StringBuilder();
        for (int i = 0; i < VertexCount; i++)
        {
            sb.Append(this[i]);
            sb.Append(" -> ");
            sb.Append(string.Join(", ", GetEdgesOf(i).Select(e => $"({this[e.To]}, {e.Weight})")));
            sb.AppendLine();
        }

        return sb.ToString();
    }

    /// <summary>
    /// algorithm by Prim or Jarnik
    /// does not work with directed edges and unbounded graphs
    /// </summary>
    /// <returns></returns>
    public List<WeightedEdge> BuildMinimumSpanningTree()
    {
        var tree = new List<WeightedEdge>();

        var seen = new HashSet<int>();
        var superEdge = new PriorityQueue<WeightedEdge, double>();

        var seedIndex = new Random(DateTime.UtcNow.Millisecond).Next(0, VertexCount);

        seen.Add(seedIndex);
        superEdge.EnqueueRange(OutboundEdges(seedIndex));

        while (seen.Count < VertexCount)
        {
            var shortestEdge = superEdge.Dequeue();
            if (seen.Contains(shortestEdge.To)) continue;
            tree.Add(shortestEdge);
            seen.Add(shortestEdge.To);
            superEdge.EnqueueRange(OutboundEdges(shortestEdge.To));
        }

        return tree;

        IEnumerable<(WeightedEdge e, double Weight)> OutboundEdges(int vertexIndex) =>
            GetEdgesOf(vertexIndex)
            .Where(e => !seen.Contains(e.To))
            .Select(e => (e, e.Weight));
    }

    public string ConvertMstToString(IReadOnlyCollection<WeightedEdge> tree)
    {
        var sb = new StringBuilder();
        foreach (var edge in tree)
        {
            sb.AppendLine($"{this[edge.From]} {edge.Weight}> {this[edge.To]}");
        }

        sb.AppendLine($"Total Weight: {tree.Sum(e => e.Weight)}");
        return sb.ToString();
    }

    public DijkstraResult FindAllPaths(V origin)
    {
        int originIndex = GetIndexOf(origin);

        var distances = new double[VertexCount];
        distances[originIndex] = 0;

        var seen = new HashSet<int> { originIndex };

        var pointingEdgeByVertex = new Dictionary<int, WeightedEdge>();
        var queue = new PriorityQueue<DijkstraNode, double>();
        queue.Enqueue(new DijkstraNode(originIndex, 0), 0);

        while (queue.Count != 0)
        {
            var currentNode = queue.Dequeue();
            var currentVertex = currentNode.Vertex;
            var currentDistance = distances[currentVertex];

            foreach (var edge in GetEdgesOf(currentVertex))
            {
                var distanceTo = currentDistance + edge.Weight;
                if (!seen.Contains(edge.To) || distances[edge.To] > distanceTo)
                {
                    seen.Add(edge.To);
                    distances[edge.To] = distanceTo;

                    if (!pointingEdgeByVertex.ContainsKey(edge.To))
                        pointingEdgeByVertex.Add(edge.To, null!);
                    pointingEdgeByVertex[edge.To] = edge;
                    
                    queue.Enqueue(new DijkstraNode(edge.To, distanceTo), distanceTo);
                }
            }
        }

        return new DijkstraResult(distances, pointingEdgeByVertex);
    }

    public Dictionary<V, double> GetDistanceByVertex(double[] distance) => distance
        .Select((d, idx) => (v: this[idx], d))
        .ToDictionary(e => e.v, e => e.d);

    public List<WeightedEdge> GetPath(int start, int finish, Dictionary<int, WeightedEdge> pointingEdgeByVertex)
    {
        var path = new List<WeightedEdge>();
        for (int current = finish; current != start; )
        {
            var edge = pointingEdgeByVertex[current];
            path.Add(edge);
            current = edge.From;
        }

        path.Reverse();
        return path;
    }

    public sealed class DijkstraNode : IComparable<DijkstraNode>
    {
        public int Vertex { get; }
        public double Distance { get; }

        public DijkstraNode(int vertex, double distance) => (Vertex, Distance) = (vertex, distance);

        public void Deconstruct(out int vertex, out double distance) => (vertex, distance) = (Vertex, Distance);

        public int CompareTo(DijkstraNode? other) => Distance.CompareTo(other?.Distance ?? double.MaxValue);
    }

    public sealed class DijkstraResult
    {
        public double[] Distances { get; }
        public Dictionary<int, WeightedEdge> PointingEdgeByVertex { get; }

        public DijkstraResult(double[] distances, Dictionary<int, WeightedEdge> pointingEdgeByVertex)
        {
            Distances = distances;
            PointingEdgeByVertex = pointingEdgeByVertex;
        }
    }
}
