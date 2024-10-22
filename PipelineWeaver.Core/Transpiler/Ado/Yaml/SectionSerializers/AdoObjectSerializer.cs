using System;
using System.Collections;
using System.Reflection;
using PipelineWeaver.Ado;
using PipelineWeaver.Core.Transpiler.Ado.Yaml.SectionSerializers.Interfaces;
using PipelineWeaver.Core.Transpiler.Yaml;

namespace PipelineWeaver.Core.Transpiler.Ado.Yaml.SectionSerializers;

public class AdoObjectSerializer
{
    internal AdoYamlBuilder _builder = new AdoYamlBuilder();

    public string Serialize(AdoObjectBase obj, int startingIndent)
    {
        _builder.AppendLine(startingIndent, SerializeAdoObjectToYaml(obj, 0));

        return _builder.ToString();
    }

    private static string SerializeAdoObjectToYaml(AdoObjectBase obj, int startingIndent)
    {
        var objType = obj.GetType().GetGenericTypeDefinition();
        if (objType == typeof(AdoObject<>))
        {
            dynamic objValue = obj;
            return SerializeCSharpObjectToYaml(objValue.Value, startingIndent);
        }
        if (objType == typeof(AdoJsonObject<>))
        {
            dynamic jsonObj = obj;
            return jsonObj.HasValue ? jsonObj.Value?.ToJson() ?? string.Empty : string.Empty;
        }


        throw new ArgumentException(nameof(obj));
    }

    private static string SerializeCSharpObjectToYaml(object? obj, int startingIndent)
    {
        if (obj is null)
            return string.Empty;

        var innerDoc = new AdoYamlBuilder();

        switch (obj)
        {
            case AdoObject<Dictionary<string, string>> dict:
                innerDoc.AppendKeyValuePairs(startingIndent, dict.Value);
                break;
            case AdoObject<List<string>> list:
                innerDoc.AppendArray(null, startingIndent, list.Value?.ToArray());
                break;
            case AdoObject<string[]> array:
                innerDoc.AppendArray(null, startingIndent, array.Value);
                break;
            case AdoObject<int[]> array:
                innerDoc.AppendArray(null, startingIndent, array.Value?.Select(i => i.ToString()).ToArray());
                break;
            default:
                AppendComplexObjectToAdo(innerDoc, startingIndent, obj);
                break;
        }

        return innerDoc.ToString();
    }

    private static void AppendComplexObjectToAdo(AdoYamlBuilder doc, int startingIndent, object obj)
    {
        Type type = obj.GetType();
        PropertyInfo[] properties = type.GetProperties();
        foreach (var property in properties)
        {
            var propType = property.PropertyType;
            var propName = property.Name;
            var propValue = property.GetValue(obj);
            AppendPropertyToParameterStyleString(doc, propName, propType, propValue, startingIndent);
        }
    }

    private static void AppendPropertyToParameterStyleString(AdoYamlBuilder doc, string propertyName, Type objType, object? obj, int startingIndent)
    {
        if (obj is null)
            return;

        if (Nullable.GetUnderlyingType(objType) != null)
            objType = Nullable.GetUnderlyingType(objType)!;

        switch (Type.GetTypeCode(objType))
        {
            case TypeCode.Boolean:
            case TypeCode.String:
            case TypeCode.Char:
            case TypeCode.SByte:
            case TypeCode.Byte:
            case TypeCode.Int16:
            case TypeCode.UInt16:
            case TypeCode.Int32:
            case TypeCode.UInt32:
            case TypeCode.Int64:
            case TypeCode.UInt64:
            case TypeCode.Single:
            case TypeCode.Double:
            case TypeCode.Decimal:
                doc.AppendLine(startingIndent, $"{propertyName}: {obj}");
                break;
            case TypeCode.DateTime:
                {
                    var dateTime = (DateTime)obj;
                    doc.AppendLine(startingIndent, $"{propertyName}: {dateTime:yyyy-MM-dd HH:mm:ss}");
                }
                break;
            case TypeCode.Object:
                PropertyToParameterStyleString_Obj(propertyName, objType, obj, startingIndent, doc);
                break;

            default:
                throw new NotImplementedException();


        }
    }

    private static void PropertyToParameterStyleString_Obj(string propertyName, Type objType, object? obj, int startingIndent, AdoYamlBuilder innerDoc)
    {
        if (obj is null)
            return;

        if (objType.IsEnum || objType == typeof(Guid) || objType == typeof(TimeSpan) || objType == typeof(IntPtr) || objType == typeof(UIntPtr))
        {
            innerDoc.AppendLine(startingIndent, $"{propertyName}: {obj}");
        }
        else if (objType.IsArray)
        {
            innerDoc.AppendLine(startingIndent, $"{propertyName}:");
            var array = (Array)obj;
            foreach (var item in array)
            {
                innerDoc.AppendLine(startingIndent, $"- {SerializeCSharpObjectToYaml(item, startingIndent)}");
            }
        }
        else if (obj is IList list1)
        {
            innerDoc.AppendLine(startingIndent, $"{propertyName}:");
            var list = list1;
            foreach (var item in list)
            {
                innerDoc.AppendLine(startingIndent, $"- {SerializeCSharpObjectToYaml(item, startingIndent)}");
            }
        }
        else
        {
            innerDoc.AppendLine(startingIndent, $"{propertyName}:");
            innerDoc.AppendLine(0, $"{SerializeCSharpObjectToYaml(obj, startingIndent + 2)}");
        }

    }
}
