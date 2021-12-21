using System.Text;

namespace ClassicalProblems
{
    /// <summary>
    /// there are 3 criteria for the key:
    /// 1. the same length as input data - when shorter, a pattern could be found in the encryption
    /// 2. really random - rely on some physical process
    /// 3. completely secret - do not use it twice nor leak it out
    /// </summary>
    public class UnbreakableEncryption
    {
        public static KeyValuePair Encrypt(string s)
        {
            var originalValue = Encoding.UTF8.GetBytes(s);//length depends on code page, English 1, Russian 2 bytes
            var key = GetRandomKey(originalValue.Length);
            
            var encryptedValue = originalValue.Zip(key, (v, k) => (byte)(v ^ k)).ToArray();
            
            return new KeyValuePair{Key = key, Value = encryptedValue};
        }

        public static string Decrypt(KeyValuePair data)
        {
            var restoredValue = data.Value.Zip(data.Key, (v, k) => (byte)(v ^ k)).ToArray();
            return Encoding.UTF8.GetString(restoredValue);
        }

        private static byte[] GetRandomKey(int length)
        {
            var generator = new Random(DateTime.UtcNow.Millisecond);
            var key = new byte[length];
            
            generator.NextBytes(key);
            
            return key;
        }

        public class KeyValuePair
        {
            public byte[] Key { get; init; }
            public byte[] Value { get; init; }
        }
    }
}
