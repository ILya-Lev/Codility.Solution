using System.Text;
using System.Windows.Markup;

namespace ClassicalProblems;

public class ConstraintSatisfactoryProblem<V, D>
{
    private readonly IReadOnlyCollection<V> _variables;
    private readonly IReadOnlyDictionary<V, IReadOnlyCollection<D>> _ranges;
    private readonly IReadOnlyDictionary<V, List<Constraint<V, D>>> _constraints;

    public ConstraintSatisfactoryProblem(IReadOnlyCollection<V> variables
        , IReadOnlyDictionary<V, IReadOnlyCollection<D>> ranges)
    {
        _variables = variables;
        _ranges = ranges;

        _constraints = variables.ToDictionary(v => v, v => new List<Constraint<V, D>>());

        foreach (var variable in _variables)
        {
            if (!_ranges.ContainsKey(variable))
                throw new ArgumentException($"There is no range of possible values for variable {variable}", nameof(ranges));
        }
    }

    public void AddConstraint(Constraint<V, D> constraint)
    {
        foreach (var variable in constraint.Variables)
        {
            if (!_constraints.ContainsKey(variable))
                throw new ArgumentException(
                    $"Constraint variable is missing in CSP");

            _constraints[variable].Add(constraint);
        }
    }

    public bool IsConsistent(V variable, IReadOnlyDictionary<V, D> assignment)
    {
        return _constraints[variable].All(c => c.IsSatisfied(assignment));
    }

    public Dictionary<V, D> BacktrackingSearch(Dictionary<V, D> assignment = null)
    {
        if (assignment is null)
            assignment = new Dictionary<V, D>();

        if (assignment.Count == _variables.Count) return assignment;

        var unassignedVariable = _variables
            .First(v => !assignment.ContainsKey(v));

        foreach (var possibleValue in _ranges[unassignedVariable])
        {
            var currentAssignment = new Dictionary<V, D>(assignment)
            {
                [unassignedVariable] = possibleValue
            };

            //check whether given variable can be assigned with
            //a value from its domain (region)
            //with respect to all other rules (constraints)
            if (IsConsistent(unassignedVariable, currentAssignment))
            {
                var result = BacktrackingSearch(currentAssignment);
                if (result is not null)
                    return result;
            }
        }

        return null;
    }
}

/// <summary> base class for CSP - constraint satisfactory problems </summary>
/// <typeparam name="V">variable type</typeparam>
/// <typeparam name="D">domain type; definition range values</typeparam>
public abstract class Constraint<V, D>
{
    public IReadOnlyList<V> Variables { get; }
    protected Constraint(IReadOnlyCollection<V> letters) =>
        Variables = letters.ToArray();

    public abstract bool IsSatisfied(IReadOnlyDictionary<V, D> assignment);
}


public class MapColoringConstraint : Constraint<string, string>
{
    private readonly string _space1;
    private readonly string _space2;

    public MapColoringConstraint(string space1, string space2)
    : base(new[] { space1, space2 })
    {
        _space1 = space1;
        _space2 = space2;
    }

    public override bool IsSatisfied(IReadOnlyDictionary<string, string> assignment)
    {
        //if not colored, constraint is satisfied
        if (!assignment.ContainsKey(_space1) || !assignment.ContainsKey(_space2))
            return true;

        //cover with different colors
        return !assignment[_space1].Equals(assignment[_space2]);
    }
}

public static class ColorAustralia
{
    public static string[] Regions { get; } = new[]
    {
        "Western Australia",
        "Northern Territory",
        "South Australia",
        "Queensland",
        "New South Wales",
        "Victoria",
        "Tasmania"
    };

    public static string[] Colors { get; } = new[] { "red", "green", "blue" };

    public static Dictionary<string, string> ColorTheMap()
    {
        var domains = Regions
            .ToDictionary(r => r, r => (IReadOnlyCollection<string>)Colors);

        var constraints = new[]
        {
            new MapColoringConstraint(Regions[0], Regions[1]),

            new MapColoringConstraint(Regions[2], Regions[0]),
            new MapColoringConstraint(Regions[2], Regions[1]),
            new MapColoringConstraint(Regions[2], Regions[3]),
            new MapColoringConstraint(Regions[2], Regions[4]),
            new MapColoringConstraint(Regions[2], Regions[5]),

            new MapColoringConstraint(Regions[1], Regions[3]),

            new MapColoringConstraint(Regions[4], Regions[3]),
            new MapColoringConstraint(Regions[4], Regions[5]),

            new MapColoringConstraint(Regions[6], Regions[5]),
        };

        var csp = new ConstraintSatisfactoryProblem<string, string>(Regions, domains);

        foreach (var constraint in constraints)
        {
            csp.AddConstraint(constraint);
        }

        var coloredMap = csp.BacktrackingSearch(null);
        return coloredMap;
    }
}

