namespace ClassicalProblems;

public class Statistics
{
    private readonly IReadOnlyList<double> _sequence;

    public Statistics(IEnumerable<double> sequence) => _sequence = sequence.ToArray();

    public double SumUp() => _sequence.Sum();
    public double GetMean() => _sequence.Average();

    /// <summary>
    /// calculates population dispersion; i.e. in average calculation uses denominator = N
    /// </summary>
    public double GetVariance()
    {
        var mean = GetMean();
        return _sequence.Select(x => Math.Pow(x - mean, 2)).Average();
    }

    /// <summary>
    /// calculates selection dispersion (part of the population); i.e. in average calculation uses denominator = N-1
    /// </summary>
    /// <returns>0.0 if teh sequence contains 1 or less items; as N-1 = 0 and we cannot divide</returns>
    public double GetSelectionVariance()
    {
        if(_sequence.Count <= 1) return 0.0;

        var mean = GetMean();
        return _sequence.Select(x => Math.Pow(x - mean, 2)).Sum() / (_sequence.Count - 1);
    }

    public double GetStandardDeviation() => Math.Sqrt(GetVariance());

    /// <summary>
    /// z-score = (x-mean)/std
    /// </summary>
    /// <returns></returns>
    public IReadOnlyList<double> GetZScoredSequence()
    {
        var std = GetStandardDeviation();
        if (std == 0)
            return Enumerable.Repeat(0.0, _sequence.Count).ToArray();

        var mean = GetMean();
        return _sequence.Select(x => (x - mean) / std).ToArray();
    }

    public double GetMin() => _sequence.Min();
    public double GetMax() => _sequence.Max();
}

public class DataPoint
{
    public IReadOnlyList<double> Originals { get; }

    public DataPoint(IEnumerable<double> initials) => Originals = initials.ToArray();

    public double GetDistance(DataPoint other)
    {
        if (other.Originals.Count != Originals.Count)
            throw new ArgumentException($"Expected amount of points is {Originals.Count}, provided is {other?.Originals.Count}");

        var distances = Originals.Zip(other.Originals, (lhs, rhs) => Math.Pow(lhs - rhs, 2)).Sum();
        return Math.Sqrt(distances);
    }

    public override string ToString() => string.Join(", ", Originals.Select(p => $"{p}"));
}