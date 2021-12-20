using System.Collections;
using System.Text;

namespace ClassicalProblems
{
    public class CompressGene
    {
        private static readonly IReadOnlyDictionary<char, BitArray> _compressCypher = new Dictionary<char, BitArray>()
        {
            ['a'] = new BitArray(new[] { false, false }),
            ['c'] = new BitArray(new[] { false, true }),
            ['g'] = new BitArray(new[] { true, false }),
            ['t'] = new BitArray(new[] { true, true }),
            
            ['A'] = new BitArray(new[] { false, false }),
            ['C'] = new BitArray(new[] { false, true }),
            ['G'] = new BitArray(new[] { true, false }),
            ['T'] = new BitArray(new[] { true, true }),
        };

        private static readonly IReadOnlyDictionary<(bool, bool), char> _decompressCypher = new Dictionary<(bool, bool), char>()
        {
            [(false, false)] = 'A',
            [(false, true)] = 'C',
            [(true, false)] = 'G',
            [(true, true)] = 'T',
        };

        public static BitArray Compress(string gene)
        {
            var compressed = new BitArray(gene.Length * 2); //as each nucleotide occupies 2 bits
            for (var i = 0; i < gene.Length; i++)
            {
                compressed[2 * i] = _compressCypher[gene[i]][0];
                compressed[2 * i + 1] = _compressCypher[gene[i]][1];
            }

            return compressed;
        }

        public static string Decompress(BitArray compressed)
        {
            //another approach is to construct: key = f << 1 | s
            var sb = new StringBuilder(compressed.Length / 2);
            for (var i = 0; i < compressed.Length; i+=2)
            {
                var key = (compressed[i], compressed[i + 1]);
                var nucleotide = _decompressCypher[key];
                sb.Append(nucleotide);
            }

            return sb.ToString();
        }
    }
}
