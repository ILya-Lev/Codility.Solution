using System.Collections.Generic;
using System.Linq;

namespace Codility.Solvers
{
    public class StoneWall_0
    {
        public int BricksNumber(int[] heights)
        {
            if (heights.Length < 2)
                return heights.Length;

            return BricksNumberInChunk(heights, 0, heights.Length);
        }

        private int BricksNumberInChunk(int[] chunk, int start, int end)
        {
            if (end - start < 2)
                return end - start;

            var minHeight = chunk.Skip(start).Take(end - start).Min();

            var bricksNumber = 1;
            var areTheSame = true;
            for (int first = start, last = first + 1; last < end; last++)
            {
                if (chunk[first] > chunk[last] || last == end - 1)
                {
                    bricksNumber += areTheSame
                        ? BricksForSameHeightChunk(chunk[first], minHeight)
                        : BricksNumberInChunk(chunk, first + 1, last);

                    first = last;
                    areTheSame = true;
                }
                else if (areTheSame && chunk[first] < chunk[last])
                {
                    areTheSame = false;
                }
            }

            return bricksNumber;
        }

        private static int BricksForSameHeightChunk(int chunkHeight, int minHeight)
        {
            return chunkHeight == minHeight ? 0 : 1;
        }
    }

    public class StoneWall
    {
        public int BricksNumber(int[] heights)
        {
            return BricksNumberOfList(heights.ToList());
        }

        private int BricksNumberOfList(List<int> heights)
        {
            if (heights.Count < 2)
                return heights.Count;

            var bricksNumber = 0;
            var buildingStack = new Stack<int>();
            buildingStack.Push(heights[0]);
            var head = heights[0];
            for (int i = 1; i < heights.Count; i++)
            {
                var height = heights[i];
                if (height == buildingStack.Peek())
                    continue;

                var headVsHeight = head.CompareTo(height);
                if (headVsHeight < 0)
                    buildingStack.Push(height);
                else if (headVsHeight > 0)
                {
                    bricksNumber += BricksInStack(buildingStack);
                    buildingStack.Clear();
                    buildingStack.Push(height);
                    head = height;
                }
                else if (headVsHeight == 0)
                {
                    bricksNumber += BricksInStack(buildingStack);
                    buildingStack.Clear();
                    
                    while (i < heights.Count && heights[i] == head)
                        i++;
                    
                    if (i == heights.Count)
                        break;

                    buildingStack.Push(heights[i]);
                }
            }

            return bricksNumber + BricksInStack(buildingStack);
        }

        private int BricksInStack(Stack<int> buildingStack)
        {
            if (buildingStack.Count < 2)
                return buildingStack.Count;

            var chunk = new List<int>(buildingStack.Count);
            while (buildingStack.Count > 0)
            {
                chunk.Add(buildingStack.Pop());
            }

            return BricksNumberOfList(chunk);
        }
    }
}
