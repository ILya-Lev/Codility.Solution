using System.Linq;
using FluentAssertions;
using Xunit;
using Xunit.Abstractions;

namespace ClassicalProblems.Tests
{
    public class HanoiTowerTests
    {
        private readonly ITestOutputHelper _output;
        public HanoiTowerTests(ITestOutputHelper output) => _output = output;

        [Theory]
        [InlineData(3, 7)]
        [InlineData(10, 1_023)]
        [InlineData(20, 1_048_575)]
        //[InlineData(30, 1_073_741_823)]
        public void MoveBlocks_nBlocks_PowerOf2(int blockNumber, int stepNumber)
        {
            var steps = HanoiTower.MoveBlocks(blockNumber);
            
            steps.Should().HaveCount(stepNumber);
            
            foreach (var step in steps.Take(100))
            {
                _output.WriteLine(step);
            }
        }
    }
}
