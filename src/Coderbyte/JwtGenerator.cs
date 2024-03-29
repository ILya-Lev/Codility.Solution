using System.Security.Cryptography;
using System.Text;
using System.Text.Json;

namespace Coderbyte;

/// <summary>
/// how to - see here https://jwt.io/
/// RFC is here: https://datatracker.ietf.org/doc/html/rfc7519 
/// </summary>
public class JwtGenerator
{
    //properties have such names to be serialized in an expected way
    private record Header(string alg, string typ);

    private record Payload(string sub, string jti, long iat, string iss, string aud);

    public static string Generate(string secret
        , string issuer, string audience, string sub, string jti, long iat)
    {
        var header = new Header("HS256", "JWT");
        var payload = new Payload(sub, jti, iat, issuer, audience);

        var encoded = new object[] { header, payload }
            .Select(b => JsonSerializer.Serialize(b))
            .Select(Base64Encode)
            .ToArray();

        var signature = GenerateSignature(secret, string.Join(".", encoded));

        return string.Join(".", encoded.Concat(new[] { signature }));
    }

    private static string Base64Encode(string s)
    {
        var bytes = Encoding.UTF8.GetBytes(s);
        return Convert.ToBase64String(bytes).Trim('=');
    }

    private static string GenerateSignature(string secret, string body)
    {
        using var hmac = new HMACSHA256(Encoding.UTF8.GetBytes(secret));
        var encoded = hmac.ComputeHash(Encoding.UTF8.GetBytes(body));
        return Convert.ToBase64String(encoded).Trim('=').Replace("/", "_");
    }
}