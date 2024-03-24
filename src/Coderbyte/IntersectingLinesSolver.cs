namespace Coderbyte;

public record Rational(int N, int D);

public static class RationalExtensions
{
    public static string AsString(this Rational r)
    {
        var reduced = r.Reduce();
        return reduced.D == 1 
            ? $"{reduced.N}"
            : $"{reduced.N}/{reduced.D}";
    }

    public static Rational Multiply(this Rational lhs, Rational rhs)
        => new Rational(lhs.N * rhs.N, lhs.D * rhs.D).Reduce();

    public static Rational Multiply(this Rational r, int n) => (r with { N = r.N * n }).Reduce();

    public static Rational Divide(this Rational lhs, Rational rhs)
        => new Rational(lhs.N * rhs.D, lhs.D * rhs.N).Reduce();

    public static Rational Divide(this Rational r, int n) => (r with { D = r.D * n }).Reduce();

    public static Rational Add(this Rational lhs, Rational rhs)
        => new Rational(lhs.N * rhs.D + rhs.N * lhs.D, lhs.D * rhs.D).Reduce();

    public static Rational Add(this Rational r, int n) => (r with { N = r.N + n * r.D }).Reduce();

    public static Rational Subtract(this Rational lhs, Rational rhs) => lhs.Add(rhs.Multiply(-1)).Reduce();

    public static Rational Subtract(this Rational r, int n) => r.Add(-1*n).Reduce();

    public static Rational Reduce(this Rational r)
    {
        var gcd = FindGreatestCommonDivisor(r.N, r.D);
        return new Rational(r.N / gcd, r.D / gcd);
    }

    /// <summary>
    /// https://en.wikipedia.org/wiki/Euclidean_algorithm
    /// </summary>
    public static int FindGreatestCommonDivisor(int a, int b)
    {
        var current = Math.Max(a,b);
        var next = Math.Min(a,b);
        while (next != 0)
        {
            var tmp = current % next;
            current = next;
            next = tmp;
        }
        return current;
    }
}

/*
 Have the function IntersectingLines(strArr) take strArr which will be an array of 4 coordinates in the form: (x,y). Your program should take these points where the first 2 form a line and the last 2 form a line, and determine whether the lines intersect, and if they do, at what point. For example: if strArr is ["(3,0)","(1,4)","(0,-3)","(2,3)"], then the line created by (3,0) and (1,4) and the line created by (0,-3) (2,3) intersect at (9/5,12/5). Your output should therefore be the 2 points in fraction form in the following format: (9/5,12/5). If there is no denominator for the resulting points, then the output should just be the integers like so: (12,3). If any of the resulting points is negative, add the negative sign to the numerator like so: (-491/63,-491/67). If there is no intersection, your output should return the string "no intersection". The input points and the resulting points can be positive or negative integers.
 */
public class IntersectingLinesSolver
{
    private const string NoIntersection = "no intersection";
    public static string IntersectingLines(string[] strArr)
    {
        var points = strArr.Select(s => s.Trim().Trim(new[] { '(', ')' }))
            .Select(s => s.Split(","))
            .Select(p => (x: int.Parse(p[0]), y: int.Parse(p[1])))
            .ToArray();

        var lhs = GetLineParameters(points[0], points[1]);
        var rhs = GetLineParameters(points[2], points[3]);

        //there are 9 cases
        if (lhs.Item2 is null)//lhs is x=const
        {
            if (rhs.Item2 is null) return NoIntersection;//either the same lines or no intersection
            if (rhs.Item1 is null) return $"({lhs.Item1!.N},{rhs.Item2.N})";

            var y = rhs.Item1.Multiply(lhs.Item1!.N).Add(rhs.Item2);
            
            return $"({lhs.Item1!.N},{y.AsString()})";
        }
        if (lhs.Item1 is null)//lhs is y=const
        {
            if (rhs.Item1 is null) return NoIntersection;//either the same lines or no intersection
            if (rhs.Item2 is null) return $"({rhs.Item1.N},{lhs.Item2!.N})";

            var x = lhs.Item2.Subtract(rhs.Item2).Divide(rhs.Item1);
            
            return $"({x.AsString()},{lhs.Item2.N})";
        }

        if (rhs.Item2 is null)
        {
            var y = lhs.Item1.Multiply(rhs.Item1!.N).Add(lhs.Item2);
            return $"({rhs.Item1.N},{y.AsString()})";
        }

        if (rhs.Item1 is null)
        {
            var x = rhs.Item2.Subtract(lhs.Item2).Divide(lhs.Item1);
            return $"({x.AsString()},{rhs.Item2.N})";
        }

        //the last case - both lines have a and b defined
        if (lhs.Item1.Reduce() == rhs.Item1.Reduce())   //a1 == a2
            return NoIntersection;

        var da = rhs.Item1.Subtract(lhs.Item1);
        var interX = lhs.Item2.Subtract(rhs.Item2).Divide(da);
        var interY = lhs.Item2.Multiply(rhs.Item1).Subtract(lhs.Item1.Multiply(rhs.Item2)).Divide(da);

        return $"({interX.AsString()},{interY.AsString()})";
    }

    private static (Rational?, Rational?) GetLineParameters((int x, int y) p1, (int x, int y) p2)
    {
        if (p1.x == p2.x) return (new Rational(p1.x, 1), null);
        if (p1.y == p2.y) return (null, new Rational(p1.y, 1));
        
        var a = new Rational(p2.y - p1.y, p2.x - p1.x);
        var b = new Rational(p2.x * p1.y - p1.x * p2.y, p2.x - p1.x);

        return (a, b);
    }
}