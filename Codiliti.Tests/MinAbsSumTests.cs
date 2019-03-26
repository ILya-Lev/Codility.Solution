using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Codility.Solvers;
using FluentAssertions;
using Xunit;

namespace Codility.Tests
{
    public class MinAbsSumTests
    {
        [Fact]
        public void GetMinAbsTotal_Sample_0()
        {
            var values = new[] { 1, 5, -2, 2 };
            var total = new MinAbsSum().GetMinAbsTotal(values);
            total.Should().Be(0);
        }

        [Fact]
        public void GetMinAbsTotal_Sample1_0()
        {
            var values = new[] { 3, 3, 3, 4, 5 };
            var total = new MinAbsSum().GetMinAbsTotal(values);
            total.Should().Be(0);
        }
        
        [Fact]
        public void GetMinAbsTotal_Sample2_1()
        {
            var values = new[] { 1, -3, 5, 8, 0, -4 };
            var total = new MinAbsSum().GetMinAbsTotal(values);
            total.Should().Be(1);
        }

        [Fact]
        public void GetMinAbsTotal_2to20_1()
        {
            var values = Enumerable.Range(2, 19).ToArray();
            var total = new MinAbsSum().GetMinAbsTotal(values);
            total.Should().Be(1);
        }

        [Fact]
        public void GetMinAbsTotal_One_ItsAbs()
        {
            var values = new[] { -123 };
            var total = new MinAbsSum().GetMinAbsTotal(values);
            total.Should().Be(123);
        }

        [Fact]
        public void GetMinAbsTotal_Empty_0()
        {
            var values = new int[0];
            var total = new MinAbsSum().GetMinAbsTotal(values);
            total.Should().Be(0);
        }

        [Fact]
        public void GetMinAbsTotal_AllTheSameEvenAmount_0()
        {
            var random = new Random(DateTime.UtcNow.Millisecond);
            var values = Enumerable.Repeat(1, 100_000).Select(n => random.Next(0, 2) == 0 ? -n : n).ToArray();
            var total = new MinAbsSum().GetMinAbsTotal(values);
            total.Should().Be(0);
        }

        [Fact]
        public void GetMinAbsTotal_AllTheSameOddAmount_Item()
        {
            var random = new Random(DateTime.UtcNow.Millisecond);
            var values = Enumerable.Repeat(7, 99_999).Select(n => random.Next(0, 2) == 0 ? -n : n).ToArray();
            var total = new MinAbsSum().GetMinAbsTotal(values);
            total.Should().Be(7);
        }

        [Fact]
        public void GetMinAbsTotal_AllTheSameOddAmountSmall_Item()
        {
            var random = new Random(DateTime.UtcNow.Millisecond);
            var values = Enumerable.Repeat(7, 999).Select(n => random.Next(0, 2) == 0 ? -n : n).ToArray();
            var total = new MinAbsSum().GetMinAbsTotal(values);
            total.Should().Be(7);
        }

        [InlineData(10)]
        [InlineData(11)]
        [InlineData(100)]
        [Theory]
        public void GetMinAbsTotal_NaturalSequence_MaxDiv2Mod2(int amount)
        {
            var random = new Random(DateTime.UtcNow.Millisecond);
            var values = Enumerable.Range(1, amount).Select(n => random.Next(0, 2) == 0 ? -n : n).ToArray();
            var total = new MinAbsSum().GetMinAbsTotal(values);
            total.Should().Be((amount + 1) / 2 % 2);
        }
    }
}
