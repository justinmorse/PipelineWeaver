using PipelineWeaver.Ado;
using PipelineWeaver.Core.Transpiler.Yaml;

namespace PipelineWeaver.Tests.Ado;

public class AdoSerializerTests
{

    readonly string PATH = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "Desktop", "testPipeline.yaml");

    [SetUp]
    public void Setup()
    {
        if (File.Exists(PATH))
            File.Delete(PATH);
    }

    [Test]
    public void TestPipelineBuild()
    {

        var variables = new AdoSectionCollection<IAdoVariable>()
        {
            new AdoNameVariable{Name = "test",Value = "value"},
            new AdoGroupVariable{Group = "value"},
            new AdoTemplateVariable{Template = "value"}
        };

        var triggers = new AdoTriggerContainer()
        {
            new AdoTrigger(){TriggerType = AdoTriggerType.PathInclude,Value = "PathIncludeTrigger"},
            new AdoTrigger(){TriggerType = AdoTriggerType.PathExclude,Value = "PathExcludeTrigger"},
            new AdoTrigger(){TriggerType = AdoTriggerType.BranchExclude,Value = "BranchExcludeTrigger"},
            new AdoTrigger(){TriggerType = AdoTriggerType.BranchInclude,Value = "BranchIncludeTrigger"},
            new AdoTrigger(){TriggerType = AdoTriggerType.TagInclude,Value = "TagIncludeTrigger"},
            new AdoTrigger(){TriggerType = AdoTriggerType.TagExclude,Value = "TagExcludeTrigger"},
        };

        var resources = new AdoSectionCollection<IAdoResource>()
        {
            new AdoContainerResource(){Container = "Container",Connection = "Connection",Image = "Image",Options = "Options",Env = "Env",Ports = "Ports",Volumes = "Volumes"},
            new AdoBuildResource(){Build = "Build",Connection = "Connection", Source = "Source", Trigger = true},
            new AdoRepositoryResource(){Repository = "Repository",Project = "Project",Type = "Type",Connection = "Connection",Source = "Source",Endpoint = "Endpoint"},
            new AdoPackageResource(){Package = "Package",Type = "Type",Connection = "Connection",Version = "Version",Tag = "Tag"},
            new AdoPipelineResource(){Pipeline = "Pipeline",Connection = "Connection",Project = "Project",Source = "Source",Version = "Version",Branch = "Branch",Tags = new List<string>{"Tag1","Tag2"}}
        };

        var pools = new AdoSectionCollection<IAdoPool>()
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

        var adoJobParameters = new AdoSectionCollection<AdoParameterBase>()
        {
            new AdoBoolParameter(){Name = "Bool", Value = true},
            new AdoStringParameter(){Name = "String", Value = "Value"},
            new AdoObjectParameter<List<string>>(new List<string>(){"one","two"}){Name = "ListString"},
            new AdoObjectParameter<List<object>>(new List<object>(){weirdObj, weirdObj}){Name = "ListObj"},
            new AdoObjectParameter<Dictionary<string, string>>(new Dictionary<string, string>(){{"key1","value1"},{"key2","value2"}}){Name = "StringDict"},
            new AdoObjectParameter<Dictionary<string, bool>>(new Dictionary<string, bool>(){{"key1",true},{"key2",true}}){Name = "boolDict" },
            new AdoObjectParameter<Dictionary<string, object>>(new Dictionary<string, object>(){{"key1",weirdObj},{"key2",weirdObj}}){Name = "objDict"},
            new AdoObjectParameter<AdoTestObject>(new AdoTestObject()){Name = "AdoTestObject"}
        };

        var jobSteps = new AdoSectionCollection<AdoStepBase>(){
            new AdoScriptStep(){Script = "Script"},
            new AdoTemplateStep(){Template = "Template", Parameters = adoJobParameters },
        };

        var jobs = new AdoSectionCollection<AdoJobBase>(){
            new AdoJob(){Job = "Job", Condition = "Condition", DependsOn = new List<string>(){"Depend1","Depend2"}, DisplayName = "DisplayName", Steps = jobSteps},
            new AdoTemplateJob(){Template = "Template", Condition = "Condition", Parameters = adoJobParameters },
        };

        var stages = new AdoSectionCollection<AdoStageBase>()
        {
            new AdoStage(){Stage = "Stage", Condition = "Condition", DependsOn = new List<string>(){"Depend1","Depend2"}, DisplayName = "DisplayName", IsSkippable = true, Jobs =  jobs, LockBehavior = "LockBehavior", TemplateContext = "TemplateContext", Trigger = "Trigger", Variables = new AdoSectionCollection<IAdoVariable>() { new AdoNameVariable() { Name = "Name", Value = "Value" } }, Pools = pools },
            new AdoStageTemplate(){Template = "Template", Condition = "Condition", Parameters = new List<string>(){"Param1","Param2"}}
        };

        var a = new AdoPipeline()
        {
            Name = "test",
            Pool = "Pool",
            Variables = variables,
            Triggers = triggers,
            Resources = resources,
            Stages = stages
        };

        var doc = new AdoYamlDocument();
        doc.BuildPipeline(a);
        doc.Save(PATH);
        Assert.IsTrue(true);

    }

    //Helpers

    public class AdoTestObject
    {
        public string Field1 { get; set; } = "Value1";
        public string Field2 { get; set; } = "Value2";
        public bool Field3 { get; set; } = false;
    }

}