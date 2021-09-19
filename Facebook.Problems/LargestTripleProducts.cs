using System;

namespace Facebook.Problems
{
    public class LargestTripleProducts
    {
        public static int[] FindMaxProduct(int[] arr)
        {
            var tripleProducts = new int[arr.Length];

            Triplet triplet = null;

            for (int i = 0; i < arr.Length; i++)
            {
                if (i < 2)
                {
                    tripleProducts[i] = -1;
                    continue;
                }
                if (i == 2)
                {
                    triplet = new Triplet(arr[0], arr[1], arr[2]);
                    tripleProducts[i] = triplet.Product;
                    continue;
                }

                triplet.ChallengeItem(arr[i]);
                tripleProducts[i] = triplet.Product;
            }

            return tripleProducts;
        }

        private class Triplet
        {
            public int M1 { get; private set; }
            public int M2 { get; private set; }
            public int M3 { get; private set; }

            public int Product { get; private set; }

            public Triplet(int n1, int n2, int n3)
            {
                M1 = Math.Max(n1, Math.Max(n2, n3));
                M3 = Math.Min(n1, Math.Min(n2, n3));
                M2 = n1 + n2 + n3 - M1 - M3;
                
                Product = M1 * M2 * M3;
            }

            public void ChallengeItem(int x)
            {
                if (x <= M3) return;
                
                M3 = x;
                Product = M1 * M2 * M3;
                
                if (M3 > M2)
                {
                    (M3, M2) = (M2, M3);

                    if (M2 > M1)
                        (M1, M2) = (M2, M1);
                }
            }

            private void InitializeState(int n1, int n2, int n3)
            {
                if (n1 > n2)
                {
                    if (n1 > n3)
                    {
                        M1 = n1;
                        M2 = Math.Max(n2, n3);
                        M3 = Math.Min(n2, n3);
                    }
                    else
                    {
                        M1 = Math.Max(n1, n3);
                        M2 = Math.Min(n1, n3);
                        M3 = n2;
                    }
                }
                else
                {
                    if (n1 > n3)
                    {
                        M1 = n2;
                        M2 = Math.Max(n1, n3);
                        M3 = Math.Min(n1, n3);
                    }
                    else
                    {
                        M1 = Math.Max(n2, n3);
                        M2 = Math.Min(n2, n3);
                        M3 = n1;
                    }
                }
            }
        }
    }
}