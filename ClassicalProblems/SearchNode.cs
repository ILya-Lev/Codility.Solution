namespace ClassicalProblems;

public class SearchNode<T> : IComparable<SearchNode<T>> where T : notnull
{
    private readonly T _state;
    private readonly SearchNode<T>? _parent;
    private readonly double _cost;
    private readonly double _heuristic;

    public double Priority => _cost + _heuristic;

    public SearchNode(T state, SearchNode<T>? parent, double cost = 0, double heuristic = 0)
    {
        _state = state;
        _parent = parent;
        _cost = cost;
        _heuristic = heuristic;
    }

    /// <summary>
    /// total cost of a given state: f(n) = one step cost + heuristics cost
    /// heuristics could be used as a manhattan distance here = dx + dy
    ///
    /// so we put nodes into a PriorityQueue and get min by CompareTo - A* implementation
    /// </summary>
    public int CompareTo(SearchNode<T>? other) => (_cost + _heuristic).CompareTo(other._cost + other._heuristic);

    public static IReadOnlyCollection<T> AsPath(SearchNode<T> end)
    {
        var path = new List<T>();
            
        for (var current = end; current != null; current = current._parent)
            path.Add(current._state);

        path.Reverse();

        return path;
    }

    public static (SearchNode<T>? lastNode, int stateCount) DepthFirstSearch(T initial
        , Func<T, bool> isInGoal
        , Func<T, IReadOnlyCollection<T>> successors)
    {
        var frontier = new Stack<SearchNode<T>>();
        frontier.Push(new SearchNode<T>(initial, null));
        
        var explored = new HashSet<T> { initial };

        int stateCount = 0;
        while (frontier.Any())
        {
            stateCount++;
            var current = frontier.Pop();
            if (isInGoal(current._state))
                return (current, stateCount);

            var possibilities = successors(current._state).Where(t => !explored.Contains(t)).ToArray();
            foreach (var possibility in possibilities)
            {
                explored.Add(possibility);
                frontier.Push(new SearchNode<T>(possibility, current));
            }
        }

        return  (null, stateCount);//there is no path
    }

    public static (SearchNode<T>? lastNode, int stateCount) BreadthFirstSearch(T initial
        , Func<T, bool> isInGoal
        , Func<T, IReadOnlyCollection<T>> successors)
    {
        var frontier = new Queue<SearchNode<T>>();
        frontier.Enqueue(new SearchNode<T>(initial, null));
        
        var explored = new HashSet<T> { initial };

        int stateCount = 0;
        while (frontier.Any())
        {
            stateCount++;
            var current = frontier.Dequeue();
            if (isInGoal(current._state))
                return (current, stateCount);

            var possibilities = successors(current._state).Where(t => !explored.Contains(t)).ToArray();
            foreach (var possibility in possibilities)
            {
                explored.Add(possibility);
                frontier.Enqueue(new SearchNode<T>(possibility, current));
            }
        }

        return  (null, stateCount);//there is no path
    }

    /// <summary>
    /// for maze
    /// priority = distance from start to current point + distance from current point to finish
    ///
    /// priority queue tries to find min(priority), i.e. path closest to the main diagonal
    /// (if start is top left corner and finish is bottom right corner)
    /// </summary>
    public static (SearchNode<T>? lastNode, int stateCount) AStarSearch(T initial
        , Func<T, bool> isInGoal
        , Func<T, IReadOnlyCollection<T>> successors
        , Func<T, double> heuristics)
    {
        var frontier = new PriorityQueue<SearchNode<T>, double>();
        var initialNode = new SearchNode<T>(initial, null, 0, heuristics(initial));
        frontier.Enqueue(initialNode, initialNode.Priority);
        
        var explored = new Dictionary<T, double>() { [initial] = 0 };

        int stateCount = 0;
        while (frontier.Count > 0)
        {
            stateCount++;
            var current = frontier.Dequeue();
            if (isInGoal(current._state))
                return (current, stateCount);

            foreach (var possibility in successors(current._state))
            {
                var cost = current._cost + 1;   //as we assume successors are 1 step away from current node!
                if (!explored.ContainsKey(possibility) || explored[possibility] > cost)
                {
                    if (!explored.ContainsKey(possibility))
                        explored.Add(possibility, 0);
                    explored[possibility] = cost;

                    var child = new SearchNode<T>(possibility, current, cost, heuristics(possibility));
                    frontier.Enqueue(child, child.Priority);
                }
            }
        }

        return (null, stateCount);//there is no path
    }
}