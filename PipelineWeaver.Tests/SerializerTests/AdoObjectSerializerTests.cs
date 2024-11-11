using System.Text;
using PipelineWeaver.Ado;
using PipelineWeaver.Core.Transpiler.Ado.Yaml;
using PipelineWeaver.Core.Transpiler.Ado.Yaml.SectionSerializers;
using PipelineWeaver.Core.Transpiler.Yaml;

namespace PipelineWeaver.Tests.Ado;

public class AdoObjectSerializerTests
{
    [Test]
    public void TestObjectSerialization_dynamic()
    {
        var sb = new StringBuilder();
        sb.AppendLine("Field1: Value1");
        sb.AppendLine("Field2: Value2");
        sb.AppendLine("Field3:");
        sb.AppendLine("  Field3_1: Value3_1");
        sb.AppendLine("  Field3_2: True");
        sb.AppendLine("  Field3_3:");
        sb.AppendLine("    Field3_3_1: Value3_3_1");
        sb.AppendLine("    Field3_3_2: True");
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

        var obj = new AdoObject<dynamic>(dynamicObj);

        var serialized = new AdoObjectSerializer().Serialize(obj, 0);
        var doc = new AdoYamlDocument();
        doc.Builder = new AdoYamlBuilder();
        doc.Builder.AppendLine(0, serialized, true, false);
        Assert.That(doc.Builder.ToString(), Is.EqualTo(expected));
    }

    [Test]
    public void TestObjectSerialization_concrete()
    {
        var sb = new StringBuilder();
        sb.AppendLine("Field1: Value1");
        sb.AppendLine("Field2: Value2");
        sb.AppendLine("Field3: True");
        var expected = sb.ToString();

        var obj = new AdoObject<Helpers.AdoTestObject>(new Helpers.AdoTestObject() { Field1 = "Value1", Field2 = "Value2", Field3 = true });

        var serialized = new AdoObjectSerializer().Serialize(obj, 0);
        var doc = new AdoYamlDocument();
        doc.Builder = new AdoYamlBuilder();
        doc.Builder.AppendLine(0, serialized, true, false);
        Assert.That(doc.Builder.ToString(), Is.EqualTo(expected));
    }
}