using System.Reflection.Metadata.Ecma335;
using System.Text;
using PipelineWeaver.Ado;
using PipelineWeaver.Core.Transpiler.Ado.Yaml;
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
    public void TestParameterSerialization_bool()
    {
        var sb = new StringBuilder();
        sb.AppendLine("parameters:");
        sb.AppendLine("  boolParamName: True");
        var expected = sb.ToString();

        var adoJobParameters = new AdoSectionCollection<AdoParameterBase>()
        {
            new AdoBoolParameter(){Name = "boolParamName", Value = true},
        };

        var doc = new AdoYamlDocument();
        doc.Builder = new AdoYamlBuilder();
        adoJobParameters.AppendSection(doc.Builder, 0);
        Assert.That(doc.Builder.ToString(), Is.EqualTo(expected));

    }


    [Test]
    public void TestParameterSerialization_string()
    {
        var sb = new StringBuilder();
        sb.AppendLine("parameters:");
        sb.AppendLine("  stringParamName: Value");
        var expected = sb.ToString();

        var adoJobParameters = new AdoSectionCollection<AdoParameterBase>()
        {
            new AdoStringParameter(){Name = "stringParamName", Value = "Value"},
        };

        var doc = new AdoYamlDocument();
        doc.Builder = new AdoYamlBuilder();
        adoJobParameters.AppendSection(doc.Builder, 0);
        Assert.That(doc.Builder.ToString(), Is.EqualTo(expected));

    }


    [Test]
    public void TestParameterSerialization_strArray()
    {
        var sb = new StringBuilder();
        sb.AppendLine("parameters:");
        sb.AppendLine("  arrayStrParamName:");
        sb.AppendLine("  - one");
        sb.AppendLine("  - two");
        var expected = sb.ToString();

        var adoJobParameters = new AdoSectionCollection<AdoParameterBase>()
        {
            new AdoArrayParameter<string>(){Name = "arrayStrParamName", Value = new List<string>(){"one","two"}.ToArray()},
        };

        var doc = new AdoYamlDocument();
        doc.Builder = new AdoYamlBuilder();
        adoJobParameters.AppendSection(doc.Builder, 0);
        Assert.That(doc.Builder.ToString(), Is.EqualTo(expected));
    }


    [Test]
    public void TestParameterSerialization_boolArr()
    {
        var sb = new StringBuilder();
        sb.AppendLine("parameters:");
        sb.AppendLine("  arrayBoolParamName:");
        sb.AppendLine("  - True");
        sb.AppendLine("  - True");
        var expected = sb.ToString();

        var adoJobParameters = new AdoSectionCollection<AdoParameterBase>()
        {
            new AdoArrayParameter<bool>(){Name = "arrayBoolParamName", Value = new List<bool>() { true, true }.ToArray() },
        };

        var doc = new AdoYamlDocument();
        doc.Builder = new AdoYamlBuilder();
        adoJobParameters.AppendSection(doc.Builder, 0);
        Assert.That(doc.Builder.ToString(), Is.EqualTo(expected));

    }


    [Test]
    public void TestParameterSerialization_intArr()
    {
        var sb = new StringBuilder();
        sb.AppendLine("parameters:");
        sb.AppendLine("  arrayIntParamName:");
        sb.AppendLine("  - 100");
        sb.AppendLine("  - 101");
        var expected = sb.ToString();

        var adoJobParameters = new AdoSectionCollection<AdoParameterBase>()
        {
            new AdoArrayParameter<int>(){Name = "arrayIntParamName", Value = new List<int>() { 100, 101 }.ToArray() },
        };

        var doc = new AdoYamlDocument();
        doc.Builder = new AdoYamlBuilder();
        adoJobParameters.AppendSection(doc.Builder, 0);
        Assert.That(doc.Builder.ToString(), Is.EqualTo(expected));
    }


    [Test]
    public void TestParameterSerialization_strDict()
    {
        var sb = new StringBuilder();
        sb.AppendLine("parameters:");
        sb.AppendLine("  dictStrParamName:");
        sb.AppendLine("    key1: value1");
        sb.AppendLine("    key2: value2");
        var expected = sb.ToString();

        var adoJobParameters = new AdoSectionCollection<AdoParameterBase>()
        {
            new AdoDictionaryParameter<string>(){Name = "dictStrParamName", Value = new Dictionary<string, string>(){{"key1","value1"},{"key2","value2"}}},
        };

        var doc = new AdoYamlDocument();
        doc.Builder = new AdoYamlBuilder();
        adoJobParameters.AppendSection(doc.Builder, 0);
        Assert.That(doc.Builder.ToString(), Is.EqualTo(expected));
    }

    [Test]
    public void TestParameterSerialization_boolDict()
    {
        var sb = new StringBuilder();
        sb.AppendLine("parameters:");
        sb.AppendLine("  dictBoolParamName:");
        sb.AppendLine("    key1: True");
        sb.AppendLine("    key2: True");
        var expected = sb.ToString();

        var adoJobParameters = new AdoSectionCollection<AdoParameterBase>()
        {
            new AdoDictionaryParameter<bool>(){Name = "dictBoolParamName", Value = new Dictionary<string, bool>(){{"key1",true},{"key2",true}} },
        };

        var doc = new AdoYamlDocument();
        doc.Builder = new AdoYamlBuilder();
        adoJobParameters.AppendSection(doc.Builder, 0);
        Assert.That(doc.Builder.ToString(), Is.EqualTo(expected));
    }

    [Test]
    public void TestParameterSerialization_Obj()
    {
        var sb = new StringBuilder();
        sb.AppendLine("parameters:");
        sb.AppendLine("  adoTestObjectParamName:");
        sb.AppendLine("    Field1: Value1");
        sb.AppendLine("    Field2: Value2");
        sb.AppendLine("    Field3: False");
        var expected = sb.ToString();

        var adoJobParameters = new AdoSectionCollection<AdoParameterBase>()
        {
            new AdoObjectParameter<AdoTestObject>(new AdoTestObject()){Name = "adoTestObjectParamName"},
        };

        var doc = new AdoYamlDocument();
        doc.Builder = new AdoYamlBuilder();
        adoJobParameters.AppendSection(doc.Builder, 0);
        Assert.That(doc.Builder.ToString(), Is.EqualTo(expected));
    }

    [Test]
    public void TestParameterSerialization_objDict()
    {
        var sb = new StringBuilder();
        sb.AppendLine("parameters:");
        sb.AppendLine("  dictObjParamName:");
        sb.AppendLine("    key1:");
        sb.AppendLine("      Field1: Value1");
        sb.AppendLine("      Field2: Value2");
        sb.AppendLine("      Field3:");
        sb.AppendLine("        Field3_1: Value3_1");
        sb.AppendLine("        Field3_2: True");
        sb.AppendLine("        Field3_3:");
        sb.AppendLine("          Field3_3_1: Value3_3_1");
        sb.AppendLine("          Field3_3_2: True");
        sb.AppendLine("    key2:");
        sb.AppendLine("      Field1: Value1");
        sb.AppendLine("      Field2: Value2");
        sb.AppendLine("      Field3:");
        sb.AppendLine("        Field3_1: Value3_1");
        sb.AppendLine("        Field3_2: True");
        sb.AppendLine("        Field3_3:");
        sb.AppendLine("          Field3_3_1: Value3_3_1");
        sb.AppendLine("          Field3_3_2: True");
        var expected = sb.ToString();

        var dynamicObj = new
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
            new AdoDictionaryParameter<object>(){Name = "dictObjParamName", Value = new Dictionary<string, object>(){{"key1",dynamicObj},{"key2",dynamicObj}}},
        };

        var doc = new AdoYamlDocument();
        doc.Builder = new AdoYamlBuilder();
        adoJobParameters.AppendSection(doc.Builder, 0);
        Assert.That(doc.Builder.ToString(), Is.EqualTo(expected));
    }

    [Test]
    public void TestParameterSerialization_dynamicObj()
    {
        var sb = new StringBuilder();
        sb.AppendLine("parameters:");
        sb.AppendLine("  adoDynObjParamName:");
        sb.AppendLine("    Field1: Value1");
        sb.AppendLine("    Field2: Value2");
        sb.AppendLine("    Field3:");
        sb.AppendLine("      Field3_1: Value3_1");
        sb.AppendLine("      Field3_2: True");
        sb.AppendLine("      Field3_3:");
        sb.AppendLine("        Field3_3_1: Value3_3_1");
        sb.AppendLine("        Field3_3_2: True");
        var expected = sb.ToString();

        var dynamicObj = new
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
            new AdoObjectParameter<dynamic>(dynamicObj){Name = "adoDynObjParamName"}
        };

        var doc = new AdoYamlDocument();
        doc.Builder = new AdoYamlBuilder();
        adoJobParameters.AppendSection(doc.Builder, 0);
        Assert.That(doc.Builder.ToString(), Is.EqualTo(expected));
    }

    [Test]
    [Explicit()]
    public void TestPipelineBuild()
    {

        var variables = new AdoSectionCollection<IAdoVariable>()
        {
            new AdoNameVariable{Name = "testName",Value = "testvalue"},
            new AdoGroupVariable{Group = "groupvalue"},
            new AdoTemplateVariable{Template = "templatevalue"}
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
            new AdoHostedPool(){VmImage = "HostedPoolName"},
            new AdoNamedPool(){Name = "PoolName"}
        };

        var dynamicObj = new
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
            new AdoBoolParameter(){Name = "boolParamName", Value = true},
            new AdoStringParameter(){Name = "stringParamName", Value = "Value"},
            new AdoArrayParameter<string>(){Name = "arrayStrParamName", Value = new List<string>(){"one","two"}.ToArray()},
            new AdoArrayParameter<bool>(){Name = "arrayBoolParamName", Value = new List<bool>() { true, true }.ToArray() },
            new AdoArrayParameter<int>(){Name = "arrayIntParamName", Value = new List<int>() { 100, 101 }.ToArray() },
            new AdoDictionaryParameter<string>(){Name = "dictStrParamName", Value = new Dictionary<string, string>(){{"key1","value1"},{"key2","value2"}}},
            new AdoDictionaryParameter<bool>(){Name = "dictBoolParamName", Value = new Dictionary<string, bool>(){{"key1",true},{"key2",true}} },
            new AdoDictionaryParameter<object>(){Name = "dictObjParamName", Value = new Dictionary<string, object>(){{"key1",dynamicObj},{"key2",dynamicObj}}},
            new AdoObjectParameter<AdoTestObject>(new AdoTestObject()){Name = "adoTestObjectParamName"},
            new AdoObjectParameter<dynamic>(dynamicObj){Name = "adoDynObjParamName"}
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