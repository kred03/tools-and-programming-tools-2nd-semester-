using Interfaces;
using System;

namespace Collections
{
    public class MyCustomCollection<T> : ICustomCollection<T>
    {
        private class Node
        {
            public T Value;
            public Node Next;

            public Node(T value)
            {
                Value = value;
                Next = null;
            }
        }

        private Node head;
        private Node current;
        private int count;

        public MyCustomCollection()
        {
            head = null;
            current = null;
            count = 0;
        }

        public T this[int index]
        {
            get
            {
                if (index < 0 || index >= count)
                    throw new IndexOutOfRangeException();

                Node temp = head;
                for (int i = 0; i < index; i++)
                {
                    temp = temp.Next;
                }
                return temp.Value;
            }
            set
            {
                if (index < 0 || index >= count)
                    throw new IndexOutOfRangeException();

                Node temp = head;
                for (int i = 0; i < index; i++)
                {
                    temp = temp.Next;
                }
                temp.Value = value;
            }
        }

        public void Add(T item)
        {
            Node newNode = new Node(item);
            if (head == null)
            {
                head = newNode;
                current = newNode;
            }
            else
            {
                Node temp = head;
                while (temp.Next != null)
                {
                    temp = temp.Next;
                }
                temp.Next = newNode;
            }
            count++;
        }

        public void Remove(T item)
        {
            if (head == null) return;

            if (head.Value.Equals(item))
            {
                head = head.Next;
                count--;
                return;
            }

            Node temp = head;
            while (temp.Next != null)
            {
                if (temp.Next.Value.Equals(item))
                {
                    temp.Next = temp.Next.Next;
                    count--;
                    return;
                }
                temp = temp.Next;
            }
        }

        public T RemoveCurrent()
        {
            if (current == null)
                throw new InvalidOperationException("No current element to remove.");

            T value = current.Value;
            Remove(value);
            current = head; // Reset current to the head after removal
            return value;
        }

        public void Reset()
        {
            current = head;
        }

        public void Next()
        {
            if (current != null)
                current = current.Next;
        }

        public T Current()
        {
            if (current == null)
                throw new InvalidOperationException("No current element.");
            return current.Value;
        }

        public int Count => count;
    }
}