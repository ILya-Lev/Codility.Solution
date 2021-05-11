using System.Collections.Generic;

namespace LeetCode.Tasks
{
    public class P0992SubArray
    {
        private readonly List<int> _subArray = new List<int>();
        private readonly Dictionary<int, int> _popularity = new Dictionary<int, int>();

        public int SubarraysWithKDistinct(int[] source, int k)
        {
            _subArray.Clear();
            _popularity.Clear();

            var counter = 0;

            foreach (var number in source)
            {
                if (_popularity.Count < k)
                {
                    AppendToSubArray(number);

                    if (_popularity.Count == k)
                    {
                        counter += GetValidSubArrayAmount(_subArray, _popularity);
                    }

                    continue;
                }

                if (_popularity.Count == k)
                {
                    AppendToSubArray(number);

                    if (_popularity.Count == k)
                    {
                        counter += GetValidSubArrayAmount(_subArray, _popularity);
                    }
                    else// if (_popularity.Count > k)
                    {
                        ShrinkSubArrayFromStart(k);
                        if (_popularity.Count == k)
                        {
                            counter += GetValidSubArrayAmount(_subArray, _popularity);
                        }
                    }
                }
            }

            return counter;
        }

        /// <summary>
        /// having sub array with k+1 unique numbers inside remove numbers in front of it
        /// while the amount of unique numbers is not k again
        /// </summary>
        private void ShrinkSubArrayFromStart(in int k)
        {
            for (int i = 0; i < _subArray.Count; i++)
            {
                var head = _subArray[i];
                _popularity[head]--;
                _subArray.RemoveAt(i--);//to cancel up i++ in the end of the loop body


                if (_popularity[head] <= 0)
                {
                    _popularity.Remove(head);
                    if (_popularity.Count <= k)
                        return;
                }
            }
        }

        /// <summary>
        /// not only increase the sub array, but also the statistics - popularity of given number
        /// </summary>
        private void AppendToSubArray(int number)
        {
            _subArray.Add(number);
            _popularity[number] = _popularity.TryGetValue(number, out var currentPopularity)
                ? currentPopularity + 1
                : 1;
        }

        /// <summary>
        /// emulate removing by 1 item on each loop step in front of the sub array while it's still valid
        /// due to amount of unique items inside
        /// is static and except read only collections as should not change the state of the solver
        /// </summary>
        private static int GetValidSubArrayAmount(IReadOnlyCollection<int> subArray, IReadOnlyDictionary<int, int> popularity)
        {
            var counter = 1;    //for initial subArray
            var popularityOfShrink = new Dictionary<int, int>();

            foreach (var head in subArray)
            {
                popularityOfShrink[head] = popularityOfShrink.TryGetValue(head, out var currentPopularity)
                    ? currentPopularity + 1
                    : 1;

                if (popularity[head] <= popularityOfShrink[head])
                    return counter;

                counter++;
            }

            return counter;
        }
    }
}
