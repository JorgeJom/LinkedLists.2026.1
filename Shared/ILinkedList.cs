namespace Shared;

public interface ILinkedList<T> where T : IComparable<T>
{
    bool Contains(T data);
    void InsertAtBeginning(T data);
    void InsertAtEnding(T data);
    void InsertOrdered(T data);
    void Remove(T data);
    void Reverse();
    void Sort();
    string ToString();
}