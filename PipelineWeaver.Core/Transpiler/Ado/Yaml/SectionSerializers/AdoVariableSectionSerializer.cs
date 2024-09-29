using System;
using System.Text;
using PipelineWeaver.Ado;
using PipelineWeaver.Core.Transpiler.Ado.Yaml.SectionSerializers.Interfaces;

namespace PipelineWeaver.Core.Transpiler.Ado.Yaml.SectionSerializers;

public class AdoVariableSectionSerializer : IAdoYamlSectionSerializer
{
    internal AdoYamlBuilder _builder = new AdoYamlBuilder();

    public void AppendSection(AdoSectionBase section, AdoYamlBuilder? builder, int startingIndent)
    {
        var variables = section as AdoSectionCollection<AdoVariableBase> ?? throw new ArgumentException(nameof(section));
        if (builder is not null)
            _builder = builder;

        _builder.AppendLine(startingIndent, "variables:");
        variables.ToList().ForEach(v =>
        {
            switch (v)
            {
                case AdoNameVariable variable:
                    {
                        BuildSection(variable, startingIndent);
                        break;
                    }
                case AdoGroupVariable variable:
                    {
                        BuildSection(variable, startingIndent);
                        break;
                    }
                case AdoTemplateVariable variable:
                    {
                        BuildSection(variable, startingIndent);
                        break;
                    }
                default:
                    throw new ArgumentException(nameof(section));
            }
        });
    }


    internal void BuildSection(AdoNameVariable section, int startingIndent)
    {
        _builder.AppendLine(startingIndent, "- name: " + section.Name);
        _builder.AppendLine(startingIndent + 2, "value: " + section.Value);
    }

    internal void BuildSection(AdoGroupVariable section, int startingIndent)
    {
        _builder.AppendLine(startingIndent, "- group: " + section.Group);
    }

    internal void BuildSection(AdoTemplateVariable section, int startingIndent)
    {
        _builder.AppendLine(startingIndent, "- template: " + section.Template);
    }
}
