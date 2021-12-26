using System.Text;

namespace ClassicalProblems;

/// <summary>
/// missioner cannibal puzzle state
/// </summary>
public class MCState
{
    public static int MaxCount { get; set; }
    public int WestMissioners { get; }
    public int WestCannibals { get; }
    public int EastMissioners { get; }
    public int EastCannibals { get; }
    public bool IsBoatOnWest { get; }
        
    public MCState(int westMissioners, int westCannibals, bool westBoat)
    {
        WestMissioners = westMissioners;
        WestCannibals = westCannibals;

        EastMissioners = MaxCount - westMissioners;
        EastCannibals = MaxCount - westCannibals;

        IsBoatOnWest = westBoat;
    }

    public bool Equals(MCState other)
        => WestMissioners == other.WestMissioners 
           && WestCannibals == other.WestCannibals 
           && EastMissioners == other.EastMissioners 
           && EastCannibals == other.EastCannibals 
           && IsBoatOnWest == other.IsBoatOnWest;

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
        return Equals((MCState)obj);
    }

    public override int GetHashCode() => HashCode
        .Combine(WestMissioners, WestCannibals, EastMissioners, EastCannibals, IsBoatOnWest);

    public override string ToString() =>
        $"west: {WestMissioners} m, {WestCannibals} c; east: {EastMissioners} m, {EastCannibals} c;" +
        $" Is boat on west? {IsBoatOnWest}";

    public static string CreateReportForPath(IReadOnlyCollection<MCState> path)
    {
        var steps = path.Select((step, index) => $"step {index + 1:D2}: {step}");
        return string.Join(Environment.NewLine, steps);
    }

    public static bool IsSolved(MCState state) 
        => state.EastMissioners == MCState.MaxCount
           && state.EastCannibals == MCState.MaxCount;

    public static IReadOnlyCollection<MCState> GetNextStates(MCState c)
    {
        var states = new List<MCState>();
        if (c.IsBoatOnWest)
        {
            states.Add(new MCState(c.WestMissioners-2, c.WestCannibals, false));
            states.Add(new MCState(c.WestMissioners-1, c.WestCannibals, false));
            states.Add(new MCState(c.WestMissioners-1, c.WestCannibals-1, false));
            states.Add(new MCState(c.WestMissioners, c.WestCannibals-2, false));
            states.Add(new MCState(c.WestMissioners, c.WestCannibals-1, false));
        }
        else
        {
            states.Add(new MCState(c.WestMissioners+2, c.WestCannibals, true));
            states.Add(new MCState(c.WestMissioners+1, c.WestCannibals, true));
            states.Add(new MCState(c.WestMissioners+1, c.WestCannibals+1, true));
            states.Add(new MCState(c.WestMissioners, c.WestCannibals+2, true));
            states.Add(new MCState(c.WestMissioners, c.WestCannibals+1, true));
        }

        return states.Where(s => s.IsValid()).ToArray();
    }

    private bool IsValid() => WestMissioners >= 0 && WestMissioners <= MaxCount
                           && WestCannibals >= 0 && WestCannibals <= MaxCount
                           && EastMissioners >= 0 && EastMissioners <= MaxCount
                           && EastCannibals >= 0 && EastCannibals <= MaxCount
                           
                           && (WestMissioners >= WestCannibals || WestMissioners == 0)
                           && (EastMissioners >= EastCannibals || EastMissioners == 0);
}