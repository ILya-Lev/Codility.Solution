using System.Text;

namespace ClassicalProblems;

public class Maze
{
    private readonly int _height;
    private readonly int _width;
    private readonly MazeLocation _start;
    private readonly MazeLocation _finish;

    private readonly Cell[,] _grid;

    public Maze(int height, int width, MazeLocation start, MazeLocation finish, double sparseness)
    {
        _height = height;
        _width = width;
        _start = start;
        _finish = finish;

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

    public double GetEuclideanDistanceToGoal(MazeLocation currentLocation)
    {
        var dx = currentLocation.Column - _finish.Column;
        var dy = currentLocation.Row - _finish.Row;

        return Math.Sqrt(dx * dx + dy * dy);
    }

    public double GetManhattanDistanceToGoal(MazeLocation currentLocation)
    {
        var dx = currentLocation.Column - _finish.Column;
        var dy = currentLocation.Row - _finish.Row;

        return Math.Abs(dx) + Math.Abs(dy);
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
}