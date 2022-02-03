﻿namespace ClassicalProblems;

public class PhoneNumberMnemonics
{
    private readonly HashSet<string> _knownWords;

    private static readonly IReadOnlyDictionary<int, IReadOnlyList<char>> _symbolsByDigit = new Dictionary<int, IReadOnlyList<char>>
    {
        [0] = new []{'0'},
        [1] = new []{'1'},
        [2] = new []{'a','b','c'},
        [3] = new []{'d','e','f'},
        [4] = new []{'g','h','i'},
        [5] = new []{'j','k','l'},
        [6] = new []{'m','n','o'},
        [7] = new []{'p','q','r', 's'},
        [8] = new []{'t','u','v'},
        [9] = new []{'w','x','y', 'z'},
    };

    public PhoneNumberMnemonics(IReadOnlyList<string> knownWords) => _knownWords = new HashSet<string>(knownWords);

    public IReadOnlyList<string> GenerateWords(string number)
    {
        var letterCombinations = GenerateLetterCombinations(number);
        var words = letterCombinations.Where(c => _knownWords.Contains(c)).ToArray();
        return words;
    }

    public static IReadOnlyList<string> GenerateLetterCombinations(string number)
    {
        var searchTree = BuildSearchTree(number);
        var letterCombinations = ExtractLetterCombinations(searchTree).ToArray();
        return letterCombinations;
    }


    private static Node BuildSearchTree(string number)
    {
        var searchTree = new Node() { Symbol = null };
        
        var layer = new List<Node>() { searchTree };
        foreach (var letter in number)
        {
            var nextLayer = new List<Node>();
            var childrenSymbols = char.IsDigit(letter)
                ? _symbolsByDigit[int.Parse(new string(new[] { letter }))]
                : new[] { letter };
            foreach (var node in layer)
            {
                node.Children.AddRange(childrenSymbols.Select(c => new Node() { Symbol = c, Parent = node }));
                nextLayer.AddRange(node.Children);
            }

            layer = nextLayer;
        }

        return searchTree;
    }

    private static IEnumerable<string> ExtractLetterCombinations(Node searchTree)
    {
        var word = new List<char>();

        var seen = new HashSet<Node>();
        var stack = new Stack<Node>();
        foreach (var node in searchTree.Children)
            stack.Push(node);

        while (stack.Any())
        {
            var current = stack.Peek();
            if (seen.Contains(current))
            {
                stack.Pop();
                word.RemoveAt(word.Count - 1);
                continue;
            }

            seen.Add(current);
            word.Add(current.Symbol!.Value);

            if (!current.Children.Any())
            {
                yield return new string(word.ToArray());
                stack.Pop();
                word.RemoveAt(word.Count - 1);
                continue;
            }

            foreach (var child in current.Children)
                stack.Push(child);
        }
    }

    public class Node
    {
        public char? Symbol { get; init; }
        public Node Parent { get; set; }
        public List<Node> Children { get; } = new List<Node>();
    }
}