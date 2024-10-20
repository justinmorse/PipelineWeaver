using System.Reflection.Metadata.Ecma335;
using System.Text;
using PipelineWeaver.Ado;
using PipelineWeaver.Core.Transpiler.Ado.Yaml;
using PipelineWeaver.Core.Transpiler.Yaml;

namespace PipelineWeaver.Tests.Ado;

public class AdoParameterSerializerTests
{
    [Test]
    public void TestParameterSerialization_bool()
    {
        var sb = new StringBuilder();
        sb.AppendLine("parameters:");
        sb.AppendLine("  boolParamName: True");
        sb.AppendLine();
        var expected = sb.ToString();

        var adoJobParameters = new AdoSectionCollection<AdoParameterBase>()
        {
            new AdoBoolParameter(){Name = "boolParamName", Value = true},
        };

        var doc = new AdoYamlDocument();
        doc.Builder = new AdoYamlBuilder();
        adoJobParameters.AppendSection(doc.Builder, 0);

        Assert.That(doc.Builder.ToString(), Is.EqualTo(expected));

    }


    [Test]
    public void TestParameterSerialization_string()
    {
        var sb = new StringBuilder();
        sb.AppendLine("parameters:");
        sb.AppendLine("  stringParamName: Value");
        sb.AppendLine();
        var expected = sb.ToString();

        var adoJobParameters = new AdoSectionCollection<AdoParameterBase>()
        {
            new AdoStringParameter(){Name = "stringParamName", Value = "Value"},
        };

        var doc = new AdoYamlDocument();
        doc.Builder = new AdoYamlBuilder();
        adoJobParameters.AppendSection(doc.Builder, 0);
        Assert.That(doc.Builder.ToString(), Is.EqualTo(expected));

    }


    [Test]
    public void TestParameterSerialization_strArray()
    {
        var sb = new StringBuilder();
        sb.AppendLine("parameters:");
        sb.AppendLine("  arrayStrParamName:");
        sb.AppendLine("  - one");
        sb.AppendLine("  - two");
        sb.AppendLine();
        var expected = sb.ToString();

        var adoJobParameters = new AdoSectionCollection<AdoParameterBase>()
        {
            new AdoArrayParameter<string>(){Name = "arrayStrParamName", Value = new List<string>(){"one","two"}.ToArray()},
        };

        var doc = new AdoYamlDocument();
        doc.Builder = new AdoYamlBuilder();
        adoJobParameters.AppendSection(doc.Builder, 0);
        Assert.That(doc.Builder.ToString(), Is.EqualTo(expected));
    }


    [Test]
    public void TestParameterSerialization_boolArr()
    {
        var sb = new StringBuilder();
        sb.AppendLine("parameters:");
        sb.AppendLine("  arrayBoolParamName:");
        sb.AppendLine("  - True");
        sb.AppendLine("  - True");
        sb.AppendLine();
        var expected = sb.ToString();

        var adoJobParameters = new AdoSectionCollection<AdoParameterBase>()
        {
            new AdoArrayParameter<bool>(){Name = "arrayBoolParamName", Value = new List<bool>() { true, true }.ToArray() },
        };

        var doc = new AdoYamlDocument();
        doc.Builder = new AdoYamlBuilder();
        adoJobParameters.AppendSection(doc.Builder, 0);
        Assert.That(doc.Builder.ToString(), Is.EqualTo(expected));

    }


    [Test]
    public void TestParameterSerialization_intArr()
    {
        var sb = new StringBuilder();
        sb.AppendLine("parameters:");
        sb.AppendLine("  arrayIntParamName:");
        sb.AppendLine("  - 100");
        sb.AppendLine("  - 101");
        sb.AppendLine();
        var expected = sb.ToString();

        var adoJobParameters = new AdoSectionCollection<AdoParameterBase>()
        {
            new AdoArrayParameter<int>(){Name = "arrayIntParamName", Value = new List<int>() { 100, 101 }.ToArray() },
        };

        var doc = new AdoYamlDocument();
        doc.Builder = new AdoYamlBuilder();
        adoJobParameters.AppendSection(doc.Builder, 0);
        Assert.That(doc.Builder.ToString(), Is.EqualTo(expected));
    }


