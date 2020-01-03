using System.Collections.Generic;

namespace Codility.Solvers.Hanoi
{
    public class Step
    {
        public int DiskNumber { get; set; }
        public int OriginStick { get; set; }
        public int TargetStick { get; set; }
    }

    public class HanoiTower
    {
        public IReadOnlyList<Step> Move(in int n, in int origin, in int buffer, in int target)
        {
            if (n < 1) return new Step[0];

            var steps = new List<Step>();
            
            steps.AddRange(Move(n-1, origin, target, buffer));
            steps.Add(new Step(){DiskNumber = n, OriginStick = origin, TargetStick = target});
            steps.AddRange(Move(n-1, buffer, origin, target));
            
            return steps;
        }
    }
}