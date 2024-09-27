using System;

namespace PipelineWeaver.Ado;

public class AdoJobContainer : AdoSectionBase
{
    public List<AdoJobBase> Jobs { get; set; } = new List<AdoJobBase>();
}

public abstract class AdoJobBase
{
    public string? DisplayName { get; set; }
    public List<string>? DependsOn { get; set; }
    public string? Condition { get; set; }
    public bool? ContinueOnError { get; set; }
    public int? TimeoutInMinutes { get; set; }
    public int? CancelTimeoutInMinutes { get; set; }
    public AdoVariableContainer? Variables { get; set; }
    public string? Pool { get; set; }
    public AdoWorkspace? Workspace { get; set; }

    public AdoUses? Uses { get; set; }
    public AdoContainer? Container { get; set; }
    public Dictionary<string, string>? Services { get; set; }
    public string? TemplateContext { get; set; }
}

public class AdoJob : AdoJobBase
{
    public required string Job { get; set; }
    public AdoJobStrategy? Strategy { get; set; }
    public AdoStepContainer? Steps { get; set; }

}

public class AdoDeploymentJob : AdoJobBase
{
    public required string Deployment { get; set; }
    public AdoEnvironment? Environment { get; set; }
    public AdoDeploymentStrategyContainer? Strategy { get; set; }
}

public class AdoTemplateJob : AdoJobBase
{
    public required string Template { get; set; }
    public AdoParameterContainer? Parameters { get; set; }
}

public class AdoUses
{
    public List<string>? Repositories { get; set; }
    public List<string>? Pools { get; set; }
}

public class AdoWorkspace
{
    public string? Clean { get; set; }
}

public class AdoJobStrategy
{
    public Dictionary<string, object>? Matrix { get; set; }
    public int? MaxParallel { get; set; }
}

public class AdoEnvironment
{
    public required string Name { get; set; }
    public string? ResourceName { get; set; }
    public string? ResourceId { get; set; }
    public string? ResourceType { get; set; }
    public List<string>? Tags { get; set; }
}



