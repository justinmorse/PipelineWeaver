using System;
using System.Collections;
using System.Reflection;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.Serialization;
using PipelineWeaver.Ado;
using PipelineWeaver.Core.Transpiler.Ado.Yaml.SectionSerializers.Interfaces;

namespace PipelineWeaver.Core.Transpiler.Ado.Yaml.SectionSerializers;

public class AdoParameterSerializer : IAdoYamlSectionSerializer
{
    internal AdoYamlBuilder _builder = new AdoYamlBuilder();

    public void AppendSection(AdoSectionBase section, AdoYamlBuilder builder, int startingIndent)
    {
        switch (section)
        {
            case AdoSectionCollection<AdoParameterBase> parameters:
                AppendParameters(parameters);
                break;
            default:
                throw new ArgumentException(nameof(section));
        }
        builder.AppendLine(startingIndent, _builder.ToString(), true);
    }

    internal void AppendParameters(AdoSectionCollection<AdoParameterBase> parameters)
    {
        _builder.AppendLine(0, "parameters:");
        foreach (var p in parameters)
        {
            var type = p.GetType();
            if (type.IsGenericType)
            {
                var genericInternalType = type.GetGenericArguments()[0];
                if (type.GetGenericTypeDefinition() == typeof(AdoObjectParameter<>))
                    CallGenericMethod(methodName: nameof(AppendObjectParameter), genericType: type, parameters: [p, 2]);
                else if (type.GetGenericTypeDefinition() == typeof(AdoArrayParameter<>))
                    CallGenericMethod(methodName: nameof(AppendArrayParameter), genericType: type, parameters: [p, 2]);
                else if (type.GetGenericTypeDefinition() == typeof(AdoDictionaryParameter<>))
                {
                    if (genericInternalType != typeof(AdoObject<>))
                        CallGenericMethod(methodName: nameof(AppendDictionaryParameter), genericType: type, parameters: [p, 2]);
                    else
                        CallGenericMethod(methodName: nameof(AppendObjectDictionaryParameter), genericType: type, parameters: [p, 2]);
                }
            }
            else
                switch (p)
                {
                    case AdoStringParameter parameter:
                        AppendStringParameter(parameter, 2);
                        break;
                    case AdoBoolParameter parameter:
                        AppendBoolParameter(parameter, 2);
                        break;
                    default:
                        throw new ArgumentException(nameof(p));
                }
        }
    }

    internal void CallGenericMethod(string methodName, Type genericType, params object[] parameters)
    {
        typeof(AdoParameterSerializer).CallGenericMethod(this, methodName, genericType, parameters);
    }


    private void AppendStringParameter(AdoStringParameter parameter, int startingIndent)
    {
        if (parameter.ParameterType == AdoParameterType.Standard)
        {
            _builder.AppendLine(startingIndent, parameter.Name + ": " + parameter.ValueOrDefault);
        }
        else
        {
            _builder.AppendLine(startingIndent, "- name: " + parameter.Name);
            _builder.AppendLine(startingIndent, "  type: string");
            _builder.AppendLine(startingIndent, "  default: " + parameter.ValueOrDefault);
        }
    }

    private void AppendBoolParameter(AdoBoolParameter parameter, int startingIndent)
    {
        if (parameter.ParameterType == AdoParameterType.Standard)
        {
            _builder.AppendLine(startingIndent, parameter.Name + ": " + parameter.ValueOrDefault);
        }
        else
        {
            _builder.AppendLine(startingIndent, "- name: " + parameter.Name);
            _builder.AppendLine(startingIndent, "  type: boolean");
            _builder.AppendLine(startingIndent, "  default: " + parameter.ValueOrDefault);
        }
    }

    private void AppendArrayParameter<T>(AdoArrayParameter<T> parameter, int startingIndent)
    {
        if (parameter.ParameterType == AdoParameterType.Standard)
        {
            _builder.AppendArray(parameter.Name, startingIndent, parameter.ValueOrDefault);
        }
        else
        {
            _builder.AppendLine(startingIndent, "- name: " + parameter.Name);
            _builder.AppendLine(startingIndent, "  type: object");
            _builder.AppendArray("default", startingIndent + 2, parameter.ValueOrDefault);
        }
    }

    private void AppendDictionaryParameter<T>(AdoDictionaryParameter<T> parameter, int startingIndent)
    {
        if (parameter.ParameterType == AdoParameterType.Standard)
        {
            _builder.AppendLine(startingIndent, parameter.Name + ":");
            _builder.AppendKeyValuePairs(startingIndent, parameter.ValueOrDefault);
        }
        else
        {
            _builder.AppendLine(startingIndent, "- name: " + parameter.Name);
            _builder.AppendLine(startingIndent, "  type: object");
            _builder.AppendLine(startingIndent, "  default:");
            _builder.AppendKeyValuePairs(startingIndent + 2, parameter.ValueOrDefault);
        }
    }

    private void AppendObjectDictionaryParameter<T>(AdoDictionaryParameter<T> parameter, int startingIndent)
    {
        var newDict = new Dictionary<string, AdoObject<T>>();
        foreach (var kvp in parameter.ValueOrDefault!)
            newDict.Add(kvp.Key, new AdoObject<T>(kvp.Value));
        if (parameter.ParameterType == AdoParameterType.Standard)
        {
            _builder.AppendObjectKeyValuePairs<T>(parameter.Name, startingIndent, newDict);
        }
        else
        {
            _builder.AppendLine(startingIndent, "- name: " + parameter.Name);
            _builder.AppendLine(startingIndent, "  type: object");
            _builder.AppendObjectKeyValuePairs<T>("default", startingIndent + 2, newDict);
        }
    }

    public void AppendObjectParameter<T>(AdoObjectParameter<T> parameter, int startingIndent)
    {
        if (parameter.ValueOrDefault is null) return;
        var serializer = new AdoObjectSerializer();
        var s = serializer.Serialize(new AdoObject<T>(parameter.ValueOrDefault), startingIndent);

        if (parameter.ParameterType == AdoParameterType.Standard)
        {
            _builder.AppendLine(startingIndent, parameter.Name + ":");
            _builder.AppendLine(startingIndent, s);
        }
        else
        {
            _builder.AppendLine(startingIndent, "- name: " + parameter.Name);
            _builder.AppendLine(startingIndent, "  type: object");
            _builder.AppendLine(startingIndent, "  default:");
            _builder.AppendLine(startingIndent + 2, s);
        }


    }
}


