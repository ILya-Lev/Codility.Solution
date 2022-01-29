using static ClassicalProblems.CompetitiveSearch;

public class Program
{
    public static void Main(string[] args)
    {
        Console.WriteLine("Hello, World!");

        var positions = Enumerable.Range(1, ConnectFourBoard.CellAmount).Select(_ => (ConnectFourPiece)ConnectFourPiece.Empty).ToArray();
        var board = new ConnectFourBoard(positions, (ConnectFourPiece)ConnectFourPiece.Black);

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
         
            Console.WriteLine($"the move is to put {board.GetTurn()} into column {location}");

            moveNumber++;
            board = board.Move(location) as ConnectFourBoard;
        }

        PrintResult(moveNumber, board, human);
    }

    private static (ConnectFourPiece human, ConnectFourPiece computer) SelectSide()
    {
        Console.WriteLine("Would you like to play with black pieces? [y/n]: ");
        var response = Console.ReadKey();
        var human = response.KeyChar == 'y' || response.KeyChar == 'Y' ? ConnectFourPiece.Black : ConnectFourPiece.Red;
        var computer = human.GetOpposite() as ConnectFourPiece;
        return ((ConnectFourPiece)human, computer!);
    }

    private static int DoHumanMove(ConnectFourBoard board, ConnectFourPiece human)
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

    private static int DoComputerMove(ConnectFourBoard board) => Solver<int>.FindBestMoveAlphaBeta(board, 6);

    private static void PrintResult(int moveNumber, ConnectFourBoard board, ConnectFourPiece human)
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
            Console.WriteLine("It's a draw!");
        }
    }
}

/*
 *
 * max depth = 5
After 33 steps
B|B|B|R|R|R|B|
R|B|R|B| |R|B|
R|B|B|R| |B|R|
R|R|B|B| |R|B|
B|R|B| | |R| |
B|R|R| | |B| |
      ^           the winning move
human wins!
*/