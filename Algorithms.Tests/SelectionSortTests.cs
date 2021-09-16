using System;
using System.Linq;
using Algorithms.Solutions;
using FluentAssertions;
using Xunit;

namespace Algorithms.Tests
{
    public class SelectionSortTests
    {
        [Fact]
        public void Sort_Descending_Ascending()
        {
            var reversed = Enumerable.Range(1, 10_000).Reverse().ToArray();

            SelectionSort<int>.Sort(reversed).Should().BeInAscendingOrder();
        }
    }
}
