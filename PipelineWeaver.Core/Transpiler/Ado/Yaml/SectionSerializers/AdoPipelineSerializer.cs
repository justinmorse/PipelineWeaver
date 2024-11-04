using System;
using System.Text;
using PipelineWeaver.Ado;
using PipelineWeaver.Core.Transpiler.Ado.Yaml.SectionSerializers.Interfaces;

namespace PipelineWeaver.Core.Transpiler.Ado.Yaml.SectionSerializers;

public class AdoPipelineSerializer : IAdoYamlSectionSerializer
{
    private AdoYamlBuilder _builder = new AdoYamlBuilder();
    public void AppendSection(AdoSectionBase section, AdoYamlBuilder builder, int startingIndent, bool includeHeader = true)
    {
        var pipeline = section as AdoPipeline ?? throw new ArgumentException(nameof(section));
        _builder = builder;
        _builder.AppendLine(startingIndent, "name: " + pipeline.Name);
        AppendPool(pipeline, startingIndent);
        AppendVariables(pipeline, startingIndent);
        AppendTriggers(pipeline, startingIndent);
        AppendResources(pipeline, startingIndent);
        AppendStages(pipeline, startingIndent);
    }

    private void AppendPool(AdoPipeline pipeline, int startingIndent)
    {
        if (pipeline.Pool?.Count > 0)
        {
            if (pipeline.Pool.Count > 1)
                throw new Exception("Multiple pools not supported");
            _builder.AppendLine(startingIndent, "pool:");
            _builder.Append(startingIndent, pipeline.Pool, includeHeader: false);
            _builder.AppendEmptyLine();
        }
    }

    internal void AppendVariables(AdoPipeline pipeline, int startingIndent)
    {
        if (pipeline.Variables?.Count > 0)
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
        if (pipeline.Resources?.Count > 0)
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
