using ArgumentException = System.ArgumentException;

namespace Coderbyte;

/*
 Have the function Calculator(str) take the str parameter being passed and evaluate the mathematical expression within in. For example, if str were "2+(3-1)*3" the output should be 8. Another example: if str were "(2-0)(6/2)" the output should be 6. There can be parenthesis within the string so you must evaluate it properly according to the rules of arithmetic. The string will contain the operators: +, -, /, *, (, and ). If you have a string like this: #/#*# or #+#(#)/#, then evaluate from left to right. So divide then multiply, and for the second one multiply, divide, then add. The evaluations will be such that there will not be any decimal operations, so you do not need to account for rounding and whatnot.
 */

public class CalculatorSolver
{
    private const char O = '(';
    private const char C = ')';
    private const char D = '/';
    private const char M = '*';
    private const char A = '+';
    private const char S = '-';

    public static string Calculator(string str)
    {
        str = str.Replace(")(", ")*(");//explicit multiplication between ()
        var result = Evaluate(str);
        return result;
    }

    private static string Evaluate(string str)
    {
        int opening = -1;
        for (int i = 0; i < str.Length; i++)
        {
            if (str[i] == O)
            {
                opening = i;
                continue;
            }

            if (str[i] != C)
                continue;

            if (opening == -1)
                throw new ArgumentException($"No {O} for {C} at {i} in string: '{str}'");

            var closing = i;
            var inside = str.Substring(opening + 1, closing - opening - 1);
            var substitute = EvaluateWithoutParenthesis(inside);

            //explicit multiplication between () and digits before and/or after ()
            var before = opening > 0 && char.IsDigit(str[opening - 1]) ? $"{M}" : "";
            var after = closing + 1 < str.Length && char.IsDigit(str[closing + 1]) ? $"{M}" : "";
            str = $"{str.Substring(0, opening)}{before}{substitute}{after}{str.Substring(closing + 1)}";

            i = -1;//start all over again, as the string has just been modified
            opening = -1;
        }

        //here we guarantee we do not have any () - the format is only: d*-/+d
        return EvaluateWithoutParenthesis(str);
    }

    private static string EvaluateWithoutParenthesis(string str)
    {
        var addSubstractOnly = EvaluateAction(str, new HashSet<char>(new[] { M, D }));
        var numberOnly = EvaluateAction(addSubstractOnly, new HashSet<char>(new[] { A, S }));
        return numberOnly;
    }

    private static string EvaluateAction(string str, HashSet<char> actions)
    {
        for (int i = 1; i < str.Length - 1; i++)
        {
            if (actions.Contains(str[i]))
            {
                var leftIdx = GetNumberStart(str, i);
                var rightIdx = GetNumberEnd(str, i);

                var lhs = int.Parse(str.Substring(leftIdx, i - leftIdx));
                var rhs = int.Parse(str.Substring(i + 1, rightIdx - i));

                var value = Calculate(str[i], lhs, rhs);

                str = $"{str.Substring(0, leftIdx)}{value}{str.Substring(rightIdx + 1)}";
                i = 0;//start string processing from the beginning, as it has just been modified
                continue;
            }
        }
        return str;
    }

    private static int GetNumberStart(string str, int afterEnd)
    {
        var current = afterEnd - 1;
        while (current >= 0 && char.IsDigit(str[current]))
            current--;
        
        if (current >= 0 && str[current] == S)
            current--;//keep trailing minus

        return current + 1;//as we expect the loop to be hit at least once
    }

    private static int GetNumberEnd(string str, int beforeStart)
    {
        var current = beforeStart + 1;
        if (str[current] == S)
            current++; //allow leading minus only!
        while (current < str.Length && char.IsDigit(str[current]))
            current++;
        return current - 1;//as we expect the loop to be hit at least once
    }

    private static int Calculate(char action, int lhs, int rhs) => action switch
    {
        M => lhs * rhs,
        D => lhs / rhs,
        A => lhs + rhs,
        S => lhs - rhs,
        _ => throw new InvalidOperationException($"Cannot perform unknown action {action}")
    };
}