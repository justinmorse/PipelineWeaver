using PipelineWeaver.Core;
using PipelineWeaver.Core.Transpiler.Ado.Yaml;
using PipelineWeaver.Core.Transpiler.Ado.Yaml.SectionSerializers;
using PipelineWeaver.Core.Transpiler.Yaml;

namespace PipelineWeaver.Ado.Tests;

public class AdoSerializerTests
{

    string _path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "Desktop", "testPipeline.yaml");

    [SetUp]
    public void Setup()
    {
        if (File.Exists(_path))
            File.Delete(_path);
    }

    [Test]
    public void TestPipelineBuild()
    {

        var variables = new List<AdoVariableBase>()
        {
            new AdoNameVariable{Name = "test",Value = "value"},
            new AdoGroupVariable{Group = "value"},
            new AdoTemplateVariable{Template = "value"}
        };

        var triggers = new List<AdoTriggerBase>()
        {
            new AdoTrigger(){TriggerType = AdoTriggerType.PathInclude,Value = "PathIncludeTrigger"},
            new AdoTrigger(){TriggerType = AdoTriggerType.PathExclude,Value = "PathExcludeTrigger"},
            new AdoTrigger(){TriggerType = AdoTriggerType.BranchExclude,Value = "BranchExcludeTrigger"},
            new AdoTrigger(){TriggerType = AdoTriggerType.BranchInclude,Value = "BranchIncludeTrigger"},
            new AdoTrigger(){TriggerType = AdoTriggerType.TagInclude,Value = "TagIncludeTrigger"},
            new AdoTrigger(){TriggerType = AdoTriggerType.TagExclude,Value = "TagExcludeTrigger"},
        };

        var resources = new List<AdoResourceBase>()
        {
            new AdoContainerResource(){Container = "Container",Connection = "Connection",Image = "Image",Options = "Options",Env = "Env",Ports = "Ports",Volumes = "Volumes"},
            new AdoBuildResource(){Build = "Build",Connection = "Connection", Source = "Source", Trigger = true},
            new AdoRepositoryResource(){Repository = "Repository",Project = "Project",Type = "Type",Connection = "Connection",Source = "Source",Endpoint = "Endpoint"},
            new AdoPackageResource(){Package = "Package",Type = "Type",Connection = "Connection",Version = "Version",Tag = "Tag"},
            new AdoPipelineResource(){Pipeline = "Pipeline",Connection = "Connection",Project = "Project",Source = "Source",Version = "Version",Branch = "Branch",Tags = new List<string>{"Tag1","Tag2"}}
        };

        var pools = new List<AdoPoolBase>()
        {
            new AdoHostedPool(){VmImage = "HostedPool"},
            new AdoNamedPool(){Name = "Pool"}
        };

        var jobs = new AdoJobContainer();

        var stages = new List<AdoStageBase>()
        {
            new AdoStage(){Stage = "Stage", Condition = "Condition", DependsOn = new List<string>(){"Depend1","Depend2"}, DisplayName = "DisplayName", IsSkippable = true, Jobs = jobs, LockBehavior = "LockBehavior", TemplateContext = "TemplateContext", Trigger = "Trigger", Variables = new AdoVariableContainer() { Variables = new List<AdoVariableBase>() { new AdoNameVariable() { Name = "Name", Value = "Value" } } }, Pools = new AdoPoolContainer() { Pools = pools }},
            new AdoStageTemplate(){Template = "Template", Condition = "Condition", Parameters = new List<string>(){"Param1","Param2"}}
        };

        var a = new AdoPipeline()
        {
            Name = "test",
            Pool = "Pool",
            Variables = new AdoVariableContainer() { Variables = variables },
            Triggers = new AdoTriggerContainer() { Triggers = triggers },
            Resources = new AdoResourceContainer() { Resources = resources },
            Stages = new AdoStageContainer() { Stages = stages }
        };

        var doc = new AdoYamlDocument();
        doc.BuildPipeline(a);
        doc.Save(_path);
        Assert.IsTrue(true);

    }

}