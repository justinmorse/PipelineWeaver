using PipelineWeaver.Ado;
using PipelineWeaver.Core.Transpiler.Ado.Yaml.SectionSerializers.Interfaces;

namespace PipelineWeaver.Core.Transpiler.Ado.Yaml.SectionSerializers;

public static class SectionSerializerFactory
{
    public static IAdoYamlSectionSerializer GetSerializer(AdoSectionBase section)
    {
        switch (section)
        {
            case AdoPipeline _: return new AdoPipelineSerializer();
            case AdoSectionCollection<IAdoVariable> _: return new AdoVariableSerializer();
            case AdoTriggerContainer _: return new AdoTriggerSerializer();
            case AdoSectionCollection<IAdoResource> _: return new AdoResourceSerializer();
            case AdoSectionCollection<AdoStageBase> _: return new AdoStageSerializer();
            case AdoSectionCollection<AdoJobBase> _: return new AdoJobSerializer();
            case AdoSectionCollection<IAdoPool> _: return new AdoPoolSerializer();
            case AdoSectionCollection<AdoStepBase> _: return new AdoStepSerializer();
            case AdoSectionCollection<AdoDeploymentStrategyBase> _: return new AdoDeploymentStrategySerializer();
            case AdoSectionCollection<AdoParameterBase> _: return new AdoParameterSerializer();
            //case AdoSectionCollection<AdoTemplateParameterBase> _: return new AdoParameterSerializer();
            case AdoSectionCollection<AdoTriggerBase> _: return new AdoTriggerSerializer();
            case AdoDeploymentStrategyBase _: return new AdoDeploymentStrategySerializer();
            default: throw new NotImplementedException();
        }
    }
}

