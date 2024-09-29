using System;
using System.Text.Json;

namespace PipelineWeaver.Ado;

public class AdoObjectBase : AdoSectionBase
{
    public bool IsJsonObject { get; set; }

}
