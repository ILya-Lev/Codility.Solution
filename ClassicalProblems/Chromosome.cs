using System.Text;

namespace ClassicalProblems;

public interface IChromosome<T> : IComparable<T> where T : IChromosome<T>
{
    /// <summary> what amount of stamina does the chromosome have </summary>
    /// <returns></returns>
    double GetFitness();

    /// <summary> create a child as a combination of this and the other chromosomes </summary>
    IReadOnlyList<T> Crossover(T other);
    
    /// <summary> add random changes into the given chromosome </summary>
    void Mutate();

    T Copy();

    int IComparable<T>.CompareTo(T? other) => GetFitness().CompareTo(other?.GetFitness() ?? double.NaN);
}

public interface IParentSelectionStrategy<C> where C : IChromosome<C>
{
    IReadOnlyList<C> PickParents(IReadOnlyList<C> population);
}

/// <summary> each chromosome has a chance to be selected; the chance is proportional to its fitness </summary>
public class RouletteParentSelectionStrategy<C> : IParentSelectionStrategy<C> where C : IChromosome<C>
{
    private readonly Random _randomGenerator = new Random(DateTime.UtcNow.Millisecond);

    public IReadOnlyList<C> PickParents(IReadOnlyList<C> population)
    {
        var totalFitness = population.Sum(c => c.GetFitness());
        var wheel = population.Select(c => c.GetFitness() / totalFitness).ToArray();
        return RoulettePick(wheel, 2, population);
    }

    private List<C> RoulettePick(double[] wheel, int picksAmount, IReadOnlyList<C> population)
    {
        ValidateRoulette(wheel, population);

        var picks = new List<C>();

        for (int pickCounter = 0; pickCounter < picksAmount; pickCounter++)
        {
            var pick = _randomGenerator.NextDouble();
            for (int sector = 0; sector < wheel.Length; sector++)
            {
                pick -= wheel[sector];
                if (pick <= 0)
                {
                    picks.Add(population[sector]);
                    break;
                }
            }
        }

        return picks;
    }

    private void ValidateRoulette(double[] wheel, IReadOnlyList<C> population)
    {
        if (population.Count < wheel.Length)
            throw new Exception($"{nameof(population)} should contain" +
                                $" as many items as {nameof(wheel)} " +
                                $"but {population.Count} is less than {wheel.Length}");
    }
}

/// <summary> randomly select some chromosomes and let the strongest to win </summary>
public class TournamentParentSelectionStrategy<C> : IParentSelectionStrategy<C> where C : IChromosome<C>
{
    private readonly Random _randomGenerator = new Random(DateTime.UtcNow.Millisecond);

    public IReadOnlyList<C> PickParents(IReadOnlyList<C> population)
    {
        return TournamentPick(population.Count / 2, 2, population);
    }

    private List<C> TournamentPick(int participantsAmount, int picksAmount, IReadOnlyList<C> population)
    {
        ValidateTournament(participantsAmount, picksAmount, population);

        var participantIndexes = new HashSet<int>();
        while (participantIndexes.Count < participantsAmount)
            participantIndexes.Add(_randomGenerator.Next(0, population.Count));

        return participantIndexes
            .Select(i => population[i])
            .OrderByDescending(c => c)      //more fit are going first
            .Take(picksAmount)
            .ToList();
    }
    
    private void ValidateTournament(int participantsAmount, int picksAmount, IReadOnlyList<C> population)
    {
        if (population.Count < participantsAmount)
            throw new Exception($"{nameof(participantsAmount)} should be" +
                                $" less than or equal to " +
                                $"{population.Count}, while is {participantsAmount}");

        if (participantsAmount < picksAmount)
            throw new Exception($"{nameof(participantsAmount)} {participantsAmount}" +
                                $" should NOT be less than {nameof(picksAmount)} {picksAmount}");
    }
}

public class GeneticAlgorithm<C> where C : IChromosome<C>
{
    private readonly List<C> _population;
    private readonly double _mutationChance;
    private readonly double _crossoverChance;
    private readonly IParentSelectionStrategy<C> _parentSelectionStrategy;
    private readonly Random _randomGenerator = new Random();

    public GeneticAlgorithm(IReadOnlyCollection<C> initialPopulation
        , double mutationChance, double crossoverChance
        , IParentSelectionStrategy<C> parentSelectionStrategy)
    {
        _population = initialPopulation.ToList();
        _mutationChance = mutationChance;
        _crossoverChance = crossoverChance;//probability of 2 parents to have child != exact copy of them
        _parentSelectionStrategy = parentSelectionStrategy;
    }

    ///<summary>
    ///returns not only the best chromosome, but also statistics report per generation
    ///shall visitor be used for this purpose?
    ///</summary>
    public (C, string) Run(int maxGenerations, double fitnessThreshold)
    {
        var stats = new StringBuilder();
        var best = _population.MaxBy(c => c.GetFitness())!;
        for (int generation = 0; generation < maxGenerations; generation++)
        {
            if (best.GetFitness() >= fitnessThreshold)
                return (best, stats.ToString());

            stats.AppendLine($"Generation {generation}, the best fitness so far {best.GetFitness()}," +
                             $" an average fitness {_population.Average(c => c.GetFitness())}");

            ReproduceAndReplace();
            Mutate();
            var highest = _population.MaxBy(c => c.GetFitness())!;
            if (highest.GetFitness() > best.GetFitness())
                best = highest;
        }

        return (best, stats.ToString());
    }

    private void Mutate()
    {
        foreach (var chromosome in _population)
        {
            if (_randomGenerator.NextDouble() < _mutationChance)
                chromosome.Mutate();
        }
    }

    private void ReproduceAndReplace()
    {
        var nextGeneration = new List<C>();

        while (nextGeneration.Count < _population.Count)
        {
            //won't we select the same parents over and over again?
            var parents = _parentSelectionStrategy.PickParents(_population);

            var survivals = _randomGenerator.NextDouble() < _crossoverChance
                ? parents[0].Crossover(parents[1])
                : parents;

            nextGeneration.AddRange(survivals);
        }

        var itemsToSkip = nextGeneration.Count - _population.Count;
        _population.Clear();
        _population.AddRange(nextGeneration.Skip(itemsToSkip));
    }
}

//--------------------applications---------------------------------------------------

public class SimpleEquation : IChromosome<SimpleEquation>
{
    private const int MaxValue = 100;
    private static readonly Random _random = new Random(DateTime.UtcNow.Millisecond);

    public int X { get; private set; }
    public int Y { get; private set; }

    public SimpleEquation(int x, int y) => (X, Y) = (x, y);

    public double GetFitness() => 6 * X - X * X + 4 * Y - Y * Y;

    public IReadOnlyList<SimpleEquation> Crossover(SimpleEquation other) => new[]
    {
        new SimpleEquation(X, other.Y),
        new SimpleEquation(other.X, Y),
    };

    public void Mutate()
    {
        if (_random.NextDouble() > 0.5)
            X += _random.NextDouble() > 0.5 ? 1 : -1;
        else    
            Y += _random.NextDouble() > 0.5 ? 1 : -1;
    }

    public SimpleEquation Copy() => new SimpleEquation(X, Y);

    public override string ToString() => $"X {X}, Y {Y}, Fitness {GetFitness()}";

    public static SimpleEquation GetRandomInstance()
    {
        return new SimpleEquation(_random.Next(0, MaxValue), _random.Next(0, MaxValue));
    }
}