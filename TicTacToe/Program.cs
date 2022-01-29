using static ClassicalProblems.CompetitiveSearch;

public class Program
{
    public static void Main(string[] args)
    {
        Console.WriteLine("Hello, World!");

        var positions = Enumerable.Range(1, 9).Select(_ => TicTacToePiece.E).ToArray();
        var board = new TicTacToeBoard(positions, TicTacToePiece.X);

        var (human, computer) = SelectSide();

        var moveNumber = 0;
        while (board.GetLegalMoves().Any())
        {
            Console.Clear();
            Console.WriteLine($"step#{moveNumber}");
            Console.WriteLine(board.ToString());

            var location = board.GetTurn() == human
                ? DoHumanMove(board, human)
                : DoComputerMove(board);
            moveNumber++;
            board = board.Move(location) as TicTacToeBoard;
        }

        PrintResult(moveNumber, board, human);
    }

    private static (TicTacToePiece human, TicTacToePiece computer) SelectSide()
    {
        Console.WriteLine("Would you like to play with X? [y/n]: ");
        var response = Console.ReadKey();
        var human = response.KeyChar == 'y' || response.KeyChar == 'Y' ? TicTacToePiece.X : TicTacToePiece.O;
        var computer = human.GetOpposite() as TicTacToePiece;
        return (human, computer!);
    }

    private static int DoHumanMove(TicTacToeBoard board, TicTacToePiece human)
    {
        var location = -1;
        var legalMoves = new HashSet<int>(board.GetLegalMoves());
        var availableMoves = string.Join(",", board.GetLegalMoves().Select(m => $"{m}"));
        while (!legalMoves.Contains(location))
        {
            Console.WriteLine($"please, enter where to put next {human} [{availableMoves}]");
            location = int.TryParse(new string(Console.ReadKey().KeyChar, 1), out var loc) ? loc : -1;
        }

        return location;
    }

    private static int DoComputerMove(TicTacToeBoard board) => Solver<int>.FindBestMoveMiniMax(board, 9);

    private static void PrintResult(int moveNumber, TicTacToeBoard board, TicTacToePiece human)
    {
        Console.Clear();
        Console.WriteLine($"After {moveNumber} steps");
        Console.WriteLine(board.ToString());
        if (board.IsWin())
        {
            var winnerName = board.GetTurn() == human ? "computer" : "human";
            Console.WriteLine($"{winnerName} wins!");
        }
        else
        {
            Console.WriteLine($"It's a draw!");
        }
    }
}