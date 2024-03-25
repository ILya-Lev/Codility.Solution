namespace Coderbyte;
/*
 RREF Matrix
   Have the function RREFMatrix(strArr) take strArr which will be an array of integers represented as strings. Within the array there will also be "<>" elements which represent break points. The array will make up a matrix where the (number of break points + 1) represents the number of rows. Here is an example of how strArr may look: ["5","7","8","<>","1","1","2"]. There is one "<>", so 1 + 1 = 2. Therefore there will be two rows in the array and the contents will be row1=[5 7 8] and row2=[1 1 2]. Your program should take the given array of elements, create the proper matrix, and then through the process of Gaussian elimination create a reduced row echelon form matrix (RREF matrix). For the array above, the resulting RREF matrix would be: row1=[1 0 3], row2=[0 1 -1]. Your program should return that resulting RREF matrix in string form combining all the integers, like so: "10301-1". The matrix can have any number of rows and columns (max=6x6 matrix and min=1x1 matrix). The matrix will not necessarily be a square matrix. If the matrix is an nx1 matrix it will not contain the "<>" element. The integers in the array will be such that the resulting RREF matrix will not contain any fractional numbers.
*/
public class ReducedRowEchelonFormMatrix
{
    private const string RowSeparator = "<>";
    public static string RREFMatrix(string[] strArr)
    {
        var m = ParseMatrix(strArr);

        GoDownward(m);
        Normalize(m);
        GoUpward(m);

        return string.Join("", m.SelectMany(line => line).Select(e => $"{(int)e}"));
    }

    private static List<List<double>> ParseMatrix(string[] strArr)
    {
        var m = new List<List<double>> { new List<double>() };

        foreach (var e in strArr)
        {
            if (e == RowSeparator)
            {
                m.Add(new List<double>());
                continue;
            }
            m.Last().Add(int.Parse(e));
        }
        return m;
    }

    private static void GoDownward(List<List<double>> m)
    {
        for (int i = 1; i < m.Count; i++)
        {
            SortLines(m, i - 1);

            var currentLine = m[i - 1];
            if (currentLine.All(e => e == 0))
                continue;

            var head = currentLine.Select((e, idx) => (e, idx)).First(item => item.e != 0);
            for (int j = i; j < m.Count; j++)
            {
                if (m[j][head.idx] == 0)
                    continue;

                var f = -m[j][head.idx] * 1.0 / head.e;
                m[j] = m[j].Zip(currentLine, (lhs, rhs) =>
                {
                    var v = lhs + rhs * f;
                    var sign = Math.Sign(v);
                    return sign * Math.Round(Math.Abs(v), 5);
                }).ToList();
            }
        }
    }

    private static void SortLines(List<List<double>> m, int start)
    {
        for (int i = start; i < m.Count; i++)
        {
            if (m[i].Count > start && m[i][start] != 0)
            {
                (m[i], m[start]) = (m[start], m[i]);
                break;
            }
        }

        var last = m.Count - 1;
        for (; last >= start; last--)
            if (m[last].Any(e => e != 0))
                break;

        for (var head = start; head != last; head++)
        {
            if (m[head].All(e => e == 0))
            {
                (m[head], m[last]) = (m[last], m[head]);
                last--;
            }
        }
    }

    private static void Normalize(List<List<double>> m)
    {
        for (int i = 0; i < m.Count; i++)
        {
            var line = m[i];
            if (line.All(e => e == 0))
                continue;

            var head = line.First(e => e != 0);
            if (head == 1)
                continue;


            m[i] = line.Select(e => e / head).ToList();
        }
    }

    private static void GoUpward(List<List<double>> m)
    {
        for (int i = m.Count - 1; i > 0; i--)
        {
            var currentLine = m[i];
            if (currentLine.All(e => e == 0))
                continue;

            var head = currentLine.Select((e, idx) => (e, idx)).First(item => item.e != 0);
            for (int j = i - 1; j >= 0; j--)
            {
                if (m[j][head.idx] == 0)
                    continue;

                var f = -m[j][head.idx] * 1.0 / head.e;
                m[j] = m[j].Zip(currentLine, (lhs, rhs) =>
                {
                    var v = lhs + rhs * f;
                    var sign = Math.Sign(v);
                    return sign * Math.Round(Math.Abs(v), 5);
                }).ToList();
            }
        }
    }
}