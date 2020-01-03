using Codility.Solvers.Hanoi;
using FluentAssertions;
using System;
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
}