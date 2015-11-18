using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task.BinaryTree
{
    public sealed class BinaryTree<T> : IEnumerable<T>
    {
        private class Node<T>
        {
            public T Value { get; set; }
            public Node<T> Right { get; set; }
            public Node<T> Left { get; set; }
            public Node(T value)
            {
                this.Value = value;
            }
        }

        private Node<T> root;
        public int Count { get; private set; }
        public IComparer<T> Comparer;
        public T Min
        {
            get
            {
                if (root == null)
                    throw new InvalidOperationException();
                var current = root;
                while (current.Left != null)
                    current = current.Left;
                return current.Value;
            }
        }
        public T Max
        {
            get
            {
                if (root == null)
                    throw new InvalidOperationException();
                var current = root;
                while (current.Right != null)
                    current = current.Right;
                return current.Value;
            }
        }

        public BinaryTree()
            : this(Comparer<T>.Default)
        { }
        public BinaryTree(Comparison<T> compare)
            : this(Comparer<T>.Create(compare))
        { }
        public BinaryTree(IComparer<T> comparer)
        {
            if (comparer == null)
                this.Comparer = Comparer<T>.Default;
            else
                this.Comparer = comparer;
            if (Comparer == null)
                throw new ArgumentException("Comparer is null");
        }

        public void Add(T value)
        {
            var node = new Node<T>(value);
            int result;
            if (root == null)
                root = node;
            else
            {
                Node<T> current = root;
                Node<T> parent = null;
                do
                {
                    parent = current;
                    result = Comparer.Compare(value, current.Value);
                    current = result < 0 ? current.Left : current.Right;
                }
                while (current != null);
                if (result < 0)
                    parent.Left = node;
                else
                    parent.Right = node;
            }
            Count++;
        }
        public bool Remove(T value)
        {
            if (root == null)
                return false;
            var current = root;
            Node<T> parent = null;
            while (current != null)
            {
                int result = Comparer.Compare(value, current.Value);
                if (result == 0)
                    break;
                parent = current;
                current = result < 0 ? current.Left : current.Right;
            }
            if (current == null)
                return false;
            return Remove(current, parent);

        }
        private bool Remove(Node<T> current, Node<T> parent)
        {
            Node<T> removed = null;
            if (current.Left == null || current.Right == null)
            {
                Node<T> child = null;
                removed = current;

                if (current.Left != null)
                    child = current.Left;
                else if (current.Right != null)
                    child = current.Right;

                if (parent == null)
                    root = child;
                else
                {
                    if (parent.Left == current)
                        parent.Left = child;
                    else
                        parent.Right = child;
                }
            }
            else
            {
                Node<T> mostLeft = current.Right;
                Node<T> mostLeftParent = current;

                while (mostLeft.Left != null)
                {
                    mostLeftParent = mostLeft;
                    mostLeft = mostLeft.Left;
                }

                current.Value = mostLeft.Value;
                removed = mostLeft;

                if (mostLeftParent.Left == mostLeft)
                    mostLeftParent.Left = null;
                else
                    mostLeftParent.Right = mostLeft.Right;
            }

            Count--;
            return true;
        }


        public IEnumerator<T> GetEnumerator()
        {
            return Inorder(root).GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public IEnumerable<T> Preorder()
        {
            return Preorder(root);
        }
        private IEnumerable<T> Preorder(Node<T> node)
        {
            if (node == null)
                yield break;
            yield return node.Value;
            foreach (var e in Preorder(node.Left))
                yield return e;

            foreach (var e in Preorder(node.Right))
                yield return e;
        }
        public IEnumerable<T> Inorder()
        {
            return Inorder(root);
        }
        private IEnumerable<T> Inorder(Node<T> node)
        {
            if (node == null)
                yield break;
            foreach (var e in Preorder(node.Left))
                yield return e;
            yield return node.Value;
            foreach (var e in Preorder(node.Right))
                yield return e;
        }
        public IEnumerable<T> Postorder()
        {
            return Postorder(root);
        }
        private IEnumerable<T> Postorder(Node<T> node)
        {
            if (node == null)
                yield break;
            foreach (var e in Preorder(node.Left))
                yield return e;
            foreach (var e in Preorder(node.Right))
                yield return e;
            yield return node.Value;
        }
    }
}
