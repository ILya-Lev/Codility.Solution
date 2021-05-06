using System.Collections.Generic;
using System.Linq;

namespace LeetCode.Tasks
{
    public class P0403FrogJump_002
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

            var metadata = CollectMetadata(stones);

            if (metadata.Any(m => m.PreviousStoneDelta > m.Index))
                return false;

            return true;
        }

        private static IReadOnlyList<Stone> CollectMetadata(int[] stones)
        {
            var metadata = new List<Stone>(stones.Length - 1);
            for (int i = 1; i < stones.Length; i++)
            {
                metadata.Add(new(stones[i], i, stones[i] - stones[i - 1]));
            }
            return metadata;
        }

        private record Stone(int Value, int Index, int PreviousStoneDelta)
        {
            public int Value { get; } = Value;
            public int Index { get; } = Index;
            public int PreviousStoneDelta { get; } = PreviousStoneDelta;
        }
    }
}
