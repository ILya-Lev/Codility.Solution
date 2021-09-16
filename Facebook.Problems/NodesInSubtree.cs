using System;
using System.Collections.Generic;
using System.Linq;

namespace Facebook.Problems
{
    public class NodesInSubtree
    {
        public class Node
        {
            public int U { get; set; }
            public Node Left { get; set; }
            public Node Right { get; set; }
        }

        public class Query
        {
            public int U { get; set; }
            public char C { get; set; }
        }

        public static int[] CountOfNodes(Node root, List<Query> queries, String s)
        {
            var subtreePopulation = new Dictionary<int, int>();
            GetCurrentPopulation(root, subtreePopulation);

            return queries.Select(q => subtreePopulation[q.U]).ToArray();
        }

        private static int GetCurrentPopulation(Node root, Dictionary<int, int> population)
        {
            if (root is null) return 0;

            var subPopulation = GetCurrentPopulation(root.Left, population)
                                + GetCurrentPopulation(root.Right, population);

            population.Add(root.U, subPopulation);

            return subPopulation + 1;
        }
    }
}