    [Test]
    public void TestParameterSerialization_strDict()
    {
        var sb = new StringBuilder();
        sb.AppendLine("parameters:");
        sb.AppendLine("  dictStrParamName:");
        sb.AppendLine("    key1: value1");
        sb.AppendLine("    key2: value2");
        sb.AppendLine();
        var expected = sb.ToString();

        var adoJobParameters = new AdoSectionCollection<AdoParameterBase>()
        {
            new AdoDictionaryParameter<string>(){Name = "dictStrParamName", Value = new Dictionary<string, string>(){{"key1","value1"},{"key2","value2"}}},
        };

        var doc = new AdoYamlDocument();
        doc.Builder = new AdoYamlBuilder();
        adoJobParameters.AppendSection(doc.Builder, 0);
        Assert.That(doc.Builder.ToString(), Is.EqualTo(expected));
    }

    [Test]
    public void TestParameterSerialization_boolDict()
    {
        var sb = new StringBuilder();
        sb.AppendLine("parameters:");
        sb.AppendLine("  dictBoolParamName:");
        sb.AppendLine("    key1: True");
        sb.AppendLine("    key2: True");
        sb.AppendLine();
        var expected = sb.ToString();

        var adoJobParameters = new AdoSectionCollection<AdoParameterBase>()
        {
            new AdoDictionaryParameter<bool>(){Name = "dictBoolParamName", Value = new Dictionary<string, bool>(){{"key1",true},{"key2",true}} },
        };

        var doc = new AdoYamlDocument();
        doc.Builder = new AdoYamlBuilder();
        adoJobParameters.AppendSection(doc.Builder, 0);
        Assert.That(doc.Builder.ToString(), Is.EqualTo(expected));
    }

    [Test]
    public void TestParameterSerialization_Obj()
    {
        var sb = new StringBuilder();
        sb.AppendLine("parameters:");
        sb.AppendLine("  adoTestObjectParamName:");
        sb.AppendLine("    Field1: Value1");
        sb.AppendLine("    Field2: Value2");
        sb.AppendLine("    Field3: False");
        sb.AppendLine();
        var expected = sb.ToString();

        var adoJobParameters = new AdoSectionCollection<AdoParameterBase>()
        {
            new AdoObjectParameter<Helpers.AdoTestObject>(new Helpers.AdoTestObject()){Name = "adoTestObjectParamName"},
        };

        var doc = new AdoYamlDocument();
        doc.Builder = new AdoYamlBuilder();
        adoJobParameters.AppendSection(doc.Builder, 0);
        Assert.That(doc.Builder.ToString(), Is.EqualTo(expected));
    }

    [Test]
    public void TestParameterSerialization_objDict()
    {
        var sb = new StringBuilder();
        sb.AppendLine("parameters:");
        sb.AppendLine("  dictObjParamName:");
        sb.AppendLine("    key1:");
        sb.AppendLine("      Field1: Value1");
        sb.AppendLine("      Field2: Value2");
        sb.AppendLine("      Field3:");
        sb.AppendLine("        Field3_1: Value3_1");
        sb.AppendLine("        Field3_2: True");
        sb.AppendLine("        Field3_3:");
        sb.AppendLine("          Field3_3_1: Value3_3_1");
        sb.AppendLine("          Field3_3_2: True");
        sb.AppendLine("    key2:");
        sb.AppendLine("      Field1: Value1");
        sb.AppendLine("      Field2: Value2");
        sb.AppendLine("      Field3:");
        sb.AppendLine("        Field3_1: Value3_1");
        sb.AppendLine("        Field3_2: True");
        sb.AppendLine("        Field3_3:");
        sb.AppendLine("          Field3_3_1: Value3_3_1");
        sb.AppendLine("          Field3_3_2: True");
        sb.AppendLine();
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

        var adoJobParameters = new AdoSectionCollection<AdoParameterBase>()
        {
            new AdoDictionaryParameter<object>(){Name = "dictObjParamName", Value = new Dictionary<string, object>(){{"key1",dynamicObj},{"key2",dynamicObj}}},
        };

        var doc = new AdoYamlDocument();
        doc.Builder = new AdoYamlBuilder();
        adoJobParameters.AppendSection(doc.Builder, 0);
        Assert.That(doc.Builder.ToString(), Is.EqualTo(expected));
    }

