
using System.Collections;
using System.Reflection.Metadata.Ecma335;

namespace PipelineWeaver.Ado;
public abstract class AdoSectionBase
{
    //left empty
}

public class AdoSectionCollection<T> : AdoSectionBase, ICollection<T>
{
    internal List<T> Items { get; set; } = [];
    public int Count => Items.Count;

    public bool IsReadOnly => false;

    public void Add(T item)
    {
        Items.Add(item);
    }

    public void Clear()
    {
        Items.Clear();
    }

    public bool Contains(T item)
    {
        return Items.Contains(item);
    }

    public void CopyTo(T[] array, int arrayIndex)
    {
        Items.CopyTo(array, arrayIndex);
    }

    public IEnumerator<T> GetEnumerator()
    {
        return Items.GetEnumerator();
    }

    public bool Remove(T item)
    {
        return Items.Remove(item);
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return Items.GetEnumerator();
    }
}

public enum SerializationType
{
    Yaml
}