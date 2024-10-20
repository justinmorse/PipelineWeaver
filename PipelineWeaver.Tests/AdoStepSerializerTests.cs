using System;
using System.Text;
using PipelineWeaver.Ado;
using PipelineWeaver.Core.Transpiler.Ado.Yaml;
using PipelineWeaver.Core.Transpiler.Yaml;

namespace PipelineWeaver.Tests.Ado;

public class AdoStepSerializerTests
{
    [Test]
    public void TestStepSerialization_template()
    {
        var sb = new StringBuilder();
        sb.AppendLine("steps:");
        sb.AppendLine("- template: templatevalue");
        sb.AppendLine("  parameters:");
        sb.AppendLine("    arrayParamName:");
        sb.AppendLine("    - Value1");
        sb.AppendLine("    - Value2");
        var expected = sb.ToString();

        var steps = new AdoSectionCollection<AdoStepBase>()
        {
            new AdoTemplateStep{Template = "templatevalue", Parameters = new AdoSectionCollection<AdoParameterBase>(){new AdoArrayParameter<string>(){Name = "arrayParamName", Value = new List<string>(){"Value1", "Value2"}.ToArray()}}}
        };

        var doc = new AdoYamlDocument();
        doc.Builder = new AdoYamlBuilder();
        steps.AppendSection(doc.Builder, 0);
        Assert.That(doc.Builder.ToString(), Is.EqualTo(expected));
    }

    [Test]
    public void TestStepSerialization_template_bare()
    {
        var sb = new StringBuilder();
        sb.AppendLine("steps:");
        sb.AppendLine("- template: templatevalue");
        var expected = sb.ToString();

        var steps = new AdoSectionCollection<AdoStepBase>()
        {
            new AdoTemplateStep{Template = "templatevalue"}
        };

        var doc = new AdoYamlDocument();
        doc.Builder = new AdoYamlBuilder();
        steps.AppendSection(doc.Builder, 0);
        Assert.That(doc.Builder.ToString(), Is.EqualTo(expected));
    }

    [Test]
    public void TestStepSerialization_task()
    {
        var sb = new StringBuilder();
        sb.AppendLine("steps:");
        sb.AppendLine("- task: testtask");
        sb.AppendLine("  name: namevalue");
        sb.AppendLine("  displayName: displaynamevalue");
        sb.AppendLine("  timeoutInMinutes: 1");
        sb.AppendLine("  continueOnError: True");
        sb.AppendLine("  condition: conditionvalue");
        sb.AppendLine("  retryCountOnTaskFailure: 1");
        sb.AppendLine("  target: targetvalue");
        sb.AppendLine("  env:");
        sb.AppendLine("    env1: value1");
        sb.AppendLine("    env2: value2");
        sb.AppendLine("  enabled: True");
        sb.AppendLine("  inputs:");
        sb.AppendLine("    input1: value1");
        sb.AppendLine("    input2: value2");
        var expected = sb.ToString();

        var step = new AdoTaskStep()
        {
            Task = "testtask",
            Inputs = new Dictionary<string, string>() { { "input1", "value1" }, { "input2", "value2" }, },
        };
        AppendBaseFields(step);
        var steps = new AdoSectionCollection<AdoStepBase>()
        {
            step
        };

        var doc = new AdoYamlDocument();
        doc.Builder = new AdoYamlBuilder();
        steps.AppendSection(doc.Builder, 0);
        Assert.That(doc.Builder.ToString(), Is.EqualTo(expected));
    }

    [Test]
    public void TestStepSerialization_task_bare()
    {
        var sb = new StringBuilder();
        sb.AppendLine("steps:");
        sb.AppendLine("- task: testtask");
        var expected = sb.ToString();

        var step = new AdoTaskStep()
        {
            Task = "testtask",
        };

        var steps = new AdoSectionCollection<AdoStepBase>()
        {
            step
        };

        var doc = new AdoYamlDocument();
        doc.Builder = new AdoYamlBuilder();
        steps.AppendSection(doc.Builder, 0);
        Assert.That(doc.Builder.ToString(), Is.EqualTo(expected));
    }

