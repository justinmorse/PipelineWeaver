using System;

namespace PipelineWeaver.Ado;

public class AdoDeploymentStrategyContainer : AdoSectionBase
{
    public AdoDeploymentStrategyBase? DeploymentStrategy { get; set; }
}

public abstract class AdoDeploymentStrategyBase
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
    public required AdoStepContainer Steps { get; set; } // A list of steps to run.
    public string? Pool { get; set; } // Pool where pre deploy steps will run.
}


public class AdoDeplymentStrategyOn
{
    public AdoDeploymentStrategyItem? Failure { get; set; } // Runs on failure of any step.
    public AdoDeploymentStrategyItem? Success { get; set; } // Runs on success of all of the steps.
}