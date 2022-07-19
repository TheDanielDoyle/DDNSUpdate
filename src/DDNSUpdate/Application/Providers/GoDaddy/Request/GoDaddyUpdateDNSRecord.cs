using Newtonsoft.Json;

namespace DDNSUpdate.Application.Providers.GoDaddy.Request;

public class GoDaddyUpdateDNSRecord
{
    [JsonProperty("data")]
    public string Data { get; set; } = default!;

    [JsonProperty("port", DefaultValueHandling = DefaultValueHandling.Ignore)]
    public int? Port { get; set; }

    [JsonProperty("priority", DefaultValueHandling = DefaultValueHandling.Ignore)]
    public int? Priority { get; set; }

    [JsonProperty("protocol", DefaultValueHandling = DefaultValueHandling.Ignore)]
    public string? Protocol { get; set; }

    [JsonProperty("service", DefaultValueHandling = DefaultValueHandling.Ignore)]
    public string? Service { get; set; }

    [JsonProperty("ttl")]
    public int Ttl { get; set; }

    [JsonProperty("weight", DefaultValueHandling = DefaultValueHandling.Ignore)]
    public int? Weight { get; set; }
}