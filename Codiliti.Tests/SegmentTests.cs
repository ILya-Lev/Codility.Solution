using System;
using System.Collections.Generic;
using System.Text;
using Codility.Solvers;
using FluentAssertions;
using Xunit;

namespace Codility.Tests
{
    public class SegmentTests
    {
        [Fact]
        public void Intersects_OneBeforeAnother_False()
        {
            var first = new Segment(1,2);
            var second = new Segment(3,4);

            first.Intersects(second).Should().BeFalse();
            second.Intersects(first).Should().BeFalse();
        }

        [Fact]
        public void Intersects_SecondContainsFirstTail_True()
        {
            var first = new Segment(1,3);
            var second = new Segment(2,4);

            first.Intersects(second).Should().BeTrue();
            second.Intersects(first).Should().BeTrue();
        }

        [Fact]
        public void Intersects_FirstContainsWholeSecond_True()
        {
            var first = new Segment(1, 4);
            var second = new Segment(2, 3);

            first.Intersects(second).Should().BeTrue();
            second.Intersects(first).Should().BeTrue();
        }

        [Fact]
        public void Intersects_SecondContainsWholeFirst_True()
        {
            var first = new Segment(2, 3);
            var second = new Segment(1, 3);

            first.Intersects(second).Should().BeTrue();
            second.Intersects(first).Should().BeTrue();
        }

        [Fact]
        public void Intersects_SecondContainsFirstHead_True()
        {
            var first = new Segment(2,4);
            var second = new Segment(1,3);

            first.Intersects(second).Should().BeTrue();
            second.Intersects(first).Should().BeTrue();
        }
    }
}
