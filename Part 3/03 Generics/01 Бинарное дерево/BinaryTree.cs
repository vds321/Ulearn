using System;
using System.Collections;
using System.Collections.Generic;

namespace Generics.BinaryTrees
{
    public class BinaryTree<T> : IEnumerable<T> where T : IComparable
    {
        public BinaryTree<T> Left, Right;
        public SortedSet<T> Tree;
        public T Value;

        public BinaryTree()
        {
            Tree = new SortedSet<T>();
            Value = default;
        }

        public void Add(T item)
        {
            if (Tree.Count != 0)
            {
                if (Value.CompareTo(item) < 0)
                {
                    if (Right == null) Right = new BinaryTree<T>();
                    Right.Add(item);
                }
                else
                {
                    if (Left == null) Left = new BinaryTree<T>();
                    Left.Add(item);
                }
            }
            else
            {
                Value = item;
            }
            Tree.Add(item);
        }

        public void AddRange(IEnumerable<T> array)
        {
            foreach (var item in array)
            {
                Add(item);
            }
        }

        public IEnumerator<T> GetEnumerator() => Tree.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => Tree.GetEnumerator();
    }

    public class BinaryTree
    {
        public static BinaryTree<T> Create<T>(params T[] args) where T : IComparable
        {
            var tree = new BinaryTree<T>();
            tree.AddRange(args);
            return tree;
        }
    }
}
