namespace ClassicalProblems;

public class PhoneNumberMnemonics
{
    private readonly HashSet<string> _knownWords;

    private static readonly IReadOnlyDictionary<int, IReadOnlyList<char>> _symbolsByDigit = new Dictionary<int, IReadOnlyList<char>>
    {
        [0] = new[] { 'o' },
        [1] = new[] { '1' },
        [2] = new[] { 'a', 'b', 'c' },
        [3] = new[] { 'd', 'e', 'f' },
        [4] = new[] { 'g', 'h', 'i' },
        [5] = new[] { 'j', 'k', 'l' },
        [6] = new[] { 'm', 'n', 'o' },
        [7] = new[] { 'p', 'q', 'r', 's' },
        [8] = new[] { 't', 'u', 'v' },
        [9] = new[] { 'w', 'x', 'y', 'z' },
    };

    private readonly int _scoreThreshold = 4;

    public PhoneNumberMnemonics(IReadOnlyList<string> knownWords) => _knownWords = new HashSet<string>(knownWords);

    public IReadOnlyList<string> GenerateWords(string number)
    {
        var letterCombinations = GenerateLetterCombinations(number);
        var words = letterCombinations
            .Select(combination => (combination, CalculateScore(combination)))
            .Where(tuple => tuple.Item2 > _scoreThreshold)
            .OrderByDescending(tuple => tuple.Item2)
            .Select(tuple => tuple.Item1)
            .Distinct()
            .Select(combination => RestoreZeroCharacter(combination, number))
            .ToArray();

        return words;
    }

    public static IReadOnlyList<string> GenerateLetterCombinations(string number)
    {
        var searchTree = BuildSearchTree(number);
        var letterCombinations = ExtractLetterCombinations(searchTree).ToArray();
        return letterCombinations;
    }

    private int CalculateScore(string combination, int start = 0, int scoreSoFar = 0)
    {
        int score = scoreSoFar;
        for (int currentStart = start; currentStart < combination.Length - 1; currentStart++)
        {
            for (int end = currentStart + 1; end < combination.Length; end++)
            {
                var candidate = combination.Substring(currentStart, end - currentStart + 1);
                if (!_knownWords.Contains(candidate))
                    continue;
                
                var currentScore = CalculateScore(combination, end + 1, scoreSoFar + candidate.Length);
                if (currentScore > score)
                    score = currentScore;
            }
        }
        return score;
    }

    private static string RestoreZeroCharacter(string combination, string number)
    {
        if (number.All(c => c != '0')) return combination;

        var letters = combination.ToCharArray();
        for (int i = 0; i < number.Length; i++)
        {
            if (number[i] == '0' && letters[i] == 'o')
                letters[i] = '0';
        }
        
        return new string(letters);
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