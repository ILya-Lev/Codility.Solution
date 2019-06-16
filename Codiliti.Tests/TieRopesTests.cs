using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Codility.Solvers;
using FluentAssertions;
using Xunit;

namespace Codility.Tests
{
    public class TieRopesTests
    {
        [Fact]
        public void GetRopeNumber_Sample_3()
        {
            var ropes = new int[] { 1, 2, 3, 4, 1, 1, 3 };
            var amount = new TieRopes().GetRopeNumber(4, ropes);
            amount.Should().Be(3);
        }

        [Fact]
        public void GetRopeNumber_Empty_0()
        {
            var ropes = new int[0];
            var amount = new TieRopes().GetRopeNumber(4, ropes);
            amount.Should().Be(0);
        }

        [Fact]
        public void GetRopeNumber_TooBigLimit_0()
        {
            var ropes = new int[] { 1, 2, 3, 4, 1, 1, 2 };
            var amount = new TieRopes().GetRopeNumber(ropes.Sum() + 1, ropes);
            amount.Should().Be(0);
        }

        [Fact]
        public void GetRopeNumber_AllTheSameEqualLimit_NumberOfRopes()
        {
            var ropes = Enumerable.Repeat(1, 100_000).ToArray();
            var amount = new TieRopes().GetRopeNumber(1, ropes);
            amount.Should().Be(ropes.Length);
        }
    }
}
