using Newtonsoft.Json;

namespace DDNSUpdate.Application.Providers.GoDaddy.Request
{
    public class GoDaddyUpdateDNSRecordRequest
    {
        [JsonProperty("data")]
        public string Data { get; set; } = default!;

        [JsonProperty("name")]
        public string Name { get; set; } = default!;

        [JsonProperty("port")]
        public long Port { get; set; }

        [JsonProperty("priority")]
        public long Priority { get; set; }

        [JsonProperty("protocol")]
        public string Protocol { get; set; } = default!;

        [JsonProperty("service")]
        public string Service { get; set; } = default!;

        [JsonProperty("ttl")]
        public long Ttl { get; set; }

        [JsonProperty("weight")]
        public long Weight { get; set; }
    }
}
