using System;

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
