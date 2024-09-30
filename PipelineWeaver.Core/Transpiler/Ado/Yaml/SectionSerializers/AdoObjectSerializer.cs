using System;
using System.Collections;
using System.Reflection;
using PipelineWeaver.Ado;
using PipelineWeaver.Core.Transpiler.Ado.Yaml.SectionSerializers.Interfaces;

namespace PipelineWeaver.Core.Transpiler.Ado.Yaml.SectionSerializers;

public class AdoObjectSerializer : IAdoYamlSectionSerializer
{
    internal AdoYamlBuilder _builder = new AdoYamlBuilder();

    public void AppendSection(AdoSectionBase section, AdoYamlBuilder? builder, int startingIndent)
    {
        if (builder is not null)
            _builder = builder;

        var objSection = section as AdoObjectBase ?? throw new ArgumentException(nameof(section));

        _builder.AppendLine(startingIndent, SerializeAdoObjectToYaml(objSection, startingIndent));
    }

    private static string SerializeAdoObjectToYaml(AdoObjectBase obj, int startingIndent)
    {
        if (obj is AdoObject<object> adoObj)
            return SerializeCSharpObjectToYaml(adoObj.Value, startingIndent);

        if (obj is AdoJsonObject<object> jsonObj)
            return jsonObj.HasValue ? jsonObj.Value?.ToJson() ?? string.Empty : string.Empty;

        return string.Empty;
    }

    private static string SerializeCSharpObjectToYaml(object? obj, int startingIndent)
    {
        if (obj is null)
            return string.Empty;

        var innerDoc = new AdoYamlBuilder();

        switch (obj)
        {
            case AdoObject<Dictionary<string, string>> dict:
                innerDoc.AppendKeyValuePairs(null, startingIndent, dict.Value);
                break;
            case AdoObject<List<string>> list:
                innerDoc.AppendList(null, startingIndent, list.Value);
                break;
            default:
                innerDoc.AppendLine(startingIndent, SeralizeComplexObjectToAdo(startingIndent, obj));
                break;
        }

        return innerDoc.ToString();
    }

    private static string SeralizeComplexObjectToAdo(int startingIndent, object obj)
    {
        var innerDoc = new AdoYamlBuilder();
        Type type = obj.GetType();
        PropertyInfo[] properties = type.GetProperties();

        foreach (var property in properties)
        {
            innerDoc.AppendLine(startingIndent, PropertyToParameterStyleString(property.Name, property.GetType(), property.GetValue(obj), startingIndent));
        }

        return innerDoc.ToString();
    }

    private static string PropertyToParameterStyleString(string propertyName, Type objType, object? obj, int startingIndent)
    {
        if (obj is null)
            return string.Empty;

        var innerDoc = new AdoYamlBuilder();
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
                innerDoc.AppendLine(startingIndent, $"{propertyName}: {obj}");
                break;
            case TypeCode.DateTime:
                {
                    var dateTime = (DateTime)obj;
                    innerDoc.AppendLine(startingIndent, $"{propertyName}: {dateTime:yyyy-MM-dd HH:mm:ss}");
                }
                break;
            case TypeCode.Object:
                PropertyToParameterStyleString_Obj(propertyName, objType, obj, startingIndent, innerDoc);
                break;

            default:
                throw new NotImplementedException();


        }
        return innerDoc.ToString();
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
            SerializeCSharpObjectToYaml(obj, startingIndent);
        }

    }
}
