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

    public void AppendSection(AdoSectionBase section, AdoYamlBuilder? builder, int startingIndent)
    {
        switch (section)
        {
            case AdoSectionCollection<AdoParameterBase> parameters:
                AppendParameters(parameters);
                break;
            //case AdoSectionCollection<AdoTemplateParameterBase> templateParameters:
            //AppendTemplateParameters(templateParameters, startingIndent);
            //break;
            default:
                throw new ArgumentException(nameof(section));
        }
        builder?.AppendLine(startingIndent, _builder.ToString(), true);
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
        _builder.AppendLine(startingIndent, parameter.Name + ": " + parameter.Value);
    }

    private void AppendBoolParameter(AdoBoolParameter parameter, int startingIndent)
    {
        _builder.AppendLine(startingIndent, parameter.Name + ": " + parameter.Value);
    }

    private void AppendArrayParameter<T>(AdoArrayParameter<T> parameter, int startingIndent)
    {
        _builder.AppendArray(parameter.Name, startingIndent, parameter.Value);
    }

    private void AppendDictionaryParameter<T>(AdoDictionaryParameter<T> parameter, int startingIndent)
    {
        _builder.AppendKeyValuePairs(parameter.Name, startingIndent, parameter.Value);
    }

    private void AppendObjectDictionaryParameter<T>(AdoDictionaryParameter<T> parameter, int startingIndent)
    {
        var newDict = new Dictionary<string, AdoObject<T>>();
        foreach (var kvp in parameter.Value)
            newDict.Add(kvp.Key, new AdoObject<T>(kvp.Value));
        _builder.AppendObjectKeyValuePairs<T>(parameter.Name, startingIndent, newDict);
    }

    public void AppendObjectParameter<T>(AdoObjectParameter<T> parameter, int startingIndent)
    {
        if (parameter.Value is null) return;

        _builder.AppendLine(startingIndent, parameter.Name + ":");
        var serializer = new AdoObjectSerializer();
        var s = serializer.Serialize(new AdoObject<T>(parameter.Value), startingIndent);
        _builder.AppendLine(startingIndent, s);
    }

    /* internal void AppendTemplateParameters(AdoSectionCollection<AdoTemplateParameterBase> parameters, int startingIndent)
    {
        _builder.AppendLine(startingIndent, "parameters:");
        foreach (var p in parameters)
        {
            var type = p.GetType();
            if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(AdoObjectTemplateParameter<>))
                typeof(AdoParameterSerializer).CallGenericMethod(this, methodName: nameof(AppendObjectTemplateParameter), genericType: type, parameters: [p, startingIndent + 2]);
            else
            {
                switch (p)
                {
                    case AdoStringTemplateParameter parameter:
                        AppendStringTemplateParameter(parameter, startingIndent + 2);
                        break;
                    case AdoBoolTemplateParameter parameter:
                        AppendBoolTemplateParameter(parameter, startingIndent + 2);
                        break;
                    default:
                        throw new ArgumentException(nameof(p));
                }
            }
        }
    } */

    /* private void AppendStringTemplateParameter(AdoStringTemplateParameter parameter, int startingIndent)
    {
        _builder.AppendLine(startingIndent, $"- name: {parameter.Name}");
        _builder.AppendLine(startingIndent + 2, $"type: string");
        if (!string.IsNullOrWhiteSpace(parameter.Default))
            _builder.AppendLine(startingIndent + 2, $"default: {parameter.Default}");
    }

    private void AppendBoolTemplateParameter(AdoBoolTemplateParameter parameter, int startingIndent)
    {
        _builder.AppendLine(startingIndent, $"- name: {parameter.Name}");
        _builder.AppendLine(startingIndent + 2, $"type: bool");
        if (parameter.Default != null)
            _builder.AppendLine(startingIndent + 2, $"default: {parameter.Default}");
    }

    private void AppendObjectTemplateParameter<T>(AdoObjectTemplateParameter<T> parameter, int startingIndent)
    {
        _builder.AppendLine(startingIndent, $"- name: {parameter.Name}");
        _builder.AppendLine(startingIndent + 2, $"type: object");
        if (parameter.Default != null)
        {
            _builder.AppendLine(startingIndent + 2, $"default:");
            _builder.Append(startingIndent + 4, parameter.Default);
        }
    } */
}


