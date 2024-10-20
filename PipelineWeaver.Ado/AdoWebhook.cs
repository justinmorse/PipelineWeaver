using System;

namespace PipelineWeaver.Ado;

public class AdoWebhook
{
    public required string Webhook { get; set; }
    public required string Connection { get; set; }
    public string? Type { get; set; }
    public List<AdoWebhookFilter>? Filters { get; set; }
}

public class AdoWebhookFilter
{
    public required string Path { get; set; }
    public required string Value { get; set; }
}
