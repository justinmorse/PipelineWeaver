using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PipelineWeaver.Ado
{
    public class AdoParameterContainer : AdoSectionBase
    {
        public List<AdoParameterBase> Parameters { get; set; } = new List<AdoParameterBase>();
    }

    public class AdoTemplateParameterContainer : AdoSectionBase
    {
        public List<AdoTemplateParameter> Parameters { get; set; } = new List<AdoTemplateParameter>();
    }

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
    public class AdoObjectParameter<T> : AdoParameterBase
    {

        public required T Value { get; set; }
    }



    public abstract class AdoTemplateParameter
    {
        public required string Name { get; set; }

    }

    public class AdoStringTemplateParameter : AdoTemplateParameter
    {
        public required string Value { get; set; }
        public string? Default { get; set; }
    }

    public class AdoBoolTemplateParameter : AdoTemplateParameter
    {
        public required bool Value { get; set; }
        public bool? Default { get; set; }
    }

    public class AdoObjectTemplateParameter<T> : AdoTemplateParameter
    {
        public required T Value { get; set; }
        public required T Default { get; set; }
    }
}