using System;
namespace PipelineWeaver.Ado;

public abstract class AdoStepBase
{
    public string? Name { get; set; }
    public int? TimeoutInMinutes { get; set; }
    public string? DisplayName { get; set; }
    public bool? ContinueOnError { get; set; }
    public string? Condition { get; set; }
    public int? RetryCountOnTaskFailure { get; set; }
    public string? Target { get; set; }
    public Dictionary<string, string>? Env { get; set; }
    public bool? Enabled { get; set; }
}



public class AdoBashStep : AdoStepBase
{
    public required string Bash { get; set; }
    public bool? FailOnStderr { get; set; }
    public string? WorkingDirectory { get; set; }

}

public class AdoCheckoutStep : AdoStepBase
{
    public required string Checkout { get; set; }
    public bool? Clean { get; set; }
    public int? FetchDepth { get; set; }
    public string? FetchFilter { get; set; }
    public bool? FetchTags { get; set; }
    public bool? Lfs { get; set; }
    public bool? PersistCredentials { get; set; }
    public string? Submodules { get; set; }
    public string? Path { get; set; }
    public bool? WorkspaceRepo { get; set; }
}

public class AdoDownloadStep : AdoStepBase
{
    public required string Download { get; set; }
    public string? Artifact { get; set; }
    public string? Patterns { get; set; }
}

public class AdoDownloadBuildStep : AdoStepBase
{
    public required string DownloadBuild { get; set; }
    public string? Artifact { get; set; }
    public string? Path { get; set; }
    public string? Patterns { get; set; }
}

public class AdoGetPackageStep : AdoStepBase
{
    public required string GetPackage { get; set; }
    public string? Path { get; set; }
}

public class AdoPowershellStep : AdoStepBase
{
    public required string Powershell { get; set; }
    public string? ErrorActionPreference { get; set; }
    public bool? FailOnStderr { get; set; }
    public bool? IgnoreLASTEXITCODE { get; set; }
    public string? WorkingDirectory { get; set; }
}

public class AdoPwshStep : AdoStepBase
{
    public required string Pwsh { get; set; }
    public string? ErrorActionPreference { get; set; }
    public bool? FailOnStderr { get; set; }
    public bool? IgnoreLASTEXITCODE { get; set; }
    public string? WorkingDirectory { get; set; }
}

public class AdoPublishStep : AdoStepBase
{
    public required string Publish { get; set; }
    public string? Artifact { get; set; }
}

public class AdoReviewAppStep : AdoStepBase
{
    public required string ReviewApp { get; set; }
}

public class AdoScriptStep : AdoStepBase
{
    public required string Script { get; set; }
    public bool? FailOnStderr { get; set; }
    public string? WorkingDirectory { get; set; }
}

public class AdoTaskStep : AdoStepBase
{
    public required string Task { get; set; }
    public Dictionary<string, string>? Inputs { get; set; }
}

public class AdoTemplateStep : AdoStepBase
{
    public required string Template { get; set; }
    public AdoSectionCollection<AdoParameterBase>? Parameters { get; set; }
}
