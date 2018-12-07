using Codility.Solvers;
using FluentAssertions;
using System;
using System.Linq;
using Xunit;

namespace Codility.Tests
{
    public class SortedCollectionTests
    {
        [Fact]
        public void Add_DescendingUnique_Ascending()
        {
            var input = Enumerable.Range(1, 100).Reverse().ToArray();

            var collection = new SortedCollection<int>();

            foreach (var n in input)
            {
                collection.Add(n);
            }

            collection.Should().Equal(input.Reverse());
        }

        [Fact]
        public void Add_WithDuplicates_Ascending()
        {
            var input = new[] { 1, 3, 2, 6, 3, 7, 2, 5, 1, 7, 2, 5, 3, 6, 0 };
            var expected = new[] { 0, 1, 1, 2, 2, 2, 3, 3, 3, 5, 5, 6, 6, 7, 7 };

            var collection = new SortedCollection<int>();

            foreach (var n in input)
            {
                collection.Add(n);
            }

            collection.Should().Equal(expected);
        }

        [Fact]
        public void Add_TheSame_Ascending()
        {
            var input = Enumerable.Repeat(10.0, 10_000).ToArray();

            var collection = new SortedCollection<double>();

            foreach (var n in input)
            {
                collection.Add(n);
            }

            collection.Should().Equal(input);
        }

        [Fact]
        public void Add_Random_Ascending()
        {
            var random = new Random(DateTime.UtcNow.Millisecond);
            var input = Enumerable.Range(1, 10_000)
                .Select(n => random.Next(1, 1_000))
                .ToArray();

            var collection = new SortedCollection<double>();

            foreach (var n in input)
            {
                collection.Add(n);
            }

            collection.Should().Equal(input.OrderBy(n => n));
        }

        [Fact]
        public void Contains_RandomExist_Find()
        {
            var random = new Random(DateTime.UtcNow.Millisecond);
            var input = Enumerable.Range(1, 10_000)
                .Select(n => random.Next(1, 1_000))
                .ToArray();

            var collection = new SortedCollection<int>();
            collection.AddRange(input);

            foreach (var n in input)
            {
                collection.Contains(n).Should().BeTrue();
            }

            collection.Should().BeInAscendingOrder();
        }
    }
}