    [Test]
    public void TestStepSerialization_script()
    {
        var sb = new StringBuilder();
        sb.AppendLine("steps:");
        sb.AppendLine("- script: testscript");
        sb.AppendLine("  name: namevalue");
        sb.AppendLine("  displayName: displaynamevalue");
        sb.AppendLine("  timeoutInMinutes: 1");
        sb.AppendLine("  continueOnError: True");
        sb.AppendLine("  condition: conditionvalue");
        sb.AppendLine("  retryCountOnTaskFailure: 1");
        sb.AppendLine("  target: targetvalue");
        sb.AppendLine("  env:");
        sb.AppendLine("    env1: value1");
        sb.AppendLine("    env2: value2");
        sb.AppendLine("  enabled: True");
        var expected = sb.ToString();

        var step = new AdoScriptStep() { Script = "testscript" };
        AppendBaseFields(step);
        var steps = new AdoSectionCollection<AdoStepBase>()
        {
            step
        };

        var doc = new AdoYamlDocument();
        doc.Builder = new AdoYamlBuilder();
        steps.AppendSection(doc.Builder, 0);
        Assert.That(doc.Builder.ToString(), Is.EqualTo(expected));
    }

    [Test]
    public void TestStepSerialization_script_bare()
    {
        var sb = new StringBuilder();
        sb.AppendLine("steps:");
        sb.AppendLine("- script: testscript");
        var expected = sb.ToString();

        var step = new AdoScriptStep() { Script = "testscript" };
        var steps = new AdoSectionCollection<AdoStepBase>()
        {
            step
        };

        var doc = new AdoYamlDocument();
        doc.Builder = new AdoYamlBuilder();
        steps.AppendSection(doc.Builder, 0);
        Assert.That(doc.Builder.ToString(), Is.EqualTo(expected));
    }

    [Test]
    public void TestStepSerialization_reviewapp()
    {
        var sb = new StringBuilder();
        sb.AppendLine("steps:");
        sb.AppendLine("- reviewApp: testreviewapp");
        sb.AppendLine("  name: namevalue");
        sb.AppendLine("  displayName: displaynamevalue");
        sb.AppendLine("  timeoutInMinutes: 1");
        sb.AppendLine("  continueOnError: True");
        sb.AppendLine("  condition: conditionvalue");
        sb.AppendLine("  retryCountOnTaskFailure: 1");
        sb.AppendLine("  target: targetvalue");
        sb.AppendLine("  env:");
        sb.AppendLine("    env1: value1");
        sb.AppendLine("    env2: value2");
        sb.AppendLine("  enabled: True");
        var expected = sb.ToString();

        var step = new AdoReviewAppStep() { ReviewApp = "testreviewapp" };
        AppendBaseFields(step);
        var steps = new AdoSectionCollection<AdoStepBase>() { step };

        var doc = new AdoYamlDocument();
        doc.Builder = new AdoYamlBuilder();
        steps.AppendSection(doc.Builder, 0);
        Assert.That(doc.Builder.ToString(), Is.EqualTo(expected));
    }

    [Test]
    public void TestStepSerialization_reviewapp_bare()
    {
        var sb = new StringBuilder();
        sb.AppendLine("steps:");
        sb.AppendLine("- reviewApp: testreviewapp");
        var expected = sb.ToString();

        var step = new AdoReviewAppStep() { ReviewApp = "testreviewapp" };
        var steps = new AdoSectionCollection<AdoStepBase>() { step };

        var doc = new AdoYamlDocument();
        doc.Builder = new AdoYamlBuilder();
        steps.AppendSection(doc.Builder, 0);
        Assert.That(doc.Builder.ToString(), Is.EqualTo(expected));
    }

