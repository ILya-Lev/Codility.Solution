using System.Runtime.CompilerServices;
using System.Text;

namespace ClassicalProblems;

public static class CompetitiveSearch
{
    public interface IPiece { IPiece GetOpposite(); }

    /// <summary> state design pattern </summary>
    /// <typeparam name="TMove">for TicTacToe is a cell number - where to put a symbol</typeparam>
    public interface IBoard<TMove>
    {
        IPiece GetTurn();
        IBoard<TMove> Move(TMove location);
        IReadOnlyList<TMove> GetLegalMoves();
        bool IsWin();
        bool IsDraw() => !IsWin() && GetLegalMoves().Count == 0;

        /// <summary>
        /// analyze the position to understand which player dominates at the moment
        /// </summary>
        double Evaluate(IPiece player);
    }

    //todo: think over memoization technique to increase performance
    public static class Solver<TMove>
    {
        public static TMove FindBestMoveMiniMax(IBoard<TMove> board, int maxDepth)
        {
            return DoFindBestMove(board, (b, p) => MiniMaxDepthSearch(b, false, p, maxDepth));
        }

        public static TMove FindBestMoveAlphaBeta(IBoard<TMove> board, int maxDepth)
        {
            return DoFindBestMove(board
                , (b, p) => AlphaBetaDepthSearch(b, false, p, maxDepth, double.MinValue, double.MaxValue));
        }

        private static TMove DoFindBestMove(IBoard<TMove> board, Func<IBoard<TMove>, IPiece, double> getBestScore)
        {
            TMove bestMove = default;
            var bestEvaluation = double.MinValue;
            var moves = board.GetLegalMoves();
            foreach (var move in moves)
            {
                var evaluation = getBestScore(board.Move(move), board.GetTurn());
                if (evaluation > bestEvaluation)
                {
                    bestEvaluation = evaluation;
                    bestMove = move;
                }
            }

            return bestMove;
        }
        
        private static double MiniMaxDepthSearch(IBoard<TMove> board
            , bool isMaximizing
            , IPiece originalPlayer
            , int remainingDepth)
        {
            if (board.IsWin() || board.IsDraw() || remainingDepth == 0)
                return board.Evaluate(originalPlayer);

            var moves = board.GetLegalMoves();

            var finalScore = isMaximizing ? double.MinValue : double.MaxValue;
            foreach (var move in moves)
            {
                var evaluation = MiniMaxDepthSearch(board.Move(move), !isMaximizing, originalPlayer, remainingDepth - 1);
                finalScore = isMaximizing
                    ? Math.Max(finalScore, evaluation)
                    : Math.Min(finalScore, evaluation);
            }
            return finalScore;
        }
    
        private static double AlphaBetaDepthSearch(IBoard<TMove> board, bool isMaximizing, IPiece originalPlayer, int maxDepth
            , double alpha, double beta)
        {
            if (board.IsWin() || board.IsDraw() || maxDepth <= 0)
                return board.Evaluate(originalPlayer);

            if (isMaximizing)
            {
                foreach (var move in board.GetLegalMoves())
                {
                    alpha = Math.Max(alpha,
                        AlphaBetaDepthSearch(board.Move(move), !isMaximizing, originalPlayer, maxDepth - 1, alpha, beta));
                    if (beta <= alpha)//this branch won't give better result
                        break;
                }

                return alpha;
            }
            else
            {
                foreach (var move in board.GetLegalMoves())
                {
                    beta = Math.Min(beta,
                        AlphaBetaDepthSearch(board.Move(move), !isMaximizing, originalPlayer, maxDepth - 1, alpha, beta));
                    if (beta <= alpha)//this branch won't give better result
                        break;
                }

                return beta;
            }
        }
    }

    public class TicTacToePiece : IPiece
    {
        public static TicTacToePiece E { get; } = new(" ");
        public static TicTacToePiece X { get; } = new("X");
        public static TicTacToePiece O { get; } = new("O");

        private readonly string _name;

        private TicTacToePiece(string name = "") => _name = name;

        public IPiece GetOpposite() =>
            _name == X._name
            ? O
            : _name == O._name
                ? X
                : E;

        public override string ToString() => _name;
    }

    public class TicTacToeBoard : IBoard<int>
    {
        private const int CellAmount = 9;

        private readonly TicTacToePiece[] _positions;
        private readonly TicTacToePiece _turn;

        //the very first turn game state
        public TicTacToeBoard()
        {
            _positions = Enumerable.Range(1, CellAmount).Select(_ => TicTacToePiece.E).ToArray();//the field is empty
            _turn = TicTacToePiece.X;//first player puts X
        }

