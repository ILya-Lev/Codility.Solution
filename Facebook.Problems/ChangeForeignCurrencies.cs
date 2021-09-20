using System.Linq;

namespace Facebook.Problems
{
    public class ChangeForeignCurrencies
    {
        public static bool CanGetExactChange(int targetMoney, int[] denominations)
        {
            var denominatorsDescending = denominations.OrderByDescending(v => v);

            var remainingChange = targetMoney;

            foreach (var denominator in denominatorsDescending)
            {
                remainingChange %= denominator;
                if (remainingChange == 0) return true;
            }
            return false;
        }

        public static bool CanGetExactChange_Recursive(int targetMoney, int[] denominations)
        {
            var d = denominations.OrderByDescending(v => v).ToArray();
            return DoDefine(targetMoney, d);
        }

        private static bool DoDefine(int targetMoney, int[] denominators, int current = 0)
        {
            if (current >= denominators.Length) return false;

            var change = targetMoney % denominators[current];
            if (change == 0) return true;
            return DoDefine(change, denominators, current + 1);
        }
    }
}