    [Test]
    public void TestStepSerialization_pwsh()
    {
        var sb = new StringBuilder();
        sb.AppendLine("steps:");
        sb.AppendLine("- pwsh: testpwsh");
        sb.AppendLine("  name: namevalue");
        sb.AppendLine("  displayName: displaynamevalue");
        sb.AppendLine("  timeoutInMinutes: 1");
        sb.AppendLine("  continueOnError: True");
        sb.AppendLine("  condition: conditionvalue");
        sb.AppendLine("  retryCountOnTaskFailure: 1");
        sb.AppendLine("  target: targetvalue");
        sb.AppendLine("  env:");
        sb.AppendLine("    env1: value1");
        sb.AppendLine("    env2: value2");
        sb.AppendLine("  enabled: True");
        sb.AppendLine("  failOnStderr: True");
        sb.AppendLine("  errorActionPreference: erroractionpreferencevalue");
        sb.AppendLine("  ignoreLASTEXITCODE: True");
        sb.AppendLine("  workingDirectory: workingdirectoryvalue");

        var expected = sb.ToString();

        var step = new AdoPwshStep() { Pwsh = "testpwsh", ErrorActionPreference = "erroractionpreferencevalue", FailOnStderr = true, IgnoreLASTEXITCODE = true, WorkingDirectory = "workingdirectoryvalue" };
        AppendBaseFields(step);
        var steps = new AdoSectionCollection<AdoStepBase>() { step };

        var doc = new AdoYamlDocument();
        doc.Builder = new AdoYamlBuilder();
        steps.AppendSection(doc.Builder, 0);
        Assert.That(doc.Builder.ToString(), Is.EqualTo(expected));
    }

    [Test]
    public void TestStepSerialization_pwsh_bare()
    {
        var sb = new StringBuilder();
        sb.AppendLine("steps:");
        sb.AppendLine("- pwsh: testpwsh");
        var expected = sb.ToString();

        var step = new AdoPwshStep() { Pwsh = "testpwsh" };
        var steps = new AdoSectionCollection<AdoStepBase>() { step };

        var doc = new AdoYamlDocument();
        doc.Builder = new AdoYamlBuilder();
        steps.AppendSection(doc.Builder, 0);
        Assert.That(doc.Builder.ToString(), Is.EqualTo(expected));
    }

    [Test]
    public void TestStepSerialization_publish_bare()
    {
        var sb = new StringBuilder();
        sb.AppendLine("steps:");
        sb.AppendLine("- publish: testpublish");
        var expected = sb.ToString();

        var step = new AdoPublishStep() { Publish = "testpublish" };
        var steps = new AdoSectionCollection<AdoStepBase>() { step };

        var doc = new AdoYamlDocument();
        doc.Builder = new AdoYamlBuilder();
        steps.AppendSection(doc.Builder, 0);
        Assert.That(doc.Builder.ToString(), Is.EqualTo(expected));
    }

    [Test]
    public void TestStepSerialization_powershell()
    {
        var sb = new StringBuilder();
        sb.AppendLine("steps:");
        sb.AppendLine("- powershell: testpowershell");
        sb.AppendLine("  name: namevalue");
        sb.AppendLine("  displayName: displaynamevalue");
        sb.AppendLine("  timeoutInMinutes: 1");
        sb.AppendLine("  continueOnError: True");
        sb.AppendLine("  condition: conditionvalue");
        sb.AppendLine("  retryCountOnTaskFailure: 1");
        sb.AppendLine("  target: targetvalue");
        sb.AppendLine("  env:");
        sb.AppendLine("    env1: value1");
        sb.AppendLine("    env2: value2");
        sb.AppendLine("  enabled: True");
        sb.AppendLine("  failOnStderr: True");
        sb.AppendLine("  errorActionPreference: erroractionpreferencevalue");
        sb.AppendLine("  ignoreLASTEXITCODE: True");
        sb.AppendLine("  workingDirectory: workingdirectoryvalue");

        var expected = sb.ToString();

        var step = new AdoPowershellStep() { Powershell = "testpowershell", ErrorActionPreference = "erroractionpreferencevalue", FailOnStderr = true, IgnoreLASTEXITCODE = true, WorkingDirectory = "workingdirectoryvalue" };
        AppendBaseFields(step);
        var steps = new AdoSectionCollection<AdoStepBase>() { step };

        var doc = new AdoYamlDocument();
        doc.Builder = new AdoYamlBuilder();
        steps.AppendSection(doc.Builder, 0);
        Assert.That(doc.Builder.ToString(), Is.EqualTo(expected));
    }

