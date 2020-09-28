using DDNSUpdate.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace DDNSUpdate.Application.Providers.GoDaddy.Request
{
    public class GoDaddyUpdateDNSRecordsRequest
    {
        public GoDaddyUpdateDNSRecordsRequest(
            string apiKey,
            string apiSecret,
            DNSRecordType dNSRecordType,
            string domainName,
            IEnumerable<GoDaddyUpdateDNSRecordRequest> records)
        {
            ApiKey = apiKey;
            ApiSecret = apiSecret;
            DNSRecordType = dNSRecordType;
            DomainName = domainName;
            Records = records;
        }

        public string ApiKey { get; }

        public string ApiSecret { get; }

        public DNSRecordType DNSRecordType { get; }

        public string DomainName { get; }

        public IEnumerable<GoDaddyUpdateDNSRecordRequest> Records { get; }
    }
}
