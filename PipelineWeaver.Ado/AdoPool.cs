using System;

namespace PipelineWeaver.Ado;

public interface IAdoPool
{
    //empty
}

public class AdoNamedPool : IAdoPool
{
    public string? Name { get; set; }
    public List<string>? Demands { get; set; }
}

public class AdoHostedPool : IAdoPool
{
    public string? VmImage { get; set; }
}