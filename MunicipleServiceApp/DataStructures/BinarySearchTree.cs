using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MunicipleServiceApp.DataStructures
{
    public class BinarySearchTree<T> where T : IComparable<T>
    {
        private class Node
        {
            public T Data;
            public Node Left;
            public Node Right;

            public Node(T data)
            {
                Data = data;
                Left = null;
                Right = null;
            }
        }

        private Node root;

        public void Insert(T value)
        {
            root = InsertRec(root, value);
        }

        private Node InsertRec(Node root, T value)
        {
            if (root == null)
                return new Node(value);

            if (value.CompareTo(root.Data) < 0)
                root.Left = InsertRec(root.Left, value);
            else if (value.CompareTo(root.Data) > 0)
                root.Right = InsertRec(root.Right, value);

            return root;
        }

        public List<T> InOrderTraversal()
        {
            var result = new List<T>();
            InOrderRec(root, result);
            return result;
        }

        private void InOrderRec(Node root, List<T> result)
        {
            if (root != null)
            {
                InOrderRec(root.Left, result);
                result.Add(root.Data);
                InOrderRec(root.Right, result);
            }
        }
    }
}
