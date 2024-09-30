using System;
using System.Reflection;
using System.Text.Json;

namespace PipelineWeaver.Core.Transpiler;

public static class TypeExtensions
{
    public static void CallGenericMethod(this Type callerType, object? instance, string methodName, Type genericType, IEnumerable<object>? parameters)
    {
        var expectedParameterTypes = parameters?.Select(p => p.GetType()).ToArray() ?? Array.Empty<Type>();

        MethodInfo? method = null;
        var methods = callerType.GetMethods();
        foreach (var m in methods)
        {
            if (m.Name == methodName)
            {
                var parameterTypes = m.GetParameters().Select(p => p.ParameterType).ToArray();
                if (parameterTypes.Length == expectedParameterTypes.Length)
                {
                    bool match = true;
                    for (int i = 0; i < expectedParameterTypes.Length; i++)
                        match &= parameterTypes[i].Similar(expectedParameterTypes[i]);
                    if (match)
                    {
                        method = m;
                        break;
                    }
                }
            }
        }
        if (method is null)
            throw new ArgumentException(nameof(method));
        var genericArgument = genericType.GetTypeInfo().GetGenericArguments().Single();
        var genericMethod = method.MakeGenericMethod(genericArgument);
        genericMethod.Invoke(instance, parameters?.ToArray() ?? Array.Empty<object>());
    }

    public static bool Similar(this Type reference, Type type)
    {
        if (reference.IsGenericParameter && type.IsGenericParameter)
        {
            return reference.GenericParameterPosition == type.GenericParameterPosition;
        }

        return ComparableType(reference) == ComparableType(type);

        Type ComparableType(Type cType)
            => cType.IsGenericType ? cType.GetGenericTypeDefinition() : cType;
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
