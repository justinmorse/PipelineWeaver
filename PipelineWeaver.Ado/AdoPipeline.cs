using System;

namespace PipelineWeaver.Ado;

public class AdoPipeline : AdoSectionBase
{
    public string? Name { get; set; }
    public string? Pool { get; set; }
    public AdoVariableContainer? Variables { get; set; }
    public AdoTriggerContainer? Triggers { get; set; }
    public AdoResourceContainer? Resources { get; set; }
    public AdoStageContainer? Stages { get; set; }
}