//int - queen's number
//(int row, int col) - queen's position in given solution
public class QueensConstraint : Constraint<int, (int row, int col)>
{
    private readonly int _lhs;
    private readonly int _rhs;

    public QueensConstraint(int lhs, int rhs)
        : base(new[] { lhs, rhs })
    {
        _lhs = lhs;
        _rhs = rhs;
    }

    public override bool IsSatisfied(IReadOnlyDictionary<int, (int row, int col)> assignment)
    {
        if (!assignment.ContainsKey(_lhs) || !assignment.ContainsKey(_rhs))
            return true;

        var (lhsRow, lhsCol) = assignment[_lhs];
        var (rhsRow, rhsCol) = assignment[_rhs];

        return !AreOnTheSameRow(lhsRow, rhsRow)
            && !AreOnTheSameColumn(lhsCol, rhsCol)
            && !AreOnTheSameDiagonal((lhsRow, lhsCol), (rhsRow, rhsCol));
    }

    private static bool AreOnTheSameDiagonal((int lhsRow, int lhsCol) lhs
                                           , (int rhsRow, int rhsCol) rhs)
    {
        for (int shift = 1; shift < 8; shift++)
        {
            var mainDownRow = lhs.lhsRow + shift;
            var mainDownCol = lhs.lhsCol + shift;
            if (mainDownRow == rhs.rhsRow && mainDownCol == rhs.rhsCol)
                return true;

            var mainUpRow = lhs.lhsRow - shift;
            var mainUpCol = lhs.lhsCol - shift;
            if (mainUpRow == rhs.rhsRow && mainUpCol == rhs.rhsCol)
                return true;

            var secondaryDownRow = lhs.lhsRow + shift;
            var secondaryDownCol = lhs.lhsCol - shift;
            if (secondaryDownRow == rhs.rhsRow && secondaryDownCol == rhs.rhsCol)
                return true;

            var secondaryUpRow = lhs.lhsRow - shift;
            var secondaryUpCol = lhs.lhsCol + shift;
            if (secondaryUpRow == rhs.rhsRow && secondaryUpCol == rhs.rhsCol)
                return true;
        }

        return false;
    }

    private static bool AreOnTheSameColumn(int lhsCol, int rhsCol) => lhsCol == rhsCol;

    private static bool AreOnTheSameRow(int lhsRow, int rhsRow) => lhsRow == rhsRow;
}

public static class QueensProblem
{
    public static Dictionary<int, (int row, int col)> PlaceFigures()
    {
        var variables = Enumerable.Range(1, 8).ToArray();
        var board = Enumerable.Range(1, 8)
            .SelectMany(row => Enumerable.Range(1, 8).Select(col => (row, col)))
            .ToArray();

        var ranges = variables.ToDictionary(v => v, v => (IReadOnlyCollection<(int, int)>)board);

        //count should be (64-8)/2 + 8 = 36
        var constraints = new List<QueensConstraint>();
        for (int lhs = 0; lhs < 8; lhs++)
            for (int rhs = lhs + 1; rhs < 8; rhs++)
            {
                constraints.Add(new QueensConstraint(variables[lhs], variables[rhs]));
            }

        var csp = new ConstraintSatisfactoryProblem<int, (int row, int col)>(
            variables, ranges);

        foreach (var constraint in constraints)
        {
            csp.AddConstraint(constraint);
        }

        return csp.BacktrackingSearch(null);
    }
}

public class WordGrid
{
    private const int AlphabetSize = 26;
    private const char FirstLetter = 'A';

    private readonly int _height;
    private readonly int _width;
    private char[,] _grid;

    public WordGrid(int height, int width)
    {
        _height = height;
        _width = width;
        _grid = new char[height, width];

        var generator = new Random(DateTime.UtcNow.Millisecond);
        for (int row = 0; row < height; row++)
            for (int col = 0; col < width; col++)
            {
                _grid[row, col] = (char)(generator.Next(0, AlphabetSize) + FirstLetter);
            }
    }

    public void InitializeWithWord(string word, IReadOnlyCollection<GridLocation> locations)
    {
        var letters = locations.Zip(word, (l, c) => (l.Row, l.Column, c)).ToArray();
        foreach (var letter in letters)
        {
            _grid[letter.Row, letter.Column] = letter.c;
        }
    }

