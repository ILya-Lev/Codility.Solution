using System;
using System.Collections.Generic;
using System.Linq;

namespace LuxoftPolandContest;
public static class Sudoku
{
    public record Position(int Row, int Column)
    {
        public int Square => (Row / 3) * 3 + (Column / 3);
    }

    public record Cell(int Digit, Position Position)
    {
        public bool IsEmpty => Digit == 0;

        public List<int> Missing { get; }
            = Digit == 0
                ? Enumerable.Range(1, 9).ToList()
                : [];

        public override string ToString()
        {
            var suffix = IsEmpty ? string.Join(", ", Missing.Select(d => $"{d}")) : "";
            return $"{Digit}@{Position.Row}X{Position.Column} {suffix}".Trim();
        }
    }

    public record Field(Dictionary<Position, Cell> Cells);

    public const int Size = 9;

    public static Field Create(int[][] digits) => new
    (
        digits
            .SelectMany((row, r) => row.Select((digit, c) => new Cell(digit, new Position(r, c))))
            .ToDictionary(cell => cell.Position, cell => cell)
    );

    public static Field Clone(Field field) => field with
    {
        Cells = field.Cells.ToDictionary(p => p.Key, p => p.Value with { Digit = p.Value.Digit })
    };

    public static Field Solve(Field field)
    {
        var choices = new Stack<(Position, int)>();
        var current = Clone(field);
        while (choices.Any())
        {
            current = SolveNaive(current);
            if (GetEmptyCount(current) == 0)
                return current;

            var cellOfMinOptions = current.Cells.Values.Where(c => c.IsEmpty).MinBy(c => c.Missing.Count);
            foreach (var option in cellOfMinOptions.Missing.ToArray()) //deep copy for safe iteration
            {
                var clone = Clone(current);
                clone.Cells[cellOfMinOptions.Position] = cellOfMinOptions with { Digit = option };
                clone = SolveNaive(clone);
                if (GetEmptyCount(clone) == 0 && IsConsistent(clone))
                    return clone;
            }
        }

        var empty = GetEmptyCount(current);
        return current;
    }

    private static Field SolveNaive(Field field)
    {
        var emptyCount = Size * Size;
        while (emptyCount != GetEmptyCount(field))
        {
            emptyCount = GetEmptyCount(field);
            IterateThroughField(field);
        }
        return field;
    }

    private static int GetEmptyCount(Field field) => field.Cells.Count(c => c.Value.IsEmpty);

    private static bool IsConsistent(Field field)
    {
        var frequencyTable = Enumerable.Range(1, 9).ToDictionary(d => d, d => 0);
        for (int r = 0; r < Size; r++)
        {
            var row = field.Cells.Where(p => p.Key.Row == r && !p.Value.IsEmpty).Select(p => p.Value);
            foreach (var cell in row)
            {
                frequencyTable[cell.Digit]++;
            }
        }
        if (frequencyTable.Values.Any(v => v > 1))
            return false;

        frequencyTable = Enumerable.Range(1, 9).ToDictionary(d => d, d => 0);
        for (int c = 0; c < Size; c++)
        {
            var column = field.Cells.Where(p => p.Key.Column == c && !p.Value.IsEmpty).Select(p => p.Value);
            foreach (var cell in column)
            {
                frequencyTable[cell.Digit]++;
            }
        }
        if (frequencyTable.Values.Any(v => v > 1))
            return false;

        frequencyTable = Enumerable.Range(1, 9).ToDictionary(d => d, d => 0);
        for (int s = 0; s < Size; s++)
        {
            var square = field.Cells.Where(p => p.Key.Square == s && !p.Value.IsEmpty).Select(p => p.Value);
            foreach (var cell in square)
            {
                frequencyTable[cell.Digit]++;
            }
        }
        return !frequencyTable.Values.Any(v => v > 1);
    }

