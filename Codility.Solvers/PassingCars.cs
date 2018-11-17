using System.Collections.Generic;

namespace Codility.Solvers
{
    public class PassingCars
    {
        private readonly List<int> _toWest;
        private readonly List<int> _toEast;

        public PassingCars(int[] directions)
        {
            _toWest = new List<int>();
            _toEast = new List<int>();
            for (var index = 0; index < directions.Length; index++)
            {
                var direction = directions[index];
                if (direction == 0)
                {
                    _toEast.Add(index);
                }
                else
                {
                    _toWest.Add(index);
                }
            }
        }

        public int Amount()
        {
            var totalPassing = 0L;
            var westIndex = 0;
            for (int eastIndex = 0; eastIndex < _toEast.Count;)
            {
                var followingWest = FollowingWest(eastIndex);
                var goingEast = followingWest - _toEast[eastIndex];
                if (goingEast <= 0)
                    break;

                westIndex = IndexByValue(westIndex, followingWest);
                if (westIndex < 0)
                    break;

                var goingWest = _toWest.Count - westIndex;
                var passingAmount = (long)goingEast * goingWest;

                totalPassing += passingAmount;
                if (totalPassing > 1_000_000_000)
                    return -1;

                eastIndex += goingEast;
            }

            return (int)totalPassing;
        }

        private int IndexByValue(int startToSearchAt, int value)
        {
            //as it turned out this change was crucial to make whole algorithm run in O(n)
            //ideally there should be binary search, as the list is guarantee to be ascendingly sorted
            return _toWest.IndexOf(value, startToSearchAt);
        }

        private int FollowingWest(int currentEast)
        {
            for (int shift = 1; currentEast + shift < _toEast.Count; shift++)
            {
                var followingWest = _toEast[currentEast] + shift;
                if (followingWest != _toEast[currentEast + shift])
                {
                    return followingWest;
                }
            }

            return _toEast[_toEast.Count - 1] + 1;
        }

    }
}
