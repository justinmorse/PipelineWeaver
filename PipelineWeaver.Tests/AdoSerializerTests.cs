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

        var weirdObj = new
        {
            Field1 = "Value1",
            Field2 = "Value2",
            Field3 = new
            {
                Field3_1 = "Value3_1",
                Field3_2 = true,
                Field3_3 = new
                {
                    Field3_3_1 = "Value3_3_1",
                    Field3_3_2 = true
                }
            }
        };

        var adoJobParameters = new List<AdoParameterBase>()
        {
            new AdoBoolParameter(){Name = "Bool", Value = true},
            new AdoStringParameter(){Name = "String", Value = "Value"},
            new AdoObjectParameter<List<string>>(){Name = "ListString", Value = new List<string>(){"one","two"}},
            new AdoObjectParameter<List<object>>(){Name = "ListObj", Value= new List<object>(){weirdObj, weirdObj}},
            new AdoObjectParameter<Dictionary<string, string>>(){Name = "StringDict", Value = new Dictionary<string, string>(){{"key1","value1"},{"key2","value2"}}},
            new AdoObjectParameter<Dictionary<string, bool>>(){Name = "boolDict", Value = new Dictionary<string, bool>(){{"key1",true},{"key2",true}}},
            new AdoObjectParameter<Dictionary<string, object>>(){Name = "objDict", Value = new Dictionary<string, object>(){{"key1",weirdObj},{"key2",weirdObj}}},
        };

        var jobSteps = new List<AdoStepBase>(){
            new AdoScriptStep(){Script = "Script"},
            new AdoTemplateStep(){Template = "Template", Parameters = new AdoParameterContainer() { Parameters = adoJobParameters }},
        };

        var jobs = new List<AdoJobBase>(){
            new AdoJob(){Job = "Job", Condition = "Condition", DependsOn = new List<string>(){"Depend1","Depend2"}, DisplayName = "DisplayName", Steps = new AdoStepContainer{Steps = jobSteps}},
            new AdoTemplateJob(){Template = "Template", Condition = "Condition", Parameters = new AdoParameterContainer() { Parameters = adoJobParameters }},
        };

        var stages = new List<AdoStageBase>()
        {
            new AdoStage(){Stage = "Stage", Condition = "Condition", DependsOn = new List<string>(){"Depend1","Depend2"}, DisplayName = "DisplayName", IsSkippable = true, Jobs = new AdoJobContainer(){Jobs = jobs}, LockBehavior = "LockBehavior", TemplateContext = "TemplateContext", Trigger = "Trigger", Variables = new AdoVariableContainer() { Variables = new List<AdoVariableBase>() { new AdoNameVariable() { Name = "Name", Value = "Value" } } }, Pools = new AdoPoolContainer() { Pools = pools }},
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