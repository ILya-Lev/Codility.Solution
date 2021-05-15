namespace LeetCode.Tasks
{
    public class P0007ReverseInteger
    {
        public int Reverse(in int x)
        {
            var initial = x;
            var reversed = 0;
            while (initial != 0)
            {
                if (initial > 0 && reversed > (int.MaxValue - initial % 10) / 10)
                    return 0;

                if (initial < 0 && reversed < (int.MinValue - initial % 10) / 10)
                    return 0;

                reversed = reversed * 10 + initial % 10;
                initial /= 10;
            }

            return reversed;
        }
    }
}
