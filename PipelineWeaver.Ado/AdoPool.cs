using System;

namespace PipelineWeaver.Ado;


public class AdoPoolContainer : AdoSectionBase
{

    public List<AdoPoolBase>? Pools { get; set; }
}

public abstract class AdoPoolBase
{
    //empty
}

public class AdoNamedPool : AdoPoolBase
{
    public string? Name { get; set; }
    public List<string>? Demands { get; set; }
}

public class AdoHostedPool : AdoPoolBase
{
    public string? VmImage { get; set; }
}