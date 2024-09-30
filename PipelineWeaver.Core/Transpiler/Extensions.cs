using System;
using System.Text.Json;

namespace PipelineWeaver.Core.Transpiler;

public static class TypeExtensions
{
    public static void CallGenericMethod(this Type callerType, string methodName, Type genericType, object[]? parameters)
    {
        var method = callerType.GetMethod(methodName, System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.NonPublic);
        if (method is null)
            throw new ArgumentException(nameof(method));
        var genericMethod = method.MakeGenericMethod(genericType);
        genericMethod.Invoke(null, parameters);
    }
}

public static class StringExtensions
{
    public static List<string> SplitLinesAtNewLine(this string multilineString)
    {
        return [.. multilineString.Split(Environment.NewLine)];
    }

    public static string Join(this List<string> list, string separator)
    {
        return string.Join(separator, list);
    }
}

public static class AdoObjectExtensions
{
    public static string ToJson(this object obj)
    {
        return JsonSerializer.Serialize(obj);
    }

    public static string ToJson<T>(this T obj)
    {
        return JsonSerializer.Serialize(obj);
    }
}
