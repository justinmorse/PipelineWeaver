using System;

namespace PipelineWeaver.Ado;

public class AdoContainer
{
    public required string Image { get; set; } // Required. Container image tag.
    public string? Endpoint { get; set; } // ID of the service endpoint connecting to a private container registry.
    public Dictionary<string, string>? Env { get; set; } // Variables to map into the container's environment.
    public bool? MapDockerSocket { get; set; } // Set this flag to false to force the agent not to setup the /var/run/docker.sock volume on container jobs.
    public string? Options { get; set; } // Options to pass into container host.
    public List<string>? Ports { get; set; } // Ports to expose on the container.
    public List<string>? Volumes { get; set; } // Volumes to mount on the container.
    public MountReadOnly? MountReadOnly { get; set; } // Volumes to mount read-only, the default is all false.
}

public class MountReadOnly
{
    public bool? Work { get; set; } // Mount the work directory as readonly.
    public bool? Externals { get; set; } // Mount the externals directory as readonly.
    public bool? Tools { get; set; } // Mount the tools directory as readonly.
    public bool? Tasks { get; set; } // Mount the tasks directory as readonly.
}