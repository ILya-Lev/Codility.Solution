using System.Collections.Generic;
using System.Linq;

namespace LeetCode.Tasks
{
    public class P0403FrogJump
    {
        public int TryJumpCounter { get; set; }
        /// <summary>
        /// preconditions:
        /// 1. input array is sorted in an ascending order
        /// 2. there are at least 2 stones
        /// 3. first stone has to be 0
        /// </summary>
        /// <param name="stones"></param>
        /// <returns></returns>
        public bool CanCross(int[] stones)
        {
            if (stones?.Length >= 2 == false || stones[0] != 0)
                return false;

            if (!CanDoFirstJump(stones))
                return false;

            if (!CanCoverLastStone(stones))
                return false;

            return FindOutIfCanCross(stones.ToList(), 1, 1);
        }

        private static bool CanDoFirstJump(IReadOnlyList<int> stones) => stones[1] == 1;

        private bool CanCoverLastStone(IReadOnlyCollection<int> stones) => stones.Last() <= stones.Count * (stones.Count - 1) / 2;

        private bool FindOutIfCanCross(List<int> stones, int position, int jumpSize)
        {
            if (stones.Count - 1 == position) //standing on the last stone
                return true;

            return TryJump(stones, position, jumpSize - 1)
                || TryJump(stones, position, jumpSize)
                || TryJump(stones, position, jumpSize + 1);
        }

        private bool TryJump(List<int> stones, int position, int jumpSize)
        {
            if (jumpSize < 1) return false;

            TryJumpCounter++;
            var nextStoneValue = stones[position] + jumpSize;
            var nextPosition = stones.BinarySearch(nextStoneValue);

            if (nextPosition > position && nextPosition < stones.Count)
                return FindOutIfCanCross(stones, nextPosition, jumpSize);   //recursive call

            return false;
        }
    }
}
