﻿namespace ClassicalProblems;

public class NeuralNetwork
{
    public static class Utils
    {
        public static double CalculateDotProduct(IReadOnlyList<double> lhs, IReadOnlyList<double> rhs)
        {
            if (lhs.Count != rhs.Count)
                throw new ArgumentException($"lhs and rhs have different length: {lhs.Count} vs {rhs.Count}");

            var dotProduct = 0.0;
            for (int i = 0; i < lhs.Count; i++)
            {
                dotProduct += lhs[i] * rhs[i];
            }

            return dotProduct;
        }

        public static double GetSigmoidFor(double x) => 1 / (1 + 1 / Math.Exp(x));
        
        public static double GetSigmoidDerivativeFor(double x)
        {
            var s = GetSigmoidFor(x);
            return s * (1 - s);
        }
    }
}