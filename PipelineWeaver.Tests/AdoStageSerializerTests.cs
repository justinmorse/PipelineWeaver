using System.Text;
using PipelineWeaver.Ado;
using PipelineWeaver.Core.Transpiler.Ado.Yaml;
using PipelineWeaver.Core.Transpiler.Yaml;

namespace PipelineWeaver.Tests.Ado;

public class AdoStageSerializerTests
{

    [Test]
    public void TestStageSerializer_stage()
    {
        var sb = new StringBuilder();
        sb.AppendLine("stages:");
        sb.AppendLine("- stage: Stage");
        sb.AppendLine("  dependsOn:");
        sb.AppendLine("  - Depend1");
        sb.AppendLine("  - Depend2");
        sb.AppendLine("  displayName: DisplayName");
        sb.AppendLine("  variables:");
        sb.AppendLine("  - name: Name");
        sb.AppendLine("    value: Value");
        sb.AppendLine("  lockBehavior: LockBehavior");
        sb.AppendLine("  trigger: Trigger");
        sb.AppendLine("  isSkippable: True");
        sb.AppendLine("  templateContext: TemplateContext");
        sb.AppendLine("  pools:");
        sb.AppendLine("    name: PoolName");
        sb.AppendLine("  jobs:");
        sb.AppendLine("  - job: Job");
        sb.AppendLine("    displayName: DisplayName");
        sb.AppendLine("    dependsOn:");
        sb.AppendLine("    - Depend1");
        sb.AppendLine("    - Depend2");
        sb.AppendLine("    steps:");
        sb.AppendLine("    - script: Script");
        var expected = sb.ToString();

        var pools = new AdoSectionCollection<IAdoPool>()
        {
            new AdoNamedPool(){Name = "PoolName"}
        };
        var jobSteps = new AdoSectionCollection<AdoStepBase>()
        {
            new AdoScriptStep(){Script = "Script"},
        };

        var jobs = new AdoSectionCollection<AdoJobBase>()
        {
            new AdoJob(){Job = "Job", Condition = "JobCondition", DependsOn = new List<string>(){"Depend1","Depend2"}, DisplayName = "DisplayName", Steps = jobSteps},
        };
        var stages = new AdoSectionCollection<AdoStageBase>()
        {
            new AdoStage(){
                Stage = "Stage",
                Condition = "Condition",
                DependsOn = new List<string>(){"Depend1","Depend2"},
                DisplayName = "DisplayName",
                IsSkippable = true,
                Jobs =  jobs,
                LockBehavior = "LockBehavior",
                TemplateContext = "TemplateContext",
                Trigger = "Trigger",
                Variables = new AdoSectionCollection<IAdoVariable>() { new AdoNameVariable() { Name = "Name", Value = "Value" } },
                Pools = pools },

        };
        var doc = new AdoYamlDocument();
        doc.Builder = new AdoYamlBuilder();
        stages.AppendSection(doc.Builder, 0);
        Assert.That(doc.Builder.ToString(), Is.EqualTo(expected));
    }

    [Test]
    public void TestStageSerializer_template()
    {
        var sb = new StringBuilder();
        sb.AppendLine("stages:");
        sb.AppendLine("- template: Template");
        sb.AppendLine("  parameters:");
        sb.AppendLine("    stringParamName: Value");
        sb.AppendLine("  dependsOn:");
        sb.AppendLine("  - Depend1");
        sb.AppendLine("  - Depend2");
        var expected = sb.ToString();

        var parameters = new AdoSectionCollection<AdoParameterBase>()
        {
            new AdoStringParameter(){Name = "stringParamName", Value = "Value"}
        };

        var stages = new AdoSectionCollection<AdoStageBase>()
        {
            new AdoStageTemplate()
            {
                Template = "Template",
                Condition = "Condition",
                DependsOn = new List<string>(){"Depend1","Depend2"},
                Parameters = parameters
                },

        };
        var doc = new AdoYamlDocument();
        doc.Builder = new AdoYamlBuilder();
        stages.AppendSection(doc.Builder, 0);
        Assert.That(doc.Builder.ToString(), Is.EqualTo(expected));
    }
}
