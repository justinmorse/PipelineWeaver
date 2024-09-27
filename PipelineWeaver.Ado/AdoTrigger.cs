using System;

namespace PipelineWeaver.Ado;

public class AdoTriggerContainer : AdoSectionBase
{
    public bool Batch { get; set; }
    public required List<AdoTriggerBase> Triggers { get; set; }
}

public abstract class AdoTriggerBase
{
    public AdoTriggerType TriggerType { get; set; }
    public required string Value { get; set; }
}

public class AdoTrigger : AdoTriggerBase
{
}

public enum AdoTriggerType
{
    PathInclude,
    BranchInclude,
    TagInclude,
    PathExclude,
    BranchExclude,
    TagExclude
}





