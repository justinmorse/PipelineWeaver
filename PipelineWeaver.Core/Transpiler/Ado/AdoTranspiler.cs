using System;
using PipelineWeaver.Ado;
using PipelineWeaver.Core.Transpiler.Yaml;

namespace PipelineWeaver.Core.Transpiler.Ado;

public class AdoTranspiler
{
    public AdoYamlDocument Document { get; set; } = new AdoYamlDocument();
    public AdoSectionBase Section { get; set; }
    public AdoTranspiler(AdoPipeline pipeline)
    {
        Section = pipeline;
    }
}