    public List<List<GridLocation>> GenerateDomain(string word)
    {
        var domain = new List<List<GridLocation>>();
        for (int row = 0; row < _height; row++)
        {
            for (int col = 0; col < _width; col++)
            {
                if (col + word.Length <= _width)
                {
                    domain.Add(GenerateRight(row, col, word.Length));
                    if (row + word.Length <= _height)
                        domain.Add(GenerateDiagonalRight(row, col, word.Length));
                }

                if (row + word.Length <= _height)
                {
                    domain.Add(GenerateDown(row, col, word.Length));
                    if (col - word.Length >= 0)
                        domain.Add(GenerateDiagonalLeft(row, col, word.Length));
                }
            }
        }

        return domain;
    }

    private List<GridLocation> GenerateDiagonalLeft(int row, int col, int length) => Enumerable
        .Range(0, length)
        .Select(shift => new GridLocation(row + shift, col - shift))
        .ToList();

    private List<GridLocation> GenerateDown(int row, int col, int length) => Enumerable
        .Range(row, length)
        .Select(r => new GridLocation(r, col))
        .ToList();

    private List<GridLocation> GenerateDiagonalRight(int row, int col, int length) => Enumerable
        .Range(0, length)
        .Select(shift => new GridLocation(row + shift, col + shift))
        .ToList();

    private List<GridLocation> GenerateRight(int row, int col, int length) => Enumerable
        .Range(col, length)
        .Select(c => new GridLocation(row, c))
        .ToList();

    public override string ToString()
    {
        var sb = new StringBuilder(_height * _width);
        for (int row = 0; row < _height; row++)
        {
            for (int col = 0; col < _width; col++)
            {
                sb.Append(_grid[row, col]);
            }

            sb.AppendLine();
        }

        return sb.ToString();
    }

    public class GridLocation
    {
        public int Row { get; }
        public int Column { get; }

        public GridLocation(int row, int column) => (Row, Column) = (row, column);

        protected bool Equals(GridLocation other) => Row == other.Row
                                                  && Column == other.Column;

        public override bool Equals(object? obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((GridLocation)obj);
        }

        public override int GetHashCode() => HashCode.Combine(Row, Column);
    }

    public class WordSearchConstraint : Constraint<string, List<GridLocation>>
    {
        public WordSearchConstraint(IReadOnlyCollection<string> words) : base(words) { }

        public override bool IsSatisfied(IReadOnlyDictionary<string, List<GridLocation>> assignment)
        {
            var allLocations = assignment.Values.SelectMany(v => v).ToArray();
            var allUniqueLocations = new HashSet<GridLocation>(allLocations);

            //if we have duplicates
            return allLocations.Length == allUniqueLocations.Count;
        }
    }
}

public class SendMoreMoneyConstraint : Constraint<char, int>
{
    private readonly char[] _letters;
    public SendMoreMoneyConstraint(char[] letters) : base(letters) => _letters = letters;

    public override bool IsSatisfied(IReadOnlyDictionary<char, int> assignment)
    {
        if (!AreDigitsUnique(assignment)) return false;

        if (!AllVariablesAssigned(assignment)) return true; //give it a chance to move on

        var m = assignment['M'];
        if (m == 0) return false;

        var s = assignment['S'];
        var e = assignment['E'];
        var n = assignment['N'];
        var d = assignment['D'];
        var o = assignment['O'];
        var r = assignment['R'];
        var y = assignment['Y'];

        var send = s * 1_000 + e * 100 + n * 10 + d;
        var more = m * 1_000 + o * 100 + r * 10 + e;
        var money = m * 10_000 + o * 1_000 + n * 100 + e * 10 + y;

        return send + more == money;
    }

    private bool AllVariablesAssigned(IReadOnlyDictionary<char, int> assignment) => _letters.All(assignment.ContainsKey);

    private bool AreDigitsUnique(IReadOnlyDictionary<char, int> assignment)
    {
        return new HashSet<int>(assignment.Values).Count == assignment.Count;
    }
}

public class RectangleFill
{
    private readonly int _height;
    private readonly int _width;
    private readonly Rectangle _masterRectangle;
    private readonly int[,] _grid;

    public RectangleFill(int width, int height)
    {
        _height = height;
        _width = width;
        _masterRectangle = new Rectangle(width, height);

        _grid = new int[height, width];
    }

