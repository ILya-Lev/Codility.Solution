using Codility.Solvers;
using FluentAssertions;
using System;
using System.Collections.Generic;
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

        private class Item : IComparable<Item>
        {
            public Item(int data)
            {
                Data = data;
            }
            public int Data { get; }
            public int CompareTo(Item other)
            {
                return Data.CompareTo(other.Data);
            }
        }

        [Fact]
        public void Add_DescendingUniqueObjects_AscendingNullFirst()
        {
            var input = new[] { new Item(5), new Item(4), new Item(3), new Item(2), new Item(1), null };

            var collection = new SortedCollection<Item>();
            collection.AddRange(input);

            collection.Should().Equal(input.Reverse());
        }

        [Fact]
        public void Add_AscendingUniqueObjects_DescendingNullLast()
        {
            var input = new[] { new Item(5), new Item(4), new Item(3), new Item(2), new Item(1), null }.Reverse().ToArray();

            var collection = new SortedCollection<Item>(inAscendingOrder: false);
            collection.AddRange(input);

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

            collection.Should().Equal(input.Select(n => (double)n).OrderBy(n => n));
            collection.Should().BeInAscendingOrder();
        }

        [Fact]
        public void Add_Random_Descending()
        {
            var random = new Random(DateTime.UtcNow.Millisecond);
            var input = Enumerable.Range(1, 10_000)
                .Select(n => random.Next(1, 1_000))
                .ToArray();

            var collection = new SortedCollection<double>(inAscendingOrder: false);

            foreach (var n in input)
            {
                collection.Add(n);
            }

            collection.Should().Equal(input.Select(n => (double)n).OrderByDescending(n => n));
            collection.Should().BeInDescendingOrder();
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

        [Fact]
        public void Contains_RandomAbsent_NotFound()
        {
            var random = new Random(DateTime.UtcNow.Millisecond);
            var input = Enumerable.Range(1, 10_000)
                .Select(n => random.Next(1, 1_000))
                .ToArray();

            var collection = new SortedCollection<int>();
            collection.AddRange(input);

            var existing = new HashSet<int>(input);
            var absent = Enumerable.Range(1, 1_000)
                .Where(n => !existing.Contains(n))
                .ToArray();

            absent.Length.Should().BeGreaterThan(0);
            foreach (var n in absent)
            {
                collection.Contains(n).Should().BeFalse();
            }

            collection.Should().BeInAscendingOrder();
        }

        [Fact]
        public void Contains_DescendingRandomAbsent_NotFound()
        {
            var random = new Random(DateTime.UtcNow.Millisecond);
            var input = Enumerable.Range(1, 10_000)
                .Select(n => random.Next(1, 1_000))
                .ToArray();

            var collection = new SortedCollection<int>(inAscendingOrder: false);
            collection.AddRange(input);

            var existing = new HashSet<int>(input);
            var absent = Enumerable.Range(1, 1_000)
                .Where(n => !existing.Contains(n))
                .ToArray();

            absent.Length.Should().BeGreaterThan(0);
            foreach (var n in absent)
            {
                collection.Contains(n).Should().BeFalse();
            }

            collection.Should().BeInDescendingOrder();
        }

        [Fact]
        public void Remove_OneInstance_ItemIsRemoved()
        {
            var input = new[] { 1, 2, 3, 4, 5, 6, 7 };
            var collection = new SortedCollection<int>(inAscendingOrder: false);
            collection.AddRange(input);

            collection.Remove(3).Should().BeTrue();
            collection.Contains(3).Should().BeFalse();
        }

        [Fact]
        public void Remove_ManyInstances_ItemIsRemoved()
        {
            var input = new[] { 3, 1, 2, 3, 4, 5, 3, 6, 7 };
            var collection = new SortedCollection<int>(inAscendingOrder: false);
            collection.AddRange(input);

            collection.Remove(3).Should().BeTrue();
            collection.Contains(3).Should().BeFalse();
            collection.Should().BeInDescendingOrder();
        }
    }
}
