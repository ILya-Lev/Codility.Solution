using System.Collections.Generic;

namespace Algorithms.Solutions
{
    public class MatrixMultiplication
    {
        public class MatrixSize
        {
            public int Height { get; set; }
            public int Width { get; set; }
        }

        public class MultiplicationOrder
        {
            public List<(MatrixSize lhs, MatrixSize rhs)> Order { get; set; }
            public int OperationsCount { get; set; }
        }
        //public static
    }
}