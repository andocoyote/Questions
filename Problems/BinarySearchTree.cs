using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practice.Problems
{
    internal class Problem13
    {
        public static void BinarySearchTreeTest()
        {
            BinarySearchTree<int> tree = new BinarySearchTree<int>();
            tree.Insert(5);
            tree.Insert(3);
            tree.Insert(7);
            tree.Insert(2);
            tree.Insert(4);
            tree.Insert(6);
            tree.Insert(8);

            Console.WriteLine("Breadth-first traversal:");
            tree.PrintBreadthFirst();

            Console.WriteLine("Depth-first traversal:");
            tree.PrintDepthFirst();
        }
    }

    public class BinarySearchTree<T> where T : IComparable<T>
    {
        internal class Node
        {
            public T Data { get; set; }
            public Node? Left { get; set; }
            public Node? Right { get; set; }

            public Node(T data)
            {
                Data = data;
            }
        }

        private Node? root;

        public void Insert(T data)
        {
            root = InsertRecursive(root, data);
        }

        private Node InsertRecursive(Node? node, T data)
        {
            if (node == null)
            {
                return new Node(data);
            }

            if (data.CompareTo(node.Data) < 0)
            {
                node.Left = InsertRecursive(node.Left, data);
            }
            else if (data.CompareTo(node.Data) > 0)
            {
                node.Right = InsertRecursive(node.Right, data);
            }

            return node;
        }

        public void Delete(T data)
        {
            root = DeleteRecursive(root, data);
        }

        private Node? DeleteRecursive(Node? node, T data)
        {
            if (node == null)
            {
                return null; // Node not found
            }

            if (data.CompareTo(node.Data) < 0)
            {
                // Value to delete is in the left subtree
                node.Left = DeleteRecursive(node.Left, data);
            }
            else if (data.CompareTo(node.Data) > 0)
            {
                // Value to delete is in the right subtree
                node.Right = DeleteRecursive(node.Right, data);
            }
            else
            {
                // Node to delete found
                if (node.Left == null)
                {
                    // Node has no left child or is a leaf node
                    return node.Right;
                }
                else if (node.Right == null)
                {
                    // Node has no right child
                    return node.Left;
                }
                else
                {
                    // Node has two children
                    // Find the minimum value in the right subtree (or maximum value in the left subtree)
                    node.Data = FindMinValue(node.Right);

                    // Delete the node with the minimum value in the right subtree
                    node.Right = DeleteRecursive(node.Right, node.Data);
                }
            }

            return node;
        }

        private T FindMinValue(Node node)
        {
            T minValue = node.Data;
            while (node.Left != null)
            {
                minValue = node.Left.Data;
                node = node.Left;
            }
            return minValue;
        }

        public void PrintBreadthFirst()
        {
            if (root == null)
            {
                Console.WriteLine("Tree is empty");
                return;
            }

            Queue<Node> queue = new Queue<Node>();
            queue.Enqueue(root);

            while (queue.Count > 0)
            {
                Node current = queue.Dequeue();
                Console.Write(current.Data + " ");

                if (current.Left != null)
                {
                    queue.Enqueue(current.Left);
                }

                if (current.Right != null)
                {
                    queue.Enqueue(current.Right);
                }
            }

            Console.WriteLine();
        }

        public void PrintDepthFirst()
        {
            PrintDepthFirstRecursive(root);
            Console.WriteLine();
        }

        private void PrintDepthFirstRecursive(Node? node)
        {
            if (node == null)
            {
                return;
            }

            PrintDepthFirstRecursive(node.Left);
            Console.Write(node.Data + " ");
            PrintDepthFirstRecursive(node.Right);
        }
    }

}
