using System;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using FluentAssertions;
using Xunit;
using Xunit.Abstractions;

namespace Luxoft.GeneralCsTest;

public class Counter
{
    public int Value { get; }
    public Counter(int initialValue) => Value = initialValue;
    public Counter Increment() => new Counter(Value+1);
}

public struct Rectangle
{
    public int Height { get; set; }
    public int Width { get; set; }
    public int Area => Height * ++Width;

    public readonly override string ToString() => $"{Area}";
}

public class CounterTests
{
    private readonly ITestOutputHelper _output;
    public CounterTests(ITestOutputHelper output) => _output = output;

    [Fact]
    public void Rectangle_AreaAndToString_ReadOnlyDoesNotChangeState()
    {
        var r = new Rectangle() { Height = 10, Width = 10 };
        var a = r.ToString();
        var b = $"{r.Area}";
        var isTheSame = a == b;
        isTheSame.Should().BeFalse();
    }

    [Fact]
    public void table_Condition_result()
    {
        var digits = Enumerable.Range(0, 10).ToArray();
        var t1 = digits.Select(d => digits.Select(d1 => d * d1)).ToArray();
        var t2 = digits.SelectMany(d => digits, (lhs, rhs) => lhs * rhs).ToArray();
        var t3 = digits.Zip(digits, (lhs, rhs) => lhs * rhs).ToArray();
        var t4 = digits.Join(digits, outer => outer, inner => inner, (lhs, rhs) => lhs * rhs).ToArray();
        _output.WriteLine("");
    }

    [Fact]
    public void Counter_Increment_InfiniteLoop()
    {
        var cc = 1;
        for (var c = new Counter(1); c.Value < 10; c.Increment())
        {
            _output.WriteLine($"{c.Value}");
            if (cc++ > 30)
                break;
        }
    }

    //[Fact]
    //public void GetFirstIndexOf_Condition_result()
    //{
    //    var result = GetFirstIndexOf(new[] { 2, 3, 5 }, 4);

    //    int GetFirstIndexOf(int[] numbers, int key)
    //    {
    //        for (int i = 0; i < 10; i++)
    //            if (numbers[i] == key)
    //                break;
    //        return i;
    //    }
    //}
}