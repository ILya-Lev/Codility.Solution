using Algorithms.Solutions;
using FluentAssertions;
using System.Linq;
using Xunit;

namespace Algorithms.Tests
{
    [Trait("Category", "Unit")]
    public class HeapTests
    {
        private static MinHeap<int> CreateAndPopulateMinHeap(int[] sequence)
        {
            var heap = new MinHeap<int>();
            for (int i = 0; i < sequence.Length; i++)
            {
                heap.Insert(sequence[i]);

                heap.Count.Should().Be(i + 1);
                heap.Items.Should().Equal(sequence.Take(i + 1));
            }

            return heap;
        }

        [Fact]
        public void Insert_Series_CheckStorage()
        {
            var sequence = new[] { 4, 4, 8, 9, 4, 12, 9, 11, 13, 7, 10 };
            var heap = CreateAndPopulateMinHeap(sequence);

            heap.Insert(5);
            heap.Count.Should().Be(12);
            heap.Items.Should().Equal(new[] { 4, 4, 5, 9, 4, 8, 9, 11, 13, 7, 10, 12 });
        }

        [Fact]
        public void Heapify_MinSeries_CheckStorage()
        {
            var sequence = new[] { 4, 4, 8, 9, 4, 12, 9, 11, 13, 7, 10, 5 };
            var heap = MinHeap<int>.Heapify(sequence);

            heap.Count.Should().Be(sequence.Length);
            heap.Items.Should().Equal(new[] { 4, 4, 5, 9, 4, 8, 9, 11, 13, 7, 10, 12 });
        }

        [Fact]
        public void Extract_WhenHave9Items_CheckStorage()
        {
            var sequence = new[] { 4, 4, 8, 9, 4, 12, 9, 11, 13 };
            var heap = CreateAndPopulateMinHeap(sequence);

            heap.Extract().Should().Be(4);
            heap.Count.Should().Be(sequence.Length - 1);
            heap.Items.Should().Equal(new[] { 4, 4, 8, 9, 13, 12, 9, 11 });
        }

        [Fact]
        public void Extract_WhenHave5Items_CheckStorage()
        {
            var sequence = new[] { 4, 9, 10, 11, 13 };
            var heap = CreateAndPopulateMinHeap(sequence);

            heap.Extract().Should().Be(4);
            heap.Count.Should().Be(sequence.Length - 1);
            heap.Items.Should().Equal(new[] { 9, 11, 10, 13 });
        }

        [Fact]
        public void MaxInsert_Series_CheckStorage()
        {
            var sequence = new[] { 4, 9, 10, 11, 13 };
            var heap = new MaxHeap<int>(5);

            heap.Insert(4);
            heap.Items.Should().Equal(new[] { 4 });

            heap.Insert(9);
            heap.Items.Should().Equal(new[] { 9, 4 });

            heap.Insert(10);
            heap.Items.Should().Equal(new[] { 10, 4, 9 });

            heap.Insert(11);
            heap.Items.Should().Equal(new[] { 11, 10, 9, 4 });

            heap.Insert(13);
            heap.Items.Should().Equal(new[] { 13, 11, 9, 4, 10 });
        }
    }
}