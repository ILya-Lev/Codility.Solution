namespace ClassicalProblems
{
    public class CalculatePi
    {
        public static decimal Get(int iterations)
        {
            decimal result = 0m;
            for (int i = 0; i < iterations; i++)
            {
                var sign = i % 2 == 0 ? 1 : -1;
                result += sign * 1m / (2 * i + 1);
            }

            return 4 * result;
        }
    }
}
