using FluentAssertions;

namespace Coderbyte.Tests;

[Trait("Category", "Unit")]
public class ReducedRowEchelonFormMatrixTests
{
    public static object[][] TestData() => new []
    {
        new object[] { new string[] {"2","4","8","<>","6","12","14"}, "120001" },
        new object[] { new string[] {"2","2","4","<>","1","1","8","<>","7","6","5"}, "100010001" },
        new object[] { new string[] {"5","7","8","<>","1","1","2"}, "10301-1" },
        
    };

    [Theory, MemberData(nameof(TestData))]
    public void RREFMatrix_Sample_MatchExpectations(string[] matrix, string result)
    {
        ReducedRowEchelonFormMatrix.RREFMatrix(matrix).Should().Be(result);
    }
}