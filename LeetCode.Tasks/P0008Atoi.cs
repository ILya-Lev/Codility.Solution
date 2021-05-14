using System.Collections.Generic;

namespace LeetCode.Tasks
{
    public class P0008Atoi
    {
        private static readonly IReadOnlyDictionary<char, int> _digitMap = new Dictionary<char, int>()
        {
            ['0'] = 0,
            ['1'] = 1,
            ['2'] = 2,
            ['3'] = 3,
            ['4'] = 4,
            ['5'] = 5,
            ['6'] = 6,
            ['7'] = 7,
            ['8'] = 8,
            ['9'] = 9,
        };

        public int MyAtoi(string s)
        {
            int? sign = null;
            int? number = null;
            foreach (var c in s.ToCharArray())
            {
                if (c == '-')
                {
                    //stop parsing as either sign is met second time
                    //or sign is met after the number
                    if (sign.HasValue || number.HasValue) break;
                    sign = -1;
                    continue;
                }

                if (c == '+')
                {
                    //stop parsing as either sign is met second time
                    //or sign is met after the number
                    if (sign.HasValue || number.HasValue) break;
                    sign = +1;
                    continue;
                }

                if (c == ' ')
                {
                    //stop parsing as the white space is met either after a sign or a number
                    if (sign.HasValue || number.HasValue) break;

                    //skip leading whitespace, by design only ' '
                    continue;
                }

                if (char.IsDigit(c))
                {
                    if (number is null)
                    {
                        number = _digitMap[c];
                        continue;
                    }

                    if (sign != -1 && number > (int.MaxValue - _digitMap[c]) / 10)
                    {
                        number = int.MaxValue;
                        //stop parsing as number has reached its upper boundary = int.max
                        break;
                    }

                    if (sign == -1 && number > (int.MaxValue - _digitMap[c] + 1L) / 10)
                    {
                        number = int.MinValue;
                        //stop parsing as number has reached its lower boundary = int.min
                        break;
                    }

                    number = 10 * number + _digitMap[c];
                    continue;
                }

                break;//stop parsing as soon as non digit or non whitespace is met
            }

            //if no sign is specified explicitly treat number as positive
            //if no number has been parsed from the input return 0
            return (sign ?? +1) * (number ?? 0);
        }
    }
}
