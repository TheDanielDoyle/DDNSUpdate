using Newtonsoft.Json;

namespace DDNSUpdate.Application.Providers.DigitalOcean.Responses;

public class DigitalOceanGetDomainRecordResponse
{
    [JsonProperty("data")]
    public string Data { get; set; } = default!;

    [JsonProperty("flags")]
    public int? Flags { get; set; }

    [JsonProperty("id")]
    public int Id { get; set; } = default!;

    [JsonProperty("name")]
    public string Name { get; set; } = default!;

    [JsonProperty("port")]
    public int? Port { get; set; }

    [JsonProperty("priority")]
    public int? Priority { get; set; }

    [JsonProperty("tag")]
    public string? Tag { get; set; }

    [JsonProperty("ttl")]
    public int? Ttl { get; set; }

    [JsonProperty("type")]
    public string Type { get; set; } = default!;

    [JsonProperty("weight")]
    public int? Weight { get; set; }
}