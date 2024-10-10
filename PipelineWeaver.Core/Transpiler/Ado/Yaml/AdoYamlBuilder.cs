using System;
using System.Collections;
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

        section.AppendSection(this, startingIndent);
    }

    public void AppendLine(int indention, string? line, bool removeEmptyLines = true)
    {
        if (string.IsNullOrWhiteSpace(line)) return;

        var split = line.SplitLinesAtNewLine();
        if (removeEmptyLines)
            split = split.Where(s => s.Trim() != string.Empty).ToList();
        var indentionStr = new string(' ', indention);
        split.ForEach(l => _sb.AppendLine($"{indentionStr}{l}"));
    }

    public void AppendEmptyLine()
    {
        _sb.AppendLine();
    }

    public void AppendArray<T>(string? sectionName, int indention, T[]? items)
    {
        if (items is null || !items.Any()) return;

        var indentionStr = new string(' ', indention);
        if (!string.IsNullOrWhiteSpace(sectionName))
            _sb.AppendLine($"{indentionStr}{sectionName}:");
        Array.ForEach(items, t => _sb.AppendLine($"{indentionStr}- {t}"));
    }

    public void AppendKeyValuePairs<T>(string? sectionName, int indention, Dictionary<string, T>? items)
    {
        if (items is null || !items.Any()) return;
        if (!string.IsNullOrWhiteSpace(sectionName))
            AppendLine(indention, $"{sectionName}:");
        if (typeof(T) == typeof(object))
        {
            items.Keys.ToList().ForEach(t =>
            {
                var obj = items[t] as AdoObjectBase;
                if (obj is null)
                    obj = new AdoObject<T>(items[t]);
                var serializer = new AdoObjectSerializer();
                var s = serializer.Serialize(obj, indention);
                AppendLine(indention + 2, $"{t}:");
                AppendLine(indention + 2, s);
            });
        }
        else
            items.Keys.ToList().ForEach(t => AppendLine(indention + 2, $"{t}: {items[t]}"));
    }

    // public void AppendKeyValuePairs_Obj<T>(string? sectionName, int indention, Dictionary<string, AdoObject<T>>? items)
    // {
    //     if (items is null || !items.Any()) return;
    //     var indentionStr = new string(' ', indention);
    //     if (!string.IsNullOrWhiteSpace(sectionName))
    //         _sb.AppendLine($"{indentionStr}{sectionName}:");

    //     items.Keys.ToList().ForEach(t =>
    //     {
    //         var serializer = new AdoObjectSerializer();
    //         var s = serializer.Serialize(items[t], indention + 2);
    //         _sb.AppendLine($"{indentionStr}  {t}:");
    //         _sb.AppendLine($"{indentionStr}    {s}");
    //     });
    // }


    internal void AppendObjectArray<T>(string sectionName, int indention, AdoObject<T>[] items)
    {
        if (items is null || !items.Any()) return;
        var indentionStr = new string(' ', indention);
        if (!string.IsNullOrWhiteSpace(sectionName))
            _sb.AppendLine($"{indentionStr}{sectionName}:");
        Array.ForEach(items, obj =>
        {
            var serializer = new AdoObjectSerializer();
            var s = serializer.Serialize(obj, indention);
            _sb.AppendLine(s);
        });
    }

    internal void AppendObjectKeyValuePairs<T>(string sectionName, int indention, Dictionary<string, AdoObject<T>>? items)
    {
        if (items is null || !items.Any()) return;

        var indentionStr = new string(' ', indention);
        if (!string.IsNullOrWhiteSpace(sectionName))
            _sb.AppendLine($"{indentionStr}{sectionName}:");
        items.Keys.ToList().ForEach(t =>
        {
            var serializer = new AdoObjectSerializer();
            var s = serializer.Serialize(items[t], indention + 2);
            _sb.AppendLine(s);
        });
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

