
using DDNSUpdate.Domain;

namespace DDNSUpdate.Application.Providers.GoDaddy.Request
{
    public class GoDaddyGetDNSRecordsRequest
    {
        public GoDaddyGetDNSRecordsRequest(string apiKey, string apiSecret, DNSRecordType dnsRecordType, string domainName)
        {
            ApiKey = apiKey;
            ApiSecret = apiSecret;
            DNSRecordType = dnsRecordType;
            DomainName = domainName;
        }

        public string ApiKey { get; }

        public string ApiSecret { get; }

        public DNSRecordType DNSRecordType { get; }

        public string DomainName { get; }
    }
}
