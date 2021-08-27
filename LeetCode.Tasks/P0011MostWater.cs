using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCode.Tasks
{
    public class P0011MostWater
    {
        /// <summary> O(n^2) - too slow </summary>
        public int NaiveMaxArea(int[] height)
        {
            var max = 0;
            for (int i = 0; i < height.Length; i++)
            {
                for (int j = i + 1; j < height.Length; j++)
                {
                    var currentArea = Math.Min(height[j], height[i]) * (j - i);
                    max = Math.Max(currentArea, max);
                }
            }
            return max;
        }

        /// <summary>
        /// still too slow - for natural numbers sequence ...
        /// </summary>
        public int MaxArea(int[] height) => GetMaxArea(height, 0, height.Length-1);

        private int GetMaxArea(int[] height, int start, int end)
        {
            var currentArea = GetArea(height, start, end);
            while (start < end)
            {
                if (height[start] < height[end])
                    start = MoveStart(height, start, end);
                else if (height[start] > height[end])
                    end = MoveEnd(height, start, end);
                else 
                    break;

                currentArea = Math.Max(currentArea, GetArea(height, start, end));
            }

            if (start < end)
                return Math.Max(currentArea
                                ,Math.Max(GetMaxArea(height, start + 1, end)
                                        , GetMaxArea(height, start, end - 1))
                );

            return currentArea;
        }

        private int MoveEnd(int[] heights, int start, int end)
        {
            for (int i = end - 1; i > start; i--)
            {
                if (heights[i] > heights[end])
                    return i;
            }

            return start;
        }

        private int MoveStart(int[] heights, int start, int end)
        {
            for (int i = start+1; i < end; i++)
            {
                if (heights[i] > heights[start])
                    return i;
            }

            return end;
        }

        private int GetArea(int[] heights, int start, int end) => 
            (end - start) 
            * Math.Min(heights[start], heights[end]);
    }
}
