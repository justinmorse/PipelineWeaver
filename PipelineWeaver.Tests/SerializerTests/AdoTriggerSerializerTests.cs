using System;
using System.Text;
using PipelineWeaver.Ado;
using PipelineWeaver.Core.Transpiler.Ado.Yaml;
using PipelineWeaver.Core.Transpiler.Yaml;

namespace PipelineWeaver.Tests.Ado;

public class AdoTriggerSerializerTests
{
    [Test]
    public void TestTriggerSerialization_taginclude()
    {
        var sb = new StringBuilder();
        sb.AppendLine("triggers:");
        sb.AppendLine("  batch: true");
        sb.AppendLine("  tags:");
        sb.AppendLine("    include:");
        sb.AppendLine("    - testTriggerValue");
        var expected = sb.ToString();

        var adoTriggerSection = new AdoTriggerContainer()
        {
            new AdoTrigger(){TriggerType = AdoTriggerType.TagInclude, Value = "testTriggerValue"},
        };
        adoTriggerSection.Batch = true;

        var doc = new AdoYamlDocument();
        doc.Builder = new AdoYamlBuilder();
        adoTriggerSection.AppendSection(doc.Builder, 0);
        Assert.That(doc.Builder.ToString(), Is.EqualTo(expected));
    }

    [Test]
    public void TestTriggerSerialization_tagexclude()
    {
        var sb = new StringBuilder();
        sb.AppendLine("triggers:");
        sb.AppendLine("  batch: true");
        sb.AppendLine("  tags:");
        sb.AppendLine("    exclude:");
        sb.AppendLine("    - testTriggerValue");
        var expected = sb.ToString();

        var adoTriggerSection = new AdoTriggerContainer()
        {
            new AdoTrigger(){TriggerType = AdoTriggerType.TagExclude, Value = "testTriggerValue"},
        };
        adoTriggerSection.Batch = true;

        var doc = new AdoYamlDocument();
        doc.Builder = new AdoYamlBuilder();
        adoTriggerSection.AppendSection(doc.Builder, 0);
        Assert.That(doc.Builder.ToString(), Is.EqualTo(expected));
    }

    [Test]
    public void TestTriggerSerialization_pathinclude()
    {
        var sb = new StringBuilder();
        sb.AppendLine("triggers:");
        sb.AppendLine("  batch: true");
        sb.AppendLine("  paths:");
        sb.AppendLine("    include:");
        sb.AppendLine("    - testTriggerValue");
        var expected = sb.ToString();

        var adoTriggerSection = new AdoTriggerContainer()
        {
            new AdoTrigger(){TriggerType = AdoTriggerType.PathInclude, Value = "testTriggerValue"},
        };
        adoTriggerSection.Batch = true;

        var doc = new AdoYamlDocument();
        doc.Builder = new AdoYamlBuilder();
        adoTriggerSection.AppendSection(doc.Builder, 0);
        Assert.That(doc.Builder.ToString(), Is.EqualTo(expected));
    }

    [Test]
    public void TestTriggerSerialization_pathexclude()
    {
        var sb = new StringBuilder();
        sb.AppendLine("triggers:");
        sb.AppendLine("  batch: true");
        sb.AppendLine("  paths:");
        sb.AppendLine("    exclude:");
        sb.AppendLine("    - testTriggerValue");
        var expected = sb.ToString();

        var adoTriggerSection = new AdoTriggerContainer()
        {
            new AdoTrigger(){TriggerType = AdoTriggerType.PathExclude, Value = "testTriggerValue"},
        };
        adoTriggerSection.Batch = true;

        var doc = new AdoYamlDocument();
        doc.Builder = new AdoYamlBuilder();
        adoTriggerSection.AppendSection(doc.Builder, 0);
        Assert.That(doc.Builder.ToString(), Is.EqualTo(expected));
    }

    [Test]
    public void TestTriggerSerialization_branchinclude()
    {
        var sb = new StringBuilder();
        sb.AppendLine("triggers:");
        sb.AppendLine("  batch: true");
        sb.AppendLine("  branches:");
        sb.AppendLine("    include:");
        sb.AppendLine("    - testTriggerValue");
        var expected = sb.ToString();

        var adoTriggerSection = new AdoTriggerContainer()
        {
            new AdoTrigger(){TriggerType = AdoTriggerType.BranchInclude, Value = "testTriggerValue"},
        };
        adoTriggerSection.Batch = true;

        var doc = new AdoYamlDocument();
        doc.Builder = new AdoYamlBuilder();
        adoTriggerSection.AppendSection(doc.Builder, 0);
        Assert.That(doc.Builder.ToString(), Is.EqualTo(expected));
    }

    [Test]
    public void TestTriggerSerialization_branchexclude()
    {
        var sb = new StringBuilder();
        sb.AppendLine("triggers:");
        sb.AppendLine("  batch: true");
        sb.AppendLine("  branches:");
        sb.AppendLine("    exclude:");
        sb.AppendLine("    - testTriggerValue");
        var expected = sb.ToString();

        var adoTriggerSection = new AdoTriggerContainer()
        {
            new AdoTrigger(){TriggerType = AdoTriggerType.BranchExclude, Value = "testTriggerValue"},
        };
        adoTriggerSection.Batch = true;

        var doc = new AdoYamlDocument();
        doc.Builder = new AdoYamlBuilder();
        adoTriggerSection.AppendSection(doc.Builder, 0);
        Assert.That(doc.Builder.ToString(), Is.EqualTo(expected));
    }

    [Test]
    public void TestTriggerSerialization_all()
    {
        var sb = new StringBuilder();
        sb.AppendLine("triggers:");
        sb.AppendLine("  batch: true");
        sb.AppendLine("  branches:");
        sb.AppendLine("    include:");
        sb.AppendLine("    - testTriggerValue");
        sb.AppendLine("    exclude:");
        sb.AppendLine("    - testTriggerValue");
        sb.AppendLine("  paths:");
        sb.AppendLine("    include:");
        sb.AppendLine("    - testTriggerValue");
        sb.AppendLine("    exclude:");
        sb.AppendLine("    - testTriggerValue");
        sb.AppendLine("  tags:");
        sb.AppendLine("    include:");
        sb.AppendLine("    - testTriggerValue");
        sb.AppendLine("    exclude:");
        sb.AppendLine("    - testTriggerValue");

        var expected = sb.ToString();

        var adoTriggerSection = new AdoTriggerContainer()
        {
            new AdoTrigger(){TriggerType = AdoTriggerType.BranchExclude, Value = "testTriggerValue"},
            new AdoTrigger(){TriggerType = AdoTriggerType.BranchInclude, Value = "testTriggerValue"},
            new AdoTrigger(){TriggerType = AdoTriggerType.PathExclude, Value = "testTriggerValue"},
            new AdoTrigger(){TriggerType = AdoTriggerType.PathInclude, Value = "testTriggerValue"},
            new AdoTrigger(){TriggerType = AdoTriggerType.TagExclude, Value = "testTriggerValue"},
            new AdoTrigger(){TriggerType = AdoTriggerType.TagInclude, Value = "testTriggerValue"},
        };
        adoTriggerSection.Batch = true;

        var doc = new AdoYamlDocument();
        doc.Builder = new AdoYamlBuilder();
        adoTriggerSection.AppendSection(doc.Builder, 0);
        Assert.That(doc.Builder.ToString(), Is.EqualTo(expected));
    }
}
