using Newtonsoft.Json;

namespace DDNSUpdate.Application.Providers.GoDaddy.Response;

public class GoDaddyGetDNSRecordResponse
{
    [JsonProperty("data")]
    public string Data { get; set; } = default!;

    [JsonProperty("name")]
    public string Name { get; set; } = default!;

    [JsonProperty("port")]
    public int? Port { get; set; }

    [JsonProperty("priority")]
    public int? Priority { get; set; }

    [JsonProperty("protocol")]
    public string? Protocol { get; set; } = default!;

    [JsonProperty("service")]
    public string? Service { get; set; } = default!;

    [JsonProperty("ttl")]
    public int Ttl { get; set; }

    [JsonProperty("type")]
    public string Type { get; set; } = default!;

    [JsonProperty("weight")]
    public int? Weight { get; set; }
}