    public Rectangle[] CoverWithRectangles(IReadOnlyCollection<Rectangle> rectangles)
    {
        IReadOnlyCollection<RectangleLocation> allLocations = Enumerable.Range(0, _height)
            .SelectMany(row => Enumerable.Range(0, _width).Select(col => new RectangleLocation(row, col)))
            .ToArray();

        var domain = rectangles.ToDictionary(r => r, r => allLocations);
        var csp = new ConstraintSatisfactoryProblem<Rectangle, RectangleLocation>(rectangles, domain);
        csp.AddConstraint(new RectangleConstraint(rectangles, _masterRectangle));

        var solution = csp.BacktrackingSearch();
        var coverage = solution?.Select(p => new Rectangle(p.Key.Width, p.Key.Height) { TopLeft = p.Value }).ToArray();

        if (coverage is not null)
            MarkGrid(coverage);

        return coverage;
    }

    private void MarkGrid(IReadOnlyCollection<Rectangle> coverage)
    {
        for (int row = 0; row < _height; row++)
            for (int col = 0; col < _width; col++)
            {
                foreach (var rectangle in coverage)
                {
                    if (rectangle.Contains(row, col))
                        _grid[row, col] = rectangle.Id;
                }
            }
    }

    public override string ToString()
    {
        var sb = new StringBuilder();
        for (int row = 0; row < _height; row++)
        {
            for (int col = 0; col < _width; col++)
            {
                if (_grid[row, col] != 0)
                    sb.Append($"{_grid[row, col]}");
                else
                    sb.Append(" ");
            }

            sb.AppendLine();
        }

        return sb.ToString();
    }

    //for each rectangle we want to know Bottom Left position
    public class RectangleConstraint : Constraint<Rectangle, RectangleLocation>
    {
        private readonly IReadOnlyCollection<Rectangle> _rectangles;
        private readonly Rectangle _masterRectangle;

        public RectangleConstraint(IReadOnlyCollection<Rectangle> rectangles, Rectangle masterRectangle)
        : base(rectangles)
        {
            _rectangles = rectangles;
            _masterRectangle = masterRectangle;
        }

        public override bool IsSatisfied(IReadOnlyDictionary<Rectangle, RectangleLocation> assignment)
        {
            return AllInsideMaster(assignment) && !AnyOverlaps(assignment);
        }

        //private bool ContainsAll(IReadOnlyDictionary<Rectangle, RectangleLocation> assignment)
        //{
        //    return _rectangles.All(assignment.ContainsKey);
        //}

        private bool AnyOverlaps(IReadOnlyDictionary<Rectangle, RectangleLocation> assignment)
        {
            var rectangleSet = _rectangles
                .Where(assignment.ContainsKey)
                .Select(r => new Rectangle(r.Width, r.Height) { TopLeft = assignment[r] })
                .ToArray();
            for (int i = 0; i < rectangleSet.Length; i++)
                for (int j = i + 1; j < rectangleSet.Length; j++)
                {
                    if (rectangleSet[i].Overlaps(rectangleSet[j])
                        || rectangleSet[i].Contains(rectangleSet[j])
                        || rectangleSet[j].Contains(rectangleSet[i]))
                        return true;
                }

            return false;
        }

        private bool AllInsideMaster(IReadOnlyDictionary<Rectangle, RectangleLocation> assignment)
        {
            foreach (var pair in assignment)
            {
                if (!_masterRectangle.Contains(pair.Value)
                 || !_masterRectangle.Contains(pair.Key.GetBottomRightFor(pair.Value)))
                    return false;
            }

            return true;
        }
    }

    public class RectangleLocation
    {
        public int Row { get; }
        public int Column { get; }

        public RectangleLocation(int row, int col) => (Row, Column) = (row, col);
        public void Deconstruct(out int row, out int column) => (row, column) = (Row, Column);

        public static bool operator >=(RectangleLocation current, RectangleLocation other) =>
            current.Row >= other.Row && current.Column >= other.Column;

        public static bool operator <=(RectangleLocation current, RectangleLocation other) =>
            current.Row <= other.Row && current.Column <= other.Column;

        public override string ToString() => $"({Row},{Column})";
    }

    public class Rectangle
    {
        private static int _id = 0;
        public int Id { get; } = _id++ % 10;

        public RectangleLocation TopLeft { get; set; } = new(0, 0);
        public RectangleLocation BottomRight => new(TopLeft.Row + Height - 1, TopLeft.Column + Width - 1);
        public int Width { get; }
        public int Height { get; }

        public Rectangle(int height, int width) => (Height, Width) = (height, width);

        public RectangleLocation GetBottomRightFor(RectangleLocation topLeft) =>
            new(topLeft.Row + Height - 1, topLeft.Column + Width - 1);

        public bool Contains(int row, int col) => Contains(new RectangleLocation(row, col));
        public bool Contains(RectangleLocation location) => TopLeft <= location && BottomRight >= location;
        public bool Contains(Rectangle other) => TopLeft <= other.TopLeft && BottomRight >= other.BottomRight;
        public bool Overlaps(Rectangle other) => TopLeft <= other.BottomRight && BottomRight >= other.TopLeft;

