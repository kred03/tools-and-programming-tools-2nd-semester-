using MyProject.Interfaces;
using MyProject.CustomException;
using System.Collections;

namespace MyProject.Collection
{
    public class MyCustomCollection<T> : ICustomCollection<T>, IEnumerable<T>
    {
        private class Node
        {
            public T Data;
            public Node? Next;
            public Node(T data)
            {
                Data = data;
                Next = null;
            }
        }

        private Node? head;
        private Node? current;
        private int count;

        public MyCustomCollection()
        {
            current = head = null;
            count = 0;
        }

        public T this[int index]
        {
            get
            {
                if (index < 0 || index >= count) throw new IndexOutOfRangeException("Индекс вне границ коллекции.");

                Node? node = head;
                for (int i = 0; i < index; i++)
                {
                    node = node?.Next;
                }
                return node!.Data;
            }
            set
            {
                if (index < 0 || index >= count) throw new IndexOutOfRangeException("Индекс вне границ коллекции.");

                Node? node = head;
                for (int i = 0; i < index; i++)
                {
                    node = node?.Next;
                }
                node!.Data = value;
            }
        }

        public int Count => count;

        public void Add(T item)
        {
            Node newNode = new(item);
            if (head == null)
            {
                head = newNode;
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
            if (head == null) throw new ItemNotFoundException("Элемент не найден в коллекции, так как коллекция пуста.");

            Node temp = head;
            Node? prev = null;

            // голова - нужный элемент
            if (temp.Data.Equals(item))
            {
                head = temp.Next;
                count--;
                return;
            }

            // поиск нужного узла
            while (temp != null && !temp.Data.Equals(item))
            {
                prev = temp;
                temp = temp.Next;
            }

            // элемент не найден
            if (temp == null) throw new ItemNotFoundException("Элемент не найден в коллекции");

            // удаление узла
            prev!.Next = temp.Next; // prev не может быть null
            count--;
        }

        public T RemoveCurrent()
        {
            if (current == null) throw new InvalidOperationException("Текущая позиция не задана.");

            T data = current.Data;
            Remove(data); // Удаляем элемент
            return data;
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
            if (current == null) throw new InvalidOperationException("Текущая позиция не задана.");
            return current.Data;
        }

        //public IEnumerator<T> GetEnumerator()
        //{
        //    for (int i = 0; i < count; i++)
        //    {
        //        yield return this[i]; //O(n^2)
        //    }
        //}
        public IEnumerator<T> GetEnumerator()
        {
            Node? currentNode = head;
            while (currentNode != null)
            {
                yield return currentNode.Data;
                currentNode = currentNode.Next;
            }
        }
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
