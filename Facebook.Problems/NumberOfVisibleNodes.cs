using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Facebook.Problems
{
    public class NumberOfVisibleNodes
    {
        public class Node
        {
            public int Value { get; }
            public Node Left { get; set; }
            public Node Right { get; set; }

            public Node(int value) => Value = value;
        }

        public static int VisibleNodes(Node root)
        {
            var level = new Queue<Node>();
            level.Enqueue(root);

            var maxDepth = 0;
            while (level.Any())
            {
                level.Enqueue(null);//current level is completely traversed token
                maxDepth++;
                for (var current = level.Dequeue(); current != null; current = level.Dequeue())
                {
                    if (current.Left != null) level.Enqueue(current.Left);
                    if (current.Right != null) level.Enqueue(current.Right);
                }
            }
            return maxDepth;
        }

        public static int VisibleNodesByDepth(Node root)
        {
            var depth = new Stack<Node>();
            depth.Push(root);

            var maxDepth = 0;
            while (depth.Any())
            {
                var current = depth.Peek();
                if (current.Left != null)
                {
                    depth.Push(current.Left);
                    current.Left = null;
                    continue;
                }

                if (current.Right != null)
                {
                    depth.Push(current.Right);
                    current.Right = null;
                    continue;
                }

                maxDepth = Math.Max(maxDepth, depth.Count);
                depth.Pop();
            }
            return maxDepth;
        }

        public static List<int> InAscendingOrder(Node root)
        {
            var sorted = new List<int>();

            var stack = new Stack<Node>();
            stack.Push(root);

            while (stack.Count > 0)
            {
                var current = stack.Peek();
                if (current.Left != null)
                {
                    stack.Push(current.Left);
                    current.Left = null;
                    continue;
                }
                
                sorted.Add(current.Value);
                stack.Pop();

                if (current.Right != null)
                    stack.Push(current.Right);
            }

            return sorted;
        }

        private static List<int> _ascending = new List<int>();
        public static List<int> InAscendingOrderRecursion(Node root)
        {
            _ascending.Clear();

            if (root != null) FillIn(root);

            return _ascending;
        }

        private static void FillIn(Node root)
        {
            if (root.Left != null) FillIn(root.Left);
            
            _ascending.Add(root.Value);
            
            if (root.Right != null) FillIn(root.Right);
        }
    }
}
