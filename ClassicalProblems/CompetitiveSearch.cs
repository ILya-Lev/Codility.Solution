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

    public static class MiniMax
    {
        public static TMove FindBestMove<TMove>(IBoard<TMove> board, int maxDepth)
        {
            TMove bestMove = default;
            var bestEvaluation = double.MinValue;
            var moves = board.GetLegalMoves();
            foreach (var move in moves)
            {
                var evaluation = MiniMaxDepthSearch(board.Move(move), false, board.GetTurn(), maxDepth);
                if (evaluation > bestEvaluation)
                {
                    bestEvaluation = evaluation;
                    bestMove = move;
                }
            }

            return bestMove;
        }

        private static double MiniMaxDepthSearch<TMove>(IBoard<TMove> board
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
    }

    public class TicTacToePiece : IPiece
    {
        public static TicTacToePiece E { get; } = new();
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
}