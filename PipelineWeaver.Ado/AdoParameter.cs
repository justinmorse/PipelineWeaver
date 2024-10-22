using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;

namespace PipelineWeaver.Ado
{
    public abstract class AdoParameterBase
    {
        public required string Name { get; set; }
        public required AdoParameterType ParameterType { get; set; }

    }

    public class AdoStringParameter : AdoParameterBase
    {
        public string? ValueOrDefault { get; set; }
    }
    public class AdoBoolParameter : AdoParameterBase
    {
        public bool? ValueOrDefault { get; set; }
    }

    public class AdoArrayParameter<T> : AdoParameterBase
    {
        public T[]? ValueOrDefault { get; set; }
    }

    public class AdoDictionaryParameter<T> : AdoParameterBase
    {
        public Dictionary<string, T>? ValueOrDefault { get; set; }
    }

    public class AdoObjectParameter<T> : AdoParameterBase
    {
        public T? ValueOrDefault { get; set; }

    }

    public class AdoJsonObjectParameter<T> : AdoParameterBase
    {
        public T? ValueOrDefault { get; set; }
        public required bool SerializeAsSingleLine { get; set; }
    }

    public enum AdoParameterType
    {
        Standard,
        Template
    }
}