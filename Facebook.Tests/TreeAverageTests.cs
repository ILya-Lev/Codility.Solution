using System;
using Facebook.Problems;
using FluentAssertions;
using FluentAssertions.Execution;
using Xunit;

namespace Facebook.Tests
{
    public class TreeAverageTests
    {
        private readonly TreeAverage _sut = new();
        
        [Fact]
        public void CalculateAveragePerLevel_Null_Empty()
        {
            var averagePerLevel = _sut.CalculateAveragePerLevel(null);
            averagePerLevel.Should().BeEmpty();
        }

        [Fact]
        public void CalculateAveragePerLevel_RootLevelOnly_EqualData()
        {
            var root = new TreeAverage.Node<int>(10);
            
            var averagePerLevel = _sut.CalculateAveragePerLevel(root);
            
            averagePerLevel.Should().HaveCount(1);
            averagePerLevel[0].Should().Be(10);
        }

        [Fact]
        public void CalculateAveragePerLevel_5Levels_5Values()
        {
            var root = new TreeAverage.Node<int>(4)
            {
                LeftChild = new TreeAverage.Node<int>(7)
                {
                    LeftChild = new TreeAverage.Node<int>(10),
                    RightChild = new TreeAverage.Node<int>(2)
                    {
                        RightChild = new TreeAverage.Node<int>(6)
                        {
                            LeftChild = new TreeAverage.Node<int>(2)
                        }
                    }
                },
                RightChild = new TreeAverage.Node<int>(9)
                {
                    RightChild = new TreeAverage.Node<int>(6)
                }
            };
            
            var averagePerLevel = _sut.CalculateAveragePerLevel(root);

            using var scope = new AssertionScope();
            averagePerLevel.Should().HaveCount(5);
            averagePerLevel[0].Should().Be(4);
            averagePerLevel[1].Should().Be((7+9)/2.0);
            averagePerLevel[2].Should().Be((10+2+6)/3.0);
            averagePerLevel[3].Should().Be(6);
            averagePerLevel[4].Should().Be(2);
        }
    }
}
