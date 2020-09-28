using Newtonsoft.Json;
using System.Collections.Generic;

namespace DDNSUpdate.Application.Providers.DigitalOcean.Responses
{
    public class DigitalOceanGetDomainRecordsResponse
    {
        [JsonProperty("domain_records")]
        public IEnumerable<DigitalOceanGetDomainRecordResponse> DomainRecords { get; set; } = new List<DigitalOceanGetDomainRecordResponse>();
    }
}
