using FluentAssertions;
using System.Linq;
using Xunit;
using Xunit.Abstractions;

namespace ClassicalProblems.Tests;

public class PhoneNumberMnemonicsTests
{
    private readonly ITestOutputHelper _output;
    public PhoneNumberMnemonicsTests(ITestOutputHelper output) => _output = output;

    [Fact]
    public void GenerateLetterCombinations_Number_AllCombinations()
    {
        var combinations = PhoneNumberMnemonics.GenerateLetterCombinations("0854723118");//4*3^6 = 2916 combinations
     
        combinations.Should().HaveCount(2916);
        foreach (var combination in combinations.Take(9))
            _output.WriteLine(combination);
    }

    [Fact]
    public void GenerateWords_27753_Apple()
    {
        var sut = new PhoneNumberMnemonics(new[] { "apple", "fruit", "grape", "pickle" });
        var combinations = sut.GenerateWords("27753");//3^3*4^2 = 432
     
        combinations.Single().Should().Be("apple");
    }

    [Fact]
    public void GenerateWords_69_27753_MyApple()
    {
        var sut = new PhoneNumberMnemonics(new[] { "apple", "my", "me", "we", "it", "is", "fruit", "grape", "pickle" });
        
        var combinations = sut.GenerateWords("69-27753");
        
        combinations.First().Should().Be("my-apple");
    }

    [Fact]
    public void GenerateWords_1440787_Ghosts()
    {
        var sut = new PhoneNumberMnemonics(new[] { "apple", "my", "me", "we", "it", "is", "fruit", "grape", "pickle", "ghost", "ghosts" });
        
        var combinations = sut.GenerateWords("1440787");
        
        combinations.First().Should().Be("1gh0sts");
    }
}