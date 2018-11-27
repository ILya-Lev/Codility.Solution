using System.Collections.Generic;
using System.Linq;

namespace Codility.Solvers
{
    public class EquiLeader
    {
        private readonly int[] _values;
        private readonly Dictionary<int, int> _lhsFrequenceTable = new Dictionary<int, int>();
        private readonly Dictionary<int, int> _rhsFrequenceTable = new Dictionary<int, int>();

        public EquiLeader(int[] values)
        {
            _values = values;
            InitializeFrequenceTable(_rhsFrequenceTable);
        }

        private void InitializeFrequenceTable(Dictionary<int, int> frequence)
        {
            if (_values.Length == 0) return;

            foreach (var value in _values)
            {
                IncreaseFrequence(frequence, value);
            }
        }

        private static void IncreaseFrequence(Dictionary<int, int> frequence, int value)
        {
            if (!frequence.ContainsKey(value))
                frequence.Add(value, 1);
            else
                frequence[value]++;
        }
        private static void DecreaseFrequence(Dictionary<int, int> frequence, int value)
        {
            if (frequence.ContainsKey(value))
            {
                if (frequence[value] > 1)
                    frequence[value]--;
                else
                    frequence.Remove(value);
            }
        }

        private static int? GetDenominator(Dictionary<int, int> frequenceTable, int length)
        {
            if (frequenceTable.Count > length / 2 + 1)
                return null;

            var maxPair = frequenceTable.First();
            if (maxPair.Value * 2 > length)
                return maxPair.Key;

            foreach (var pair in frequenceTable)
            {
                if (pair.Value > maxPair.Value)
                {
                    maxPair = pair;
                    if (maxPair.Value * 2 > length)
                        return maxPair.Key;
                }
            }

            return null;
        }

        public int GetNumberOfEquiLeaders()
        {
            var equiLeaderAmount = 0;
            for (int i = 0; i < _values.Length - 1; i++)
            {
                IncreaseFrequence(_lhsFrequenceTable, _values[i]);
                DecreaseFrequence(_rhsFrequenceTable, _values[i]);

                var lhsDenominator = GetDenominator(_lhsFrequenceTable, i + 1);
                if (lhsDenominator == null)
                    continue;
                var rhsDenominator = GetDenominator(_rhsFrequenceTable, _values.Length - i - 1);
                if (lhsDenominator == rhsDenominator)
                    equiLeaderAmount++;
            }

            return equiLeaderAmount;
        }
    }
}
