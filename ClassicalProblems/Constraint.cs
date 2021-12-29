using System.Text;

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
    protected Constraint(IReadOnlyCollection<V> variables) =>
        Variables = variables.ToArray();

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