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

}
