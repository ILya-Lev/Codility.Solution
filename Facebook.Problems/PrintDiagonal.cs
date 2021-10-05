using System.Collections.Generic;
using System.Text;

namespace Facebook.Problems
{
    /// <summary>
    /// a task from the actual screening 2021-10-05
    /// this one was the first one; did not see the pattern from the start
    /// 2 tips from the interviewer helped me
    /// I was hurrying up all the time!!!
    /// </summary>
    public class PrintDiagonal
    {
        public static List<string> Print(int[,] matrix)
        {
            var diagonals = new List<string>();
            //1. compose diagonals starting at the first row of the matrix
            for (int diagonalNumber = 0; diagonalNumber < matrix.GetLength(1); diagonalNumber++)
            {
                var diagonal = new StringBuilder();
                for (var i = 0; i < matrix.GetLength(0) && i <= diagonalNumber; i++)
                {
                    var j = diagonalNumber - i;
                    diagonal.Append($"{matrix[i, j]} ");
                }

                diagonals.Add(diagonal.ToString());
            }

            //2. compose diagonals starting at the last column of the matrix
            for (int diagonalNumber = matrix.GetLength(1);
                diagonalNumber < matrix.GetLength(1) + matrix.GetLength(0) - 1;
                diagonalNumber++)
            {
                var diagonal = new StringBuilder();
                for (var j = matrix.GetLength(1) - 1;
                    j >= 0 && diagonalNumber - j < matrix.GetLength(0);
                    j--)
                {
                    var i = diagonalNumber - j;
                    diagonal.Append($"{matrix[i, j]} ");
                }

                diagonals.Add(diagonal.ToString());
            }

            return diagonals;
        }
    }
}