    [Test]
    public void TestParameterSerialization_dynamicObj()
    {
        var sb = new StringBuilder();
        sb.AppendLine("parameters:");
        sb.AppendLine("  adoDynObjParamName:");
        sb.AppendLine("    Field1: Value1");
        sb.AppendLine("    Field2: Value2");
        sb.AppendLine("    Field3:");
        sb.AppendLine("      Field3_1: Value3_1");
        sb.AppendLine("      Field3_2: True");
        sb.AppendLine("      Field3_3:");
        sb.AppendLine("        Field3_3_1: Value3_3_1");
        sb.AppendLine("        Field3_3_2: True");
        sb.AppendLine();
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

        var adoJobParameters = new AdoSectionCollection<AdoParameterBase>()
        {
            new AdoObjectParameter<dynamic>(dynamicObj){Name = "adoDynObjParamName"}
        };

        var doc = new AdoYamlDocument();
        doc.Builder = new AdoYamlBuilder();
        adoJobParameters.AppendSection(doc.Builder, 0);
        Assert.That(doc.Builder.ToString(), Is.EqualTo(expected));
    }

    [Test]
    public void TestParameterSerialization_all()
    {
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

        var expected = GetAllExpected();

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

        var doc = new AdoYamlDocument();
        doc.Builder = new AdoYamlBuilder();
        adoJobParameters.AppendSection(doc.Builder, 0);
        Assert.That(doc.Builder.ToString(), Is.EqualTo(expected));
    }

    public static string GetAllExpected()
    {
        var sb = new StringBuilder();
        sb.AppendLine("parameters:");
        sb.AppendLine("  boolParamName: True");
        sb.AppendLine("  stringParamName: Value");
        sb.AppendLine("  arrayStrParamName:");
        sb.AppendLine("  - one");
        sb.AppendLine("  - two");
        sb.AppendLine("  arrayBoolParamName:");
        sb.AppendLine("  - True");
        sb.AppendLine("  - True");
        sb.AppendLine("  arrayIntParamName:");
        sb.AppendLine("  - 100");
        sb.AppendLine("  - 101");
        sb.AppendLine("  dictStrParamName:");
        sb.AppendLine("    key1: value1");
        sb.AppendLine("    key2: value2");
        sb.AppendLine("  dictBoolParamName:");
        sb.AppendLine("    key1: True");
        sb.AppendLine("    key2: True");
        sb.AppendLine("  dictObjParamName:");
        sb.AppendLine("    key1:");
        sb.AppendLine("      Field1: Value1");
        sb.AppendLine("      Field2: Value2");
        sb.AppendLine("      Field3:");
        sb.AppendLine("        Field3_1: Value3_1");
        sb.AppendLine("        Field3_2: True");
        sb.AppendLine("        Field3_3:");
        sb.AppendLine("          Field3_3_1: Value3_3_1");
        sb.AppendLine("          Field3_3_2: True");
        sb.AppendLine("    key2:");
        sb.AppendLine("      Field1: Value1");
        sb.AppendLine("      Field2: Value2");
        sb.AppendLine("      Field3:");
        sb.AppendLine("        Field3_1: Value3_1");
        sb.AppendLine("        Field3_2: True");
        sb.AppendLine("        Field3_3:");
        sb.AppendLine("          Field3_3_1: Value3_3_1");
        sb.AppendLine("          Field3_3_2: True");
        sb.AppendLine("  adoTestObjectParamName:");
        sb.AppendLine("    Field1: Value1");
        sb.AppendLine("    Field2: Value2");
        sb.AppendLine("    Field3: False");
        sb.AppendLine("  adoDynObjParamName:");
        sb.AppendLine("    Field1: Value1");
        sb.AppendLine("    Field2: Value2");
        sb.AppendLine("    Field3:");
        sb.AppendLine("      Field3_1: Value3_1");
        sb.AppendLine("      Field3_2: True");
        sb.AppendLine("      Field3_3:");
        sb.AppendLine("        Field3_3_1: Value3_3_1");
        sb.AppendLine("        Field3_3_2: True");
        sb.AppendLine();
        return sb.ToString();
    }
}