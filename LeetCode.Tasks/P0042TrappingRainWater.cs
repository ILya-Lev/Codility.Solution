using System;

namespace LeetCode.Tasks
{
    public class P0042TrappingRainWater
    {
        public int Trap(int[] height)
        {
            return DoTrap(height, 0, height.Length - 1);
        }

        private int DoTrap(int[] height, int start, int end)
        {
            var water = 0;
            while (start < end)
            {
                var current = 0;
                if (height[start] <= height[end])
                {
                    for (current = start + 1; current < end; current++)
                    {
                        if (height[current] >= height[start])
                            break;
                    }
                    water += GetWaterAmount(height, height[start], start + 1, current);
                    start = current;
                    continue;
                }

                //height[start] > height[end]
                for (current = end - 1; current > start; current--)
                {
                    if (height[current] >= height[end])
                        break;
                }
                water += GetWaterAmount(height, height[end], current + 1, end);
                end = current;
            }

            return water;
        }

        private int GetWaterAmount(int[] height, int edgeHeight, int start, int endExclusively)
        {
            var water = 0;
            for (int i = start; i < endExclusively; i++)
            {
                water += edgeHeight - height[i];
            }
            return water;
        }
    }
}