        protected bool Equals(Rectangle other)
        {
            return TopLeft.Equals(other.TopLeft) && Width == other.Width && Height == other.Height;
        }

        public override bool Equals(object? obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Rectangle)obj);
        }

        public override int GetHashCode() => HashCode.Combine(TopLeft, Width, Height);
    }
}

//sudoku
public class Sudoku
{
    public const int Size = 9;
    private readonly int[,] _grid = new int[Size, Size];
    private static readonly Dictionary<int, List<(int row, int col)>> _rowLocations = new();
    private static readonly Dictionary<int, List<(int row, int col)>> _colLocations = new();
    private static readonly Dictionary<int, List<(int row, int col)>> _squareLocations = new();

    public Sudoku(int[,] initialDigits)
    {
        for (int row = 0; row < Size; row++)
            for (int col = 0; col < Size; col++)
            {
                _grid[row, col] = initialDigits[row, col];
            }
    }

    public IReadOnlyCollection<int> GetRange(int row, int col)
    {
        if (_grid[row, col] != 0) return new[] { _grid[row, col] };

        var range = Enumerable.Range(1, Size).ToArray();

        var locations = GetLocationsForRow(row)
            .Concat(GetLocationsForCol(col))
            .Concat(GetLocationsForSquare(GetSquareId(row, col)));

        foreach ((int row, int col) location in locations)
        {
            var digit = _grid[location.row, location.col];
            if (digit != 0)
                range[digit - 1] = 0;
        }

        return range.Where(d => d != 0).ToArray();
    }

    public bool IsFilledIn()
    {
        for (int row = 0; row < Size; row++)
            for (int col = 0; col < Size; col++)
            {
                if (_grid[row, col] == 0)
                    return false;
            }

        return true;
    }

    public void FillIn(Dictionary<(int row, int col), int> digitsByLocation)
    {
        foreach (var pair in digitsByLocation)
        {
            var (row, col) = pair.Key;
            _grid[row, col] = pair.Value;
        }
    }

    public override string ToString()
    {
        var sb = new StringBuilder(Size * Size);
        for (int row = 0; row < Size; row++)
        {
            for (int col = 0; col < Size; col++)
                sb.Append($"{_grid[row, col]}");

            sb.AppendLine();
        }

        return sb.ToString();
    }

    public static int GetSquareId(int row, int col) => row / 3 * 3 + col / 3;

    public static IReadOnlyCollection<(int row, int col)> GetLocationsForRow(int row)
    {
        if (!_rowLocations.TryGetValue(row, out var locations))
        {
            locations = new List<(int row, int col)>();
            for (int col = 0; col < Size; col++)
                locations.Add((row, col));

            _rowLocations.Add(row, locations);
        }
        return locations;
    }
    public static IReadOnlyCollection<(int row, int col)> GetLocationsForCol(int col)
    {
        if (!_colLocations.TryGetValue(col, out var locations))
        {
            locations = new List<(int row, int col)>();
            for (int row = 0; row < Size; row++)
                locations.Add((row, col));

            _colLocations.Add(col, locations);
        }
        return locations;
    }

    public static IReadOnlyCollection<(int row, int col)> GetLocationsForSquare(int id)
    {
        if (!_squareLocations.TryGetValue(id, out var locations))
        {
            locations = new List<(int row, int col)>();
            for (int row = 0; row < Size / 3; row++)
                for (int col = 0; col < Size / 3; col++)
                {
                    var r = id / 3 * 3 + row;
                    var c = id % 3 * 3 + col;
                    locations.Add((r, c));
                }

            _squareLocations.Add(id, locations);
        }
        return locations;
    }

    public class RowConstraint : Constraint<(int row, int col), int>
    {
        private readonly int _currentRow;
        public RowConstraint(IReadOnlyCollection<(int row, int col)> cells, int currentRow) : base(cells)
        {
            _currentRow = currentRow;
        }

        public override bool IsSatisfied(IReadOnlyDictionary<(int row, int col), int> assignment)
        {
            var uniqueDigits = new HashSet<int>(Size);
            var allDigits = new List<int>(Size);
            for (int col = 0; col < Sudoku.Size; col++)
            {
                if (assignment.TryGetValue((_currentRow, col), out var digit) && digit != 0)
                {
                    uniqueDigits.Add(digit);
                    allDigits.Add(digit);
                }
            }

            return uniqueDigits.Count == allDigits.Count;
        }
    }

