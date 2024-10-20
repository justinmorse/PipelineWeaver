using System.Text;
using NUnit.Framework.Internal;
using PipelineWeaver.Ado;
using PipelineWeaver.Core.Transpiler.Ado.Yaml;
using PipelineWeaver.Core.Transpiler.Yaml;
namespace PipelineWeaver.Tests.Ado;

public class AdoResourceSerializerTests
{

    [Test]
    public void AdoResourceSerializerTests_pipeline()
    {
        var sb = new StringBuilder();
        sb.AppendLine("resources:");
        sb.AppendLine("  pipelines:");
        sb.AppendLine("  - pipeline: Pipeline");
        sb.AppendLine("    connection: Connection");
        sb.AppendLine("    project: Project");
        sb.AppendLine("    source: Source");
        sb.AppendLine("    version: Version");
        sb.AppendLine("    branch: Branch");
        sb.AppendLine("    tags: Tag1 Tag2");
        var expected = sb.ToString();

        var resource = new AdoPipelineResource()
        {
            Pipeline = "Pipeline",
            Connection = "Connection",
            Version = "Version",
            Project = "Project",
            Source = "Source",
            Branch = "Branch",
            Tags = ["Tag1", "Tag2"],
            Trigger = true,
            Triggers = [
                    new AdoTrigger(){TriggerType = AdoTriggerType.PathInclude, Value = "Value"}
                ],
            StageTriggers = new List<string>() { "StageTriggers1", "StageTriggers2" },
        };

        var resources = new AdoSectionCollection<IAdoResource>(){
            resource
        };

        var doc = new AdoYamlDocument();
        doc.Builder = new AdoYamlBuilder();
        resources.AppendSection(doc.Builder, 0);
        Assert.That(doc.Builder.ToString(), Is.EqualTo(expected));
    }

    [Test]
    public void AdoResourceSerializerTests_build()
    {
        var sb = new StringBuilder();
        sb.AppendLine("resources:");
        sb.AppendLine("  builds:");
        sb.AppendLine("  - build: Build");
        sb.AppendLine("    project: Type");
        sb.AppendLine("    connection: Connection");
        sb.AppendLine("    source: Source");
        sb.AppendLine("    trigger: True");
        var expected = sb.ToString();

        var resource = new AdoBuildResource()
        {
            Build = "Build",
            Connection = "Connection",
            Type = "Type",
            Source = "Source",
            Trigger = true
        };

        var resources = new AdoSectionCollection<IAdoResource>(){
            resource
        };

        var doc = new AdoYamlDocument();
        doc.Builder = new AdoYamlBuilder();
        resources.AppendSection(doc.Builder, 0);
        Assert.That(doc.Builder.ToString(), Is.EqualTo(expected));
    }

    [Test]
    public void AdoResourceSerializerTests_repository()
    {
        var sb = new StringBuilder();
        sb.AppendLine("resources:");
        sb.AppendLine("  repositories:");
        sb.AppendLine("  - repository: Repository");
        sb.AppendLine("    project: Project");
        sb.AppendLine("    type: Type");
        sb.AppendLine("    connection: Connection");
        sb.AppendLine("    source: Source");

        var expected = sb.ToString();

        var resource = new AdoRepositoryResource()
        {
            Repository = "Repository",
            Ref = "Ref",
            Name = "Name",
            Endpoint = "Endpoint",
            Connection = "Connection",
            Type = "Type",
            Source = "Source",
            Project = "Project"
        };

        var resources = new AdoSectionCollection<IAdoResource>(){
            resource
        };

        var doc = new AdoYamlDocument();
        doc.Builder = new AdoYamlBuilder();
        resources.AppendSection(doc.Builder, 0);
        Assert.That(doc.Builder.ToString(), Is.EqualTo(expected));
    }

    [Test]
    public void AdoResourceSerializerTests_package()
    {
        var sb = new StringBuilder();
        sb.AppendLine("resources:");
        sb.AppendLine("  packages:");
        sb.AppendLine("  - package: Package");
        sb.AppendLine("    type: Type");
        sb.AppendLine("    connection: Connection");
        sb.AppendLine("    version: Version");
        var expected = sb.ToString();

        var resource = new AdoPackageResource()
        {
            Package = "Package",
            Version = "Version",
            Tag = "Tag",
            Endpoint = "Endpoint",
            Connection = "Connection",
            Type = "Type",
        };

        var resources = new AdoSectionCollection<IAdoResource>(){
            resource
        };

        var doc = new AdoYamlDocument();
        doc.Builder = new AdoYamlBuilder();
        resources.AppendSection(doc.Builder, 0);
        Assert.That(doc.Builder.ToString(), Is.EqualTo(expected));
    }

    [Test]
    public void AdoResourceSerializerTests_container()
    {
        var sb = new StringBuilder();
        sb.AppendLine("resources:");
        sb.AppendLine("  containers:");
        sb.AppendLine("  - container: Container");
        sb.AppendLine("    connection: Connection");
        sb.AppendLine("    image: Image");
        sb.AppendLine("    options: Options");
        sb.AppendLine("    env: Env");
        sb.AppendLine("    ports: Ports");
        sb.AppendLine("    volumes: Volumes");
        var expected = sb.ToString();

        var resource = new AdoContainerResource()
        {
            Container = "Container",
            Image = "Image",
            Env = "Env",
            Ports = "Ports",
            Connection = "Connection",
            Volumes = "Volumes",
            Options = "Options"
        };

        var resources = new AdoSectionCollection<IAdoResource>(){
            resource
        };

        var doc = new AdoYamlDocument();
        doc.Builder = new AdoYamlBuilder();
        resources.AppendSection(doc.Builder, 0);
        Assert.That(doc.Builder.ToString(), Is.EqualTo(expected));
    }

    [Test]
    public void AdoResourceSerializerTests_webhooks()
    {
        var sb = new StringBuilder();
        sb.AppendLine("resources:");
        sb.AppendLine("  webhooks:");
        sb.AppendLine("  - webhook: Webhook");
        sb.AppendLine("    connection: Connection");
        sb.AppendLine("    type: Type");
        sb.AppendLine("    filters:");
        sb.AppendLine("    - path: Path");
        sb.AppendLine("      value: Value");
        var expected = sb.ToString();

        var resource = new AdoWebhookResource()
        {
            Webhook = new AdoWebhook()
            {
                Webhook = "Webhook",
                Type = "Type",
                Connection = "Connection",
                Filters =
                [
                    new()
                    {
                        Path = "Path",
                        Value = "Value"
                    }
                ]
            }
        };

        var resources = new AdoSectionCollection<IAdoResource>(){
            resource
        };

        var doc = new AdoYamlDocument();
        doc.Builder = new AdoYamlBuilder();
        resources.AppendSection(doc.Builder, 0);
        Assert.That(doc.Builder.ToString(), Is.EqualTo(expected));
    }
}
