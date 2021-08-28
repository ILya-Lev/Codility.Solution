using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCode.Tasks
{
    public class P0011MostWater
    {
        public class Rectangle
        {
            public int FirstIndex { get; set; }
            public int SecondIndex { get; set; }
            public int FirstHeight { get; set; }
            public int SecondHeight { get; set; }
            public int Area => Math.Min(FirstHeight, SecondHeight) * (SecondIndex - FirstIndex);
        }
        /// <summary> O(n^2) - too slow </summary>
        public Rectangle NaiveMaxArea(int[] height)
        {
            var max = new Rectangle();
            for (int i = 0; i < height.Length; i++)
            {
                for (int j = i + 1; j < height.Length; j++)
                {
                    var currentArea = Math.Min(height[j], height[i]) * (j - i);
                    if (currentArea > max.Area)
                    {
                        max.FirstIndex = i;
                        max.SecondIndex = j;
                        max.FirstHeight = height[i];
                        max.SecondHeight = height[j];
                    }
                }
            }
            return max;
        }

        /// <summary>
        /// I believe may lead to wrong answers as in case of collision only front is moved
        /// </summary>
        public int MaxArea(int[] height) => GetMaxArea(height, 0, height.Length-1);

        private int GetMaxArea(int[] height, int start, int end)
        {
            var currentArea = 0;
            while (start < end)
            {
                currentArea = Math.Max(currentArea, GetArea(height, start, end));
                if (height[start] <= height[end])
                {
                    start++;
                    continue;
                }

                if (height[start] > height[end])
                {
                    end--;
                    continue;
                }
            }

            currentArea = Math.Max(currentArea, GetArea(height, start, end));

            return currentArea;
        }

        private int GetArea(int[] heights, int start, int end) => 
            (end - start) 
            * Math.Min(heights[start], heights[end]);
    }
}