    public class ColConstraint : Constraint<(int row, int col), int>
    {
        private readonly int _currentCol;
        public ColConstraint(IReadOnlyCollection<(int row, int col)> cells, int currentCol) : base(cells)
        {
            _currentCol = currentCol;
        }

        public override bool IsSatisfied(IReadOnlyDictionary<(int row, int col), int> assignment)
        {
            var uniqueDigits = new HashSet<int>(Size);
            var allDigits = new List<int>(Size);
            for (int row = 0; row < Size; row++)
            {
                if (assignment.TryGetValue((row, _currentCol), out var digit) && digit != 0)
                {
                    uniqueDigits.Add(digit);
                    allDigits.Add(digit);
                }
            }

            return uniqueDigits.Count == allDigits.Count;
        }
    }

    public class SquareConstraint : Constraint<(int row, int col), int>
    {
        private readonly int _id;
        public SquareConstraint(IReadOnlyCollection<(int row, int col)> cells, int id) : base(cells)
        {
            _id = id;
        }

        public override bool IsSatisfied(IReadOnlyDictionary<(int row, int col), int> assignment)
        {
            var uniqueDigits = new HashSet<int>(Size);
            var allDigits = new List<int>(Size);
            for (int r = 0; r < Size / 3; r++)
                for (int c = 0; c < Size / 3; c++)
                {
                    var row = _id / 3 * 3 + r;
                    var col = _id % 3 * 3 + c;
                    if (assignment.TryGetValue((row, col), out var digit) && digit != 0)
                    {
                        uniqueDigits.Add(digit);
                        allDigits.Add(digit);
                    }
                }

            return uniqueDigits.Count == allDigits.Count;
        }
    }

    public class SudokuConstraint : Constraint<(int row, int col), int>//domain = digits
    {
        public SudokuConstraint(IReadOnlyCollection<(int row, int col)> cells) : base(cells) { }

        public override bool IsSatisfied(IReadOnlyDictionary<(int row, int col), int> assignment) => !assignment
            .Any(p => DoesRowContainDuplicates(p.Key, p.Value, assignment)
                   || DoesColContainDuplicates(p.Key, p.Value, assignment)
                   || DoesSquareContainDuplicates(p.Key, p.Value, assignment));

        private bool DoesRowContainDuplicates((int row, int col) cell, int digit
            , IReadOnlyDictionary<(int row, int col), int> assignment)
        {
            foreach ((int row, int col) in GetLocationsForRow(cell.row))
            {
                if (col == cell.col) continue;//do not compare the input cell with itself

                if (assignment.TryGetValue((row, col), out var currentDigit) && currentDigit == digit)
                    return true;
            }

            return false;
        }

        private bool DoesColContainDuplicates((int row, int col) cell, int digit
            , IReadOnlyDictionary<(int row, int col), int> assignment)
        {
            foreach ((int row, int col) in GetLocationsForCol(cell.col))
            {
                if (row == cell.row) continue;//do not compare the input cell with itself

                if (assignment.TryGetValue((row, col), out var currentDigit) && currentDigit == digit)
                    return true;
            }

            return false;
        }

        private bool DoesSquareContainDuplicates((int row, int col) cell, int digit
            , IReadOnlyDictionary<(int row, int col), int> assignment)
        {
            foreach ((int row, int col) in GetLocationsForSquare(GetSquareId(cell.row, cell.col)))
            {
                if (row == cell.row && col == cell.col)
                    continue;//do not compare the input cell with itself

                if (assignment.TryGetValue((row, col), out var currentDigit) && currentDigit == digit)
                    return true;
            }

            return false;
        }
    }
}

public class AnimalPuzzle
{
    public static int GetFinalResult()
    {
        var variables = new[] { 'a', 'b', 'c' };
        var range = Enumerable.Range(0, 60).ToArray();
        var ranges = variables.ToDictionary(v => v, v => (IReadOnlyCollection<int>)range);

        var csp = new ConstraintSatisfactoryProblem<char, int>(variables, ranges);
        csp.AddConstraint(new AnimalConstraint(variables));

        var solution = csp.BacktrackingSearch();
        if (solution is null) return 0;

        return solution['a'] + solution['b'] * solution['c'];
    }

    public static double GetFinalResultFlexible(IReadOnlyList<string> equations, string expression)
    {
        var parser = new EqualityParser(equations);

        var variables = parser.Variables;
        var maxValue = (int)parser.GetMaxValue();
        var range = Enumerable.Range(0, maxValue).Select(n => (double)n).ToArray();
        var ranges = variables.ToDictionary(v => v, v => (IReadOnlyCollection<double>)range);

        var csp = new ConstraintSatisfactoryProblem<char, double>(variables, ranges);
        csp.AddConstraint(new AnimalConstraintFlexible(variables, parser.AreAllSatisfied));

        var solution = csp.BacktrackingSearch();
        if (solution is null) return 0;

        var expressionParser = new EqualityParser.ExpressionParser(expression);
        return expressionParser.Calculate(solution);
    }

