using System;
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
            case AdoParameterContainer parameters:
                AppendParameters(parameters, startingIndent);
                break;
            case AdoTemplateParameterContainer templateParameters:
                AppendTemplateParameters(templateParameters, startingIndent);
                break;
            default:
                throw new ArgumentException(nameof(section));
        };
    }

    internal void AppendParameters(AdoParameterContainer parameters, int startingIndent)
    {
        _builder.AppendLine(startingIndent, "parameters:");
        foreach (var p in parameters.Parameters)
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



    private void AppendStringParameter(AdoStringParameter parameter, int v)
    {
        throw new NotImplementedException();
    }

    private void AppendBoolParameter(AdoBoolParameter parameter, int v)
    {
        throw new NotImplementedException();
    }

    private void AppendObjectParameter<T>(AdoObjectParameter<T> parameter, int v)
    {
        throw new NotImplementedException();
    }

    internal void AppendTemplateParameters(AdoTemplateParameterContainer parameters, int startingIndent)
    {
        _builder.AppendLine(startingIndent, "parameters:");
        foreach (var p in parameters.Parameters)
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

    private void AppendStringTemplateParameter(AdoStringTemplateParameter parameter, int v)
    {
        throw new NotImplementedException();
    }

    private void AppendBoolTemplateParameter(AdoBoolTemplateParameter parameter, int v)
    {
        throw new NotImplementedException();
    }

    private void AppendObjectTemplateParameter<T>(AdoObjectTemplateParameter<T> parameter, int v)
    {
        throw new NotImplementedException();
    }
}