        public TicTacToeBoard(TicTacToePiece[] positions, TicTacToePiece turn)
        {
            _positions = positions;
            _turn = turn ?? throw new ArgumentNullException(nameof(turn));
        }

        public IPiece GetTurn() => _turn;

        public IBoard<int> Move(int location)
        {
            var nextPositions = _positions.ToArray();//deep copy
            nextPositions[location] = _turn;//I'd add check whether the location is not occupied yet
            return new TicTacToeBoard(nextPositions, _turn.GetOpposite() as TicTacToePiece);
        }

        public IReadOnlyList<int> GetLegalMoves()
        {
            if (IsWin())
                return Array.Empty<int>();

            return _positions
                .Select((p, i) => (p, i))
                .Where(t => t.p == TicTacToePiece.E)
                .Select(t => t.i).ToArray();
        }

        public bool IsWin() => CheckIfPlayerWins(_turn) || CheckIfPlayerWins(_turn.GetOpposite() as TicTacToePiece);

        public double Evaluate(IPiece player)
        {
            if (!IsWin()) return 0;

            //-1 if _turn == player as the method is called for the opposite player when the original one has just won
            return _turn == player ? -1 : 1;
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            for (int row = 0; row < 3; row++)
            {
                for (int col = 0; col < 3; col++)
                {
                    sb.Append(_positions[row * 3 + col].ToString());
                    if (col != 2)
                        sb.Append("|");
                }

                sb.AppendLine();
                if (row != 2)
                    sb.AppendLine("-----");
            }

            return sb.ToString();
        }

        private bool CheckIfPlayerWins(TicTacToePiece player)
        {
            var rowMatch = _positions.Select((p, i) => (p, i))
                .GroupBy(t => t.i / 3, t => t.p) //group cells of the same row
                .Select(g => g.All(p => p == player))
                .Any(g => g);

            if (rowMatch) return true;

            var colMatch = _positions.Select((p, i) => (p, i))
                .GroupBy(t => t.i % 3, t => t.p) //group cells of the same col
                .Select(g => g.All(p => p == player))
                .Any(g => g);

            if (colMatch) return true;

            var mainDiagonalMatch = new[] { 0, 4, 8 }.All(i => _positions[i] == player);
            if (mainDiagonalMatch)
                return true;

            var secondaryDiagonalMatch = new[] { 2, 4, 6 }.All(i => _positions[i] == player);
            if (secondaryDiagonalMatch)
                return true;

            return false;
        }
    }

    public class ConnectFourPiece : IPiece
    {
        private readonly string _name;
        public static IPiece Black { get; } = new ConnectFourPiece("B");
        public static IPiece Red { get; } = new ConnectFourPiece("R");
        public static IPiece Empty { get; } = new ConnectFourPiece(" ");

        private ConnectFourPiece(string name) => _name = name;

        public IPiece GetOpposite() =>
            this == Black
                ? Red
                : this == Red
                    ? Black
                    : Empty;

        public override string ToString() => _name;
    }

    //7 columns and 6 rows
    public class ConnectFourBoard : IBoard<int>
    {
        public const int CellAmount = 42;
        private const int Width = 7;
        private const int WinningSet = 4;

        private static IReadOnlyList<Segment> Segments { get; }

        private readonly ConnectFourPiece[] _positions;
        private readonly ConnectFourPiece _turn;

        static ConnectFourBoard()
        {
            Segments = Enumerable.Range(0, CellAmount - 1)
                .SelectMany(location => new[]
                {
                    Segment.CreateInRow(location),
                    Segment.CreateInColumn(location),
                    Segment.CreateInMainDiagonal(location),
                    Segment.CreateInMinorDiagonal(location),
                })
                .Where(s => s is not null)
                .Distinct()
                .ToArray()!;
        }


        //the very first turn game state
        public ConnectFourBoard()
        {
            _positions = Enumerable.Range(1, CellAmount).Select(_ => (ConnectFourPiece)ConnectFourPiece.Empty).ToArray();//the field is empty
            _turn = ConnectFourPiece.Black as ConnectFourPiece;//first player puts Black pieces
        }

        public ConnectFourBoard(ConnectFourPiece[] positions, ConnectFourPiece turn)
        {
            _positions = positions;
            _turn = turn ?? throw new ArgumentNullException(nameof(turn));
        }

        public IPiece GetTurn() => _turn;

        public IBoard<int> Move(int column)
        {
            var nextPositions = _positions.ToArray();//deep copy
            var firstEmptyRow = FindEmptyRow(column);
            var location = GetLocation(firstEmptyRow, column);
            nextPositions[location] = _turn;
            return new ConnectFourBoard(nextPositions, _turn.GetOpposite() as ConnectFourPiece);
        }

