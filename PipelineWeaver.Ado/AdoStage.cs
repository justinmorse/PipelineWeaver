using System;

namespace PipelineWeaver.Ado;

public class AdoStageContainer : AdoSectionBase
{
    public List<AdoStageBase>? Stages { get; set; }
}

public abstract class AdoStageBase
{
    public List<string>? DependsOn { get; set; }
    public string? Condition { get; set; }
}

public class AdoStage : AdoStageBase
{
    public required string Stage { get; set; }
    public string? DisplayName { get; set; }
    public AdoVariableContainer? Variables { get; set; }
    public string? LockBehavior { get; set; }
    public string? Trigger { get; set; }
    public bool? IsSkippable { get; set; }
    public string? TemplateContext { get; set; }
    public AdoPoolContainer? Pools { get; set; }
    public AdoJobContainer? Jobs { get; set; }

}

public class AdoStageTemplate : AdoStageBase
{
    public required string Template { get; set; }
    public List<string>? Parameters { get; set; }
}


