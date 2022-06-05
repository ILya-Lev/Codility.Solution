using System.Collections.Generic;
using System.Linq;

namespace Luxoft.GeneralCsTest;

public class HuTypewriter
{
    private readonly HashSet<char> _typewriterLine;

    public HuTypewriter(IEnumerable<char> typewriterLine = null)
    {
        _typewriterLine = new HashSet<char>(typewriterLine ?? "ertabdfyh".ToCharArray());
    }

    public string FindTheLongestWord(string input)
    {
        var words = input.Split(",").Select(w => w.Trim()).Where(w => !string.IsNullOrEmpty(w)).ToArray();
        
        var maxSuitableLength = 0;  //as an optimization - do not check words of same or smaller length
        var maxSuitableWord = "";
        
        for (int i = words.Length-1; i >= 0; i--)//as last of equal length words should be returned
        {
            if (words[i].Length > maxSuitableLength && ContainsOnlyTheLine(words[i]))
            {
                maxSuitableLength = words[i].Length;
                maxSuitableWord = words[i];
            }
        }

        return maxSuitableWord;
    }

    private bool ContainsOnlyTheLine(string word) => word.ToCharArray().All(ch => _typewriterLine.Contains(ch));
}