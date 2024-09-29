using System;

namespace PipelineWeaver.Ado;

public class AdoTemplate : AdoSectionBase
{
    public Dictionary<string, object>? Parameters { get; set; }
    public required AdoSectionCollection<AdoSectionBase> Section { get; set; }

}

public class AdoExtendsTemplate : AdoTemplate
{
    public required string Extends { get; set; }
}

