using System;

namespace LeetCode.Tasks
{
    public class P0042TrappingRainWater
    {
        public int Trap(int[] height)
        {
            int water = 0, start = 0, end = height.Length-1;
            while (start < end)
            {
                int current = 0, edge = 0;
                if (height[start] <= height[end])
                {
                    edge = height[start];
                    for (current = start + 1; current < end; current++)
                    {
                        if (height[current] >= edge)
                            break;
                        water += edge - height[current];
                    }
                    start = current;
                    continue;
                }

                //height[start] > height[end]
                edge = height[end];
                for (current = end - 1; current > start; current--)
                {
                    if (height[current] >= edge)
                        break;
                    water += edge - height[current];
                }
                end = current;
            }

            return water;
        }
    }
}