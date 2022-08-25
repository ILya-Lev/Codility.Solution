namespace DynamicProgramming;

public class FibonacciSequenceGenerator
{
    /// <summary> returns Nth number of the fibonacci sequence </summary>
    public static long GetNth(int n) => GetSequence().Skip(n - 1).First();

    private static IEnumerable<long> GetSequence()
    {
        long a = 0, b = 1;
        while (true)
        {
            yield return a;
            yield return b;
            var c = a + b;
            a = b;
            b = c;
        }
    }
}