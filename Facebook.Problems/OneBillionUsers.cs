using System.Linq;

namespace Facebook.Problems
{
    public class OneBillionUsers
    {
        public static int GetBillionUsersDay(double[] growthRates)
        {
            var totalUsers = 0.0;
            var usersPerApp = Enumerable.Repeat(1.0, growthRates.Length).ToArray();
            const double target = 1e+9;
            const double precision = 1e-3;

            //space O(gr.len); time O(rg.Len * log(target)/log(max grow rate))
            var currentDay = 0;
            while (totalUsers - target < precision)
            {
                totalUsers = 0;
                currentDay++;

                for (int i = 0; i < usersPerApp.Length; i++)
                {
                    usersPerApp[i] *= growthRates[i];
                    totalUsers += usersPerApp[i];
                }
            }
            return currentDay;
        }
    }
}