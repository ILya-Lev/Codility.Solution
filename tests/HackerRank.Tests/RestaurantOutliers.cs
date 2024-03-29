using FluentAssertions;

namespace HackerRank.Tests;

public class RestaurantOutliersTests
{
    [Fact]
    public void getRelevantFoodOutlets_Denver_2()
    {
        var result = RestaurantOutliers.getRelevantFoodOutlets("Denver", 50);
        result.Should().HaveCount(2);
    }
}