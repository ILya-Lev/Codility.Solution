using System;
using System.Collections.Generic;
using System.Linq;

namespace Facebook.Problems
{
    public class TreeAverage
    {
        public class Node<T>
        {
            public T Data { get; }
            public Node<T> LeftChild { get; set; }
            public Node<T> RightChild { get; set; }

            public Node(T data, Node<T> left = null, Node<T> right = null)
            {
                Data = data;
                LeftChild = left;
                RightChild = right;
            }
        }

        public Dictionary<int, double> CalculateAveragePerLevel(Node<int> root)
        {
            if (root is null) return new Dictionary<int, double>();

            //take slices of each level and calculate average on each level
            //lets start with a queue
            var levelQueue = new Queue<Node<int>>();//the queue always contains 1 complete level of the tree
            levelQueue.Enqueue(root);

            var averagePerLevel = new Dictionary<int, double>();
            int level = 0;

            while (levelQueue.Any())
            {
                var levelAverage = CalculateAverage(levelQueue);
                averagePerLevel.Add(level++, levelAverage);

                levelQueue.Enqueue(null);//show me where to stop
                var currentNode = levelQueue.Dequeue();
                while (currentNode != null)
                {
                    if (currentNode.LeftChild != null) levelQueue.Enqueue(currentNode.LeftChild);
                    if (currentNode.RightChild != null) levelQueue.Enqueue(currentNode.RightChild);
                    
                    currentNode = levelQueue.Dequeue();
                    //current level nodes| null | next level nodes
                }
            }

            return averagePerLevel;
        }

        private double CalculateAverage(Queue<Node<int>> nodes) => nodes.Any(n => n != null)
            ? nodes.Average(n => n.Data)
            : double.NaN;
    }
}
