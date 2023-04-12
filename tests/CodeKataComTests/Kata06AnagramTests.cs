using CodeKataCom;
using FluentAssertions;
using Xunit.Abstractions;

namespace CodeKataComTests;

public class Kata06AnagramTests
{
    private readonly ITestOutputHelper _output;
    public Kata06AnagramTests(ITestOutputHelper output) => _output = output;

    [Fact]
    public void FindAnagrams_SimpleExample_ShouldBeOneGroup()
    {
        var words = new[] { "crepitus", "cuprites", "pictures", "piecrust" };
        var groups = words.FindAnagrams().Single();
        groups.Should().BeEquivalentTo(words);
    }

    /// <summary>
    /// see http://codekata.com/kata/kata06-anagrams/ for the reference
    /// </summary>
    [Fact]
    public void FindAnagrams_TextFile_FindAll()
    {
        var path = Path.Combine(Directory.GetCurrentDirectory(), "wordlist.txt");
        var words = File.ReadAllLines(path);

        var groups = words.FindAnagrams().ToArray();
        
        //groups.Should().HaveCount(20683);
        _output.WriteLine($"{groups.Length} groups has been found");
        foreach (var group in groups.Take(15))
        {
            _output.WriteLine(string.Join(", ", group));
        }
    }
}