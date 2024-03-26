namespace Coderbyte;

public record Term(int factor, int power);

public static class TermExtensions
{
    public static Term Multiply(this Term lhs, Term rhs)
        => new Term(lhs.factor * rhs.factor, lhs.power + rhs.power);

    public static bool CanAggregate(this Term lhs, Term rhs) => lhs.power == rhs.power;

    public static Term Aggregate(this Term lhs, Term rhs)
        => new Term(lhs.factor + rhs.factor, lhs.power);

    public static string AsString(this Term t, char variable)
    {
        var factor = (t.factor > 0 ? "+" : "-") 
                   + (t.factor == 1 ? "" : $"{Math.Abs(t.factor)}");

        var v = t.power != 0 ? $"{variable}" : "";

        var power = t.power != 1 && t.power != 0 ? $"^{t.power}" : "";
        
        return $"{factor}{v}{power}";
    }
}

/*
 Polynomial Expansion

   Have the function PolynomialExpansion(str) take str which will be a string representing a polynomial containing only (+/-) integers, a letter, parenthesis, and the symbol "^", and return it in expanded form. For example: if str is "(2x^2+4)(6x^3+3)", then the output should be "12x^5+24x^3+6x^2+12". Both the input and output should contain no spaces. The input will only contain one letter, such as "x", "y", "b", etc. There will only be four parenthesis in the input and your output should contain no parenthesis. The output should be returned with the highest exponential element first down to the lowest.
   
   More generally, the form of str will be: ([+/-]{num}[{letter}[{^}[+/-]{num}]]...[[+/-]{num}]...)(copy) where "[]" represents optional features, "{}" represents mandatory features, "num" represents integers and "letter" represents letters such as "x".
*/
public class PolynomialExpansionSolver
{
    public static string PolynomialExpansion(string str)
    {
        var leftPartEnd = str.IndexOf(')');
        var variable = str.ToCharArray().First(char.IsLetter);
        var lhs = Parse(variable, str.Substring(0, leftPartEnd).Trim(new[] { '(', ')' }));
        var rhs = Parse(variable, str.Substring(leftPartEnd + 1).Trim(new[] { '(', ')' }));

        var multiply = lhs.SelectMany(a => rhs.Select(a.Multiply)).ToArray();
        var aggregate = multiply.GroupBy(t => t.power, t => t)
            .Select(g => new Term(g.Sum(tt => tt.factor), g.Key))
            .OrderByDescending(t => t.power)
            .ToArray();

        return string.Join("", aggregate.Select(t => t.AsString(variable))).Trim('+');
    }

    private static List<Term> Parse(char variable, string s)
    {
        var terms = new List<Term>();
        int factor = 0, power = 0;

        var part = new List<char>();
        var isPower = false;
        var isVariable = false;

        foreach (var c in s)
        {
            if (c == '+' || c == '-')
            {
                if (isVariable == true && isPower == false)
                {
                    terms.Add(new Term(factor, 1));
                    part.Clear();
                    isVariable = false;
                }
                if (part.Count == 0) { part.Add(c); continue; }

                if (isPower)
                {
                    power = int.Parse(new string(part.ToArray()));
                    terms.Add(new Term(factor, power));
                    part.Clear();
                    isPower = false;
                    isVariable = false;
                }
                else
                {
                    factor = int.Parse(new string(part.ToArray()));
                    terms.Add(new Term(factor, 0));
                    part.Clear();
                }

                { part.Add(c); continue; }
            }
            if (char.IsDigit(c)) { part.Add(c); continue; }

            if (c == variable)
            {
                isVariable = true;
                factor = int.Parse(new string(part.ToArray()));
                part.Clear();
            }

            if (c == '^')
            {
                isPower = true;
            }
        }

        //the last part
        if (isPower)
        {
            power = int.Parse(new string(part.ToArray()));
            terms.Add(new Term(factor, power));
        }
        else if (isVariable)
        {
            terms.Add(new Term(factor, 1));
        }
        else
        {
            factor = int.Parse(new string(part.ToArray()));
            terms.Add(new Term(factor, 0));
        }
        return terms;
    }
}