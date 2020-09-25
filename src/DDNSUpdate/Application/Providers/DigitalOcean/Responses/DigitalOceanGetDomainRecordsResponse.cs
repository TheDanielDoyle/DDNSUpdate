﻿using System.Collections.Generic;
using Newtonsoft.Json;

namespace DDNSUpdate.Application.Providers.DigitalOcean.Responses
{
    public class DigitalOceanGetDomainRecordsResponse
    {
        [JsonProperty("domain_record")]
        public IEnumerable<DigitalOceanGetDomainRecordResponse> DomainRecords { get; set; } = new List<DigitalOceanGetDomainRecordResponse>();
    }
}