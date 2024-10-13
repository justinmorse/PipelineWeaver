using System;
using System.Text;
using PipelineWeaver.Ado;
using PipelineWeaver.Core.Transpiler.Ado.Yaml;
using PipelineWeaver.Core.Transpiler.Yaml;
using static PipelineWeaver.Tests.Ado.Helpers;

namespace PipelineWeaver.Tests.Ado;

public class AdoStepSerializerTests
{
    [SetUp]
    public void Setup()
    {
        if (File.Exists(PATH))
            File.Delete(PATH);
    }

    [Test]
    public void TestStepSerialization_template()
    {
        untested
        var sb = new StringBuilder();
        sb.AppendLine("variables:");
        sb.AppendLine("- template: templatevalue");
        var expected = sb.ToString();

        var steps = new AdoSectionCollection<AdoStepBase>()
        {
            new AdoTemplateVariable{Template = "templatevalue"}
        };

        var doc = new AdoYamlDocument();
        doc.Builder = new AdoYamlBuilder();
        variables.AppendSection(doc.Builder, 0);
        Assert.That(doc.Builder.ToString(), Is.EqualTo(expected));
    }
}
