﻿namespace Coderbyte;
/*
   Have the function PatternChaser(str) take str which will be a string and return the longest pattern within the string. A pattern for this challenge will be defined as: if at least 2 or more adjacent characters within the string repeat at least twice. So for example "aabecaa" contains the pattern aa, on the other hand "abbbaac" doesn't contain any pattern. Your program should return yes/no pattern/null. So if str were "aabejiabkfabed" the output should be yes abe. If str were "123224" the output should return no null. The string may either contain all characters (a through z only), integers, or both. But the parameter will always be a string type. The maximum length for the string being passed in will be 20 characters. If a string for example is "aa2bbbaacbbb" the pattern is "bbb" and not "aa". You must always return the longest pattern possible.
 */
public class PatternChaser
{
    public static string FindPattern(string str)
    {
        //naive impl
        var patterns = new HashSet<string>();
        var nonPatterns = new HashSet<string>();
        for (int outerShift = 0; outerShift < str.Length - 3; outerShift++)
        {
            for (int m = (str.Length - outerShift) / 2; m >= 2; m--)
            {
                var candidate = str.Substring(outerShift, m);
                if (patterns.Contains(candidate) || nonPatterns.Contains(candidate))
                    break;

                for (int shift = outerShift + m; shift + m <= str.Length; shift++)
                {
                    if (str.Substring(shift, m).Equals(candidate))
                    {
                        patterns.Add(candidate);
                        break;
                    }
                }

                if (!patterns.Contains(candidate))
                    nonPatterns.Add(candidate);
            }
        }

        return patterns.Any()
            ? $"yes {patterns.MaxBy(p => p.Length)}"
            : "no null";
    }
}