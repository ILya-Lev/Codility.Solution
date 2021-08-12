using FluentAssertions;
using LeetCode.Tasks;
using Xunit;

namespace LeetCode.Tests
{
    public class P1493LongestOnesSequenceTests
    {
        private readonly P1493LongestOnesSequence _sut = new();

        [Fact]
        public void LongestSubarray_OnlyZeros_0()
        {
            _sut.LongestSubarray(new[] { 0, 0, 0, 0 }).Should().Be(0);
            _sut.LongestSubarray(new[] { 0, 0, 0 }).Should().Be(0);
            _sut.LongestSubarray(new[] { 0, 0 }).Should().Be(0);
            _sut.LongestSubarray(new[] { 0 }).Should().Be(0);
        }

        [Fact]
        public void LongestSubarray_OnlyOnes_LenM1()
        {
            _sut.LongestSubarray(new[] { 1, 1, 1, 1 }).Should().Be(3);
            _sut.LongestSubarray(new[] { 1, 1, 1 }).Should().Be(2);
            _sut.LongestSubarray(new[] { 1, 1 }).Should().Be(1);
            _sut.LongestSubarray(new[] { 1 }).Should().Be(0);
        }

        [Fact]
        public void LongestSubarray_ZeroAtEnd_LengthButOne()
        {
            _sut.LongestSubarray(new[] { 0, 1, 1, 1 }).Should().Be(3);
            _sut.LongestSubarray(new[] { 0, 1, 1 }).Should().Be(2);
            _sut.LongestSubarray(new[] { 0, 1 }).Should().Be(1);
        }

        [Fact]
        public void LongestSubarray_Zebra_One()
        {
            _sut.LongestSubarray(new[] { 0, 1, 0, 1 }).Should().Be(2);
            _sut.LongestSubarray(new[] { 0, 1, 0 }).Should().Be(1);
        }
    }
}
