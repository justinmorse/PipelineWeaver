using System;
using System.Text.Json;

namespace PipelineWeaver.Ado;

public abstract class AdoObjectBase
{
}

public class AdoObject<T> : AdoObjectBase
{
    public Type? Type => Value?.GetType();
    public bool HasValue => Value is not null;
    public T? Value { get; set; }

    public AdoObject()
    {
        Value = default;
    }

    public AdoObject(T? value)
    {
        Value = value;
    }
}

public class AdoJsonObject<T> : AdoObject<T>
{
    public bool SerializeAsSingleLine { get; set; }
    public AdoJsonObject(T value, bool serializeAsSingleLine) : base(value)
    {
        SerializeAsSingleLine = serializeAsSingleLine;
    }
}



