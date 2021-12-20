namespace ClassicalProblems
{
    public class Fibonacci
    {
        public static long GetRecursion(int n)
        {
            if (n == 0) return 0;
            if (n == 1) return 1;
            if (n == 2) return 1;

            return GetRecursion(n - 1) + GetRecursion(n - 2);
        }

        private static readonly Dictionary<int, long> _sequence = new()
        {
            [0]=0,
            [1]=1,
            [2]=1,
        };

        public static long GetDynamic(int n)
        {
            if (_sequence.ContainsKey(n)) return _sequence[n];
        
            var current = GetDynamic(n - 1) + GetDynamic(n - 2);
            _sequence.Add(n, current);
            return current;
        }

        public static long GetViaSequence(int n) => GetSequence().Skip(n).First();

        public static IEnumerable<long> GetSequence()
        {
            long a = 0, b = 1;
            while (true)
            {
                yield return a;
                yield return b;
                a += b;
                b += a;
            }
        }
    }
}