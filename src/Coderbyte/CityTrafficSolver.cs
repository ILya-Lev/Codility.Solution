namespace Coderbyte;

/*
    Have the function CityTraffic(strArr) read strArr which will be a representation of an undirected graph in a form similar to an adjacency list. Each element in the input will contain an integer which will represent the population for that city, and then that will be followed by a comma separated list of its neighboring cities and their populations (these will be separated by a colon). For example: strArr may be
   ["1:[5]", "4:[5]", "3:[5]", "5:[1,4,3,2]", "2:[5,15,7]", "7:[2,8]", "8:[7,38]", "15:[2]", "38:[8]"]. This graph then looks like the following picture:
                                               see on the screenshot above

   Each node represents the population of that city and each edge represents a road to that city. Your goal is to determine the maximum traffic that would occur via a single road if everyone decided to go to that city. For example: if every single person in all the cities decided to go to city 7, then via the upper road the number of people coming in would be (8 + 38) = 46. If all the cities beneath city 7 decided to go to it via the lower road, the number of people coming in would be (2 + 15 + 1 + 3 + 4 + 5) = 30. So the maximum traffic coming into the city 7 would be 46 because the maximum value of (30, 46) = 46.
   
   Your program should determine the maximum traffic for every single city and return the answers in a comma separated string in the format: city:max_traffic,city:max_traffic,... The cities should be outputted in sorted order by the city number. For the above example, the output would therefore be: 1:82,2:53,3:80,4:79,5:70,7:46,8:38,15:68,38:45. The cities will all be unique positive integers and there will not be any cycles in the graph. There will always be at least 2 cities in the graph.
 */

public class CityTrafficSolver
{
    public class Node
    {
        public int N { get; init; }
        public List<Node> Next { get; } = new();
        public List<int> NextNs { get; } = new();
    }

    public static string CityTraffic(string[] strArr)
    {
        var nodes = ParseNodes(strArr);
        AssignNexts(nodes);

        var total = nodes.Keys.Sum();
        var maxTraffic = new Dictionary<int, int>();
        foreach (var node in nodes)
        {
            if (node.Value.Next.Count == 1)
            {
                maxTraffic.Add(node.Value.N, total - node.Value.N);
                continue;
            }
            var max = node.Value.Next.Select(n => GetSum(n, node.Value.N)).Max();
            maxTraffic.Add(node.Value.N, max);
        }

        var parts = maxTraffic.OrderBy(p => p.Key).Select(p => $"{p.Key}:{p.Value}");
        return string.Join(",", parts);
    }

    private static Dictionary<int, Node> ParseNodes(string[] strArr)
    {
        var nodes = new Dictionary<int, Node>();
        foreach (var s in strArr)
        {
            var colon = s.IndexOf(":");
            var n = int.Parse(s.Substring(0, colon));
            var node = new Node() { N = n };

            var parts = s.Substring(colon + 1).Trim('[').Trim(']').Split(",");
            var nextNs = parts.Select(int.Parse);
            node.NextNs.AddRange(nextNs);

            nodes.Add(n, node);
        }
        return nodes;
    }

    private static void AssignNexts(Dictionary<int, Node> nodes)
    {
        foreach (var p in nodes)
            foreach (var n in p.Value.NextNs)
                p.Value.Next.Add(nodes[n]);
    }

    private static int GetSum(Node n, int parent){
        var total = 0;
        var seen = new HashSet<int>();
        seen.Add(parent);
        var frontier = new Stack<Node>();
        frontier.Push(n);

        while(frontier.Any()){
            var current = frontier.Pop();
            total += current.N;
            seen.Add(current.N);

            foreach(var c in current.Next.Where(cc => !seen.Contains(cc.N)))
                frontier.Push(c);
        }
        return total;
    }

}