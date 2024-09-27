using System;
using System.Text;
using System.Text.Json;
using PipelineWeaver.Ado;
using PipelineWeaver.Core.Transpiler.Ado.Yaml.SectionSerializers;

namespace PipelineWeaver.Core.Transpiler.Ado.Yaml;

public class AdoYamlBuilder
{
    internal StringBuilder _sb { get; set; }

    public AdoYamlBuilder()
    {
        _sb = new StringBuilder();
    }
    public void Append(int startingIndent, AdoSectionBase? section)
    {
        if (section is null) return;

        section?.AppendSection(this, startingIndent);
    }

    public void AppendLine(int indention, string? line)
    {
        if (string.IsNullOrWhiteSpace(line)) return;

        var split = line.SplitLinesAtNewLine();
        var indentionStr = new string(' ', indention);
        split.ForEach(l => _sb.AppendLine($"{indentionStr}{l}"));
    }

    public void AppendEmptyLine()
    {
        _sb.AppendLine();
    }

    public void AppendList(string sectionName, int indention, List<string>? items)
    {
        if (items is null || !items.Any()) return;

        var indentionStr = new string(' ', indention);
        _sb.AppendLine($"{indentionStr}{sectionName}:");
        items.ForEach(t => _sb.AppendLine($"{indentionStr}- {t}"));
    }

    public void AppendKeyValuePairs(string sectionName, int indention, Dictionary<string, string>? items)
    {
        if (items is null || !items.Any()) return;

        var indentionStr = new string(' ', indention);
        _sb.AppendLine($"{indentionStr}{sectionName}:");
        items.Keys.ToList().ForEach(t => _sb.AppendLine($"{indentionStr}- {t}: {items[t]}"));
    }

    public override string ToString()
    {
        return _sb.ToString();
    }
}

public static class AdoSerializerHelpers
{
    public static void AppendSection(this AdoSectionBase section, AdoYamlBuilder builder, int startingIndent, SerializationType type = SerializationType.Yaml)
    {

        if (type != SerializationType.Yaml)
            throw new NotImplementedException();

        var serializer = SectionSerializerFactory.GetSerializer(section);
        var innerBuilder = new AdoYamlBuilder();
        serializer.AppendSection(section, innerBuilder, startingIndent);
        var serializedString = innerBuilder.ToString();
        if (serializedString.EndsWith(Environment.NewLine))
            serializedString = serializedString.Substring(0, serializedString.Length - Environment.NewLine.Length);
        builder.AppendLine(startingIndent, serializedString);
    }
}

public static class StringExtensions
{
    public static List<string> SplitLinesAtNewLine(this string multilineString)
    {
        return [.. multilineString.Split(Environment.NewLine)];
    }

    public static string Join(this List<string> list, string separator)
    {
        return string.Join(separator, list);
    }
}

public static class DictionaryExtensions
{
    public static string ToJson(this Dictionary<string, object> dict)
    {
        return JsonSerializer.Serialize(dict);
    }
}