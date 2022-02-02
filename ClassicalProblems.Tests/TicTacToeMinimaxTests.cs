using FluentAssertions;
using Xunit;
using Xunit.Abstractions;
using static ClassicalProblems.CompetitiveSearch;

namespace ClassicalProblems.Tests;

public class TicTacToeMiniMaxTests
{
    private readonly ITestOutputHelper _output;

    public TicTacToeMiniMaxTests(ITestOutputHelper output)
    {
        _output = output;
    }

    [Fact]
    public void FindBestMove_OneStepToWin_Cell6()
    {
        var positions = new[]
        {
            TicTacToePiece.X, TicTacToePiece.O, TicTacToePiece.X,
            TicTacToePiece.X, TicTacToePiece.E, TicTacToePiece.O,
            TicTacToePiece.E, TicTacToePiece.E, TicTacToePiece.O,
        };
        var board = new TicTacToeBoard(positions, TicTacToePiece.X);

        var winningPosition = Solver<int>.FindBestMoveMiniMax(board, 8);

        winningPosition.Should().Be(6);
    }

    [Fact]
    public void FindBestMove_BlockOpponentNotToLoose_Cell2()
    {
        var positions = new[]
        {
            TicTacToePiece.X, TicTacToePiece.E, TicTacToePiece.E,
            TicTacToePiece.E, TicTacToePiece.E, TicTacToePiece.O,
            TicTacToePiece.E, TicTacToePiece.X, TicTacToePiece.O,
        };
        var board = new TicTacToeBoard(positions, TicTacToePiece.X);

        var winningPosition = Solver<int>.FindBestMoveMiniMax(board, 8);

        winningPosition.Should().Be(2);
    }

    [Fact]
    public void FindBestMove_TwoStepsToWin_Cells1or4()
    {
        var positions = new[]
        {
            TicTacToePiece.X, TicTacToePiece.E, TicTacToePiece.E,
            TicTacToePiece.E, TicTacToePiece.E, TicTacToePiece.O,
            TicTacToePiece.O, TicTacToePiece.X, TicTacToePiece.E,
        };
        var board = new TicTacToeBoard(positions, TicTacToePiece.X);

        var winningPosition = Solver<int>.FindBestMoveMiniMax(board, 8);

        winningPosition.Should().BeOneOf(new[] { 1, 4 });
        _output.WriteLine($"winning move is {winningPosition}");
    }
}