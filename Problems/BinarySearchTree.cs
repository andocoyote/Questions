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

        }

        public static void BinarySearchTree()
        {
        
        }
    }

    public class BinarySearchTree
    {
        internal class Node
        {
            public int Value { get; set; }
            public Node? Left { get; set; }
            public Node? Right { get; set; }

            public Node(int value)
            {
                Value = value;
            }
        }

        private Node? root;

        public void Insert(int value)
        {
            root = InsertRecursive(root, value);
        }

        private Node InsertRecursive(Node? node, int value)
        {
            if (node == null)
            {
                // If the tree is empty, create a new node
                return new Node(value);
            }

            // Insert value into the appropriate subtree
            if (value < node.Value)
            {
                // Insert into the left subtree
                node.Left = InsertRecursive(node.Left, value);
            }
            else if (value > node.Value)
            {
                // Insert into the right subtree
                node.Right = InsertRecursive(node.Right, value);
            }

            // If the value already exists, do nothing

            return node;
        }

        public void Delete(int value)
        {
            root = DeleteRecursive(root, value);
        }

        private Node? DeleteRecursive(Node? node, int value)
        {
            if (node == null)
            {
                return null; // Node not found
            }

            if (value < node.Value)
            {
                // Value to delete is in the left subtree
                node.Left = DeleteRecursive(node.Left, value);
            }
            else if (value > node.Value)
            {
                // Value to delete is in the right subtree
                node.Right = DeleteRecursive(node.Right, value);
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
                    node.Value = FindMinValue(node.Right);

                    // Delete the node with the minimum value in the right subtree
                    node.Right = DeleteRecursive(node.Right, node.Value);
                }
            }

            return node;
        }

        private int FindMinValue(Node node)
        {
            int minValue = node.Value;
            while (node.Left != null)
            {
                minValue = node.Left.Value;
                node = node.Left;
            }
            return minValue;
        }
    }

}
