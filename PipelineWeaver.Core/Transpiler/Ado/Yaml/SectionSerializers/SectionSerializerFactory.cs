using PipelineWeaver.Ado;
using PipelineWeaver.Core.Transpiler.Ado.Yaml.SectionSerializers.Interfaces;

namespace PipelineWeaver.Core.Transpiler.Ado.Yaml.SectionSerializers;

public class SectionSerializerFactory
{
    public static IAdoYamlSectionSerializer GetSerializer(AdoSectionBase section)
    {
        switch (section)
        {
            case AdoPipeline _: return new AdoPipelineSerializer();
            case AdoVariableContainer _: return new AdoVariableSectionSerializer();
            case AdoTriggerContainer _: return new AdoTriggerSerializer();
            case AdoResourceContainer _: return new AdoResourceSerializer();
            case AdoStageContainer _: return new AdoStageSerializer();
            case AdoJobContainer _: return new AdoJobSerializer();
            case AdoPoolContainer _: return new AdoPoolSerializer();
            case AdoStepContainer _: return new AdoStepSerializer();
            case AdoDeploymentStrategyContainer _: return new AdoDeploymentStrategySerializer();
            default: throw new NotImplementedException();
        }
    }
}

