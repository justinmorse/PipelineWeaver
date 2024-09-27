using System;

namespace PipelineWeaver.Ado;

public class AdoVariableContainer : AdoSectionBase
{
    public List<AdoVariableBase> Variables { get; set; } = new List<AdoVariableBase>();
}

public abstract class AdoVariableBase
{
    //Empty
}

public class AdoNameVariable : AdoVariableBase
{
    public required string Name { get; set; }
    public required string Value { get; set; }
}

public class AdoGroupVariable : AdoVariableBase
{
    public required string Group { get; set; }
}

public class AdoTemplateVariable : AdoVariableBase
{
    public required string Template { get; set; }
}
