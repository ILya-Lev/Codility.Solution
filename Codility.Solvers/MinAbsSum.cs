using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Codility.Solvers
{
    public class MinAbsSum
    {
        public int GetMinAbsTotal(int[] numbers)
        {
            if (numbers == null || numbers.Length == 0) return 0;

            var descendingAbs = ToDescendingAbs(numbers);
            var minAbsTotal = descendingAbs.Length > 1_000
                ? TraverseOptimalPath(descendingAbs)
                : TraverseAllPathes(descendingAbs);

            return minAbsTotal;
        }

        private int TraverseAllPathes(int[] values)
        {
            var state = new HashSet<int>() { values[0] };
            foreach (var value in values.Skip(1))
            {
                var nextState = new HashSet<int>();
                foreach (var subState in state)
                {
                    nextState.Add(Math.Abs(subState + value));
                    nextState.Add(Math.Abs(subState - value));
                }
                state = nextState;
            }

            return state.Min();
        }

        private int TraverseOptimalPath(int[] descendingAbs)
        {
            var state = new State(0, descendingAbs[0]);
            foreach (var step in descendingAbs.Skip(1))
            {
                var nextValue = state.GetNextValue();
                state = new State(nextValue, step);
            }

            return state.GetNextValue();
        }


        private int[] ToDescendingAbs(int[] numbers) => numbers.Select(Math.Abs).OrderByDescending(n => n).ToArray();

        private class State
        {
            private readonly int _value;
            private readonly int _step;

            public State(int value, int step)
            {
                _value = value;
                _step = step;
            }

            public int GetNextValue()
            {
                var left = Math.Abs(_value + _step);
                var right = Math.Abs(_value - _step);

                return Math.Min(left, right);
            }
        }
    }

    //public class MinAbsSum
    //{
    //    /// <summary>
    //    /// holds limit of calculation
    //    /// </summary>
    //    private long Limit;

    //    public int GetMinAbsTotal(int[] input)
    //    {
    //        // order abs values
    //        var sorted = input
    //            .Select(i => i < 0 ? -i : i) // abs values
    //            .OrderByDescending(i => i);

    //        // get full sum
    //        long sum = 0;
    //        foreach (var i in sorted)
    //            sum += i;

    //        // not always 0 reachable
    //        Limit = sum % 2;

    //        long halfSum = sum / 2;
    //        long globalMin = int.MaxValue;

    //        var list = sorted.ToList();
    //        while (list.Any())
    //        {
    //            // taking items one by one from largest to smalest
    //            long currentMin = halfSum - list.First();
    //            list.RemoveAt(0);

    //            // proceed combinations to find half of sum
    //            // other half should go negative to reach min 
    //            currentMin = ProceedCombinations(list, currentMin);

    //            globalMin = currentMin < globalMin ? currentMin : globalMin;

    //            if (globalMin == Limit) break; // limit reached
    //        }

    //        return (int)globalMin;
    //    }

    //    private long ProceedCombinations(IEnumerable<int> opList, long halfSum)
    //    {
    //        long negate = halfSum, positive = halfSum;
    //        foreach (var i in opList)
    //        {
    //            // reminder positive
    //            if (positive - i >= 0)
    //            {
    //                positive -= i;
    //                if (positive == Limit)
    //                    return Limit; // limit reached!
    //            }

    //            // reminder negate 
    //            else if ((i - positive) < negate)
    //            {
    //                negate = i - positive;
    //                if (negate == Limit)
    //                    return Limit; // limit reached!
    //            }
    //        }

    //        // return min reminder
    //        return positive < negate ? positive : negate;
    //    }
    //}
}
