using System;
using System.Text;
using PipelineWeaver.Ado;
using PipelineWeaver.Core.Transpiler.Ado.Yaml;
using PipelineWeaver.Core.Transpiler.Yaml;

namespace PipelineWeaver.Tests.Ado;

public class AdoDeploymentStragetySerializerTests
{
    [Test]
    public void TestDeploymentStrategy_runonce()
    {
        var sb = new StringBuilder();
        sb.AppendLine("runOnce:");
        sb.AppendLine("  preDeploy:");
        sb.AppendLine("    steps:");
        sb.AppendLine("    - script: Script");
        sb.AppendLine("    pool: PreDeploy");
        sb.AppendLine("  deploy:");
        sb.AppendLine("    steps:");
        sb.AppendLine("    - script: Script");
        sb.AppendLine("    pool: PostDeploy");
        sb.AppendLine("  routeTraffic:");
        sb.AppendLine("    steps:");
        sb.AppendLine("    - script: Script");
        sb.AppendLine("    pool: RouteTraffic");
        sb.AppendLine("  postRouteTraffic:");
        sb.AppendLine("    steps:");
        sb.AppendLine("    - script: Script");
        sb.AppendLine("    pool: PostRouteTraffic");
        sb.AppendLine("  on:");
        sb.AppendLine("    failure:");
        sb.AppendLine("      steps:");
        sb.AppendLine("      - script: Script");
        sb.AppendLine("      pool: OnFailure");
        sb.AppendLine("    success:");
        sb.AppendLine("      steps:");
        sb.AppendLine("      - script: Script");
        sb.AppendLine("      pool: OnSuccess");

        var expected = sb.ToString();

        var adoStrategy = new AdoRunOnceDeploymentStrategy();
        SetUpStrategy(adoStrategy);
        var strategies = new AdoSectionCollection<AdoDeploymentStrategyBase>() { adoStrategy };
        var doc = new AdoYamlDocument();
        doc.Builder = new AdoYamlBuilder();
        strategies.AppendSection(doc.Builder, 0);
        Assert.That(doc.Builder.ToString(), Is.EqualTo(expected));
    }

    [Test]
    public void TestDeploymentStrategy_rolling()
    {
        var sb = new StringBuilder();
        sb.AppendLine("rolling:");
        sb.AppendLine("  maxParallel: 1");
        sb.AppendLine("  preDeploy:");
        sb.AppendLine("    steps:");
        sb.AppendLine("    - script: Script");
        sb.AppendLine("    pool: PreDeploy");
        sb.AppendLine("  deploy:");
        sb.AppendLine("    steps:");
        sb.AppendLine("    - script: Script");
        sb.AppendLine("    pool: PostDeploy");
        sb.AppendLine("  routeTraffic:");
        sb.AppendLine("    steps:");
        sb.AppendLine("    - script: Script");
        sb.AppendLine("    pool: RouteTraffic");
        sb.AppendLine("  postRouteTraffic:");
        sb.AppendLine("    steps:");
        sb.AppendLine("    - script: Script");
        sb.AppendLine("    pool: PostRouteTraffic");
        sb.AppendLine("  on:");
        sb.AppendLine("    failure:");
        sb.AppendLine("      steps:");
        sb.AppendLine("      - script: Script");
        sb.AppendLine("      pool: OnFailure");
        sb.AppendLine("    success:");
        sb.AppendLine("      steps:");
        sb.AppendLine("      - script: Script");
        sb.AppendLine("      pool: OnSuccess");

        var expected = sb.ToString();

        var adoStrategy = new AdoRollingDeploymentStrategy() { MaxParallel = 1 };
        SetUpStrategy(adoStrategy);
        var strategies = new AdoSectionCollection<AdoDeploymentStrategyBase>() { adoStrategy };
        var doc = new AdoYamlDocument();
        doc.Builder = new AdoYamlBuilder();
        strategies.AppendSection(doc.Builder, 0);
        Assert.That(doc.Builder.ToString(), Is.EqualTo(expected));
    }

    [Test]
    public void TestDeploymentStrategy_canary()
    {
        var sb = new StringBuilder();
        sb.AppendLine("canary:");
        sb.AppendLine("  increments: 1");
        sb.AppendLine("  preDeploy:");
        sb.AppendLine("    steps:");
        sb.AppendLine("    - script: Script");
        sb.AppendLine("    pool: PreDeploy");
        sb.AppendLine("  deploy:");
        sb.AppendLine("    steps:");
        sb.AppendLine("    - script: Script");
        sb.AppendLine("    pool: PostDeploy");
        sb.AppendLine("  routeTraffic:");
        sb.AppendLine("    steps:");
        sb.AppendLine("    - script: Script");
        sb.AppendLine("    pool: RouteTraffic");
        sb.AppendLine("  postRouteTraffic:");
        sb.AppendLine("    steps:");
        sb.AppendLine("    - script: Script");
        sb.AppendLine("    pool: PostRouteTraffic");
        sb.AppendLine("  on:");
        sb.AppendLine("    failure:");
        sb.AppendLine("      steps:");
        sb.AppendLine("      - script: Script");
        sb.AppendLine("      pool: OnFailure");
        sb.AppendLine("    success:");
        sb.AppendLine("      steps:");
        sb.AppendLine("      - script: Script");
        sb.AppendLine("      pool: OnSuccess");
        var expected = sb.ToString();

        var adoStrategy = new AdoCanaryDeploymentStrategy() { Increments = 1 };
        SetUpStrategy(adoStrategy);
        var strategies = new AdoSectionCollection<AdoDeploymentStrategyBase>() { adoStrategy };
        var doc = new AdoYamlDocument();
        doc.Builder = new AdoYamlBuilder();
        strategies.AppendSection(doc.Builder, 0);
        Assert.That(doc.Builder.ToString(), Is.EqualTo(expected));
    }



    //Helpers
    private void SetUpStrategy(AdoDeploymentStrategyBase strategy)
    {
        var jobSteps = new AdoSectionCollection<AdoStepBase>(){
            new AdoScriptStep(){Script = "Script"},
        };

        strategy.PreDeploy = new AdoDeploymentStrategyItem() { Pool = "PreDeploy", Steps = jobSteps };
        strategy.Deploy = new AdoDeploymentStrategyItem() { Pool = "PostDeploy", Steps = jobSteps };
        strategy.RouteTraffic = new AdoDeploymentStrategyItem() { Pool = "RouteTraffic", Steps = jobSteps };
        strategy.PostRouteTraffic = new AdoDeploymentStrategyItem() { Pool = "PostRouteTraffic", Steps = jobSteps };
        strategy.On = new AdoDeploymentStrategyOn()
        {
            Failure = new AdoDeploymentStrategyItem() { Pool = "OnFailure", Steps = jobSteps },
            Success = new AdoDeploymentStrategyItem() { Pool = "OnSuccess", Steps = jobSteps },
        };
    }
}
