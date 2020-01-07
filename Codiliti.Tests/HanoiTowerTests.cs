using Codility.Solvers.Hanoi;
using FluentAssertions;
using System;
using Codility.Solvers.HanoiObjects;
using Xunit;
using Xunit.Sdk;

namespace Codility.Tests
{
    public class HanoiTowerTests : IClassFixture<TestOutputHelper>
    {
        private readonly TestOutputHelper _output;

        public HanoiTowerTests(TestOutputHelper output)
        {
            _output = output;
        }

        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        [InlineData(4)]
        [InlineData(5)]
        [Theory]
        public void Move_NDisks_InOptimalWay(in int n)
        {
            var tower = new HanoiTower();
            
            var steps = tower.Move(n, 1, 2, 3);
            
            steps.Should().HaveCount((int)Math.Pow(2,n) - 1);
            foreach (var step in steps)
            {
                _output.WriteLine($"disk # {step.DiskNumber} goes from {step.OriginStick} to {step.TargetStick}");
            }
        }
    }

    public class HanoiGameSolverTests : IClassFixture<TestOutputHelper>
    {
        private readonly TestOutputHelper _output;

        public HanoiGameSolverTests(TestOutputHelper output)
        {
            _output = output;
        }

        [InlineData(3, 7)]
        [InlineData(0, 0)]
        [InlineData(1, 1)]
        [InlineData(4, 15)]
        [Theory]
        public void Solve_N_2powNMin1(in int n, in int stepsAmount)
        {
            var solver = new HanoiGameSolver(n);
            
            solver.Solve();
            
            var log = solver.GetLog();
            _output.WriteLine(log);
            log.Split("\n", StringSplitOptions.RemoveEmptyEntries)
                .Should()
                .HaveCount(stepsAmount);
        }
    }
}