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

public static class DictionaryExtensions
{
    public static string ToJson(this Dictionary<string, object> dict)
    {
        return JsonSerializer.Serialize(dict);
    }
}
