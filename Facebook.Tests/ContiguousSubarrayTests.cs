using System;
using System.Diagnostics;
using System.Linq;
using Facebook.Problems;
using FluentAssertions;
using Xunit;

namespace Facebook.Tests
{
    public class ContiguousSubarrayTests
    {
        [Fact]
        public void CountSubarrays_Sample1_ExpectedResult()
        {
            ContiguousSubarray.CountSubarrays(new[] { 3, 4, 1, 6, 2 })
                .Should().Equal(new []{1,3,1,5,1});
        }

        [Fact]
        public void CountSubarrays_NaturalNumbers_TheSame()
        {
            var numbers = Enumerable.Range(1, 10).ToArray();
            
            ContiguousSubarray.CountSubarrays(numbers)
                .Should().Equal(numbers);
        }

        [Fact]
        public void CountSubarrays_ReverseNaturalNumbers_TheSame()
        {
            var numbers = Enumerable.Range(1, 1_000_000).Reverse().ToArray();
            
            ContiguousSubarray.CountSubarrays(numbers)
                .Should().Equal(numbers);
        }

        [Fact]
        public void CountSubarrays_Random_CompareTheTwoSolutions()
        {
            var numbers = Enumerable.Range(1, 10_000).ToArray();
            Shuffle(numbers);

            var watchFast = Stopwatch.StartNew();
            var fast = ContiguousSubarray.CountSubarrays(numbers);
            watchFast.Stop();

            var watchSlow = Stopwatch.StartNew();
            var slow = ContiguousSubarray1.CountSubarrays(numbers);
            watchSlow.Stop();

            slow.Should().BeEquivalentTo(fast);
            watchSlow.ElapsedMilliseconds.Should().BeLessOrEqualTo(watchFast.ElapsedMilliseconds);
        }

        private void Shuffle(int[] numbers)
        {
            var generator = new Random(DateTime.UtcNow.Millisecond);
            for (int i = 0; i < numbers.Length; i++)
            {
                var lhs = generator.Next(0, numbers.Length);
                var rhs = generator.Next(0, numbers.Length);
                Swap(numbers, lhs, rhs);
            }

            //interesting swap via deconstruction.....
            static void Swap(int[] array, int indexLhs, int indexRhs)
            {
                (array[indexLhs], array[indexRhs]) = (array[indexRhs], array[indexLhs]);
            }
        }
    }
}