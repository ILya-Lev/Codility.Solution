namespace CodeKataCom;

public static class Kata06Anagrams
{
    public static IEnumerable<string[]> FindAnagrams(this IEnumerable<string> words) => words
        .AsParallel()
        .WithDegreeOfParallelism(Environment.ProcessorCount + 1)
        .GroupBy(w => new string(w.ToLowerInvariant().ToCharArray().OrderBy(c => c).ToArray()))
        .Where(g => g.Distinct(StringComparer.OrdinalIgnoreCase).Count() > 1)
        .Select(g => g.ToArray());
}