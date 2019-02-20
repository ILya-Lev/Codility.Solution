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
            var minAbsTotal = TraverseOptimalPath(descendingAbs);

            return minAbsTotal;
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
}
