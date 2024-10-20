using System.Text;
using NUnit.Framework.Internal;
using PipelineWeaver.Ado;
using PipelineWeaver.Core.Transpiler.Ado.Yaml;
using PipelineWeaver.Core.Transpiler.Yaml;
namespace PipelineWeaver.Tests.Ado;

public class AdoJobSerializerTests
{
    [Test]
    public void AdoJobSerializerTests_Job()
    {
        var sb = new StringBuilder();
        sb.AppendLine("jobs:");
        sb.AppendLine("- job: Job");
        sb.AppendLine("  displayName: DisplayName");
        sb.AppendLine("  dependsOn:");
        sb.AppendLine("  - Depend1");
        sb.AppendLine("  - Depend2");
        sb.AppendLine("  steps:");
        sb.AppendLine("  - script: Script");
        var expected = sb.ToString();

        var jobSteps = new AdoSectionCollection<AdoStepBase>(){
            new AdoScriptStep(){Script = "Script"},
        };

        var jobs = new AdoSectionCollection<AdoJobBase>(){
            new AdoJob(){Job = "Job", Condition = "JobCondition", DependsOn = new List<string>(){"Depend1","Depend2"}, DisplayName = "DisplayName", Steps = jobSteps},
        };

        var doc = new AdoYamlDocument();
        doc.Builder = new AdoYamlBuilder();
        jobs.AppendSection(doc.Builder, 0);
        Assert.That(doc.Builder.ToString(), Is.EqualTo(expected));
    }

    [Test]
    public void AdoJobSerializerTests_TemplateJob()
    {
        var sb = new StringBuilder();
        sb.AppendLine("jobs:");
        sb.AppendLine("- template: JobTemplate");
        sb.AppendLine("  parameters:");
        sb.AppendLine("    stringParamName: Value");
        var expected = sb.ToString();

        var adoJobParameters = new AdoSectionCollection<AdoParameterBase>()
        {
            new AdoStringParameter(){Name = "stringParamName", Value = "Value"},
        };

        var jobs = new AdoSectionCollection<AdoJobBase>(){

            new AdoTemplateJob(){Template = "JobTemplate", Condition = "JobTemplateCondition", Parameters = adoJobParameters },
        };

        var doc = new AdoYamlDocument();
        doc.Builder = new AdoYamlBuilder();
        jobs.AppendSection(doc.Builder, 0);
        Assert.That(doc.Builder.ToString(), Is.EqualTo(expected));
    }

    [Test]
    public void AdoJobSerializerTests_DeploymentJob()
    {
        var sb = new StringBuilder();
        sb.AppendLine("jobs:");
        sb.AppendLine("- deployment: DeploymentJob");
        sb.AppendLine("  displayName: ");
        sb.AppendLine("  condition: ");
        sb.AppendLine("    runOnce:");
        sb.AppendLine("      preDeploy:");
        sb.AppendLine("        steps:");
        sb.AppendLine("        - script: Script");
        sb.AppendLine("        pool: PreDeploy");
        sb.AppendLine("      deploy:");
        sb.AppendLine("        steps:");
        sb.AppendLine("        - script: Script");
        sb.AppendLine("        pool: PostDeploy");
        sb.AppendLine("      routeTraffic:");
        sb.AppendLine("        steps:");
        sb.AppendLine("        - script: Script");
        sb.AppendLine("        pool: RouteTraffic");
        sb.AppendLine("      postRouteTraffic:");
        sb.AppendLine("        steps:");
        sb.AppendLine("        - script: Script");
        sb.AppendLine("        pool: PostRouteTraffic");
        sb.AppendLine("      on:");
        sb.AppendLine("        failure:");
        sb.AppendLine("          steps:");
        sb.AppendLine("          - script: Script");
        sb.AppendLine("          pool: OnFailure");
        sb.AppendLine("        success:");
        sb.AppendLine("          steps:");
        sb.AppendLine("          - script: Script");
        sb.AppendLine("          pool: OnSuccess");
        var expected = sb.ToString();

        var jobSteps = new AdoSectionCollection<AdoStepBase>(){
            new AdoScriptStep(){Script = "Script"},
        };


        var jobs = new AdoSectionCollection<AdoJobBase>(){

            new AdoDeploymentJob()
            {
                Deployment = "DeploymentJob",
                Environment = new AdoEnvironment(){Name = "Environment"},
                Strategy = new AdoRunOnceDeploymentStrategy(){
                    PreDeploy = new AdoDeploymentStrategyItem(){Pool = "PreDeploy", Steps = jobSteps},
                    Deploy = new AdoDeploymentStrategyItem(){Pool = "PostDeploy", Steps = jobSteps},
                    RouteTraffic = new AdoDeploymentStrategyItem(){Pool = "RouteTraffic", Steps = jobSteps},
                    PostRouteTraffic = new AdoDeploymentStrategyItem(){Pool = "PostRouteTraffic", Steps = jobSteps},
                    On = new AdoDeploymentStrategyOn()
                    {
                        Failure = new AdoDeploymentStrategyItem(){Pool = "OnFailure", Steps = jobSteps},
                        Success = new AdoDeploymentStrategyItem(){Pool = "OnSuccess", Steps = jobSteps},
                    }
                }
            },
        };

        var doc = new AdoYamlDocument();
        doc.Builder = new AdoYamlBuilder();
        jobs.AppendSection(doc.Builder, 0);
        doc.Save(Helpers.PATH);
        Assert.That(doc.Builder.ToString(), Is.EqualTo(expected));
    }
}
