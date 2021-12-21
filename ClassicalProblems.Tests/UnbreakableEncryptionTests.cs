using FluentAssertions;
using Xunit;

namespace ClassicalProblems.Tests
{
    public class UnbreakableEncryptionTests
    {
        [Fact]
        public void Encrypt_Decrypt_Coincide()
        {
            var s = "Hit the road, Jack! Дополнительные символы для проверки кодировки: ёйъь";
            
            var encryption = UnbreakableEncryption.Encrypt(s);
            encryption.Key.Length.Should().Be(encryption.Value.Length);

            var restored = UnbreakableEncryption.Decrypt(encryption);
            
            restored.Should().Be(s);
        }
    }
}
