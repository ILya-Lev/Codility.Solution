using FluentAssertions;

namespace Coderbyte.Tests;

[Trait("Category", "Unit")]
public class JwtGeneratorTests
{
    [Fact]
    public void Generate_Sample_ExpectedJwt()
    {
        var jwt = JwtGenerator.Generate("blah blah blah", "me", "everyone", "123456789", "what is it?", 1516239022);

        jwt.Should()
.Be("eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9" 
+ ".eyJzdWIiOiIxMjM0NTY3ODkiLCJqdGkiOiJ3aGF0IGlzIGl0PyIsImlhdCI6MTUxNjIzOTAyMiwiaXNzIjoibWUiLCJhdWQiOiJldmVyeW9uZSJ9" 
+ ".XkHPCZOn2DfRdhYPv6Ca2zoyDFHeRhusuPabL73IMjQ");
    }
}