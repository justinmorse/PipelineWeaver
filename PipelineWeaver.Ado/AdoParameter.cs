using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PipelineWeaver.Ado
{
    public abstract class AdoParameterBase : AdoSectionBase
    {
        public required string Name { get; set; }
    }

    public class AdoStringParameter : AdoParameterBase
    {
        public required string Value { get; set; }
    }
    public class AdoBoolParameter : AdoParameterBase
    {
        public required bool Value { get; set; }
    }

    public class AdoObjectParameter<T> : AdoParameterBase
    {
        public AdoObject<T> Value { get; set; }

        public AdoObjectParameter(T value)
        {
            Value = new AdoObject<T>(value);
        }
    }

    public class AdoJsonObjectParameter<T> : AdoParameterBase
    {
        public required AdoJsonObject<T> Value { get; set; }

        public AdoJsonObjectParameter(T value, bool serializeAsSingleLine)
        {
            Value = new AdoJsonObject<T>(value, serializeAsSingleLine);
        }
    }



    public abstract class AdoTemplateParameterBase
    {
        public required string Name { get; set; }

    }

    public class AdoStringTemplateParameter : AdoTemplateParameterBase
    {
        public string? Default { get; set; }
    }

    public class AdoBoolTemplateParameter : AdoTemplateParameterBase
    {
        public bool? Default { get; set; }
    }

    public class AdoObjectTemplateParameter<T> : AdoTemplateParameterBase
    {
        public required AdoObject<T> Default { get; set; }

        public AdoObjectTemplateParameter(T defaultValue)
        {
            Default = new AdoObject<T>(defaultValue);
        }
    }

    public class AdoJsonObjectTemplateParameter<T> : AdoTemplateParameterBase where T : AdoObject<T>
    {
        public required AdoJsonObject<T> Default { get; set; }

        public AdoJsonObjectTemplateParameter(T defaultValue, bool serializeAsSingleLine)
        {
            Default = new AdoJsonObject<T>(defaultValue, serializeAsSingleLine);
        }
    }
}