using System;
using System.Text;
using PipelineWeaver.Ado;
using PipelineWeaver.Core.Transpiler.Ado.Yaml.SectionSerializers.Interfaces;

namespace PipelineWeaver.Core.Transpiler.Ado.Yaml.SectionSerializers;

public class AdoDeploymentStrategySerializer : IAdoYamlSectionSerializer
{
    internal AdoYamlBuilder _builder = new AdoYamlBuilder();
    public void AppendSection(AdoSectionBase section, AdoYamlBuilder? builder, int startingIndentation)
    {
        var deploymentStrategy = section as AdoDeploymentStrategyContainer ?? throw new ArgumentException(nameof(section));
        if (builder is not null)
            _builder = builder;

        if (deploymentStrategy.DeploymentStrategy is not null)
        {
            AppendDeploymentStrategies(deploymentStrategy.DeploymentStrategy, startingIndentation);
        }
    }

    private void AppendDeploymentStrategies(AdoDeploymentStrategyBase deploymentStrategy, int startingIndentation)
    {
        switch (deploymentStrategy)
        {
            case AdoRollingDeploymentStrategy strategy:
                AppendRollingDeploymentStrategy(strategy, startingIndentation);
                break;
            case AdoCanaryDeploymentStrategy strategy:
                AppendCanaryDeploymentStrategy(strategy, startingIndentation);
                break;
            case AdoRunOnceDeploymentStrategy strategy:
                AppendRunOnceDeploymentStrategy(strategy, startingIndentation);
                break;
            default:
                throw new ArgumentException(nameof(deploymentStrategy));
        }
    }

    private void AppendRunOnceDeploymentStrategy(AdoRunOnceDeploymentStrategy strategy, int startingIndentation)
    {
        //Implement these three strategies and then tet out the whole thing. 
        //You will have to fix a lot of indention issues.
        //here
        throw new NotImplementedException();
    }

    private void AppendRollingDeploymentStrategy(AdoRollingDeploymentStrategy strategy, int startingIndentation)
    {
        throw new NotImplementedException();
    }

    private void AppendCanaryDeploymentStrategy(AdoCanaryDeploymentStrategy strategy, int startingIndentation)
    {
        throw new NotImplementedException();
    }


}
