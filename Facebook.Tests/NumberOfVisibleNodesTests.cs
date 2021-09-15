using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Facebook.Problems;
using FluentAssertions;
using Xunit;

namespace Facebook.Tests
{
    public class NumberOfVisibleNodesTests
    {
        private readonly NumberOfVisibleNodes.Node _root = new (8)
        {
            Left = new NumberOfVisibleNodes.Node(3)
            {
                Left = new NumberOfVisibleNodes.Node(1),
                Right = new NumberOfVisibleNodes.Node(6)
                {
                    Left = new NumberOfVisibleNodes.Node(4),
                    Right = new NumberOfVisibleNodes.Node(7),
                }
            },
            Right = new NumberOfVisibleNodes.Node(10)
            {
                Right = new NumberOfVisibleNodes.Node(14)
                {
                    Left = new NumberOfVisibleNodes.Node(13)
                }
            }
        };


        [Fact]
        public void VisibleNodes_Sample_4()
        {
            NumberOfVisibleNodes.VisibleNodes(_root).Should().Be(4);
        }

        [Fact]
        public void VisibleNodesByDepth_Sample_4()
        {
            NumberOfVisibleNodes.VisibleNodesByDepth(_root).Should().Be(4);
        }

        [Fact]
        public void InAscendingOrder_Sample_Ok()
        {
            NumberOfVisibleNodes.InAscendingOrder(_root)
                .Should().Equal(new[] { 1, 3, 4, 6, 7, 8, 10, 13, 14 });
        }

        [Fact]
        public void InAscendingOrderRecursion_Sample_Ok()
        {
            NumberOfVisibleNodes.InAscendingOrderRecursion(_root)
                .Should().Equal(new[] { 1, 3, 4, 6, 7, 8, 10, 13, 14 });
        }
    }
}
