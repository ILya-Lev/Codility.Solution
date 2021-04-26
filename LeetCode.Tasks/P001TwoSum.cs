using System.Collections.Generic;
using System.Linq;

namespace LeetCode.Tasks
{
    public class P001TwoSum
    {
        public int[] TwoSum(int[] nums, int target)
        {
            var lookup = nums.Select((n, idx) => new { n, idx })
                .GroupBy(p => p.n, p => p.idx)
                .ToDictionary(g => g.Key, g => g.ToArray());

            for (int i = 0; i < nums.Length; i++)
            {
                var secondAddition = target - nums[i];
                if (lookup.TryGetValue(secondAddition, out var secondIndexes) && secondIndexes.Length > 0)
                {
                    var secondIndex = secondIndexes.FirstOrDefault(idx => idx > i);
                    if (secondIndex != 0)
                        return new[] { i, secondIndex };
                }
            }

            throw new KeyNotFoundException();
        }
    }
}
