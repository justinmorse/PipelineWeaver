using System;
using PipelineWeaver.Ado;

namespace PipelineWeaver.ProjectTemplate.Pipelines;

public class MyPipeline : AdoPipeline
{
    public MyPipeline()
    {
        var variables = new AdoSectionCollection<IAdoVariable>()
        {
            new AdoNameVariable { Name = "nameName", Value = "nameValue" },
            new AdoGroupVariable { Group = "groupValue" },
            new AdoTemplateVariable { Template = "templateValue" }
        };

        var triggers = new AdoTriggerContainer()
        {
            new AdoTrigger() { TriggerType = AdoTriggerType.PathInclude, Value = "PathIncludeTrigger" },
            new AdoTrigger() { TriggerType = AdoTriggerType.PathExclude, Value = "PathExcludeTrigger" },
            new AdoTrigger() { TriggerType = AdoTriggerType.BranchExclude, Value = "BranchExcludeTrigger" },
            new AdoTrigger() { TriggerType = AdoTriggerType.BranchInclude, Value = "BranchIncludeTrigger" },
            new AdoTrigger() { TriggerType = AdoTriggerType.TagInclude, Value = "TagIncludeTrigger" },
            new AdoTrigger() { TriggerType = AdoTriggerType.TagExclude, Value = "TagExcludeTrigger" },
        };
        triggers.Batch = true;

        var resources = new AdoSectionCollection<IAdoResource>()
        {
            new AdoContainerResource()
            {
                Container = "Container", Connection = "Connection", Image = "Image", Options = "Options", Env = "Env",
                Ports = "Ports", Volumes = "Volumes"
            },
            new AdoBuildResource() { Build = "Build", Connection = "Connection", Source = "Source", Trigger = true },
            new AdoRepositoryResource()
            {
                Repository = "Repository", Project = "Project", Type = "Type", Connection = "Connection",
                Source = "Source", Endpoint = "Endpoint"
            },
            new AdoPackageResource()
                { Package = "Package", Type = "Type", Connection = "Connection", Version = "Version", Tag = "Tag" },
            new AdoPipelineResource()
            {
                Pipeline = "Pipeline", Connection = "Connection", Project = "Project", Source = "Source",
                Version = "Version", Branch = "Branch", Tags =
                    ["Tag1", "Tag2"]
            }
        };

        var pools = new AdoSectionCollection<IAdoPool>()
        {
            new AdoHostedPool() { VmImage = "HostedPoolName" },
            new AdoNamedPool() { Name = "PoolName" }
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
            new AdoBoolParameter()
                { Name = "boolParamName", ValueOrDefault = true, ParameterType = AdoParameterType.Standard },
            new AdoStringParameter()
                { Name = "stringParamName", ValueOrDefault = "Value", ParameterType = AdoParameterType.Standard },
            new AdoArrayParameter<string>()
            {
                Name = "arrayStrParamName", ValueOrDefault = new List<string>() { "one", "two" }.ToArray(),
                ParameterType = AdoParameterType.Standard
            },
            new AdoArrayParameter<bool>()
            {
                Name = "arrayBoolParamName", ValueOrDefault = new List<bool>() { true, true }.ToArray(),
                ParameterType = AdoParameterType.Standard
            },
            new AdoArrayParameter<int>()
            {
                Name = "arrayIntParamName", ValueOrDefault = new List<int>() { 100, 101 }.ToArray(),
                ParameterType = AdoParameterType.Standard
            },
            new AdoDictionaryParameter<string>()
            {
                Name = "dictStrParamName",
                ValueOrDefault = new Dictionary<string, string>() { { "key1", "value1" }, { "key2", "value2" } },
                ParameterType = AdoParameterType.Standard
            },
            new AdoDictionaryParameter<bool>()
            {
                Name = "dictBoolParamName",
                ValueOrDefault = new Dictionary<string, bool>() { { "key1", true }, { "key2", true } },
                ParameterType = AdoParameterType.Standard
            },
            new AdoDictionaryParameter<object>()
            {
                Name = "dictObjParamName",
                ValueOrDefault = new Dictionary<string, object>() { { "key1", dynamicObj }, { "key2", dynamicObj } },
                ParameterType = AdoParameterType.Standard
            },
            new AdoObjectParameter<dynamic>()
                { Name = "adoDynObjParamName", ValueOrDefault = dynamicObj, ParameterType = AdoParameterType.Standard },
        };

        var jobSteps = new AdoSectionCollection<AdoStepBase>()
        {
            new AdoScriptStep() { Script = "Script" },
            new AdoTemplateStep()
            {
                Template = "StepTemplate", Parameters =
                [
                    new AdoBoolParameter()
                        { Name = "boolParamName", ValueOrDefault = true, ParameterType = AdoParameterType.Standard }
                ]
            },
        };

        var jobs = new AdoSectionCollection<AdoJobBase>()
        {
            new AdoJob()
            {
                Job = "Job", Condition = "JobCondition", DependsOn = ["Depend1", "Depend2"],
                DisplayName = "DisplayName", Steps = jobSteps
            },
            new AdoTemplateJob()
                { Template = "JobTemplate", Condition = "JobTemplateCondition", Parameters = adoJobParameters },
        };

        var parameters = new AdoSectionCollection<AdoParameterBase>()
        {
            new AdoStringParameter()
                { Name = "stringParamName", ValueOrDefault = "Value", ParameterType = AdoParameterType.Standard },
        };
        var stages = new AdoSectionCollection<AdoStageBase>()
        {
            new AdoStage()
            {
                Stage = "Stage", Condition = "Condition", DependsOn = ["Depend1", "Depend2"],
                DisplayName = "DisplayName", IsSkippable = true, Jobs = jobs, LockBehavior = "LockBehavior",
                TemplateContext = "TemplateContext", Trigger = "Trigger", Variables =
                    [new AdoNameVariable() { Name = "Name", Value = "Value" }],
                Pools = pools
            },
            new AdoStageTemplate()
                { Template = "StageTemplate", Condition = "StageTemplateCondition", Parameters = parameters }
        };

        Name = "test";
        Pool = [new AdoNamedPool() { Name = "Pool", Demands = ["Demand1", "Demand2"] }];
        Variables = variables;
        Triggers = triggers;
        Resources = resources;
        Stages = stages;
    }
}