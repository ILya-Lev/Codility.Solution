namespace Coderbyte;

/*
 Weighted Path
 
Have the function WeightedPath(strArr) take strArr which will be an array of strings which models a non-looping weighted Graph. The structure of the array will be as follows: The first element in the array will be the number of nodes N (points) in the array as a string. The next N elements will be the nodes which can be anything (A, B, C .. Brick Street, Main Street .. etc.). Then after the Nth element, the rest of the elements in the array will be the connections between all of the nodes along with their weights (integers) separated by the pipe symbol (|). They will look like this: (A|B|3, B|C|12 .. Brick Street|Main Street|14 .. etc.). Although, there may exist no connections at all.

An example of strArr may be: ["4","A","B","C","D","A|B|1","B|D|9","B|C|3","C|D|4"]. Your program should return the shortest path when the weights are added up from node to node from the first Node to the last Node in the array separated by dashes. So in the example above the output should be A-B-C-D. Here is another example with strArr being ["7","A","B","C","D","E","F","G","A|B|1","A|E|9","B|C|2","C|D|1","D|F|2","E|D|6","F|G|2"]. The output for this array should be A-B-C-D-F-G. There will only ever be one shortest path for the array. If no path between the first and last node exists, return -1. The array will at minimum have two nodes. Also, the connection A-B for example, means that A can get to B and B can get to A. A path may not go through any Node more than once.
*/
public class ShortestWeightedPathFinder
{
    private record Node(string Name)
    {
        public List<(Node N, int W)> Next { get; } = new();//N - next node, W - weight of the edge
    }

    public static string WeightedPath(string[] strArr)
    {
        var n = int.Parse(strArr[0]);
        var nodes = strArr.Skip(1).Take(n).ToDictionary(s => s, s => new Node(s));
        AssignLinks(strArr.Skip(1 + n), nodes);

        var start = nodes.First().Value;
        var end = nodes.Last().Value;

        var steps = FindSteps(nodes, start);
        var path = ComposePath(steps, end, start);

        return path.Any()
          ? string.Join("-", path.Select(p => p.Name))
          : "-1";
    }

    private static void AssignLinks(IEnumerable<string> links, Dictionary<string, Node> nodes)
    {
        foreach (var link in links)
        {
            var parts = link.Split('|');
            var weight = int.Parse(parts[2]);

            var parent = nodes[parts[0]];
            var child = nodes[parts[1]];

            parent.Next.Add((child, weight));
            child.Next.Add((parent, weight));
        }
    }

    private static Dictionary<Node, Node> FindSteps(Dictionary<string, Node> nodes, Node start)
    {

        var steps = new Dictionary<Node, Node>();//key = child, value = parent
        steps.Add(start, null);
        var frontier = new PriorityQueue<Node, int>();
        frontier.Enqueue(start, 0);
        var distances = new Dictionary<Node, int>();
        distances.Add(start, 0);

        while (frontier.Count > 0)
        {
            var current = frontier.Dequeue();
            var distance = distances[current];
            foreach (var (child, weight) in current.Next)
            {
                var newDistance = distance + weight;
                if (distances.TryGetValue(child, out var oldDistance) && oldDistance <= newDistance)
                    continue;

                distances[child] = newDistance;
                frontier.Enqueue(child, newDistance);
                steps[child] = current;
            }
        }

        return steps;
    }

    private static List<Node> ComposePath(Dictionary<Node, Node> steps, Node end, Node start)
    {
        var path = new List<Node>();
        path.Add(end);
        var current = end;
        while (current != start)
        {
            if (!steps.TryGetValue(current, out var next))
                return new List<Node>();//the graph is disjoined - path does not exist

            path.Add(next);
            current = next;
        }
        path.Reverse();
        return path;
    }
}