        public IReadOnlyList<int> GetLegalMoves()
        {
            if (IsWin())
                return Array.Empty<int>();

            var topRowPieceWithIndex = _positions.TakeLast(Width).Select((p, i) => (p, i));

            var topRowEmptyPieceColumns = topRowPieceWithIndex
                .Where(t => t.p == ConnectFourPiece.Empty)
                .Select(t => t.i)
                .ToArray();

            return topRowEmptyPieceColumns;
        }

        public bool IsWin()
        {
            return Segments.Any(s => AreAllTheSameFill(s, ConnectFourPiece.Black as ConnectFourPiece) 
                                  || AreAllTheSameFill(s, ConnectFourPiece.Red as ConnectFourPiece));
        }

        public double Evaluate(IPiece player) => Segments.Sum(EvaluateSegment);

        public override string ToString()
        {
            var sb = new StringBuilder();
            for (int row = 0; row < CellAmount/Width; row++)
            {
                for (int col = 0; col < Width; col++)
                {
                    var location = row * Width + col;
                    sb.Append(_positions[location]);
                    sb.Append("|");
                }

                sb.AppendLine();
            }

            //print col indexes
            for (int col = 0; col < Width; col++)
            {
                sb.Append(col);
                sb.Append("|");
            }

            sb.AppendLine();

            return sb.ToString();
        }

        private double EvaluateSegment(Segment segment)
        {
            var blackCount = GetMatchingCount(segment, ConnectFourPiece.Black as ConnectFourPiece);
            var redCount = GetMatchingCount(segment, ConnectFourPiece.Red as ConnectFourPiece);

            if (blackCount > 0 && redCount > 0) return 0.0;

            var score = Math.Max(blackCount, redCount) switch
            {
                2 => 1.0,
                3 => 100.0,
                4 => 1_000_000.0,
                _ => 0.0
            };

            var negate = (blackCount > redCount && _turn == ConnectFourPiece.Red)
                      || (blackCount < redCount && _turn == ConnectFourPiece.Black);
            
            return negate ? -score : score;
        }

        private bool AreAllTheSameFill(Segment segment, ConnectFourPiece piece)
        {
            return GetMatchingCount(segment, piece) == segment.Locations.Count;
        }

        private int GetMatchingCount(Segment segment, ConnectFourPiece piece)
        {
            return segment.Locations.Count(l => _positions[l] == piece);
        }

        private int FindEmptyRow(int column, [CallerMemberName] string caller = "")
        {
            for (int row = 0; row < CellAmount / Width; row++)
            {
                var location = GetLocation(row, column);
                if (_positions[location] == ConnectFourPiece.Empty)
                    return row;
            }

            throw new ArgumentException($"Column {column} is already full - {caller} should have not been called for it!");
        }

        private static int GetLocation(int row, int column) => row * Width + column;

        private class Segment
        {
            private const int CoordinateShift = 3;

            public IReadOnlyList<int> Locations { get; }

            private Segment(int[] locations) => Locations = locations;

            public static Segment? CreateInRow(int location)
            {
                var endColumn = location % Width + CoordinateShift;
                if (endColumn >= Width) return null;

                var locations = Enumerable.Range(location, CoordinateShift + 1).ToArray();

                return new Segment(locations);
            }

            public static Segment? CreateInColumn(int location)
            {
                var endRow = location / Width + CoordinateShift;
                if (endRow >= CellAmount / Width) return null;

                var col = location % Width;
                var row = location / Width;
                var locations = Enumerable.Range(0, CoordinateShift + 1).Select(shift => (row + shift) * Width + col).ToArray();

                return new Segment(locations);
            }

            public static Segment? CreateInMainDiagonal(int location)
            {
                var endRow = location / Width + CoordinateShift;
                if (endRow >= CellAmount / Width) return null;

                var endColumn = location % Width + CoordinateShift;
                if (endColumn >= Width) return null;

                var row = location / Width;
                var col = location % Width;
                
                var locations = Enumerable.Range(0, CoordinateShift + 1).Select(shift => (row + shift) * Width + (col+shift)).ToArray();

                return new Segment(locations);
            }

            public static Segment? CreateInMinorDiagonal(int location)
            {
                var endRow = location / Width + CoordinateShift;
                if (endRow >= CellAmount / Width) return null;

                var endColumn = location % Width - CoordinateShift;
                if (endColumn >= Width) return null;

                var row = location / Width;
                var col = location % Width;
                
                var locations = Enumerable.Range(0, CoordinateShift + 1).Select(shift => (row + shift) * Width + (col-shift)).ToArray();

                return new Segment(locations);
            }
        }
    }
}