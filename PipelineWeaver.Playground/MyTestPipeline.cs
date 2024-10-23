using PipelineWeaver.Ado;
using PipelineWeaver.Core.Transpiler.Ado.Yaml;
using PipelineWeaver.Core.Transpiler.Yaml;

namespace PipelineWeaver.Playground;

public class MyTestPipeline : AdoPipeline
{

    public MyTestPipeline()
    {
        SetupPipeline();
        BuildPipeline();
    }

    public void SetupPipeline()
    {
        Name = "MyTestPipeline";

        Triggers = new AdoTriggerContainer
        {
            new AdoTrigger
            {
                TriggerType = AdoTriggerType.BranchInclude,
                Value = "main",
            },
            new AdoTrigger
            {
                TriggerType = AdoTriggerType.BranchExclude,
                Value = "feature_branches",
            }
        };

        Pool = new AdoHostedPool
        {
            VmImage = "ubuntu-latest",
        };

        Variables = new AdoSectionCollection<IAdoVariable>
        {
            new AdoNameVariable
            {
                Name = "tag",
                Value = "$(Build.BuildNumber)",
            },
            new AdoNameVariable
            {
                Name = "ImageName",
                Value = "demo Image",
            },
            new AdoNameVariable
            {
                Name = "python.version",
                Value = "3.8",
            }
        };

        var lintStage = new AdoStage()
        {
            Stage = "Lint",
            DisplayName = "Lint",
            Jobs = new AdoSectionCollection<AdoJobBase>
            {
                new AdoJob
                {
                    Job = "Lint",
                    DisplayName = "Lint",
                    Steps = new AdoSectionCollection<AdoStepBase>()
                    {
                        new AdoScriptStep
                        {
                            Script = @"            python3 -m pip install black
            python3 -m pip install pylint",
            DisplayName = "Install Dependencies"
                        },
                        new AdoScriptStep
                        {
                            DisplayName = "Apply formatting",
                            Script = @"#app is the folder in which the application code resides
            python3 -m black ./app"
                        },
                        new AdoScriptStep
                        {
                            DisplayName = "Static code analysis with pylint",
                            Script = @"python3 -m pylint ./app --recursive=true --exit-zero"
                        }
                    }
                }
            },
        };

    }

    public void BuildPipeline()
    {
        var doc = new AdoYamlDocument();
        doc.BuildPipeline(this);
        var path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "Desktop", "testPipeline.yaml");
        doc.Save(path);
    }
}
