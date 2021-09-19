using System;
using System.Linq;
using Algorithms.Solutions;
using FluentAssertions;
using Xunit;

namespace Algorithms.Tests
{
    [Trait("Category", "Unit")]
    public class SelectionSortTests
    {
        [Fact]
        public void SelectionSort_Descending_Ascending()
        {
            var reversed = Enumerable.Range(1, 10_000).Reverse().ToArray();

            SelectionSort<int>.Sort(reversed).Should().BeInAscendingOrder();
        }

        [Fact]
        public void HeapSort_Descending_Ascending()
        {
            var reversed = Enumerable.Range(1, 10_000).Reverse().ToArray();

            HeapSort<int>.Sort(reversed).Should().BeInAscendingOrder();
        }
    }
}