    public class AnimalConstraint : Constraint<char, int>
    {
        private readonly IReadOnlyList<char> _letters;

        //a,b,c
        //a+a+a=60
        //b+b+a=28
        //c+c+b=10
        //a+c*b = d - find d
        public AnimalConstraint(IReadOnlyList<char> letters) : base(letters) => _letters = letters;

        public override bool IsSatisfied(IReadOnlyDictionary<char, int> assignment)
        {
            if (!AllDigitsAreUnique(assignment)) return false;
            if (!AllLettersAreAssigned(assignment)) return true;//continue filling it up

            var a = assignment['a'];
            var b = assignment['b'];
            var c = assignment['c'];

            return MatchFirstCondition(a) && MatchSecondCondition(a, b) && MatchThirdCondition(b, c);
        }

        private bool MatchFirstCondition(int a) => a + a + a == 60;
        private bool MatchSecondCondition(int a, int b) => b + b + a == 28;
        private bool MatchThirdCondition(int b, int c) => c + c + b == 10;

        private bool AllDigitsAreUnique(IReadOnlyDictionary<char, int> assignment)
        {
            return new HashSet<int>(assignment.Values).Count == assignment.Count;
        }

        private bool AllLettersAreAssigned(IReadOnlyDictionary<char, int> assignment)
        {
            return _letters.All(assignment.ContainsKey);
        }
    }

    public class AnimalConstraintFlexible : Constraint<char, double>
    {
        private readonly IReadOnlyList<char> _letters;
        private readonly Func<IReadOnlyDictionary<char, double>, bool> _predicate;

        public AnimalConstraintFlexible(IReadOnlyList<char> letters
            , Func<IReadOnlyDictionary<char, double>, bool> predicate)
            : base(letters)
        {
            _letters = letters;
            _predicate = predicate;
        }

        public override bool IsSatisfied(IReadOnlyDictionary<char, double> assignment)
        {
            if (!AllDigitsAreUnique(assignment)) return false;
            if (!AllLettersAreAssigned(assignment)) return true;//continue filling it up
            return _predicate(assignment);
        }

        private bool AllDigitsAreUnique(IReadOnlyDictionary<char, double> assignment)
        {
            return new HashSet<double>(assignment.Values).Count == assignment.Count;
        }

        private bool AllLettersAreAssigned(IReadOnlyDictionary<char, double> assignment)
        {
            return _letters.All(assignment.ContainsKey);
        }
    }
}


//the problem is inspired by
//https://dou.ua/lenta/interviews/dev-challenge-experience/?from=tge&utm_source=telegram&utm_medium=social
public class EqualityParser
{
    private readonly IReadOnlyList<string> _equalities;
    private readonly IReadOnlyDictionary<string, (ExpressionParser, ExpressionParser)> _expressions;

    private char[] _variables;

    public char[] Variables => _variables ??= GetVariables();

    public EqualityParser(IReadOnlyList<string> equalities)
    {
        _equalities = equalities;
        _expressions = _equalities.ToDictionary(e => e, SplitToExpressions);
    }

    public bool AreAllSatisfied(IReadOnlyDictionary<char, double> assignment) => _expressions.Values
        .All(e => e.Item1.Calculate(assignment) == e.Item2.Calculate(assignment));

    public double GetMaxValue()
    {
        var maxValues = _expressions.Values
            .Select(e => Math.Max(e.Item1.GetMaxValue(), e.Item2.GetMaxValue()))
            .ToArray();

        return maxValues.Any() ? maxValues.Max() : 100;
    }
    
    private (ExpressionParser, ExpressionParser) SplitToExpressions(string equality)
    {
        var equalityLocation = equality.IndexOf('=');
        if (equalityLocation == -1)
            throw new ArgumentException($"Equality symbol is missing in: {equality}");

        var lhs = equality.Substring(0, equalityLocation);
        var rhs = equality.Substring(equalityLocation + 1);

        return (new ExpressionParser(lhs), new ExpressionParser(rhs));
    }

    private char[] GetVariables() => _equalities
        .SelectMany(e => e.ToLowerInvariant().ToCharArray())
        .Where(char.IsLetter)
        .Distinct()
        .ToArray();

