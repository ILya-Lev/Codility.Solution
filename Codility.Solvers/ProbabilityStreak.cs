using System;

namespace Codility.Solvers
{
    public class ProbabilityStreak
    {
        private readonly double _stepUp;
        private readonly double _stepDown;
        private readonly Random _randomGen = new Random(DateTime.UtcNow.Millisecond);

        public ProbabilityStreak(double stepUp, double stepDown)
        {
            _stepUp = Math.Abs(stepUp);
            _stepDown = Math.Abs(stepDown);
        }

        public double DoStep(double seed)
        {
            var factor = _randomGen.Next(0, 10) % 2 == 1 ? (1 + _stepUp) : (1 - _stepDown);
            return seed * factor;
        }
    }
}