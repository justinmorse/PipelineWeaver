using System;
using System.Text;
using PipelineWeaver.Ado;
using PipelineWeaver.Core.Transpiler.Ado.Yaml.SectionSerializers.Interfaces;

namespace PipelineWeaver.Core.Transpiler.Ado.Yaml.SectionSerializers;

public class AdoPipelineSerializer : IAdoYamlSectionSerializer
{
    internal AdoYamlBuilder _builder = new AdoYamlBuilder();
    public void AppendSection(AdoSectionBase section, AdoYamlBuilder? builder, int startingIndent)
    {
        var pipeline = section as AdoPipeline ?? throw new ArgumentException(nameof(section));
        if (builder is not null)
            _builder = builder;

        _builder.AppendLine(startingIndent, "name: " + pipeline.Name);
        _builder.AppendEmptyLine();
        AppendPool(pipeline, startingIndent);
        AppendVariables(pipeline, startingIndent);
        AppendTriggers(pipeline, startingIndent);
        AppendResources(pipeline, startingIndent);
        AppendStages(pipeline, startingIndent);
        _builder.AppendEmptyLine();

    }

    private void AppendPool(AdoPipeline pipeline, int startingIndent)
    {
        if (!string.IsNullOrWhiteSpace(pipeline.Pool))
        {
            _builder.AppendLine(startingIndent, $"pool: {pipeline.Pool}");
            _builder.AppendEmptyLine();
        }
    }

    internal void AppendVariables(AdoPipeline pipeline, int startingIndent)
    {
        if (pipeline.Variables?.Variables.Count > 0)
        {
            _builder.Append(startingIndent, pipeline.Variables);
            _builder.AppendEmptyLine();
        }
    }

    internal void AppendTriggers(AdoPipeline pipeline, int startingIndent)
    {
        _builder.Append(startingIndent, pipeline.Triggers);
        _builder.AppendEmptyLine();
    }


    internal void AppendResources(AdoPipeline pipeline, int startingIndent)
    {
        if (pipeline.Resources?.Resources?.Count > 0)
        {
            _builder.Append(startingIndent, pipeline.Resources);
            _builder.AppendEmptyLine();
        }
    }

    internal void AppendStages(AdoPipeline pipeline, int startingIndent)
    {
        _builder.Append(startingIndent, pipeline.Stages);
        _builder.AppendEmptyLine();

    }


}
