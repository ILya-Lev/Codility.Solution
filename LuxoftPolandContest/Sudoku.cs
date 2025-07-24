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

    public static bool IsConsistent(Field field) => PredicateToStructures(field, IsConsistentStructure);

    private static bool IsConsistentStructure(Cell[] structure) => structure
        .Where(c => !c.IsEmpty)
        .GroupBy(c => c.Digit)
        .All(g => g.Count() == 1);

    private static bool IsConsistentWithStructure(Field field, Position position, int digit)
    {
        var row = field.Cells.Values.Where(c => c.Position.Row == position.Row).ToArray();
        var column = field.Cells.Values.Where(c => c.Position.Column == position.Column).ToArray();
        var square = field.Cells.Values.Where(c => c.Position.Square == position.Square).ToArray();
        
        return IsConsistentStructure(row)
               && IsConsistentStructure(column)
               && IsConsistentStructure(square)
               && row.Where(c => !c.IsEmpty).All(c => c.Digit != digit)
               && column.Where(c => !c.IsEmpty).All(c => c.Digit != digit)
               && square.Where(c => !c.IsEmpty).All(c => c.Digit != digit);
    }

    public static int CountEmptyCells(Field field) => field.Cells.Values.Count(c => c.IsEmpty);

    public static void ApplyToStructures(Field field, Action<Field, Cell[]> action)
    {
        for (var r = 0; r < Size; r++)
        {
            var row = field.Cells.Values.Where(c => c.Position.Row == r).ToArray();
            action(field, row);
        }
        for (var c = 0; c < Size; c++)
        {
            var column = field.Cells.Values.Where(cell => cell.Position.Column == c).ToArray();
            action(field, column);
        }
        for (var s = 0; s < Size; s++)
        {
            var square = field.Cells.Values.Where(c => c.Position.Square == s).ToArray();
            action(field, square);
        }
    }

    public static bool PredicateToStructures(Field field, Func<Cell[], bool> predicate)
    {
        for (var r = 0; r < Size; r++)
        {
            var row = field.Cells.Values.Where(c => c.Position.Row == r).ToArray();
            if (!predicate(row)) return false;
        }
        for (var c = 0; c < Size; c++)
        {
            var column = field.Cells.Values.Where(cell => cell.Position.Column == c).ToArray();
            if (!predicate(column)) return false;
        }
        for (var s = 0; s < Size; s++)
        {
            var square = field.Cells.Values.Where(c => c.Position.Square == s).ToArray();
            if (!predicate(square)) return false;
        }

        return true;
    }

    public static Field Solve(Field field)
    {
        var emptyCounter = Size * Size;
        var attempt = 40;
        while (attempt-- > 0)
        {
            while (emptyCounter != CountEmptyCells(field))
            {
                emptyCounter = CountEmptyCells(field);
                ApplyToStructures(field, ReduceOptionsByFilledDigits);
            }

            if (emptyCounter == 0)
                return field;

            emptyCounter = Size * Size;
            
            ApplyToStructures(field, ReduceOptionsByMissingPair);
            ApplyToStructures(field, ReduceOptionsByFilledDigits);
            ReduceOptionsByTracedRays(field);
            ApplyToStructures(field, ReduceOptionsByFilledDigits);

            if (attempt % 4 == 0 && attempt > 0)
                RefillOptions(field);
        }

        return field;
    }

    private static void ReduceOptionsByFilledDigits(Field field, Cell[] structure)
    {
        var filledInDigits = structure.Where(c => !c.IsEmpty).Select(c => c.Digit).ToArray();
        foreach (var cell in structure.Where(c => c.IsEmpty))
        {
            var missing = cell.Missing.Except(filledInDigits).ToArray();
            if (missing.Length == 1 && IsConsistentWithStructure(field, cell.Position, missing[0]))
            {
                field.Cells[cell.Position] = cell with { Digit = missing[0] };
                return;
            }

            cell.Missing.Clear();
            cell.Missing.AddRange(missing);
        }
    }

    private static void ReduceOptionsByMissingPair(Field field, Cell[] structure)
    {
        var twoCellOptions = structure
            .Where(c => c.IsEmpty)
            .SelectMany(c => c.Missing.Select(m => (m, c)))
            .GroupBy(item => item.m, i => i.c)
            .Where(g => g.Count() == 2)
            .ToArray();

        for (var i = 0; i < twoCellOptions.Length; i++)
        {
            var lhs = twoCellOptions[i].ToHashSet();

            for (int j = i + 1; j < twoCellOptions.Length; j++)
            {
                if (twoCellOptions[j].All(c => lhs.Contains(c)))
                {
                    var optionsPair = new[] { twoCellOptions[i].Key, twoCellOptions[j].Key };

                    lhs.First().Missing.Clear();
                    lhs.First().Missing.AddRange(optionsPair);
                    lhs.Last().Missing.Clear();
                    lhs.Last().Missing.AddRange(optionsPair);

                    break;
                }
            }
        }

        var oneCellOptions = structure
            .Where(c => c.IsEmpty)
            .SelectMany(c => c.Missing.Select(m => (m, c)))
            .GroupBy(item => item.m, i => i.c)
            .Where(g => g.Count() == 1)
            .Select(g => (g.Key, g.Single()))
            .ToArray();

        foreach (var (digit, cell) in oneCellOptions)
        {
            if (IsConsistentWithStructure(field, cell.Position, digit))
                field.Cells[cell.Position] = cell with { Digit = digit };
        }
    }

    private static void ReduceOptionsByTracedRays(Field field)
    {
        for (var s = 0; s < Size; s++)
        {
            var options = field.Cells.Values.Where(c => c.Position.Square == s && c.IsEmpty)
                .SelectMany(c => c.Missing.Select(m => (m, c)))
                .GroupBy(item => item.m, item => item.c)
                .ToArray();

            var columnTrace = options
                .Select(g => (g.Key, g.Select(c => c.Position.Column).Distinct()))
                .Where(item => item.Item2.Count() == 1)
                .Select(item => (item.Key, item.Item2.Single()))
                .ToArray();

            foreach (var (digit, col) in columnTrace)
            {
                var column = field.Cells.Values
                    .Where(c => c.Position.Column == col && c.Position.Square != s && c.IsEmpty)
                    .ToArray();

                foreach (var cell in column)
                {
                    cell.Missing.Remove(digit);
                }
            }

            var rowTrace = options
                .Select(g => (g.Key, g.Select(c => c.Position.Row).Distinct()))
                .Where(item => item.Item2.Count() == 1)
                .Select(item => (item.Key, item.Item2.Single()))
                .ToArray();

            foreach (var (digit, r) in rowTrace)
            {
                var row = field.Cells.Values
                    .Where(c => c.Position.Row == r && c.Position.Square != s && c.IsEmpty)
                    .ToArray();

                foreach (var cell in row)
                {
                    cell.Missing.Remove(digit);
                }
            }
        }
    }

    private static void RefillOptions(Field field)
    {
        var fullOptionsSet = Enumerable.Range(1, 9).ToArray();
        foreach (var cell in field.Cells.Values.Where(c => c.IsEmpty && c.Missing.Count == 0))
        {
            cell.Missing.AddRange(fullOptionsSet);
        }
    }
}
