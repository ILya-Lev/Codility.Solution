namespace ClassicalProblems;

/// <summary>
/// in short TSP, zadacha komivoyajora
/// </summary>
public class TravelingSalesmanProblem
{
    private readonly IReadOnlyDictionary<string, IReadOnlyDictionary<string, int>> _distances =
        new Dictionary<string, IReadOnlyDictionary<string, int>>(StringComparer.OrdinalIgnoreCase)
    {
        ["Ratland"] = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase)
        {
            ["Berlingtone"] = 67,
            ["White-Riwer"] = 46,
            ["Benningtone"] = 55,
            ["Brattlboro"] = 75,
        },
        ["Berlingtone"] = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase)
        {
            ["Ratland"] = 67,
            ["White-Riwer"] = 91,
            ["Benningtone"] = 122,
            ["Brattlboro"] = 153,
        },
        ["White-Riwer"] = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase)
        {
            ["Ratland"] = 46,
            ["Berlingtone"] = 91,
            ["Benningtone"] = 98,
            ["Brattlboro"] = 65,
        },
        ["Benningtone"] = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase)
        {
            ["Ratland"] = 55,
            ["Berlingtone"] = 122,
            ["White-Riwer"] = 98,
            ["Brattlboro"] = 40,
        },
        ["Brattlboro"] = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase)
        {
            ["Ratland"] = 75,
            ["Berlingtone"] = 153,
            ["White-Riwer"] = 65,
            ["Benningtone"] = 40,
        },
    };

    //public TravelingSalesmanProblem(IReadOnlyDictionary<string, IReadOnlyDictionary<string, int>> distances)
    //{
    //    _distances = distances;
    //}

    public Path GetShortestPathByPermutations(string origin)
    {
        var towns = _distances.Keys.ToArray();
        
        var path = GenerateAllPermutations(towns)
            .Where(p => p[0].Equals(origin, StringComparison.OrdinalIgnoreCase))
            .MinBy(GetTotalDistance)!;

        return new Path()
        {
            Towns = path,
            TotalDistance = GetTotalDistance(path)
        };
    }

    private int GetTotalDistance(string[] path)
    {
        var total = 0;
        for (int i = 0; i < path.Length-1; i++)
        {
            var departureTown = path[i];
            var arrivalTown = path[i+1];
            total += _distances[departureTown][arrivalTown];
        }

        //return back
        total += _distances[path.Last()][path.First()];

        return total;
    }

    public static IReadOnlyList<T[]> GenerateAllPermutations<T>(T[] sequence)
    {
        var storage = new List<T[]>();
        GenerateAllPermutations(sequence, storage, sequence.Length - 1);
        return storage.ToArray();
    }

    private static void GenerateAllPermutations<T>(T[] sequence, List<T[]> permutations, int endIndex)
    {
        if (endIndex < 0)
        {
            permutations.Add(sequence);//another one is built
            return;
        }

        var tmp = sequence.ToArray();//deep copy
        for (int i = 0; i <= endIndex; i++)
        {
            (tmp[i], tmp[endIndex]) = (tmp[endIndex], tmp[i]);

            GenerateAllPermutations<T>(tmp, permutations, endIndex-1);//all possible permutations if ith is in the end

            (tmp[endIndex], tmp[i]) = (tmp[i], tmp[endIndex]);
        }
    }

    public class Path
    {
        public string[] Towns { get; set; }
        public int TotalDistance { get; set; }

        public override string ToString() => $"path: {string.Join("->", Towns)}, total distance: {TotalDistance}";
    }
}