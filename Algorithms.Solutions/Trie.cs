using System;
using System.Collections.Generic;

namespace Algorithms.Solutions
{
    public class Trie<TValue> where TValue : IEquatable<TValue>, new()
    {
        private readonly Func<string, string> _keyConverter = k => k;
        private readonly Node _root = new Node();

        public Trie(bool isCaseInsensitive)
        {
            if (isCaseInsensitive)
                _keyConverter = k => k.ToLowerInvariant();
        }

        public void Add(string key, TValue value)
        {
            var convertedKey = ConvertKey(key);

            var currentNode = _root;
            for (var i = 0; i < convertedKey.Length; i++)
            {
                var c = convertedKey[i];
                if (!currentNode.Children.ContainsKey(c))
                {
                    var child = new Node();
                    if (i + 1 == convertedKey.Length) child.Data = value;

                    currentNode.Children.Add(c, child);
                }
                else if (IsLastChar(i, convertedKey))
                {
                    if (!currentNode.Children[c].Data.Equals(value))
                        throw new InvalidOperationException($"Cannot override value by the key {convertedKey}");

                    if (currentNode.Children[c].Data.Equals(default))
                        currentNode.Children[c].Data = value;
                }

                currentNode = currentNode.Children[c];
            }
        }

        public TValue Find(string key)
        {
            var convertedKey = ConvertKey(key);

            var currentNode = _root;
            for (var i = 0; i < convertedKey.Length; i++)
            {
                var c = convertedKey[i];
                if (!currentNode.Children.ContainsKey(c)) return default;

                if (IsLastChar(i, convertedKey)) return currentNode.Children[c].Data;

                currentNode = currentNode.Children[c];
            }

            return default;//unreachable piece of code; is here to workaround the compiler only
        }

        private string ConvertKey(string key)
        {
            if (string.IsNullOrWhiteSpace(key))
                throw new ArgumentNullException(nameof(key));

            return _keyConverter(key);
        }

        private static bool IsLastChar(int i, string convertedKey) => i + 1 == convertedKey.Length;

        private class Node
        {
            public Dictionary<char, Node> Children { get; } = new Dictionary<char, Node>();
            public TValue Data { get; set; } = default(TValue);
        }
    }
}