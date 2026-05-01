using Shared;

namespace SimpleList;

public class SinglyLinkedList<T> : ILinkedList<T> where T : IComparable<T>
{
    private Node<T>? _head;

    public SinglyLinkedList()
    {
        _head = null;
    }

    override public string ToString()
    {
        var current = _head;
        var result = string.Empty;
        while (current != null)
        {
            result += $"{current.Data} -> ";
            current = current.Next;
        }
        result += "null";
        return result;
    }

    public void InsertAtBeginning(T data)
    {
        var newNode = new Node<T>(data);
        newNode.Next = _head;
        _head = newNode;
    }

    public void InsertAtEnding(T data)
    {
        var newNode = new Node<T>(data);
        if (_head == null)
        {
            _head = newNode;
            return;
        }
        var current = _head;
        while (current.Next != null)
        {
            current = current.Next;
        }
        current.Next = newNode;
    }

    public bool Contains(T data)
    {
        var current = _head;
        while (current != null)
        {
            if (current.Data != null && current.Data.Equals(data))
            {
                return true;
            }
            current = current.Next;
        }
        return false;
    }

    public void Remove(T data)
    {
        if (_head == null) return;
        if (_head.Data != null && _head.Data.Equals(data))
        {
            _head = _head.Next;
            return;
        }
        var current = _head;
        while (current.Next != null)
        {
            if (current.Next.Data != null && current.Next.Data.Equals(data))
            {
                current.Next = current.Next.Next;
                return;
            }
            current = current.Next;
        }
    }

    public void Reverse()
    {
        Node<T>? previous = null;
        var current = _head;
        while (current != null)
        {
            var next = current.Next;
            current.Next = previous;
            previous = current;
            current = next;
        }
        _head = previous;
    }

    public void InsertOrdered(T data)
    {
        throw new NotImplementedException();
    }

    public void Sort()
    {
        throw new NotImplementedException();
    }
}