    public class ExpressionParser
    {
        private static readonly HashSet<char> _supportedOperations = new (new[]
        {
            '+', '-', '*', '/'
        });
        private readonly List<Term> _terms;
        private readonly PriorityQueue<Operation, int> _operationQueue;
        
        public string Expression { get; }

        public ExpressionParser(string expression)
        {
            Expression = expression;
            _terms = PreprocessExpression(expression);

            var operations = _terms.OfType<Operation>().Select(p => (p, p.Priority));
            _operationQueue = new PriorityQueue<Operation, int>(operations);
        }

        public double GetMaxValue()
        {
            var values = _terms.OfType<Operand>().Select(p => Math.Abs(p.Value)).ToArray();
            return values.Any() ? values.Max() : 0;
        }

        public double Calculate(IReadOnlyDictionary<char, double> assignment)
        {
            var terms = _terms.ToList();//deep copy
            var queue = new PriorityQueue<Operation, int>(_operationQueue.UnorderedItems);//deep copy

            while (queue.Count != 0)
            {
                var op = queue.Dequeue();
            
                var lhs = GetOperand(terms, op.Position-1, assignment);
                var rhs = GetOperand(terms, op.Position+1, assignment);

                var value = op.Evaluate(lhs, rhs);
                var operand = new Operand(value) { Position = op.Position - 1 };

                terms.RemoveAt(op.Position-1);
                terms.RemoveAt(op.Position-1);
                terms.RemoveAt(op.Position-1);

                terms.Insert(operand.Position, operand);
            
                for (int i = op.Position; i < terms.Count; i++)
                    terms[i].Position = i;
            }

            return (terms.First() as Operand)!.Value;
        }

        private static Operand GetOperand(List<Term> terms, int position,
            IReadOnlyDictionary<char, double> assignment)
        {
            if (terms[position] is Operand operand) return operand;
            
            if (terms[position] is Variable variable)
            {
                if (!assignment.TryGetValue(variable.Name[0], out var value))
                    throw new Exception($"Variable {variable} is not assigned");
                return new Operand(value) { Position = position };
            }

            throw new Exception($"term at position {position} is not an Operand");
        }
        
        private List<Term> PreprocessExpression(string expression)
        {
            var terms = new List<Term>();
            for (var i = 0; i < expression.Length; i++)
            {
                if (char.IsLetter(expression[i]))
                {
                    var nameParts = new List<char>();
                    while (i < expression.Length && char.IsLetter(expression[i]))
                        nameParts.Add(expression[i++]);
                    i--;//as we do i++ in outer loop
                    var variable = new Variable(new string(nameParts.ToArray()));
                    terms.Add(variable);
                }
                else if (_supportedOperations.Contains(expression[i]))
                {
                    var operation = new Operation(expression[i]);
                    terms.Add(operation);
                }
                else if (char.IsDigit(expression[i]) || char.IsSeparator(expression[i]))
                {
                    var numberParts = new List<char>();
                    while (i < expression.Length
                           && (char.IsDigit(expression[i]) || char.IsSeparator(expression[i])))
                    {
                        numberParts.Add(expression[i++]);
                    }
                    i--;//as we do i++ in outer loop

                    var value = double.Parse(new string(numberParts.ToArray()));
                    var operand = new Operand(value);
                    terms.Add(operand);
                }
            }

            for (int i = 0; i < terms.Count; i++)
            {
                terms[i].Position = i;
            }
            
            return terms;
        }
    }

    private class Term
    {
        public int Position { get; set; }
    }

    private class Operation : Term
    {
        private readonly Func<double, double, double> _evaluator;
        public int Priority { get; }
        public char Token { get; }

        public Operation(char token)
        {
            Token = token;
            Priority = token switch
            {
                '*' => 1,
                '/' => 1,
                '+' => 2,
                '-' => 2,
                _ => throw new InvalidOperationException($"Token '{token}' is not supported as an operation")
            };
            
            _evaluator = token switch
            {
                '*' => (a, b) => a*b,
                '/' => (a, b) => b==0.0 ? double.NaN : a/b,
                '+' => (a, b) => a+b,
                '-' => (a, b) => a-b,
            };
        }

        public double Evaluate(Operand lhs, Operand rhs) => _evaluator(lhs.Value, rhs.Value);
        public override string ToString() => $"{Position}: {Token}";
    }

    private class Operand : Term
    {
        public double Value { get; }
        public Operand(double value) => Value = value;
        public override string ToString() => $"{Position}: {Value}";
    }

    private class Variable : Term
    {
        public string Name { get; }
        public Variable(string name) => Name = name;
        public override string ToString() => $"{Position}: {Name}";
    }
}

