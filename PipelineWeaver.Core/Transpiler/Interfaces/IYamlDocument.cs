using System;

namespace PipelineWeaver.Core.Transpiler.Interfaces;

public interface IYamlDocument
{
    void BuildPipeline(Object pipeline);
    void BuildTemplate(Object pipeline);
}