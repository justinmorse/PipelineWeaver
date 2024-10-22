using System;

namespace PipelineWeaver.Ado;

public class AdoContainer
{
    public required string Image { get; set; }
    public string? Endpoint { get; set; }
    public Dictionary<string, string>? Env { get; set; }
    public bool? MapDockerSocket { get; set; }
    public string? Options { get; set; }
    public List<string>? Ports { get; set; }
    public List<string>? Volumes { get; set; }
    public MountReadOnly? MountReadOnly { get; set; }
}

public class MountReadOnly
{
    public bool? Work { get; set; }
    public bool? Externals { get; set; }
    public bool? Tools { get; set; }
    public bool? Tasks { get; set; }
}