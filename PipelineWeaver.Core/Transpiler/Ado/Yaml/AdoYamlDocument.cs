using System;
using System.Text;
using PipelineWeaver.Ado;
using PipelineWeaver.Core.Transpiler.Ado.Yaml;
using PipelineWeaver.Core.Transpiler.Interfaces;

namespace PipelineWeaver.Core.Transpiler.Yaml;

public class AdoYamlDocument : IYamlDocument
{
    public AdoYamlBuilder Builder { get; set; } = new AdoYamlBuilder();

    public void BuildPipeline(Object pipeline)
    {
        var pipelineObj = pipeline as AdoPipeline;
        if (pipelineObj == null)
            throw new ArgumentException(nameof(pipelineObj));

        Builder.Append(0, pipelineObj);
    }

    public void BuildTemplate(object pipeline)
    {
        throw new NotImplementedException();
    }

    public void Save(string path)
    {
        if(File.Exists(path))
            File.Delete(path);
        File.WriteAllText(path, Builder.ToString());
    }
}