    [Test]
    public void TestStepSerialization_powershell_bare()
    {
        var sb = new StringBuilder();
        sb.AppendLine("steps:");
        sb.AppendLine("- powershell: testpowershell");
        var expected = sb.ToString();

        var step = new AdoPowershellStep() { Powershell = "testpowershell" };
        var steps = new AdoSectionCollection<AdoStepBase>() { step };

        var doc = new AdoYamlDocument();
        doc.Builder = new AdoYamlBuilder();
        steps.AppendSection(doc.Builder, 0);
        Assert.That(doc.Builder.ToString(), Is.EqualTo(expected));
    }

    [Test]
    public void TestStepSerialization_getpackage()
    {
        var sb = new StringBuilder();
        sb.AppendLine("steps:");
        sb.AppendLine("- getPackage: testgetpackage");
        sb.AppendLine("  path: testpath");
        sb.AppendLine("  name: namevalue");
        sb.AppendLine("  displayName: displaynamevalue");
        sb.AppendLine("  timeoutInMinutes: 1");
        sb.AppendLine("  continueOnError: True");
        sb.AppendLine("  condition: conditionvalue");
        sb.AppendLine("  retryCountOnTaskFailure: 1");
        sb.AppendLine("  target: targetvalue");
        sb.AppendLine("  env:");
        sb.AppendLine("    env1: value1");
        sb.AppendLine("    env2: value2");
        sb.AppendLine("  enabled: True");

        var expected = sb.ToString();

        var step = new AdoGetPackageStep() { GetPackage = "testgetpackage", Path = "testpath" };
        AppendBaseFields(step);
        var steps = new AdoSectionCollection<AdoStepBase>() { step };

        var doc = new AdoYamlDocument();
        doc.Builder = new AdoYamlBuilder();
        steps.AppendSection(doc.Builder, 0);
        Assert.That(doc.Builder.ToString(), Is.EqualTo(expected));
    }

    public void TestStepSerialization_getpackage_bare()
    {
        var sb = new StringBuilder();
        sb.AppendLine("steps:");
        sb.AppendLine("- getPackage: testgetpackage");
        var expected = sb.ToString();

        var step = new AdoGetPackageStep() { GetPackage = "testgetpackage" };
        AppendBaseFields(step);
        var steps = new AdoSectionCollection<AdoStepBase>() { step };

        var doc = new AdoYamlDocument();
        doc.Builder = new AdoYamlBuilder();
        steps.AppendSection(doc.Builder, 0);
        Assert.That(doc.Builder.ToString(), Is.EqualTo(expected));
    }


    [Test]
    public void TestStepSerialization_downloadbuild()
    {
        var sb = new StringBuilder();
        sb.AppendLine("steps:");
        sb.AppendLine("- downloadBuild: testdownloadbuild");
        sb.AppendLine("  artifact: testartifact");
        sb.AppendLine("  path: testpath");
        sb.AppendLine("  patterns: testpatterns");
        sb.AppendLine("  name: namevalue");
        sb.AppendLine("  displayName: displaynamevalue");
        sb.AppendLine("  timeoutInMinutes: 1");
        sb.AppendLine("  continueOnError: True");
        sb.AppendLine("  condition: conditionvalue");
        sb.AppendLine("  retryCountOnTaskFailure: 1");
        sb.AppendLine("  target: targetvalue");
        sb.AppendLine("  env:");
        sb.AppendLine("    env1: value1");
        sb.AppendLine("    env2: value2");
        sb.AppendLine("  enabled: True");

        var expected = sb.ToString();

        var step = new AdoDownloadBuildStep() { DownloadBuild = "testdownloadbuild", Path = "testpath", Artifact = "testartifact", Patterns = "testpatterns" };
        AppendBaseFields(step);
        var steps = new AdoSectionCollection<AdoStepBase>() { step };

        var doc = new AdoYamlDocument();
        doc.Builder = new AdoYamlBuilder();
        steps.AppendSection(doc.Builder, 0);
        Assert.That(doc.Builder.ToString(), Is.EqualTo(expected));
    }

