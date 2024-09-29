using System;
using System.Collections;

namespace PipelineWeaver.Ado;

public abstract class AdoDeploymentStrategyBase : AdoSectionBase
{
    public AdoDeploymentStrategyItem? PreDeploy { get; set; }
    public AdoDeploymentStrategyItem? Deploy { get; set; }
    public AdoDeploymentStrategyItem? RouteTraffic { get; set; }
    public AdoDeploymentStrategyItem? PostRouteTraffic { get; set; }
    public AdoDeplymentStrategyOn? On { get; set; }
}

public class AdoRunOnceDeploymentStrategy : AdoDeploymentStrategyBase
{
    //left empty
}

public class AdoRollingDeploymentStrategy : AdoRunOnceDeploymentStrategy
{
    public int? MaxParallel { get; set; }
}

public class AdoCanaryDeploymentStrategy : AdoRunOnceDeploymentStrategy
{
    public int? Increments { get; set; }
}

public class AdoDeploymentStrategyItem
{
    public required AdoSectionCollection<AdoStepBase> Steps { get; set; }
    public string? Pool { get; set; }
}


public class AdoDeplymentStrategyOn
{
    public AdoDeploymentStrategyItem? Failure { get; set; }
    public AdoDeploymentStrategyItem? Success { get; set; }
}