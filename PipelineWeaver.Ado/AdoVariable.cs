using System;

namespace PipelineWeaver.Ado;

public interface IAdoVariable
{
    //Empty
}

public class AdoNameVariable : IAdoVariable
{
    public required string Name { get; set; }
    public required string Value { get; set; }
}

public class AdoGroupVariable : IAdoVariable
{
    public required string Group { get; set; }
}

public class AdoTemplateVariable : IAdoVariable
{
    public required string Template { get; set; }
}
