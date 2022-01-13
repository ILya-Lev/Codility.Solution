using System.IO.Compression;
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

    #region genes
    public int X { get; private set; }
    public int Y { get; private set; }
    #endregion genes

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

public class SendMoreMoney : IChromosome<SendMoreMoney>
{
    private static readonly Random _random = new(DateTime.UtcNow.Millisecond);

    #region genes
    public char[] Variables { get; }
    #endregion genes

    public SendMoreMoney(IReadOnlyCollection<char> variables) => Variables = variables.ToArray();//deep copy

    public double GetFitness()
    {
        var s = GetValue('s');
        var m = GetValue('m');

        var (send, more, money) = GetNumbers();

        var difference = Math.Abs(money - send - more);

        difference += s == 0 ? 1000 : 0;        //avoid numbers starting from 0
        difference += m == 0 ? 1000 : 0;

        return 1.0 / (difference + 1);
    }

    public IReadOnlyList<SendMoreMoney> Crossover(SendMoreMoney other)
    {
        var lhs = Variables;
        var rhs = other.Variables;

        var idx1 = _random.Next(0, lhs.Length);
        var idx2 = _random.Next(0, rhs.Length);

        var ch1 = lhs[idx1];
        var ch2 = rhs[idx2];

        var idx3 = lhs.Select((c, i) => (c, i)).First(element => element.c == ch2).i;
        var idx4 = rhs.Select((c, i) => (c, i)).First(element => element.c == ch1).i;

        (lhs[idx1], lhs[idx3]) = (lhs[idx3], lhs[idx1]);
        (rhs[idx2], rhs[idx4]) = (rhs[idx4], rhs[idx2]);

        return new[]
        {
            new SendMoreMoney(lhs),
            new SendMoreMoney(rhs),
        };
    }

    public void Mutate()
    {
        var lhs = _random.Next(0, Variables.Length);
        var rhs = _random.Next(0, Variables.Length);

        (Variables[lhs], Variables[rhs]) = (Variables[rhs], Variables[lhs]);
    }

    public SendMoreMoney Copy() => new(Variables);

    public override string ToString()
    {
        var (send, more, money) = GetNumbers();
        return
            $"{string.Join(", ", Variables.Select((c, i) => $"{c}={i}"))}, {send} + {more} = {money}, difference {money - send - more}, Fitness {GetFitness()}";
    }

    public static SendMoreMoney GetRandomInstance()
    {
        var chars = "sendmoremoney".ToCharArray().Distinct()//representing the phrase as a set of distinct characters
            .Concat(new[] { ' ', ' ' })//adding 2 spaces to cover 10 digits instead of 8
            .ToArray();

        //create a permutation
        for (int i = 0; i < chars.Length; i++)
        {
            var lhs = _random.Next(0, chars.Length);
            var rhs = _random.Next(0, chars.Length);

            (chars[lhs], chars[rhs]) = (chars[rhs], chars[lhs]);
        }

        return new SendMoreMoney(chars);
    }

    private (int a, int b, int c) GetNumbers()
    {
        var s = GetValue('s');//index of the char is its value; 8 chars + 2 spaces to ignore
        var e = GetValue('e');
        var n = GetValue('n');
        var d = GetValue('d');
        var m = GetValue('m');
        var o = GetValue('o');
        var r = GetValue('r');
        var y = GetValue('y');

        var send = s * 1_000 + e * 100 + n * 10 + d;
        var more = m * 1_000 + o * 100 + r * 10 + e;
        var money = m * 10_000 + o * 1_000 + n * 100 + e * 10 + y;

        return (send, more, money);
    }
    private int GetValue(char ch) => Variables.Select((c, i) => (c, i)).Single(element => element.c == ch).i;
}

public class ListCompression : IChromosome<ListCompression>
{
    private static readonly Random _random = new(DateTime.UtcNow.Millisecond);

    #region genes
    public string[] Words { get; }
    #endregion genes

    public ListCompression(IReadOnlyCollection<string> words) => Words = words.ToArray();//deep copy

    public double GetFitness() => 1.0 / GetSizeInBytes();

    public IReadOnlyList<ListCompression> Crossover(ListCompression other)
    {
        var lhs = Words;
        var rhs = other.Words;

        var idx1 = _random.Next(0, lhs.Length);
        var idx2 = _random.Next(0, rhs.Length);

        var ch1 = lhs[idx1];
        var ch2 = rhs[idx2];

        var idx3 = lhs.Select((c, i) => (c, i)).First(element => element.c == ch2).i;
        var idx4 = rhs.Select((c, i) => (c, i)).First(element => element.c == ch1).i;

        (lhs[idx1], lhs[idx3]) = (lhs[idx3], lhs[idx1]);
        (rhs[idx2], rhs[idx4]) = (rhs[idx4], rhs[idx2]);

        return new[]
        {
            new ListCompression(lhs),
            new ListCompression(rhs),
        };
    }

    public void Mutate()
    {
        var lhs = _random.Next(0, Words.Length);
        var rhs = _random.Next(0, Words.Length);

        (Words[lhs], Words[rhs]) = (Words[rhs], Words[lhs]);
    }

    public ListCompression Copy() => new(Words);

    public override string ToString() => $"{string.Join(", ", Words.Select((c, i) => $"{c}={i}"))}, {GetSizeInBytes()}, Fitness {GetFitness()}";

    public static ListCompression GetRandomInstance()
    {
        var words = new[]
        {
            "Michael",
            "Sarah", "Joshua", "Narine", "David", "Sajid", "Melanie", "Daniel",
            "Wei", "Dean", "Brian", "Murat", "Lisa"
        };

        //create a permutation
        for (int i = 0; i < words.Length; i++)
        {
            var lhs = _random.Next(0, words.Length);
            var rhs = _random.Next(0, words.Length);

            (words[lhs], words[rhs]) = (words[rhs], words[lhs]);
        }

        return new ListCompression(words);
    }

    private long GetSizeInBytes()
    {
        var originalBytes = Encoding.UTF8.GetBytes(string.Join("", Words));
        try
        {
            using var memoryStream = new MemoryStream();
            using var compressor = new GZipStream(memoryStream, CompressionMode.Compress);
            compressor.Write(originalBytes, 0, originalBytes.Length);
            return memoryStream.Length;
        }
        catch (Exception exc)
        {
            return originalBytes.LongLength;
        }
    }

}