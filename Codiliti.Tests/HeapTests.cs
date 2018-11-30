using Codility.Solvers;
using FluentAssertions;
using System;
using System.Linq;
using Xunit;
using Xunit.Sdk;

namespace Codility.Tests
{
    public class HeapTests : IClassFixture<TestOutputHelper>
    {
        private readonly TestOutputHelper _outputHelper;

        public HeapTests(TestOutputHelper outputHelper)
        {
            _outputHelper = outputHelper;
        }

        [Fact]
        public void Push_IncreasingSequenceIsMinHeap_StorageOrderTheSameAsInput()
        {
            var input = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14 };
            var heap = new Heap<int>(isMin: true);

            var totalOperations = 0;
            foreach (var item in input)
            {
                totalOperations += heap.Push(item);
                heap.Peek().Should().Be(1);
            }

            heap._storage.Should().Equal(input);
            _outputHelper.WriteLine($"{totalOperations}");
        }

        [Fact]
        public void Push_DecreasingSequenceIsMinHeap_PeekIsMin()
        {
            var input = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14 }.Reverse();
            var heap = new Heap<int>(isMin: true);

            var totalOperations = 0;
            foreach (var item in input)
            {
                totalOperations += heap.Push(item);
            }

            heap.Peek().Should().Be(1);
            _outputHelper.WriteLine($"{string.Join("; ", heap._storage)}");
            _outputHelper.WriteLine($"{totalOperations}");
        }

        [Fact]
        public void Push_RandomIsMinHeap_PeekIsMin()
        {
            var rnd = new Random(DateTime.UtcNow.Millisecond);
            var input = Enumerable.Range(1, 10_000).Select(n => rnd.Next(1, 1_000)).ToArray();
            var heap = new Heap<int>(isMin: true);

            var totalOperations = 0;
            foreach (var item in input)
            {
                totalOperations += heap.Push(item);
            }

            heap.Peek().Should().Be(input.Min());
            _outputHelper.WriteLine($"{totalOperations}");
        }

        [Fact]
        public void Push_RandomIsMaxHeap_PeekIsMax()
        {
            var rnd = new Random(DateTime.UtcNow.Millisecond);
            var input = Enumerable.Range(1, 10_000).Select(n => rnd.Next(1, 1_000)).ToArray();
            var heap = new Heap<int>(isMin: false);

            var totalOperations = 0;
            foreach (var item in input)
            {
                totalOperations += heap.Push(item);
            }

            heap.Peek().Should().Be(input.Max());
            _outputHelper.WriteLine($"{totalOperations}");
        }
    }
}
