using Codility.Solvers;
using FluentAssertions;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Xunit;
using Xunit.Sdk;

namespace Codility.Tests
{
    public class QueueOnStackTests : IClassFixture<TestOutputHelper>
    {
        private readonly TestOutputHelper _outputHelper;

        public QueueOnStackTests(TestOutputHelper outputHelper)
        {
            _outputHelper = outputHelper;
        }

        [Fact]
        public void FIFO_EnterAllBeforeExtraction()
        {
            var values = Enumerable.Range(1, 10_000_000).ToArray();
            var queueOnStack = new TestableQueue<int>(testPlain: false);

            var customStopWatch = Stopwatch.StartNew();
            var dequeued = AllInsertThanAllRemove(values, queueOnStack);
            customStopWatch.Stop();

            dequeued.Should().Equal(values);


            var queue = new TestableQueue<int>(testPlain: true);
            var plainStopWatch = Stopwatch.StartNew();
            var dequeuedFromPlain = AllInsertThanAllRemove(values, queue);
            plainStopWatch.Stop();


            _outputHelper.WriteLine($"plain queue done in {plainStopWatch.ElapsedMilliseconds} ms, custom {customStopWatch.ElapsedMilliseconds} ms; number of elements {values.Length}");

            customStopWatch.ElapsedMilliseconds.Should()
                .BeLessOrEqualTo(plainStopWatch.ElapsedMilliseconds * 3);
        }

        [Fact]
        public void FIFO_OneEnterredOneRemoved()
        {
            var values = Enumerable.Range(1, 10_000_000).ToArray();
            var queueOnStack = new TestableQueue<int>(testPlain: false);

            var customStopWatch = Stopwatch.StartNew();
            var dequeued = OneInsertedOneRemoved(values, queueOnStack);
            customStopWatch.Stop();

            dequeued.Should().Equal(values);


            var queue = new TestableQueue<int>(testPlain: true);
            var plainStopWatch = Stopwatch.StartNew();
            var dequeuedFromPlain = OneInsertedOneRemoved(values, queue);
            plainStopWatch.Stop();


            _outputHelper.WriteLine($"plain queue done in {plainStopWatch.ElapsedMilliseconds} ms, custom {customStopWatch.ElapsedMilliseconds} ms; number of elements {values.Length}");

            customStopWatch.ElapsedMilliseconds.Should()
                .BeLessOrEqualTo(plainStopWatch.ElapsedMilliseconds * 3);
        }

        private List<int> OneInsertedOneRemoved(int[] values, TestableQueue<int> queue)
        {
            var dequeued = new List<int>(values.Length);
            foreach (var value in values)
            {
                queue.Enqueue(value);
                dequeued.Add(queue.Dequeue());
            }

            return dequeued;
        }

        private static List<int> AllInsertThanAllRemove(int[] values, TestableQueue<int> queue)
        {
            foreach (var value in values)
            {
                queue.Enqueue(value);
            }

            var dequeued = new List<int>(values.Length);
            while (queue.Count > 0)
            {
                dequeued.Add(queue.Dequeue());
            }

            return dequeued;
        }

        private class TestableQueue<T>
        {
            private readonly bool _testPlain;
            private readonly Queue<T> _queue = new Queue<T>();
            private readonly QueueOnStack<T> _queueOnStack = new QueueOnStack<T>();

            public TestableQueue(bool testPlain)
            {
                _testPlain = testPlain;
            }

            public int Count => _testPlain ? _queue.Count : _queueOnStack.Count;

            public void Enqueue(T item)
            {
                if (_testPlain)
                    _queue.Enqueue(item);
                else
                    _queueOnStack.Enqueue(item);
            }

            public T Dequeue() => _testPlain ? _queue.Dequeue() : _queueOnStack.Dequeue();
        }
    }
}
