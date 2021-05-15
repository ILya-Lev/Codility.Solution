using FluentAssertions;
using LeetCode.Tasks;
using Xunit;

namespace LeetCode.Tests
{
    public class P0007ReverseIntegerTests
    {
        private readonly P0007ReverseInteger _sut = new();

        [Fact] public void Reverse_123_321() => _sut.Reverse(123).Should().Be(321);
        [Fact] public void Reverse_120_21() => _sut.Reverse(120).Should().Be(021);
        [Fact] public void Reverse_Negative123_PreserveSign() => _sut.Reverse(-123).Should().Be(-321);
        [Fact] public void Reverse_Zero_Zero() => _sut.Reverse(-0).Should().Be(0);
        [Fact] public void Reverse_IntMax_Zero() => _sut.Reverse(int.MaxValue).Should().Be(0);
        [Fact] public void Reverse_IntMin_Zero() => _sut.Reverse(int.MinValue).Should().Be(0);
        //-2147483648 min  126_087_180
        //2147483647 max
    }
}
