using Algorithms.Solutions;
using FluentAssertions;
using System;
using System.Linq;
using Xunit;

namespace Algorithms.Tests
{
    public class QuickSortTests
    {
        [Fact]
        public void Sort_Desc_Asc()
        {
            QuickSort<int>.Sort(Enumerable.Range(1, 10_000).Reverse().ToArray())
                .Should().BeInAscendingOrder();
        }

        [Fact]
        public void Sort_Asc_Asc()
        {
            QuickSort<int>.Sort(Enumerable.Range(1, 10_000).ToArray())
                .Should().BeInAscendingOrder();
        }

        [Fact]
        public void Sort_Random_Asc()
        {
            var generator = new Random(DateTime.UtcNow.Millisecond);
            var raw = Enumerable.Range(1, 10_000).Select(n => generator.Next(-10_000, 10_000)).ToArray();

            QuickSort<int>.Sort(raw).Should().BeInAscendingOrder();
        }

        [Fact]
        public void Sort_BigDesc_Asc()
        {
            var raw = Enumerable.Range(1, 10_000_000).Reverse().ToArray();
            var sorted = QuickSort<int>.Sort(raw);
            for (int i = 0; i + 1 < sorted.Length; i++)
            {
                sorted[i].Should().BeLessOrEqualTo(sorted[i + 1]);
            }
        }
    }
}
