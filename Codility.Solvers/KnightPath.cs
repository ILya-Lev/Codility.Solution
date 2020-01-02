using MoreLinq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Codility.Solvers.Knight
{
    public class Board
    {
        private readonly TextWriter _output;
        public const int Size = 8;
        private readonly int[,] _field;

        public Board(TextWriter output)
        {
            _output = output;
            _field = new int[Size, Size];
        }

        public int this[int r, int c]
        {
            get => _field[r, c];
            set => _field[r, c] = value;
        }

        public void Print(TextWriter writer = null)
        {
            writer = writer ?? _output;
            if (writer is null) return;

            for (int i = 0; i < Size; i++)
            {
                for (int j = 0; j < Size; j++)
                {
                    writer.Write($"{this[i, j]:D2} ");
                }
                writer.Write(Environment.NewLine);
            }
            writer.Write(Environment.NewLine);
            writer.Write(Environment.NewLine);
        }

        public bool ContainUnvisitedCells()
        {
            for (int i = 0; i < Size; i++)
            {
                for (int j = 0; j < Size; j++)
                {
                    if (this[i, j] >= 0)
                        return true;
                }
            }

            return false;
        }

        public bool IsWithin(int row, int column) => IsInRange(row) && IsInRange(column);

        private static bool IsInRange(int coordinate) => 0 <= coordinate && coordinate < Size;
    }

    public static class FigureStep
    {
        public static IReadOnlyList<(int r, int c)> KnightSteps(int row, int column)
        {
            return new[]
            {
                (row-1, column-2),
                (row-2, column-1),
                (row-2, column+1),
                (row-1, column+2),
                (row+1, column+2),
                (row+2, column+1),
                (row+2, column-1),
                (row+1, column-2),
            };
        }
    }

    public class KnightPath
    {
        private readonly Board _board;
        private readonly Func<int, int, IReadOnlyList<(int r, int c)>> _figureSteps;

        /// <summary> start position </summary>
        public KnightPath(Board board, Func<int, int, IReadOnlyList<(int, int)>> figureSteps)
        {
            _board = board;
            _figureSteps = figureSteps ?? FigureStep.KnightSteps;
        }

        public IReadOnlyList<(int r, int c)> Traverse(in int startRow, in int startColumn)
        {
            InitializeBoard();
            var stepNumber = 1;
            var steps = new List<(int r, int c)>(Board.Size * Board.Size);

            PutKnight(startRow, startColumn, stepNumber);
            steps.Add((startRow, startColumn));

            var r = startRow;
            var c = startColumn;

            while (stepNumber < Board.Size * Board.Size && _board.ContainUnvisitedCells())
            {
                (int nextRow, int nextColumn) = FindBestPosition(r, c);
                r = nextRow;
                c = nextColumn;

                steps.Add((r, c));
                PutKnight(r, c, ++stepNumber);
            }

            return steps;
        }

        private (int, int) FindBestPosition(in int r, in int c)
        {
            try
            {
                var nextPosition = GetValidSteps(r, c)
                    .Select(p => new
                    {
                        FurtherStepsCount = _board[p.r, p.c],
                        //OneMoreFurtherStepCount = GetValidSteps(p.r, p.c)
                        //    .Where(coord => coord.r != p.r || coord.c != p.c)
                        //    .Select(coord => _board[coord.r, coord.c])
                        //    .Min(),
                        Row = p.r,
                        Column = p.c
                    })
                    .MinBy(item => item.FurtherStepsCount/* + item.OneMoreFurtherStepCount*/)
                    .First();

                return (nextPosition.Row, nextPosition.Column);
            }
            catch (Exception exc)
            {
                throw;
            }
        }

        private IEnumerable<(int r, int c)> GetValidSteps(int r, int c)
        {
            return _figureSteps(r, c)
                .Where(p => _board.IsWithin(p.r, p.c))
                .Where(p => _board[p.r, p.c] >= 0);
        }

        private void InitializeBoard()
        {
            _board.Print();
            for (int i = 0; i < Board.Size; i++)
                for (int j = 0; j < Board.Size; j++)
                {
                    var value = _figureSteps(i, j)
                        .Count(p => _board.IsWithin(p.r, p.c));

                    _board[i, j] = value;
                }
            _board.Print();
        }

        private void PutKnight(int row, int column, int stepNumber)
        {
            _board[row, column] = -Math.Abs(stepNumber);

            for (int i = -3; i <= 3; i++)
                for (int j = -3; j <= 3; j++)
                {
                    var r = row + i;
                    var c = column + j;
                    if (!_board.IsWithin(r, c) || _board[r, c] < 0)
                        continue;

                    var value = _figureSteps(r, c)
                        .Where(p => _board.IsWithin(p.r, p.c))
                        .Count(p => _board[p.r, p.c] >= 0);

                    _board[r, c] = value;
                }
            _board.Print();
        }
    }
}