    [Test]
    public void TestStepSerialization_downloadbuild_bare()
    {
        var sb = new StringBuilder();
        sb.AppendLine("steps:");
        sb.AppendLine("- downloadBuild: testdownloadbuild");
        var expected = sb.ToString();

        var step = new AdoDownloadBuildStep() { DownloadBuild = "testdownloadbuild" };
        var steps = new AdoSectionCollection<AdoStepBase>() { step };

        var doc = new AdoYamlDocument();
        doc.Builder = new AdoYamlBuilder();
        steps.AppendSection(doc.Builder, 0);
        Assert.That(doc.Builder.ToString(), Is.EqualTo(expected));
    }

    [Test]
    public void TestStepSerialization_download()
    {
        var sb = new StringBuilder();
        sb.AppendLine("steps:");
        sb.AppendLine("- download: testdownload");
        sb.AppendLine("  artifact: testartifact");
        sb.AppendLine("  patterns: testpatterns");
        sb.AppendLine("  name: namevalue");
        sb.AppendLine("  displayName: displaynamevalue");
        sb.AppendLine("  timeoutInMinutes: 1");
        sb.AppendLine("  continueOnError: True");
        sb.AppendLine("  condition: conditionvalue");
        sb.AppendLine("  retryCountOnTaskFailure: 1");
        sb.AppendLine("  target: targetvalue");
        sb.AppendLine("  env:");
        sb.AppendLine("    env1: value1");
        sb.AppendLine("    env2: value2");
        sb.AppendLine("  enabled: True");

        var expected = sb.ToString();

        var step = new AdoDownloadStep() { Download = "testdownload", Path = "testpath", Artifact = "testartifact", Patterns = "testpatterns" };
        AppendBaseFields(step);
        var steps = new AdoSectionCollection<AdoStepBase>() { step };

        var doc = new AdoYamlDocument();
        doc.Builder = new AdoYamlBuilder();
        steps.AppendSection(doc.Builder, 0);
        Assert.That(doc.Builder.ToString(), Is.EqualTo(expected));
    }

    [Test]
    public void TestStepSerialization_download_bare()
    {
        var sb = new StringBuilder();
        sb.AppendLine("steps:");
        sb.AppendLine("- download: testdownload");
        var expected = sb.ToString();

        var step = new AdoDownloadStep() { Download = "testdownload" };
        var steps = new AdoSectionCollection<AdoStepBase>() { step };

        var doc = new AdoYamlDocument();
        doc.Builder = new AdoYamlBuilder();
        steps.AppendSection(doc.Builder, 0);
        Assert.That(doc.Builder.ToString(), Is.EqualTo(expected));
    }

    [Test]
    public void TestStepSerialization_checkout()
    {
        var sb = new StringBuilder();
        sb.AppendLine("steps:");
        sb.AppendLine("- checkout: testcheckout");
        sb.AppendLine("  clean: True");
        sb.AppendLine("  fetchDepth: 1");
        sb.AppendLine("  fetchFilter: testfetchfilter");
        sb.AppendLine("  fetchTags: True");
        sb.AppendLine("  lfs: True");
        sb.AppendLine("  persistCredentials: True");
        sb.AppendLine("  submodules: testsubmodules");
        sb.AppendLine("  path: testpath");
        sb.AppendLine("  workspaceRepo: True");
        sb.AppendLine("  name: namevalue");
        sb.AppendLine("  displayName: displaynamevalue");
        sb.AppendLine("  timeoutInMinutes: 1");
        sb.AppendLine("  continueOnError: True");
        sb.AppendLine("  condition: conditionvalue");
        sb.AppendLine("  retryCountOnTaskFailure: 1");
        sb.AppendLine("  target: targetvalue");
        sb.AppendLine("  env:");
        sb.AppendLine("    env1: value1");
        sb.AppendLine("    env2: value2");
        sb.AppendLine("  enabled: True");

        var expected = sb.ToString();

        var step = new AdoCheckoutStep()
        {
            Checkout = "testcheckout",
            Clean = true,
            FetchDepth = 1,
            Path = "testpath",
            Submodules = "testsubmodules",
            FetchFilter = "testfetchfilter",
            Lfs = true,
            FetchTags = true,
            PersistCredentials = true,
            WorkspaceRepo = true
        };
        AppendBaseFields(step);
        var steps = new AdoSectionCollection<AdoStepBase>() { step };

        var doc = new AdoYamlDocument();
        doc.Builder = new AdoYamlBuilder();
        steps.AppendSection(doc.Builder, 0);
        Assert.That(doc.Builder.ToString(), Is.EqualTo(expected));
    }

