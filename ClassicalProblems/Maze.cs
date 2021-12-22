using System.Text;

namespace ClassicalProblems;

public class Maze
{
    private readonly int _height;
    private readonly int _width;
    private readonly MazeLocation _start;
    private readonly MazeLocation _finish;
    private readonly double _sparseness;

    private readonly Cell[,] _grid;

    public Maze(int height, int width, MazeLocation start, MazeLocation finish, double sparseness)
    {
        _height = height;
        _width = width;
        _start = start;
        _finish = finish;
        _sparseness = sparseness;

        _grid = new Cell[height,width];
        FillInGrid(sparseness);

        _grid[start.Row, start.Column] = Cell.Start;
        _grid[finish.Row, finish.Column] = Cell.Goal;
    }

    public bool IsInGoal(MazeLocation location) => _finish.Equals(location);

    public IReadOnlyCollection<MazeLocation> GetPossibleSuccessors(MazeLocation current)
    {
        var onLeft = new MazeLocation(current.Row - 1, current.Column);
        var onRight = new MazeLocation(current.Row + 1, current.Column);
        var onTop = new MazeLocation(current.Row, current.Column-1);
        var onBottom = new MazeLocation(current.Row, current.Column+1);

        return new[] { onLeft, onRight, onTop, onBottom }
            .Where(location => location.IsValid(_height, _width))
            .Where(location => _grid[location.Row, location.Column] != Cell.Blocked)
            .ToArray();
    }

    public void MarkPath(IReadOnlyCollection<MazeLocation> path)
    {
        foreach (var location in path)
        {
            _grid[location.Row, location.Column] = Cell.Path;
        }
        //as path may contain start and goal, restore these values 
        _grid[_start.Row, _start.Column] = Cell.Start;
        _grid[_finish.Row, _finish.Column] = Cell.Goal;
    }

    public void CleanPath()
    {
        for (int row = 0; row < _height; row++)
        for (int column = 0; column < _width; column++)
        {
            if (_grid[row, column] == Cell.Path)
                _grid[row, column] = Cell.Empty;
        }
    }

    public override string ToString()
    {
        var sb = new StringBuilder(_height);
        for (int row = 0; row < _height; row++)
        {
            for (int column = 0; column < _width; column++)
            {
                sb.Append(_grid[row, column]);
            }

            sb.AppendLine();
        }

        return sb.ToString();
    }

    private void FillInGrid(double sparseness)
    {
        var generator = new Random(DateTime.UtcNow.Millisecond);

        for (int row = 0; row < _height; row++)
        for (int column = 0; column < _width; column++)
        {
            _grid[row, column] = generator.NextDouble() < sparseness ? Cell.Blocked : Cell.Empty;
        }
    }

    public class Cell
    {
        private static readonly IReadOnlyDictionary<string, Cell> _possibleValues;
        private readonly string _value;
            
        public static readonly Cell Empty;
        public static readonly Cell Blocked;
        public static readonly Cell Start;
        public static readonly Cell Goal;
        public static readonly Cell Path;

        private Cell(string s) => _value = s;
        static Cell()
        {
            var names = new[] { " ", "X", "S", "G", "*" };
            _possibleValues = names.ToDictionary(s => s, s => new Cell(s));
            Empty = names[0];
            Blocked = names[1];
            Start = names[2];
            Goal = names[3];
            Path = names[4];
        } 

        public static implicit operator string (Cell c) => c._value;
        public static implicit operator Cell (string s) => _possibleValues.TryGetValue(s, out var c) ? c : null;
    }

    public class MazeLocation
    {
        public int Row { get; }
        public int Column { get; }
        public MazeLocation(int row, int column) => (Row, Column) = (row, column);

        protected bool Equals(MazeLocation other) => Row == other.Row && Column == other.Column;

        public override bool Equals(object? obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((MazeLocation)obj);
        }

        public override int GetHashCode() => HashCode.Combine(Row, Column);

        public bool IsValid(int height, int width)
        {
            return Row >= 0 && Row < height
                && Column >= 0 && Column < width;
        }
    }

    public class Node<T> : IComparable<Node<T>>
    {
        private readonly T _state;
        private readonly Node<T>? _parent;
        private readonly double _cost;
        private readonly double _heuristic;

        public Node(T state, Node<T>? parent, double cost = 0, double heuristic = 0)
        {
            _state = state;
            _parent = parent;
            _cost = cost;
            _heuristic = heuristic;
        }

        public int CompareTo(Node<T>? other) => (_cost + _heuristic).CompareTo(other._cost + other._heuristic);

        public static IReadOnlyCollection<T> AsPath(Node<T> end)
        {
            var path = new List<T>();
            
            for (var current = end; current != null; current = current._parent)
                path.Add(current._state);

            path.Reverse();

            return path;
        }

        public static Node<T>? DepthFirstSearch(T initial, Func<T, bool> isInGoal, Func<T, IReadOnlyCollection<T>> successors)
        {
            var frontier = new Stack<Node<T>>();
            frontier.Push(new Node<T>(initial, null));
        
            var explored = new HashSet<T>();
            explored.Add(initial);

            while (frontier.Any())
            {
                var current = frontier.Pop();
                if (isInGoal(current._state))
                    return current;

                var possibilities = successors(current._state).Where(t => !explored.Contains(t)).ToArray();
                foreach (var possibility in possibilities)
                {
                    explored.Add(possibility);
                    frontier.Push(new Node<T>(possibility, current));
                }
            }

            return null;//there is no path
        }

    }
}