using System;
using System.Text;
using PipelineWeaver.Ado;
using PipelineWeaver.Core.Transpiler.Ado.Yaml;
using PipelineWeaver.Core.Transpiler.Yaml;

namespace PipelineWeaver.Tests.Ado;

public class AdoPoolSerializerTests
{

    [Test]
    public void TestPoolSerialization_Named()
    {
        var sb = new StringBuilder();
        sb.AppendLine("pools:");
        sb.AppendLine("  name: testPoolName");
        sb.AppendLine("  demands:");
        sb.AppendLine("  - testDemand");
        var expected = sb.ToString();

        var adoPoolSection = new AdoSectionCollection<IAdoPool>()
        {
            new AdoNamedPool(){Name = "testPoolName", Demands = new List<string>(){"testDemand"} },
        };

        var doc = new AdoYamlDocument();
        doc.Builder = new AdoYamlBuilder();
        adoPoolSection.AppendSection(doc.Builder, 0);
        Assert.That(doc.Builder.ToString(), Is.EqualTo(expected));
    }

    [Test]
    public void TestPoolSerialization_Hosted()
    {
        var sb = new StringBuilder();
        sb.AppendLine("pools:");
        sb.AppendLine("  vmImage: testPoolName");
        var expected = sb.ToString();

        var adoPoolSection = new AdoSectionCollection<IAdoPool>()
        {
            new AdoHostedPool(){VmImage = "testPoolName"},
        };

        var doc = new AdoYamlDocument();
        doc.Builder = new AdoYamlBuilder();
        adoPoolSection.AppendSection(doc.Builder, 0);
        Assert.That(doc.Builder.ToString(), Is.EqualTo(expected));
    }
}
