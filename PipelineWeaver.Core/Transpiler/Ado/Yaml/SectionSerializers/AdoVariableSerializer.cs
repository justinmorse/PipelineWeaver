using System;
using System.Text;
using PipelineWeaver.Ado;
using PipelineWeaver.Core.Transpiler.Ado.Yaml.SectionSerializers.Interfaces;

namespace PipelineWeaver.Core.Transpiler.Ado.Yaml.SectionSerializers;

public class AdoVariableSerializer : IAdoYamlSectionSerializer
{
    internal AdoYamlBuilder _builder = new AdoYamlBuilder();

    public void AppendSection(AdoSectionBase section, AdoYamlBuilder? builder, int startingIndent)
    {
        var variables = section as AdoSectionCollection<IAdoVariable> ?? throw new ArgumentException(nameof(section));

        _builder.AppendLine(0, "variables:");
        variables.ToList().ForEach(v =>
        {
            switch (v)
            {
                case AdoNameVariable variable:
                    {
                        BuildSection(variable);
                        break;
                    }
                case AdoGroupVariable variable:
                    {
                        BuildSection(variable);
                        break;
                    }
                case AdoTemplateVariable variable:
                    {
                        BuildSection(variable);
                        break;
                    }
                default:
                    throw new ArgumentException(nameof(section));
            }
        });

        builder?.AppendLine(startingIndent, _builder.ToString(), true, true);
    }


    internal void BuildSection(AdoNameVariable section)
    {
        _builder.AppendLine(0, "- name: " + section.Name);
        _builder.AppendLine(0 + 2, "value: " + section.Value);
    }

    internal void BuildSection(AdoGroupVariable section)
    {
        _builder.AppendLine(0, "- group: " + section.Group);
    }

    internal void BuildSection(AdoTemplateVariable section)
    {
        _builder.AppendLine(0, "- template: " + section.Template);
    }
}
