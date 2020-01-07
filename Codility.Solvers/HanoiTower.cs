using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

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

namespace Codility.Solvers.HanoiObjects
{
    internal class Disk
    {
        public Disk(int size)
        {
            Size = size;
        }

        public int Size { get; }
    }

    internal class Stick
    {
        private static int _idGenerator = 1;
        private readonly Stack<Disk> _disks;

        /// <summary> creates empty stick </summary>
        public Stick()
        {
            _disks = new Stack<Disk>();
            Id = _idGenerator++;
        }

        /// <summary>
        /// creates a stick with N disks on it, from smallest to largest
        /// </summary>
        public Stick(int n) : this()
        {
            for (int i = n; i > 0; --i)
            {
                Add(new Disk(i));
            }
        }

        public int Id { get; }

        public void Add(Disk disk)
        {
            if (CanAdd(disk))
            {
                _disks.Push(disk);
                return;
            }
            
            throw new Exception($"Cannot add disk of size {disk.Size} on top of disk with size {_disks.Peek().Size}");
        }

        public bool CanAdd(Disk disk) => Count == 0 || _disks.Peek().Size > disk.Size;
        public bool CanRemove() => Count != 0;
        public int Count => _disks.Count;

        public Disk Remove()
        {
            if (CanRemove())
                return _disks.Pop();

            throw new Exception($"Cannot remove any disk from the stick as it's already empty");
        }
    }

    public class HanoiGameSolver
    {
        private readonly Stick _initial;
        private readonly Stick _buffer;
        private readonly Stick _target;
        private readonly StringBuilder _log;

        /// <summary>
        /// creates Hanoi tower game solver with specific amount of disks and 3 sticks
        /// all disks are on the first stick initially
        /// </summary>
        /// <param name="n"></param>
        public HanoiGameSolver(int n)
        {
            _initial = new Stick(n);
            _buffer = new Stick();
            _target = new Stick();
            

            if (n > 31) throw new Exception($"Too many disks {n}!");
            _log = new StringBuilder((int)Math.Pow(2,n)-1);
        }

        public string GetLog() => _log.ToString();

        public void Solve()
        {
            DoSolve(_initial.Count, _initial, _buffer, _target);
        }

        private void DoSolve(int n, Stick initial, Stick buffer, Stick target)
        {
            if (n <= 0) return;

            DoSolve(n-1, initial, target, buffer);

            MoveDisk(initial, target);

            DoSolve(n-1, buffer, initial, target);
        }

        private void MoveDisk(Stick from, Stick to)
        {
            var aDisk = from.Remove();
            var message = $"{aDisk.Size:D2}: {from.Id} -> {to.Id}";
            _log.AppendLine(message);
            to.Add(aDisk);
        }
    }
}
