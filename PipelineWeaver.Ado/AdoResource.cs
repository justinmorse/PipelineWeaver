using System;

namespace PipelineWeaver.Ado;

public interface IAdoResource
{
    //Left empty 
}

public class AdoPipelineResource : IAdoResource
{
    public required string Pipeline { get; set; }
    public required string Connection { get; set; }
    public required string Version { get; set; }
    public string? Project { get; set; }
    public string? Source { get; set; }
    public string? Branch { get; set; }
    public List<string>? Tags { get; set; }
    public bool? Trigger { get; set; }
    public AdoTriggerContainer? Triggers { get; set; }
    public List<string>? StageTriggers { get; set; }

}

public class AdoBuildResource : IAdoResource
{
    public required string Build { get; set; }
    public string? Type { get; set; }
    public string? Connection { get; set; }
    public string? Source { get; set; }
    public bool? Trigger { get; set; }
}

public class AdoRepositoryResource : IAdoResource
{
    public required string Repository { get; set; }
    public string? Ref { get; set; }
    public string? Type { get; set; }
    public string? Name { get; set; }
    public string? Endpoint { get; set; }
    public required string Connection { get; set; }
    public string? Source { get; set; }
    public string? Project { get; set; }
}

public class AdoPackageResource : IAdoResource
{
    public required string Package { get; set; }
    public string? Type { get; set; }
    public string? Version { get; set; }
    public string? Endpoint { get; set; }
    public string? Connection { get; set; }
    public string? Tag { get; set; }
}

public class AdoContainerResource : IAdoResource
{
    public required string Container { get; set; }
    public string? Connection { get; set; }
    public string? Image { get; set; }
    public string? Env { get; set; }
    public string? Ports { get; set; }
    public string? Volumes { get; set; }
    public string? Options { get; set; }
}