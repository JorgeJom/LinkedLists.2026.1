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

    // ─── Inserción ordenada ascendente ───────────────────────────────────────
    public void InsertOrdered(T data)
    {
        var newNode = new Node<T>(data);

        // Lista vacía
        if (_head == null)
        {
            _head = newNode;
            _tail = newNode;
            return;
        }

        // Insertar antes de la cabeza
        if (data.CompareTo(_head.Data) <= 0)
        {
            newNode.Next = _head;
            _head.Previous = newNode;
            _head = newNode;
            return;
        }

        // Buscar posición correcta
        var current = _head;
        while (current.Next != null && current.Next.Data!.CompareTo(data) <= 0)
        {
            current = current.Next;
        }

        // Insertar al final
        if (current.Next == null)
        {
            current.Next = newNode;
            newNode.Previous = current;
            _tail = newNode;
        }
        else // Insertar en medio
        {
            newNode.Next = current.Next;
            newNode.Previous = current;
            current.Next.Previous = newNode;
            current.Next = newNode;
        }
    }

    // ─── InsertAtBeginning / InsertAtEnding (se conservan) ──────────────────
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

    // ─── Contains ────────────────────────────────────────────────────────────
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

    // ─── Remove (primera ocurrencia) ─────────────────────────────────────────
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

    // ─── RemoveAll (todas las ocurrencias) ───────────────────────────────────
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

    // ─── Lógica interna para eliminar un nodo ────────────────────────────────
    private void RemoveNode(Node<T> node)
    {
        if (node == _head && node == _tail) // único elemento
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

    // ─── Reverse (invertir la lista) ─────────────────────────────────────────
    public void Reverse()
    {
        if (_head == null || _head == _tail) return;

        var current = _head;
        while (current != null)
        {
            // Intercambiar Next y Previous en cada nodo
            var temp = current.Next;
            current.Next = current.Previous;
            current.Previous = temp;
            current = temp; // avanzar
        }

        // Intercambiar _head y _tail
        var swap = _head;
        _head = _tail;
        _tail = swap;
    }

    // ─── Sort (ordenar descendentemente = invertir la lista ya ordenada) ──────
    public void Sort()
    {
        Reverse();
    }

    // ─── Moda ────────────────────────────────────────────────────────────────
    public List<T> Mode()
    {
        // Contar frecuencias
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

    // ─── Gráfico de ocurrencias ───────────────────────────────────────────────
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

    // ─── ToString (hacia adelante) ────────────────────────────────────────────
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

    // ─── ToStringReverse (hacia atrás) ───────────────────────────────────────
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

    // Elimina la primera aparición de cada valor que sea moda
    public void RemoveFirstMode()
    {
        // Contar frecuencias respetando el orden de aparición
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

        // Recorrer de izquierda a derecha y eliminar el primer nodo cuya frecuencia sea la máxima
        current = _head;
        while (current != null)
        {
            if (frequencies.ContainsKey(current.Data!) && frequencies[current.Data!] == maxFreq)
            {
                RemoveNode(current);
                return; // solo una ocurrencia
            }
            current = current.Next;
        }
    }

    // Elimina todas las apariciones de cada valor que sea moda
    public void RemoveAllModes()
    {
        var modes = Mode();
        foreach (var mode in modes)
            RemoveAll(mode);
    }

}
