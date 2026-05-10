using Shared;

namespace DoubleList;

public class DoubleLinkedList<T> : ILinkedList<T> where T : IComparable<T>
{
    private Node<T>? _head;
    private Node<T>? _tail;

    public DoubleLinkedList()
    {
        _head = null;
        _tail = null;
    }

    // Ordered ascending insertion
    public void InsertOrdered(T data)
    {
        var newNode = new Node<T>(data);

        // Empty list
        if (_head == null)
        {
            _head = newNode;
            _tail = newNode;
            return;
        }

        // Insert before the head
        if (data.CompareTo(_head.Data) <= 0)
        {
            newNode.Next = _head;
            _head.Previous = newNode;
            _head = newNode;
            return;
        }

        // Find correct position
        var current = _head;
        while (current.Next != null && current.Next.Data!.CompareTo(data) <= 0)
        {
            current = current.Next;
        }

        // Insert at the end
        if (current.Next == null)
        {
            current.Next = newNode;
            newNode.Previous = current;
            _tail = newNode;
        }
        else // Insert in middle
        {
            newNode.Next = current.Next;
            newNode.Previous = current;
            current.Next.Previous = newNode;
            current.Next = newNode;
        }
    }

    // InsertAtBeginning / InsertAtEnding
    public void InsertAtBeginning(T data)
    {
        var newNode = new Node<T>(data);
        if (_head == null)
        {
            _head = newNode;
            _tail = newNode;
        }
        else
        {
            newNode.Next = _head;
            _head.Previous = newNode;
            _head = newNode;
        }
    }

    public void InsertAtEnding(T data)
    {
        var newNode = new Node<T>(data);
        if (_tail == null)
        {
            _head = newNode;
            _tail = newNode;
        }
        else
        {
            _tail.Next = newNode;
            newNode.Previous = _tail;
            _tail = newNode;
        }
    }

    // Contains
    public bool Contains(T data)
    {
        var current = _head;
        while (current != null)
        {
            if (current.Data!.Equals(data))
                return true;
            current = current.Next;
        }
        return false;
    }

    // Remove (first occurrence) 
    public void Remove(T data)
    {
        var current = _head;
        while (current != null)
        {
            if (current.Data!.Equals(data))
            {
                RemoveNode(current);
                return;
            }
            current = current.Next;
        }
    }

    //RemoveAll (all occurrences)
    public void RemoveAll(T data)
    {
        var current = _head;
        while (current != null)
        {
            var next = current.Next;
            if (current.Data!.Equals(data))
                RemoveNode(current);
            current = next;
        }
    }

    // Internal logic to remove a node
    private void RemoveNode(Node<T> node)
    {
        if (node == _head && node == _tail) // only element
        {
            _head = null;
            _tail = null;
        }
        else if (node == _head)
        {
            _head = _head.Next;
            _head!.Previous = null;
        }
        else if (node == _tail)
        {
            _tail = _tail.Previous;
            _tail!.Next = null;
        }
        else
        {
            node.Previous!.Next = node.Next;
            node.Next!.Previous = node.Previous;
        }
    }

    // Reverse (reverse the list) 
    public void Reverse()
    {
        if (_head == null || _head == _tail) return;

        var current = _head;
        while (current != null)
        {
            // Swap Next and Previous on each node
            var temp = current.Next;
            current.Next = current.Previous;
            current.Previous = temp;
            current = temp;
        }

        // Swap _head and _tail
        var swap = _head;
        _head = _tail;
        _tail = swap;
    }

    // Sort (sort descending = reverse the already sorted list)
    public void Sort()
    {
        Reverse();
    }

    // Fashion
    public List<T> Mode()
    {
        // Count frequencies
        var frequencies = new Dictionary<T, int>();
        var current = _head;
        while (current != null)
        {
            if (frequencies.ContainsKey(current.Data!))
                frequencies[current.Data!]++;
            else
                frequencies[current.Data!] = 1;
            current = current.Next;
        }

        if (frequencies.Count == 0) return new List<T>();

        var maxFreq = frequencies.Values.Max();
        return frequencies
            .Where(kv => kv.Value == maxFreq)
            .Select(kv => kv.Key)
            .ToList();
    }

    // Occurrence chart
    public string Chart()
    {
        var frequencies = new Dictionary<T, int>();
        var current = _head;
        while (current != null)
        {
            if (frequencies.ContainsKey(current.Data!))
                frequencies[current.Data!]++;
            else
                frequencies[current.Data!] = 1;
            current = current.Next;
        }

        var result = string.Empty;
        foreach (var kv in frequencies)
        {
            result += $"{kv.Key} {new string('*', kv.Value)}\n";
        }
        return result;
    }

    // ToString (forwards)
    public override string ToString()
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

    // ToStringReverse (backward)
    public string ToStringReverse()
    {
        var current = _tail;
        var result = string.Empty;
        while (current != null)
        {
            result += $"{current.Data} -> ";
            current = current.Previous;
        }
        result += "null";
        return result;
    }

    // Remove the first occurrence of each value that is a mode
    public void RemoveFirstMode()
    {
        // Count frequencies respecting the order of appearance
        var frequencies = new Dictionary<T, int>();
        var current = _head;
        while (current != null)
        {
            if (frequencies.ContainsKey(current.Data!))
                frequencies[current.Data!]++;
            else
                frequencies[current.Data!] = 1;
            current = current.Next;
        }

        if (frequencies.Count == 0) return;

        var maxFreq = frequencies.Values.Max();

        // Traverse from left to right and remove the first node whose frequency is the highest
        current = _head;
        while (current != null)
        {
            if (frequencies.ContainsKey(current.Data!) && frequencies[current.Data!] == maxFreq)
            {
                RemoveNode(current);
                return;
            }
            current = current.Next;
        }
    }

    // Remove all occurrences of each value that is a mode
    public void RemoveAllModes()
    {
        var modes = Mode();
        foreach (var mode in modes)
            RemoveAll(mode);
    }

}
