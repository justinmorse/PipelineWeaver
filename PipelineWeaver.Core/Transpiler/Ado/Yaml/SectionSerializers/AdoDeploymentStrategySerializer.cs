using System;
using System.Text;
using PipelineWeaver.Ado;
using PipelineWeaver.Core.Transpiler.Ado.Yaml.SectionSerializers.Interfaces;

namespace PipelineWeaver.Core.Transpiler.Ado.Yaml.SectionSerializers;

public class AdoDeploymentStrategySerializer : IAdoYamlSectionSerializer
{
    internal AdoYamlBuilder _builder = new AdoYamlBuilder();
    public void AppendSection(AdoSectionBase section, AdoYamlBuilder? builder, int startingIndent)
    {
        var deploymentStrategies = section as AdoSectionCollection<AdoDeploymentStrategyBase> ?? throw new ArgumentException(nameof(section));

        AppendDeploymentStrategies(deploymentStrategies);

        builder?.AppendLine(startingIndent, _builder.ToString(), true, true);
    }

    private void AppendDeploymentStrategies(AdoSectionCollection<AdoDeploymentStrategyBase> deploymentStrategies)
    {
        deploymentStrategies.ToList().ForEach(deploymentStrategy =>
        {
            switch (deploymentStrategy)
            {
                case AdoRollingDeploymentStrategy strategy:
                    AppendRollingDeploymentStrategy(strategy);
                    break;
                case AdoCanaryDeploymentStrategy strategy:
                    AppendCanaryDeploymentStrategy(strategy);
                    break;
                case AdoRunOnceDeploymentStrategy strategy:
                    AppendRunOnceDeploymentStrategy(strategy);
                    break;
                default:
                    throw new ArgumentException(nameof(deploymentStrategy));
            }
        });
    }

    private void AppendRunOnceDeploymentStrategy(AdoRunOnceDeploymentStrategy strategy)
    {
        _builder.AppendLine(0, "runOnce:");
        AppendDeploymentTypes(strategy);
    }

    private void AppendRollingDeploymentStrategy(AdoRollingDeploymentStrategy strategy)
    {
        _builder.AppendLine(0, "rolling:");
        _builder.AppendLine(2, "maxParallel: " + strategy.MaxParallel);
        AppendDeploymentTypes(strategy);
    }

    private void AppendCanaryDeploymentStrategy(AdoCanaryDeploymentStrategy strategy)
    {
        _builder.AppendLine(0, "canary:");
        _builder.AppendLine(2, "increments: " + strategy.Increments);
        AppendDeploymentTypes(strategy);
    }

    private void AppendDeploymentTypes(AdoDeploymentStrategyBase strategy)
    {
        if (strategy is not null)
        {
            AppendPreDeploy(strategy);
            AppendDeploy(strategy);
            AppendRouteTraffic(strategy);
            AppendPostRouteTraffic(strategy);
            AppendOn(strategy);
        }
    }

    private void AppendPreDeploy(AdoDeploymentStrategyBase strategy)
    {
        if (strategy.PreDeploy is not null)
        {
            _builder.AppendLine(2, "preDeploy:");
            if (strategy.PreDeploy.Steps is not null)
                _builder.Append(4, strategy.PreDeploy.Steps);
            if (!string.IsNullOrWhiteSpace(strategy.PreDeploy.Pool))
                _builder.AppendLine(4, $"pool: {strategy.PreDeploy.Pool}");
        }
    }

    private void AppendDeploy(AdoDeploymentStrategyBase strategy)
    {
        if (strategy.Deploy is not null)
        {
            _builder.AppendLine(2, "deploy:");
            if (strategy.Deploy.Steps is not null)
                _builder.Append(4, strategy.Deploy.Steps);
            if (!string.IsNullOrWhiteSpace(strategy.Deploy.Pool))
                _builder.AppendLine(4, $"pool: {strategy.Deploy.Pool}");
        }
    }

    private void AppendRouteTraffic(AdoDeploymentStrategyBase strategy)
    {
        if (strategy.RouteTraffic is not null)
        {
            _builder.AppendLine(2, "routeTraffic:");
            if (strategy.RouteTraffic.Steps is not null)
                _builder.Append(4, strategy.RouteTraffic.Steps);
            if (!string.IsNullOrWhiteSpace(strategy.RouteTraffic.Pool))
                _builder.AppendLine(4, $"pool: {strategy.RouteTraffic.Pool}");
        }
    }

    private void AppendPostRouteTraffic(AdoDeploymentStrategyBase strategy)
    {
        if (strategy.PostRouteTraffic is not null)
        {
            _builder.AppendLine(2, "postRouteTraffic:");
            if (strategy.PostRouteTraffic.Steps is not null)
                _builder.Append(4, strategy.PostRouteTraffic.Steps);
            if (!string.IsNullOrWhiteSpace(strategy.PostRouteTraffic.Pool))
                _builder.AppendLine(4, $"pool: {strategy.PostRouteTraffic.Pool}");
        }
    }

    private void AppendOn(AdoDeploymentStrategyBase strategy)
    {
        if (strategy.On is not null)
        {
            _builder.AppendLine(2, "on:");
            AppendOnFailure(strategy);
            AppendOnSuccess(strategy);
        }
    }

    private void AppendOnFailure(AdoDeploymentStrategyBase strategy)
    {
        if (strategy.On?.Failure is not null)
        {
            _builder.AppendLine(4, "failure:");
            if (strategy.On.Failure.Steps is not null)
                _builder.Append(6, strategy.On.Failure.Steps);
            if (!string.IsNullOrWhiteSpace(strategy.On.Failure.Pool))
                _builder.AppendLine(6, $"pool: {strategy.On.Failure.Pool}");
        }
    }

    private void AppendOnSuccess(AdoDeploymentStrategyBase strategy)
    {
        if (strategy.On?.Success is not null)
        {
            _builder.AppendLine(4, "success:");
            if (strategy.On.Success.Steps is not null)
                _builder.Append(6, strategy.On.Success.Steps);
            if (!string.IsNullOrWhiteSpace(strategy.On.Success.Pool))
                _builder.AppendLine(6, $"pool: {strategy.On.Success.Pool}");
        }
    }


}
