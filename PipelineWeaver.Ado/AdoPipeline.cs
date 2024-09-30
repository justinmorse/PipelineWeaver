using System;

namespace PipelineWeaver.Ado;

public class AdoPipeline : AdoSectionBase
{
    public string? Name { get; set; }
    public string? Pool { get; set; }
    public AdoSectionCollection<IAdoVariable>? Variables { get; set; }
    public AdoTriggerContainer? Triggers { get; set; }
    public AdoSectionCollection<IAdoResource>? Resources { get; set; }
    public AdoSectionCollection<AdoStageBase>? Stages { get; set; }
}