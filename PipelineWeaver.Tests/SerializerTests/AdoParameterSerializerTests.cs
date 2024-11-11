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
            new AdoBoolParameter(){Name = "boolParamName", ValueOrDefault = true, ParameterType = AdoParameterType.Standard},
        };

        var doc = new AdoYamlDocument();
        doc.Builder = new AdoYamlBuilder();
        adoJobParameters.AppendSection(doc.Builder, 0);

        Assert.That(doc.Builder.ToString(), Is.EqualTo(expected));

    }

    [Test]
    public void TestParameterSerialization_bool_template()
    {
        var sb = new StringBuilder();
        sb.AppendLine("parameters:");
        sb.AppendLine("  - name: boolParamName");
        sb.AppendLine("    type: boolean");
        sb.AppendLine("    default: True");
        sb.AppendLine();
        var expected = sb.ToString();

        var adoJobParameters = new AdoSectionCollection<AdoParameterBase>()
        {
            new AdoBoolParameter(){Name = "boolParamName", ValueOrDefault = true, ParameterType = AdoParameterType.Template},
        };

        var doc = new AdoYamlDocument();
        doc.Builder = new AdoYamlBuilder();
        adoJobParameters.AppendSection(doc.Builder, 0);
        doc.Save(Helpers.PATH);
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
            new AdoStringParameter(){Name = "stringParamName", ValueOrDefault = "Value", ParameterType = AdoParameterType.Standard},
        };

        var doc = new AdoYamlDocument();
        doc.Builder = new AdoYamlBuilder();
        adoJobParameters.AppendSection(doc.Builder, 0);
        Assert.That(doc.Builder.ToString(), Is.EqualTo(expected));

    }

    [Test]
    public void TestParameterSerialization_string_template()
    {
        var sb = new StringBuilder();
        sb.AppendLine("parameters:");
        sb.AppendLine("  - name: stringParamName");
        sb.AppendLine("    type: string");
        sb.AppendLine("    default: Value");
        sb.AppendLine();
        var expected = sb.ToString();

        var adoJobParameters = new AdoSectionCollection<AdoParameterBase>()
        {
            new AdoStringParameter(){Name = "stringParamName", ValueOrDefault = "Value", ParameterType = AdoParameterType.Template},
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
            new AdoArrayParameter<string>(){Name = "arrayStrParamName", ValueOrDefault = new List<string>(){"one","two"}.ToArray(), ParameterType = AdoParameterType.Standard},
        };

        var doc = new AdoYamlDocument();
        doc.Builder = new AdoYamlBuilder();
        adoJobParameters.AppendSection(doc.Builder, 0);
        Assert.That(doc.Builder.ToString(), Is.EqualTo(expected));
    }

    [Test]
    public void TestParameterSerialization_strArray_template()
    {
        var sb = new StringBuilder();
        sb.AppendLine("parameters:");
        sb.AppendLine("  - name: arrayStrParamName");
        sb.AppendLine("    type: object");
        sb.AppendLine("    default:");
        sb.AppendLine("    - one");
        sb.AppendLine("    - two");
        sb.AppendLine();
        var expected = sb.ToString();

        var adoJobParameters = new AdoSectionCollection<AdoParameterBase>()
        {
            new AdoArrayParameter<string>(){Name = "arrayStrParamName", ValueOrDefault = new List<string>(){"one","two"}.ToArray(), ParameterType = AdoParameterType.Template},
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
            new AdoArrayParameter<bool>(){Name = "arrayBoolParamName", ValueOrDefault = new List<bool>() { true, true }.ToArray() , ParameterType = AdoParameterType.Standard},
        };

        var doc = new AdoYamlDocument();
        doc.Builder = new AdoYamlBuilder();
        adoJobParameters.AppendSection(doc.Builder, 0);
        Assert.That(doc.Builder.ToString(), Is.EqualTo(expected));

    }

    [Test]
    public void TestParameterSerialization_boolArr_template()
    {
        var sb = new StringBuilder();
        sb.AppendLine("parameters:");
        sb.AppendLine("  - name: arrayBoolParamName");
        sb.AppendLine("    type: object");
        sb.AppendLine("    default:");
        sb.AppendLine("    - True");
        sb.AppendLine("    - True");
        sb.AppendLine();
        var expected = sb.ToString();

        var adoJobParameters = new AdoSectionCollection<AdoParameterBase>()
        {
            new AdoArrayParameter<bool>(){Name = "arrayBoolParamName", ValueOrDefault = new List<bool>() { true, true }.ToArray() , ParameterType = AdoParameterType.Template},
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
            new AdoArrayParameter<int>(){Name = "arrayIntParamName", ValueOrDefault = new List<int>() { 100, 101 }.ToArray() , ParameterType = AdoParameterType.Standard},
        };

        var doc = new AdoYamlDocument();
        doc.Builder = new AdoYamlBuilder();
        adoJobParameters.AppendSection(doc.Builder, 0);
        Assert.That(doc.Builder.ToString(), Is.EqualTo(expected));
    }

    [Test]
    public void TestParameterSerialization_intArr_template()
    {
        var sb = new StringBuilder();
        sb.AppendLine("parameters:");
        sb.AppendLine("  - name: arrayIntParamName");
        sb.AppendLine("    type: object");
        sb.AppendLine("    default:");
        sb.AppendLine("    - 100");
        sb.AppendLine("    - 101");
        sb.AppendLine();
        var expected = sb.ToString();

        var adoJobParameters = new AdoSectionCollection<AdoParameterBase>()
        {
            new AdoArrayParameter<int>(){Name = "arrayIntParamName", ValueOrDefault = new List<int>() { 100, 101 }.ToArray() , ParameterType = AdoParameterType.Template},
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
            new AdoDictionaryParameter<string>(){Name = "dictStrParamName", ValueOrDefault = new Dictionary<string, string>(){{"key1","value1"},{"key2","value2"}}, ParameterType = AdoParameterType.Standard},
        };

        var doc = new AdoYamlDocument();
        doc.Builder = new AdoYamlBuilder();
        adoJobParameters.AppendSection(doc.Builder, 0);
        Assert.That(doc.Builder.ToString(), Is.EqualTo(expected));
    }

    [Test]
    public void TestParameterSerialization_strDict_template()
    {
        var sb = new StringBuilder();
        sb.AppendLine("parameters:");
        sb.AppendLine("  - name: dictStrParamName");
        sb.AppendLine("    type: object");
        sb.AppendLine("    default:");
        sb.AppendLine("      key1: value1");
        sb.AppendLine("      key2: value2");
        sb.AppendLine();
        var expected = sb.ToString();

        var adoJobParameters = new AdoSectionCollection<AdoParameterBase>()
        {
            new AdoDictionaryParameter<string>(){Name = "dictStrParamName", ValueOrDefault = new Dictionary<string, string>(){{"key1","value1"},{"key2","value2"}}, ParameterType = AdoParameterType.Template},
        };

        var doc = new AdoYamlDocument();
        doc.Builder = new AdoYamlBuilder();
        adoJobParameters.AppendSection(doc.Builder, 0);
        doc.Save(Helpers.PATH);
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
            new AdoDictionaryParameter<bool>(){Name = "dictBoolParamName", ValueOrDefault = new Dictionary<string, bool>(){{"key1",true},{"key2",true}}, ParameterType = AdoParameterType.Standard},
        };

        var doc = new AdoYamlDocument();
        doc.Builder = new AdoYamlBuilder();
        adoJobParameters.AppendSection(doc.Builder, 0);
        doc.Save(Helpers.PATH);
        Assert.That(doc.Builder.ToString(), Is.EqualTo(expected));
    }

    [Test]
    public void TestParameterSerialization_boolDict_template()
    {
        var sb = new StringBuilder();
        sb.AppendLine("parameters:");
        sb.AppendLine("  - name: dictBoolParamName");
        sb.AppendLine("    type: object");
        sb.AppendLine("    default:");
        sb.AppendLine("      key1: True");
        sb.AppendLine("      key2: True");
        sb.AppendLine();
        var expected = sb.ToString();

        var adoJobParameters = new AdoSectionCollection<AdoParameterBase>()
        {
            new AdoDictionaryParameter<bool>(){Name = "dictBoolParamName", ValueOrDefault = new Dictionary<string, bool>(){{"key1",true},{"key2",true}}, ParameterType = AdoParameterType.Template},
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
            new AdoObjectParameter<Helpers.AdoTestObject>(){Name = "adoTestObjectParamName", ValueOrDefault = new Helpers.AdoTestObject(), ParameterType = AdoParameterType.Standard},
        };

        var doc = new AdoYamlDocument();
        doc.Builder = new AdoYamlBuilder();
        adoJobParameters.AppendSection(doc.Builder, 0);
        Assert.That(doc.Builder.ToString(), Is.EqualTo(expected));
    }

    [Test]
    public void TestParameterSerialization_obj_template()
    {
        var sb = new StringBuilder();
        sb.AppendLine("parameters:");
        sb.AppendLine("  - name: adoTestObjectParamName");
        sb.AppendLine("    type: object");
        sb.AppendLine("    default:");
        sb.AppendLine("      Field1: Value1");
        sb.AppendLine("      Field2: Value2");
        sb.AppendLine("      Field3: False");
        sb.AppendLine();
        var expected = sb.ToString();

        var adoJobParameters = new AdoSectionCollection<AdoParameterBase>()
        {
            new AdoObjectParameter<Helpers.AdoTestObject>(){Name = "adoTestObjectParamName", ValueOrDefault = new Helpers.AdoTestObject(), ParameterType = AdoParameterType.Template},
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
            new AdoDictionaryParameter<object>(){Name = "dictObjParamName", ValueOrDefault = new Dictionary<string, object>(){{"key1",dynamicObj},{"key2",dynamicObj}}, ParameterType = AdoParameterType.Standard},
        };

        var doc = new AdoYamlDocument();
        doc.Builder = new AdoYamlBuilder();
        adoJobParameters.AppendSection(doc.Builder, 0);
        doc.Save(Helpers.PATH);
        Assert.That(doc.Builder.ToString(), Is.EqualTo(expected));
    }

    [Test]
    public void TestParameterSerialization_objDict_template()
    {
        var sb = new StringBuilder();
        sb.AppendLine("parameters:");
        sb.AppendLine("  - name: dictObjParamName");
        sb.AppendLine("    type: object");
        sb.AppendLine("    default:");
        sb.AppendLine("      key1:");
        sb.AppendLine("        Field1: Value1");
        sb.AppendLine("        Field2: Value2");
        sb.AppendLine("        Field3:");
        sb.AppendLine("          Field3_1: Value3_1");
        sb.AppendLine("          Field3_2: True");
        sb.AppendLine("          Field3_3:");
        sb.AppendLine("            Field3_3_1: Value3_3_1");
        sb.AppendLine("            Field3_3_2: True");
        sb.AppendLine("      key2:");
        sb.AppendLine("        Field1: Value1");
        sb.AppendLine("        Field2: Value2");
        sb.AppendLine("        Field3:");
        sb.AppendLine("          Field3_1: Value3_1");
        sb.AppendLine("          Field3_2: True");
        sb.AppendLine("          Field3_3:");
        sb.AppendLine("            Field3_3_1: Value3_3_1");
        sb.AppendLine("            Field3_3_2: True");
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
            new AdoDictionaryParameter<object>(){Name = "dictObjParamName", ValueOrDefault = new Dictionary<string, object>(){{"key1",dynamicObj},{"key2",dynamicObj}}, ParameterType = AdoParameterType.Template },
        };

        var doc = new AdoYamlDocument();
        doc.Builder = new AdoYamlBuilder();
        adoJobParameters.AppendSection(doc.Builder, 0);
        doc.Save(Helpers.PATH);
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
            new AdoObjectParameter<dynamic>(){Name = "adoDynObjParamName", ValueOrDefault = dynamicObj, ParameterType = AdoParameterType.Standard},
        };

        var doc = new AdoYamlDocument();
        doc.Builder = new AdoYamlBuilder();
        adoJobParameters.AppendSection(doc.Builder, 0);
        Assert.That(doc.Builder.ToString(), Is.EqualTo(expected));
    }

    [Test]
    public void TestParameterSerialization_dynamicObj_template()
    {
        var sb = new StringBuilder();
        sb.AppendLine("parameters:");
        sb.AppendLine("  - name: adoDynObjParamName");
        sb.AppendLine("    type: object");
        sb.AppendLine("    default:");
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
            new AdoObjectParameter<dynamic>(){Name = "adoDynObjParamName", ValueOrDefault = dynamicObj, ParameterType = AdoParameterType.Template},
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
            new AdoBoolParameter(){Name = "boolParamName", ValueOrDefault = true, ParameterType = AdoParameterType.Standard},
            new AdoStringParameter(){Name = "stringParamName", ValueOrDefault = "Value", ParameterType = AdoParameterType.Standard},
            new AdoArrayParameter<string>(){Name = "arrayStrParamName", ValueOrDefault = new List<string>(){"one","two"}.ToArray(), ParameterType   = AdoParameterType.Standard},
            new AdoArrayParameter<bool>(){Name = "arrayBoolParamName", ValueOrDefault = new List<bool>() { true, true }.ToArray(), ParameterType = AdoParameterType.Standard},
            new AdoArrayParameter<int>(){Name = "arrayIntParamName", ValueOrDefault = new List<int>() { 100, 101 }.ToArray(), ParameterType = AdoParameterType.Standard},
            new AdoDictionaryParameter<string>(){Name = "dictStrParamName", ValueOrDefault = new Dictionary<string, string>(){{"key1","value1"},{"key2","value2"}}, ParameterType = AdoParameterType.Standard},
            new AdoDictionaryParameter<bool>(){Name = "dictBoolParamName", ValueOrDefault = new Dictionary<string, bool>(){{"key1",true},{"key2",true}}, ParameterType  = AdoParameterType.Standard},
            new AdoDictionaryParameter<object>(){Name = "dictObjParamName", ValueOrDefault = new Dictionary<string, object>(){{"key1",dynamicObj},{"key2",dynamicObj}}, ParameterType = AdoParameterType.Standard},
            new AdoObjectParameter<Helpers.AdoTestObject>(){Name = "adoTestObjectParamName", ValueOrDefault = new Helpers.AdoTestObject(), ParameterType = AdoParameterType.Standard},
            new AdoObjectParameter<dynamic>(){Name = "adoDynObjParamName", ValueOrDefault = dynamicObj, ParameterType = AdoParameterType.Standard},
        };

        var doc = new AdoYamlDocument();
        doc.Builder = new AdoYamlBuilder();
        adoJobParameters.AppendSection(doc.Builder, 0);
        doc.Save(Helpers.PATH);
        Assert.That(doc.Builder.ToString(), Is.EqualTo(expected));
    }

    [Test]
    public void TestParameterSerialization_all_template()
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

        var expected = GetAllExpectedTemplate();

        var adoJobParameters = new AdoSectionCollection<AdoParameterBase>()
        {
            new AdoBoolParameter(){Name = "boolParamName", ValueOrDefault = true, ParameterType = AdoParameterType.Template},
            new AdoStringParameter(){Name = "stringParamName", ValueOrDefault = "Value", ParameterType = AdoParameterType.Template},
            new AdoArrayParameter<string>(){Name = "arrayStrParamName", ValueOrDefault = new List<string>(){"one","two"}.ToArray(), ParameterType   = AdoParameterType.Template},
            new AdoArrayParameter<bool>(){Name = "arrayBoolParamName", ValueOrDefault = new List<bool>() { true, true }.ToArray(), ParameterType = AdoParameterType.Template},
            new AdoArrayParameter<int>(){Name = "arrayIntParamName", ValueOrDefault = new List<int>() { 100, 101 }.ToArray(), ParameterType = AdoParameterType.Template},
            new AdoDictionaryParameter<string>(){Name = "dictStrParamName", ValueOrDefault = new Dictionary<string, string>(){{"key1","value1"},{"key2","value2"}}, ParameterType = AdoParameterType.Template},
            new AdoDictionaryParameter<bool>(){Name = "dictBoolParamName", ValueOrDefault = new Dictionary<string, bool>(){{"key1",true},{"key2",true}}, ParameterType  = AdoParameterType.Template},
            new AdoDictionaryParameter<object>(){Name = "dictObjParamName", ValueOrDefault = new Dictionary<string, object>(){{"key1",dynamicObj},{"key2",dynamicObj}}, ParameterType = AdoParameterType.Template},
            new AdoObjectParameter<Helpers.AdoTestObject>(){Name = "adoTestObjectParamName", ValueOrDefault = new Helpers.AdoTestObject(), ParameterType = AdoParameterType.Template},
            new AdoObjectParameter<dynamic>(){Name = "adoDynObjParamName", ValueOrDefault = dynamicObj, ParameterType = AdoParameterType.Template},
        };

        var doc = new AdoYamlDocument();
        doc.Builder = new AdoYamlBuilder();
        adoJobParameters.AppendSection(doc.Builder, 0);
        doc.Save(Helpers.PATH);
        Assert.That(doc.Builder.ToString(), Is.EqualTo(expected));
    }

    private static string GetAllExpectedTemplate()
    {
        var sb = new StringBuilder();
        sb.AppendLine("parameters:");
        sb.AppendLine("  - name: boolParamName");
        sb.AppendLine("    type: boolean");
        sb.AppendLine("    default: True");
        sb.AppendLine("  - name: stringParamName");
        sb.AppendLine("    type: string");
        sb.AppendLine("    default: Value");
        sb.AppendLine("  - name: arrayStrParamName");
        sb.AppendLine("    type: object");
        sb.AppendLine("    default:");
        sb.AppendLine("    - one");
        sb.AppendLine("    - two");
        sb.AppendLine("  - name: arrayBoolParamName");
        sb.AppendLine("    type: object");
        sb.AppendLine("    default:");
        sb.AppendLine("    - True");
        sb.AppendLine("    - True");
        sb.AppendLine("  - name: arrayIntParamName");
        sb.AppendLine("    type: object");
        sb.AppendLine("    default:");
        sb.AppendLine("    - 100");
        sb.AppendLine("    - 101");
        sb.AppendLine("  - name: dictStrParamName");
        sb.AppendLine("    type: object");
        sb.AppendLine("    default:");
        sb.AppendLine("      key1: value1");
        sb.AppendLine("      key2: value2");
        sb.AppendLine("  - name: dictBoolParamName");
        sb.AppendLine("    type: object");
        sb.AppendLine("    default:");
        sb.AppendLine("      key1: True");
        sb.AppendLine("      key2: True");
        sb.AppendLine("  - name: dictObjParamName");
        sb.AppendLine("    type: object");
        sb.AppendLine("    default:");
        sb.AppendLine("      key1:");
        sb.AppendLine("        Field1: Value1");
        sb.AppendLine("        Field2: Value2");
        sb.AppendLine("        Field3:");
        sb.AppendLine("          Field3_1: Value3_1");
        sb.AppendLine("          Field3_2: True");
        sb.AppendLine("          Field3_3:");
        sb.AppendLine("            Field3_3_1: Value3_3_1");
        sb.AppendLine("            Field3_3_2: True");
        sb.AppendLine("      key2:");
        sb.AppendLine("        Field1: Value1");
        sb.AppendLine("        Field2: Value2");
        sb.AppendLine("        Field3:");
        sb.AppendLine("          Field3_1: Value3_1");
        sb.AppendLine("          Field3_2: True");
        sb.AppendLine("          Field3_3:");
        sb.AppendLine("            Field3_3_1: Value3_3_1");
        sb.AppendLine("            Field3_3_2: True");
        sb.AppendLine("  - name: adoTestObjectParamName");
        sb.AppendLine("    type: object");
        sb.AppendLine("    default:");
        sb.AppendLine("      Field1: Value1");
        sb.AppendLine("      Field2: Value2");
        sb.AppendLine("      Field3: False");
        sb.AppendLine("  - name: adoDynObjParamName");
        sb.AppendLine("    type: object");
        sb.AppendLine("    default:");
        sb.AppendLine("      Field1: Value1");
        sb.AppendLine("      Field2: Value2");
        sb.AppendLine("      Field3:");
        sb.AppendLine("        Field3_1: Value3_1");
        sb.AppendLine("        Field3_2: True");
        sb.AppendLine("        Field3_3:");
        sb.AppendLine("          Field3_3_1: Value3_3_1");
        sb.AppendLine("          Field3_3_2: True");
        sb.AppendLine();
        return sb.ToString();
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