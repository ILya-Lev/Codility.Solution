namespace Facebook.Problems
{
    public class RevenueMilestones
    {
        public static int[] GetMilestoneDays(int[] revenues, int[] milestones)
        {
            var milestoneDays = new int[milestones.Length];
            var milestoneIndex = 0;

            var totalRevenue = 0;
            for (int day = 0; day < revenues.Length && milestoneIndex < milestones.Length; day++)
            {
                totalRevenue += revenues[day];
                while (milestoneIndex < milestones.Length && totalRevenue >= milestones[milestoneIndex])
                {
                    milestoneDays[milestoneIndex++] = day + 1;
                }
            }

            while (milestoneIndex < milestoneDays.Length)
            {
                milestoneDays[milestoneIndex++] = -1;
            }

            return milestoneDays;
        }
    }
}