    private static void IterateThroughField(Field field)
    {
        foreach (var position in field.Cells.Keys)//cells we'll replace with clone; so iterate over positions
        {
            var cell = field.Cells[position];
            if (!cell.IsEmpty)
                continue;

            var options = Enumerable.Range(1, 9)
                .Except(field.Cells.Values.Where(RowPredicate(cell)).Select(c => c.Digit))
                .Except(field.Cells.Values.Where(ColumnPredicate(cell)).Select(c => c.Digit))
                .Except(field.Cells.Values.Where(SquarePredicate(cell)).Select(c => c.Digit))
                .ToArray();

            if (options.Length == 0)
                //throw new InvalidOperationException("no options left!");
                return;

            if (options.Length == 1)
            {
                field.Cells[position] = cell with { Digit = options[0] };
                continue;
            }

            var missing = cell.Missing.Intersect(options).ToArray();
            cell.Missing.Clear();
            cell.Missing.AddRange(missing);
        }

        MissingUniqueDigits(field);

        Func<Cell, bool> RowPredicate(Cell cell) => c => !c.IsEmpty && c.Position.Row == cell.Position.Row;
        Func<Cell, bool> ColumnPredicate(Cell cell) => c => !c.IsEmpty && c.Position.Column == cell.Position.Column;
        Func<Cell, bool> SquarePredicate(Cell cell) => c => !c.IsEmpty && c.Position.Square == cell.Position.Square;
    }

    private static void MissingUniqueDigits(Field field)
    {
        for (int r = 0; r < Size; r++)
        {
            var row = field.Cells.Where(p => p.Key.Row == r).Select(p => p.Value).ToArray();
            MissingUniqueDigits(field, row);
        }

        for (int c = 0; c < Size; c++)
        {
            var column = field.Cells.Where(p => p.Key.Column == c).Select(p => p.Value).ToArray();
            MissingUniqueDigits(field, column);
        }

        for (int s = 0; s < Size; s++)
        {
            var square = field.Cells.Where(p => p.Key.Square == s).Select(p => p.Value).ToArray();
            MissingUniqueDigits(field, square);
        }
    }

    private static void IterateThroughField1(Field field)
    {
        for (int r = 0; r < Size; r++)
        {
            var row = field.Cells.Where(p => p.Key.Row == r).Select(p => p.Value).ToArray();
            FillInMissingDigits(field, row);

            row = field.Cells.Where(p => p.Key.Row == r).Select(p => p.Value).ToArray();
            MissingUniqueDigits(field, row);
        }

        for (int c = 0; c < Size; c++)
        {
            var column = field.Cells.Where(p => p.Key.Column == c).Select(p => p.Value).ToArray();
            FillInMissingDigits(field, column);

            column = field.Cells.Where(p => p.Key.Column == c).Select(p => p.Value).ToArray();
            MissingUniqueDigits(field, column);
        }

        for (int s = 0; s < Size; s++)
        {
            var square = field.Cells.Where(p => p.Key.Square == s).Select(p => p.Value).ToArray();
            FillInMissingDigits(field, square);

            square = field.Cells.Where(p => p.Key.Square == s).Select(p => p.Value).ToArray();
            MissingUniqueDigits(field, square);
        }
    }

    private static void MissingUniqueDigits(Field field, Cell[] structure)
    {
        var missing = Enumerable.Range(1, 9).Except(structure.Select(c => c.Digit)).ToHashSet();

        var cellsByMissingDigit = structure.Where(c => c.IsEmpty)
            .SelectMany(c => c.Missing.Where(m => missing.Contains(m)).Select(m => (m, c)))
            .GroupBy(i => i.m, i => i.c)
            .ToDictionary(g => g.Key, g => g.ToArray());

        foreach (var (digit, cells) in cellsByMissingDigit)
        {
            if (cells.Length == 1)
            {
                var cell = cells[0];
                field.Cells[cell.Position] = cell with { Digit = digit };
                continue;
            }
        }
    }

    private static void FillInMissingDigits(Field field, Cell[] structure)
    {
        var missing = Enumerable.Range(1, 9).Except(structure.Select(c => c.Digit)).ToArray();

        if (missing.Length == 0) return;

        if (missing.Length == 1)
        {
            var emptyCell = structure.Single(c => c.IsEmpty);
            field.Cells[emptyCell.Position] = emptyCell with { Digit = missing[0] };
            return;
        }

        foreach (var cell in structure.Where(c => c.IsEmpty))
        {
            var options = cell.Missing.Intersect(missing).ToArray();
            if (options.Length == 1)
            {
                field.Cells[cell.Position] = cell with { Digit = options[0] };
                continue;
            }
            cell.Missing.Clear();
            cell.Missing.AddRange(options);
        }
    }
}
