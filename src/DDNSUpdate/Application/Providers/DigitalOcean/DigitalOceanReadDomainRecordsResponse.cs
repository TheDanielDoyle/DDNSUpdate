using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace DDNSUpdate.Application.Providers.DigitalOcean;

internal sealed record DigitalOceanReadDomainRecordsResponse
{
    [JsonPropertyName("domain_records")]
    public IList<DigitalOceanReadDomainRecord>? DomainRecords { get; set; }
}