using System;
using System.Text;
using PipelineWeaver.Ado;
using PipelineWeaver.Core.Transpiler.Ado.Yaml;
using PipelineWeaver.Core.Transpiler.Yaml;
using static PipelineWeaver.Tests.Ado.Helpers;

namespace PipelineWeaver.Tests.Ado;

public class AdoVariableSerializerTests
{
    [SetUp]
    public void Setup()
    {
        if (File.Exists(PATH))
            File.Delete(PATH);
    }

    [Test]
    public void TestVariableSerialization_name()
    {
        var sb = new StringBuilder();
        sb.AppendLine("variables:");
        sb.AppendLine("- name: testName");
        sb.AppendLine("  value: testvalue");
        var expected = sb.ToString();

        var variables = new AdoSectionCollection<IAdoVariable>()
        {
            new AdoNameVariable{Name = "testName",Value = "testvalue"},
        };

        var doc = new AdoYamlDocument();
        doc.Builder = new AdoYamlBuilder();
        variables.AppendSection(doc.Builder, 0);
        Assert.That(doc.Builder.ToString(), Is.EqualTo(expected));
    }

    [Test]
    public void TestVariableSerialization_group()
    {
        var sb = new StringBuilder();
        sb.AppendLine("variables:");
        sb.AppendLine("- group: groupvalue");
        var expected = sb.ToString();

        var variables = new AdoSectionCollection<IAdoVariable>()
        {
            new AdoGroupVariable{Group = "groupvalue"},
        };

        var doc = new AdoYamlDocument();
        doc.Builder = new AdoYamlBuilder();
        variables.AppendSection(doc.Builder, 0);
        Assert.That(doc.Builder.ToString(), Is.EqualTo(expected));
    }


    [Test]
    public void TestVariableSerialization_template()
    {
        var sb = new StringBuilder();
        sb.AppendLine("variables:");
        sb.AppendLine("- template: templatevalue");
        var expected = sb.ToString();

        var variables = new AdoSectionCollection<IAdoVariable>()
        {
            new AdoTemplateVariable{Template = "templatevalue"}
        };

        var doc = new AdoYamlDocument();
        doc.Builder = new AdoYamlBuilder();
        variables.AppendSection(doc.Builder, 0);
        Assert.That(doc.Builder.ToString(), Is.EqualTo(expected));
    }

    [Test]
    public void TestVariableSerialization_all()
    {
        var sb = new StringBuilder();
        sb.AppendLine("variables:");
        sb.AppendLine("- name: testName");
        sb.AppendLine("  value: testvalue");
        sb.AppendLine("- group: groupvalue");
        sb.AppendLine("- template: templatevalue");
        var expected = sb.ToString();

        var variables = new AdoSectionCollection<IAdoVariable>()
        {
            new AdoNameVariable{Name = "testName",Value = "testvalue"},
            new AdoGroupVariable{Group = "groupvalue"},
            new AdoTemplateVariable{Template = "templatevalue"}
        };

        var doc = new AdoYamlDocument();
        doc.Builder = new AdoYamlBuilder();
        variables.AppendSection(doc.Builder, 0);
        Assert.That(doc.Builder.ToString(), Is.EqualTo(expected));
    }
}
