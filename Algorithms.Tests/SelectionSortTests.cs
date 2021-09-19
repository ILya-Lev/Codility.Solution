using Algorithms.Solutions;
using FluentAssertions;
using System;
using System.Linq;
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

            HeapSort<int>.SortAscending(reversed).Should().BeInAscendingOrder();
        }

        [Fact]
        public void HeapSort_Ascending_Descending()
        {
            var reversed = Enumerable.Range(1, 10_000).ToArray();

            HeapSort<int>.SortDescending(reversed).Should().BeInDescendingOrder();
        }
    }
}
