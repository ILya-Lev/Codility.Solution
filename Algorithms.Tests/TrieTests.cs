using System.Linq;
using Algorithms.Solutions;
using FluentAssertions;
using Xunit;

namespace Algorithms.Tests
{
    public class TrieTests
    {
        [Fact]
        public void Add_FindPresent_MissAbsent()
        {
            var trie = new Trie<int>(false);
            var strings = new[]
            {
                "a", "aa", "aaa", "aaaa", "aba", "abaa", "abba", "b", "ba", "bca", "bachelor", "basketball", "base", "baseball"
            };

            foreach (var s in strings.Concat(strings))
            {
                trie.Add(s,s.Length);
            }

            foreach (var s in strings)
            {
                trie.Find(s).Should().Be(s.Length);
            }

            trie.Find("x").Should().Be(default(int));
        }
    }
}