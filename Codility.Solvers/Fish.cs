namespace Codility.Solvers
{
    public class Fish
    {
        public int SurvivalAmount(int[] size, int[] direction)
        {
            var biggestFishIndex = GetIndexOfMaxElement(size);
            var biggestFishDirection = direction[biggestFishIndex];
            return 0;
        }

        private int GetIndexOfMaxElement(int[] sequence)
        {
            if (sequence.Length == 0)
                return -1;
            var index = 0;
            var maxElement = sequence[0];
            for (int i = 0; i < sequence.Length; i++)
            {
                if (maxElement < sequence[i])
                {
                    maxElement = sequence[i];
                    index = i;
                }
            }

            return index;
        }
    }
}
