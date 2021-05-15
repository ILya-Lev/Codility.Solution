using AutoFixture;
using FluentAssertions;
using LeetCode.Tasks;
using Newtonsoft.Json;
using Xunit;
using Xunit.Abstractions;

namespace LeetCode.Tests
{
    [Trait("Category", "Unit")]
    public class FancyObjectTests
    {
        private readonly ITestOutputHelper _output;
        public FancyObjectTests(ITestOutputHelper output) => _output = output;

        [Fact]
        public void Serialize_BackAndForth_MatchItself()
        {
            var item = new Fixture().Create<FancyObject>();

            var rawItem = JsonConvert.SerializeObject(item);
            _output.WriteLine(rawItem);

            var restoredItem = JsonConvert.DeserializeObject<FancyObject>(rawItem);
            restoredItem.Should()
                //.IsSameOrEqualTo(item);
                .BeEquivalentTo(item);
        }
    }
}