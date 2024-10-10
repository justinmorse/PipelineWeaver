using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
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

    public class AdoArrayParameter<T> : AdoParameterBase
    {
        public required T[] Value { get; set; }
    }

    public class AdoDictionaryParameter<T> : AdoParameterBase
    {
        private Dictionary<string, T> mvalue = new();

        public required Dictionary<string, T> Value
        {
            get => mvalue;
            set
            {
                //ParameterHelpers.CheckValueIsCorrectType<T>();
                mvalue = value;
            }
        }
    }

    public class AdoObjectParameter<T> : AdoParameterBase
    {
        public T Value { get; set; }

        public AdoObjectParameter(T value)
        {
            Value = value;
        }
    }

    public class AdoJsonObjectParameter<T> : AdoParameterBase
    {
        public required T Value { get; set; }
        public bool SerializeAsSingleLine { get; set; }

        public AdoJsonObjectParameter(T value, bool serializeAsSingleLine)
        {
            Value = value;
            SerializeAsSingleLine = serializeAsSingleLine;
        }
    }



    /* public abstract class AdoTemplateParameterBase
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

    public static class ParameterHelpers
    {

        public static void CheckValueIsCorrectType<T>()
        {
            if (typeof(T).IsArray || typeof(T).IsGenericType && typeof(T).GetGenericTypeDefinition() == typeof(IList<>))
                throw new ArgumentException($"Value cannot be an array or list. Use {typeof(AdoArrayParameter<>)} instead.");

            if ((typeof(T).IsClass || typeof(T).IsInterface) && !typeof(T).IsAssignableTo(typeof(AdoObjectBase)))
                throw new ArgumentException($"Value cannot be an object directly. Use {typeof(AdoObject<>)} wrapper instead.");
        }
    }
 */

}