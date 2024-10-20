using System;
using PipelineWeaver.Ado;
using PipelineWeaver.Core.Transpiler.Yaml;

namespace PipelineWeaver.Tests.Ado;

public class AdoPipelineSerializerTests
{
    [Test]
    public void TestPipeline_basic()
    {

        var expected = File.ReadAllText("basic-pipeline.yaml");
        var variables = new AdoSectionCollection<IAdoVariable>()
        {
            new AdoNameVariable{Name = "nameName",Value = "namevalue"},
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
        triggers.Batch = true;

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
            new AdoObjectParameter<Helpers.AdoTestObject>(new Helpers.AdoTestObject()){Name = "adoTestObjectParamName"},
            new AdoObjectParameter<dynamic>(dynamicObj){Name = "adoDynObjParamName"}
        };

        var jobSteps = new AdoSectionCollection<AdoStepBase>(){
            new AdoScriptStep(){Script = "Script"},
            new AdoTemplateStep(){Template = "StepTemplate", Parameters = new AdoSectionCollection<AdoParameterBase>(){new AdoBoolParameter(){Name = "boolParamName", Value = true}}},
        };

        var jobs = new AdoSectionCollection<AdoJobBase>(){
            new AdoJob(){Job = "Job", Condition = "JobCondition", DependsOn = new List<string>(){"Depend1","Depend2"}, DisplayName = "DisplayName", Steps = jobSteps},
            new AdoTemplateJob(){Template = "JobTemplate", Condition = "JobTemplateCondition", Parameters = adoJobParameters },
        };

        var parameters = new AdoSectionCollection<AdoParameterBase>()
        {
            new AdoStringParameter(){Name = "stringParamName", Value = "Value"}
        };
        var stages = new AdoSectionCollection<AdoStageBase>()
        {
            new AdoStage(){Stage = "Stage", Condition = "Condition", DependsOn = new List<string>(){"Depend1","Depend2"}, DisplayName = "DisplayName", IsSkippable = true, Jobs =  jobs, LockBehavior = "LockBehavior", TemplateContext = "TemplateContext", Trigger = "Trigger", Variables = new AdoSectionCollection<IAdoVariable>() { new AdoNameVariable() { Name = "Name", Value = "Value" } }, Pools = pools },
            new AdoStageTemplate(){Template = "StageTemplate", Condition = "StageTemplateCondition", Parameters = parameters}
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
        Assert.That(doc.Builder.ToString(), Is.EqualTo(expected));
    }
}
