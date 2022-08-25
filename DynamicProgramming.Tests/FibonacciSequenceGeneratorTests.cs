using Xunit.Abstractions;

namespace DynamicProgramming.Tests;

//see: https://www.youtube.com/watch?v=oBt53YbR9Kk&ab_channel=freeCodeCamp.org

[Trait("Category", "Unit")]
public class FibonacciSequenceGeneratorTests
{
    private readonly ITestOutputHelper _output;
    public FibonacciSequenceGeneratorTests(ITestOutputHelper output) => _output = output;

    [Fact]
    public void GetNth_40_Observe()
    {
        var f40 = FibonacciSequenceGenerator.GetNth(40);
        _output.WriteLine($"40th Fibonacci's number is {f40:N1}");
    }
}