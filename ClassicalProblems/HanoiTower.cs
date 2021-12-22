namespace ClassicalProblems;

public class HanoiTower
{
    public const string Origin = "A";
    public const string Buffer = "B";
    public const string Destination = "C";

    public static IReadOnlyCollection<string> MoveBlocks(int amount)
    {
        var steps = new List<string>();

        DoMoveBlocks(steps, amount, Origin, Buffer, Destination);

        return steps;
    }

    private static void DoMoveBlocks(List<string> steps, int amount, string origin, string buffer, string destination)
    {
        if (amount == 1)
        {
            steps.Add($"{1:D3} {origin}->{destination}");
            return;
        }

        DoMoveBlocks(steps, amount-1, origin, destination, buffer);

        steps.Add($"{amount:D3} {origin}->{destination}");

        DoMoveBlocks(steps, amount-1, buffer, origin, destination);
    }
}