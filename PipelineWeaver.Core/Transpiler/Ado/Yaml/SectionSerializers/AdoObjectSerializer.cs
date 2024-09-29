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

        _builder.AppendLine(startingIndent, SerializeObjectToYaml(section, startingIndent));
    }



    private static string SerializeObjectToYaml(object obj, int startingIndent)
    {
        var innerDoc = new AdoYamlBuilder();

        switch (obj)
        {
            case Dictionary<string, string> dict:
                innerDoc.AppendKeyValuePairs(null, startingIndent, dict);
                break;
            case List<string> list:
                innerDoc.AppendList(null, startingIndent, list);
                break;
            default:
                SeralizeComplexObjectToAdo(startingIndent, obj);
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
                innerDoc.AppendLine(startingIndent, $"- {SerializeObjectToYaml(item, startingIndent)}");
            }
        }
        else if (obj is IList)
        {
            innerDoc.AppendLine(startingIndent, $"{propertyName}:");
            var list = (IList)obj;
            foreach (var item in list)
            {
                innerDoc.AppendLine(startingIndent, $"- {SerializeObjectToYaml(item, startingIndent)}");
            }
        }
        else
        {
            SerializeObjectToYaml(obj, startingIndent);
        }

    }
}
