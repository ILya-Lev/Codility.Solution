using System;
using System.Collections.Generic;
using System.Linq;

namespace Facebook.Problems
{
    public class Fibonacci
    {
        /// <summary> generates infinite sequence </summary>
        public static IEnumerable<long> GenerateSequence()
        {
            long a = 1L, b = 1L;
            while (true)
            {
                yield return a;
                var c = a + b;
                a = b;
                b = c;
            }
        }

        /// <summary> time O(n) as from bottom to top; space O(1) and state machine to keep state </summary>
        public static long Get(int n) => GenerateSequence().Skip(Math.Max(0, n - 1)).Take(1).Single();

        /// <summary> time O(2^n); space O(1) </summary>
        public static long GetRecursive(int n)
        {
            if (n < 3) return 1;
            return GetRecursive(n - 1) + GetRecursive(n - 2);
        }

        /// <summary> time O(n) as dictionary is O(1); space O(n) </summary>
        public static long GetMemoization(int n)
        {
            var alreadySeen = new HashSet<int>();//to reduce space consumption by the stack
            var alreadyCreated = new Dictionary<int, long>();

            var stack = new Stack<int>();
            stack.Push(n);
            while (stack.Any())
            {
                var current = stack.Peek();
                if (alreadyCreated.ContainsKey(current - 1) && alreadyCreated.ContainsKey(current - 2))
                {
                    alreadyCreated.TryAdd(current, alreadyCreated[current - 1] + alreadyCreated[current - 2]);
                    stack.Pop();
                    continue;
                }

                if (current < 3)
                {
                    alreadyCreated.TryAdd(current, 1L);
                    stack.Pop();
                    continue;
                }

                if (!alreadySeen.Contains(current - 1))
                {
                    stack.Push(current - 1);
                    alreadySeen.Add(current - 1);
                }

                if (!alreadySeen.Contains(current - 2))
                {
                    stack.Push(current - 2);
                    alreadySeen.Add(current - 2);
                }
            }

            return alreadyCreated[n];
        }
    }
}
