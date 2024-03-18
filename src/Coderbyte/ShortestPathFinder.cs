namespace Coderbyte;
/*
 Shortest Path
   Have the function ShortestPath(strArr) take strArr which will be an array of strings which models a non-looping Graph. The structure of the array will be as follows: The first element in the array will be the number of nodes N (points) in the array as a string. The next N elements will be the nodes which can be anything (A, B, C .. Brick Street, Main Street .. etc.). Then after the Nth element, the rest of the elements in the array will be the connections between all of the nodes. They will look like this: (A-B, B-C .. Brick Street-Main Street .. etc.). Although, there may exist no connections at all.
   
   An example of strArr may be: ["4","A","B","C","D","A-B","B-D","B-C","C-D"]. Your program should return the shortest path from the first Node to the last Node in the array separated by dashes. So in the example above the output should be A-B-D. Here is another example with strArr being ["7","A","B","C","D","E","F","G","A-B","A-E","B-C","C-D","D-F","E-D","F-G"]. The output for this array should be A-E-D-F-G. There will only ever be one shortest path for the array. If no path between the first and last node exists, return -1. The array will at minimum have two nodes. Also, the connection A-B for example, means that A can get to B and B can get to A.
*/

public class ShortestPathFinder
{
    public static string FindPath(string[] input)
    {
        var n = int.Parse(input[0]);
        var nodes = input.Skip(1).Take(n).ToDictionary(s => s, s => new Node(s));
        AssignLinks(nodes, input.Skip(n + 1));

        var start = nodes.First().Value;
        var end = nodes.Last().Value;

        #region Dijkstra algorithm for each edge having the same length
        var steps = GetSteps(start);
        var path = GetPath(end, start, steps);
        #endregion Dijkstra algorithm for each edge having the same length

        if (path.Count == 0) return "-1";
        return string.Join("-", path.Select(p => p.Name));
    }

    private static void AssignLinks(Dictionary<string, Node> nodes, IEnumerable<string> links)
    {
        foreach (var link in links)
        {
            var parts = link.Split('-');
            var lhs = nodes[parts[0]];
            var rhs = nodes[parts[1]];

            lhs.Next.Add(rhs);
            rhs.Next.Add(lhs);
        }
    }

    private static Dictionary<Node, Node> GetSteps(Node start)
    {
        var steps = new Dictionary<Node, Node>();//destination node is a key, origin node is a value

        var distances = new Dictionary<Node, int> { [start] = 0 };
        var frontier = new PriorityQueue<Node, int>();
        frontier.Enqueue(start, 0);

        while (frontier.Count > 0)
        {
            var current = frontier.Dequeue();
            var nextDistance = distances[current] + 1;
            foreach (var child in current.Next)
            {
                if (distances.TryGetValue(child, out var oldDistance) && oldDistance <= nextDistance)
                    continue;

                distances[child] = nextDistance;
                steps[child] = current;
                frontier.Enqueue(child, nextDistance);
            }
        }

        return steps;
    }

    private static List<Node> GetPath(Node end, Node start, Dictionary<Node, Node> steps)
    {
        var current = end;
        
        var path = new List<Node> { current };
        while (current != start)
        {
            if (!steps.TryGetValue(current, out var next))
                return new List<Node>();//path cannot be found - disconnected graph

            path.Add(next);
            current = next;
        }
        path.Reverse();
        return path;
    }

    private record Node(string Name)
    {
        public List<Node> Next { get; } = new();
    }
}

