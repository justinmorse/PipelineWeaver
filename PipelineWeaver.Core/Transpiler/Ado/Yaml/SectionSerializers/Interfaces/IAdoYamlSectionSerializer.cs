using System;
using PipelineWeaver.Ado;

namespace PipelineWeaver.Core.Transpiler.Ado.Yaml.SectionSerializers.Interfaces;

public interface IAdoYamlSectionSerializer
{
    public void AppendSection(AdoSectionBase section, AdoYamlBuilder builder, int startingIndent);
}
