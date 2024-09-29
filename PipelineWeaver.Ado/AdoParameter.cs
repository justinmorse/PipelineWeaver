using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PipelineWeaver.Ado
{
    public abstract class AdoParameterBase
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
    public class AdoObjectParameter<T> : AdoParameterBase where T : AdoObjectBase
    {
        public required T Value { get; set; }
    }



    public abstract class AdoTemplateParameter
    {
        public required string Name { get; set; }

    }

    public class AdoStringTemplateParameter : AdoTemplateParameter
    {
        public string? Default { get; set; }
    }

    public class AdoBoolTemplateParameter : AdoTemplateParameter
    {
        public bool? Default { get; set; }
    }

    public class AdoObjectTemplateParameter<T> : AdoTemplateParameter where T : AdoObjectBase
    {
        public required T Default { get; set; }
    }
}