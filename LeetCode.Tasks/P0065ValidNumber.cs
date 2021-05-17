using System;

namespace LeetCode.Tasks
{
    /// <summary>
    /// actually beats 100% of C# submissions, at 2021-05-17 23:41 EET
    /// it's meant to be hard, but I do not get why
    /// </summary>
    public class P0065ValidNumber
    {
        public bool IsNumber(string s)
        {
            var expParts = s.Split(new[] { 'e', 'E' }, StringSplitOptions.None);

            if (expParts.Length == 1) return IsDecimal(expParts[0]) || IsInteger(expParts[0]);
            if (expParts.Length == 2) return IsInteger(expParts[1]) && (IsDecimal(expParts[0]) || IsInteger(expParts[0]));
            return false;
        }

        private bool IsDecimal(string s)
        {
            bool? hasExplicitSign = null;
            bool? hasDot = null;
            bool? hasDigit = null;

            foreach (var c in s.ToCharArray())
            {
                if (c == '+' || c == '-')
                {
                    if (hasExplicitSign.HasValue) return false;//malformed - a sign is met second time
                    if (hasDigit.HasValue) return false;//malformed - a sign should be leading in this part
                    if (hasDot.HasValue) return false;//malformed - a sign should be leading in this part
                    hasExplicitSign = true;
                    continue;
                }

                if (c == '.')
                {
                    if (hasDot.HasValue) return false; //malformed - a dot is met second time
                    hasDot = true;
                    continue;
                }

                if (char.IsDigit(c))
                {
                    hasDigit = true;
                    continue;
                }
                return false; //invalid char is met
            }

            return (hasDigit ?? false) && (hasDot ?? false);
        }

        private bool IsInteger(string s)
        {
            bool? hasExplicitSign = null;
            bool? hasDigit = null;

            foreach (var c in s.ToCharArray())
            {
                if (c == '+' || c == '-')
                {
                    if (hasExplicitSign.HasValue) return false;//malformed - a sign is met second time
                    if (hasDigit.HasValue) return false;//malformed - a sign should be leading in this part
                    hasExplicitSign = true;
                    continue;
                }

                if (char.IsDigit(c))
                {
                    hasDigit = true;
                    continue;
                }
                return false; //invalid char is met
            }

            return hasDigit ?? false;
        }
    }
}