    [Test]
    public void TestStepSerialization_checkout_bare()
    {
        var sb = new StringBuilder();
        sb.AppendLine("steps:");
        sb.AppendLine("- checkout: testcheckout");
        var expected = sb.ToString();

        var step = new AdoCheckoutStep()
        {
            Checkout = "testcheckout",
        };
        var steps = new AdoSectionCollection<AdoStepBase>() { step };

        var doc = new AdoYamlDocument();
        doc.Builder = new AdoYamlBuilder();
        steps.AppendSection(doc.Builder, 0);
        Assert.That(doc.Builder.ToString(), Is.EqualTo(expected));
    }

    [Test]
    public void TestStepSerialization_bash()
    {
        var sb = new StringBuilder();
        sb.AppendLine("steps:");
        sb.AppendLine("- bash: testbash");
        sb.AppendLine("  name: namevalue");
        sb.AppendLine("  displayName: displaynamevalue");
        sb.AppendLine("  timeoutInMinutes: 1");
        sb.AppendLine("  continueOnError: True");
        sb.AppendLine("  condition: conditionvalue");
        sb.AppendLine("  retryCountOnTaskFailure: 1");
        sb.AppendLine("  target: targetvalue");
        sb.AppendLine("  env:");
        sb.AppendLine("    env1: value1");
        sb.AppendLine("    env2: value2");
        sb.AppendLine("  enabled: True");
        sb.AppendLine("  failOnStderr: True");
        sb.AppendLine("  workingDirectory: workingdirectoryvalue");

        var expected = sb.ToString();

        var step = new AdoBashStep() { Bash = "testbash", FailOnStderr = true, WorkingDirectory = "workingdirectoryvalue" };
        AppendBaseFields(step);
        var steps = new AdoSectionCollection<AdoStepBase>() { step };

        var doc = new AdoYamlDocument();
        doc.Builder = new AdoYamlBuilder();
        steps.AppendSection(doc.Builder, 0);
        Assert.That(doc.Builder.ToString(), Is.EqualTo(expected));
    }

    [Test]
    public void TestStepSerialization_bash_bare()
    {
        var sb = new StringBuilder();
        sb.AppendLine("steps:");
        sb.AppendLine("- bash: testbash");
        var expected = sb.ToString();

        var step = new AdoBashStep() { Bash = "testbash" };
        var steps = new AdoSectionCollection<AdoStepBase>() { step };

        var doc = new AdoYamlDocument();
        doc.Builder = new AdoYamlBuilder();
        steps.AppendSection(doc.Builder, 0);
        Assert.That(doc.Builder.ToString(), Is.EqualTo(expected));
    }



    //Helpers
    static void AppendBaseFields(AdoStepBase step)
    {
        step.Name = "namevalue";
        step.TimeoutInMinutes = 1;
        step.DisplayName = "displaynamevalue";
        step.ContinueOnError = true;
        step.Condition = "conditionvalue";
        step.RetryCountOnTaskFailure = 1;
        step.Target = "targetvalue";
        step.Env = new Dictionary<string, string>() { { "env1", "value1" }, { "env2", "value2" }, };
        step.Enabled = true;
    }
}
