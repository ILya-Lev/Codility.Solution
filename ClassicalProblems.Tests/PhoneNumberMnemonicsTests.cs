using System;
using System.Dynamic;
using System.Linq;
using FluentAssertions;
using Xunit;
using Xunit.Abstractions;

namespace ClassicalProblems.Tests;

public class PhoneNumberMnemonicsTests
{
    private readonly ITestOutputHelper _output;

    public PhoneNumberMnemonicsTests(ITestOutputHelper output)
    {
        _output = output;
    }

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
     
        combinations.Should().HaveCount(1);//only one match = apple
        combinations.Single().Should().Be("apple");
    }
}