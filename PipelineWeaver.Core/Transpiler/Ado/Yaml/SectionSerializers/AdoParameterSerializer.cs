using System;
using System.Collections;
using System.Reflection;
using System.Reflection.Metadata.Ecma335;
using PipelineWeaver.Ado;
using PipelineWeaver.Core.Transpiler.Ado.Yaml.SectionSerializers.Interfaces;

namespace PipelineWeaver.Core.Transpiler.Ado.Yaml.SectionSerializers;

public class AdoParameterSerializer : IAdoYamlSectionSerializer
{
    internal AdoYamlBuilder _builder = new AdoYamlBuilder();

    public void AppendSection(AdoSectionBase section, AdoYamlBuilder? builder, int startingIndent)
    {
        if (builder is not null)
            _builder = builder;

        switch (section)
        {
            case AdoSectionCollection<AdoParameterBase> parameters:
                AppendParameters(parameters, startingIndent);
                break;
            case AdoSectionCollection<AdoTemplateParameterBase> templateParameters:
                AppendTemplateParameters(templateParameters, startingIndent);
                break;
            default:
                throw new ArgumentException(nameof(section));
        }
    }

    internal void AppendParameters(AdoSectionCollection<AdoParameterBase> parameters, int startingIndent)
    {
        _builder.AppendLine(startingIndent, "parameters:");
        foreach (var p in parameters)
        {
            var type = p.GetType();
            if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(AdoObjectParameter<>))
                typeof(AdoParameterSerializer).CallGenericMethod(methodName: nameof(AppendObjectParameter), genericType: type, parameters: [p, startingIndent + 2]);
            else
                switch (p)
                {
                    case AdoStringParameter parameter:
                        AppendStringParameter(parameter, startingIndent + 2);
                        break;
                    case AdoBoolParameter parameter:
                        AppendBoolParameter(parameter, startingIndent + 2);
                        break;
                    default:
                        throw new ArgumentException(nameof(p));
                }
        }
    }



    private void AppendStringParameter(AdoStringParameter parameter, int startingIndent)
    {
        _builder.AppendLine(startingIndent, parameter.Name + ": " + parameter.Value);
    }

    private void AppendBoolParameter(AdoBoolParameter parameter, int startingIndent)
    {
        _builder.AppendLine(startingIndent, parameter.Name + ": " + parameter.Value);
    }

    private void AppendObjectParameter<T>(AdoObjectParameter<T> parameter, int startingIndent) where T : AdoObjectBase
    {
        if (parameter.Value is null) return;

        _builder.AppendLine(startingIndent, parameter.Name + ":");
        _builder.Append(startingIndent + 2, parameter.Value);
    }

    internal void AppendTemplateParameters(AdoSectionCollection<AdoTemplateParameterBase> parameters, int startingIndent)
    {
        _builder.AppendLine(startingIndent, "parameters:");
        foreach (var p in parameters)
        {
            var type = p.GetType();
            if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(AdoObjectTemplateParameter<>))
                typeof(AdoParameterSerializer).CallGenericMethod(methodName: nameof(AppendObjectTemplateParameter), genericType: type, parameters: [p, startingIndent + 2]);
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
    }

    private void AppendStringTemplateParameter(AdoStringTemplateParameter parameter, int startingIndent)
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

    private void AppendObjectTemplateParameter<T>(AdoObjectTemplateParameter<T> parameter, int startingIndent) where T : AdoObjectBase
    {
        _builder.AppendLine(startingIndent, $"- name: {parameter.Name}");
        _builder.AppendLine(startingIndent + 2, $"type: object");
        if (parameter.Default != null)
        {
            _builder.AppendLine(startingIndent + 2, $"default:");
            _builder.Append(startingIndent + 4, parameter.Default);
        }
    }
}


