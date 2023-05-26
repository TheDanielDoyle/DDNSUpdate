using System.Text.Json.Serialization;

namespace DDNSUpdate.Application.Providers.DigitalOcean;

internal sealed record DigitalOceanReadDomainRecord
{
    [JsonPropertyName("data")]
    public string Data { get; set; } = default!;

    [JsonPropertyName("flags")]
    public int? Flags { get; set; }

    [JsonPropertyName("id")]
    public int Id { get; set; } = default!;

    [JsonPropertyName("name")]
    public string Name { get; set; } = default!;

    [JsonPropertyName("port")]
    public int? Port { get; set; }

    [JsonPropertyName("priority")]
    public int? Priority { get; set; }

    [JsonPropertyName("tag")]
    public string? Tag { get; set; }

    [JsonPropertyName("ttl")]
    public int? Ttl { get; set; }

    [JsonPropertyName("type")]
    public string Type { get; set; } = default!;

    [JsonPropertyName("weight")]
    public int? Weight { get; set; }
}