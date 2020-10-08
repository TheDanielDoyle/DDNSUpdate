using System.Collections.Generic;
using DDNSUpdate.Domain;

namespace DDNSUpdate.Application.Providers.GoDaddy.Request
{
    public class GoDaddyCreateDNSRecordRequest
    {
        public GoDaddyCreateDNSRecordRequest(string apiKey, string apiSecret, IEnumerable<DNSRecord> records, string domainName)
        {
            ApiKey = apiKey;
            ApiSecret = apiSecret;
            Records = records;
            DomainName = domainName;
        }


        public string ApiKey { get; }

        public string ApiSecret { get; }

        public IEnumerable<DNSRecord> Records { get; }

        public string DomainName { get; }
    }
}