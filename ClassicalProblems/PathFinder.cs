namespace LuxoftRiddles;

public record struct Point(int X, int Y);

public interface IPathFinder
{
    List<Point>? FindPath(int[][] board, in int startX, in int startY, in int destY);
    List<Point>? FindPath(int[][] board, in int startX, in int startY, in int destX, in int destY);
    List<Point>? FindPath(int[][] board, in Point start, in Point destination);
}

public class PathFinder : IPathFinder
{
    public List<Point>? FindPath(int[][] board, in int startX, in int startY, in int destY)
        => FindPath(board, new Point(startX, startY), new Point(startX, destY));
    public List<Point>? FindPath(int[][] board, in int startX, in int startY, in int destX, in int destY)
        => FindPath(board, new Point(startX, startY), new Point(destX, destY));

    public List<Point>? FindPath(int[][] board, in Point start, in Point destination)
    {
        var maze = new Maze(board, destination);
        var lastPoint = AStarSearch.Find(start
            , p => maze.IsInGoal(p)
            , p => maze.GetPossibleSuccessors(p)
            , p => maze.GetManhattanDistanceToGoal(p));
        
        return lastPoint?.AsPath();
    }
}

public class Maze
{
    public const int Occupied = 1;
    public const int Free = 1;

    private readonly int[][] _board;

    public int Width { get; }
    public int Height { get; }
    public Point Destination { get; }

    public Maze(int[][] board, Point destination)
    {
        Destination = destination;
        _board = board;
        Height = board.Length;
        Width = board[0].Length;
    }

    public bool IsInGoal(in Point location) => Destination == location;

    public IReadOnlyCollection<Point> GetPossibleSuccessors(in Point location)
    {
        var onLeft = new Point(location.X - 1, location.Y);
        var onRight = new Point(location.X + 1, location.Y);
        var onTop = new Point(location.X, location.Y - 1);
        var onBottom = new Point(location.X, location.Y + 1);

        return new[] { onLeft, onRight, onTop, onBottom }
            .Where(point => IsValid(point))
            .Where(point => _board[point.Y][point.X] != Occupied)
            .ToArray();
    }

    public double GetManhattanDistanceToGoal(in Point location)
    {
        var dx = location.X - Destination.X;
        var dy = location.X - Destination.Y;

        return Math.Abs(dx) + Math.Abs(dy);
    }

    private bool IsValid(in Point point) => point.X >= 0 && point.X < Width && point.Y >= 0 && point.Y < Height;
}

public class SearchNode
{
    public Point Position { get; }
    public SearchNode? Parent { get; }
    public double Cost { get; }
    public double Heuristics { get; }
    public double Priority => Cost + Heuristics;

    public SearchNode(Point position, SearchNode? parent, double cost, double heuristics)
    {
        Position = position;
        Parent = parent;
        Cost = cost;
        Heuristics = heuristics;
    }

    public List<Point>? AsPath()
    {
        var path = new List<Point>();
        for (var current = this; current is not null; current = current.Parent)
            path.Add(current.Position);

        path.Reverse();
        return path;
    }
}

public static class AStarSearch
{
    public static SearchNode? Find(Point initial
        , Func<Point, bool> isInGoal
        , Func<Point, IReadOnlyCollection<Point>> getSuccessors
        , Func<Point, double> getHeuristics)
    {
        var frontier = new PriorityQueue<SearchNode, double>();
        var initialNode = new SearchNode(initial, null, 0, getHeuristics(initial));
        frontier.Enqueue(initialNode, initialNode.Priority);

        var explored = new Dictionary<Point, double>() { [initial] = 0 };

        while (frontier.Count > 0)
        {
            var current = frontier.Dequeue();
            if (isInGoal(current.Position))
                return current;

            foreach (var possibility in getSuccessors(current.Position))
            {
                var cost = current.Cost + 1;   //as we assume successors are 1 step away from current node!
                if (!explored.ContainsKey(possibility) || explored[possibility] > cost)
                {
                    if (!explored.ContainsKey(possibility))
                        explored.Add(possibility, 0);
                    explored[possibility] = cost;

                    var child = new SearchNode(possibility, current, cost, getHeuristics(possibility));
                    frontier.Enqueue(child, child.Priority);
                }
            }
        }

        return null;
    }

}