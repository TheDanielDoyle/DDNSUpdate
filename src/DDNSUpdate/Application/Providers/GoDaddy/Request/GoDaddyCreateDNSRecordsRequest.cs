using System.Collections.Generic;

namespace DDNSUpdate.Application.Providers.GoDaddy.Request;

public class GoDaddyCreateDNSRecordsRequest
{
    public GoDaddyCreateDNSRecordsRequest(string apiKey, string apiSecret, IEnumerable<GoDaddyCreateDNSRecordRequest> records, string domainName)
    {
        ApiKey = apiKey;
        ApiSecret = apiSecret;
        Records = records;
        DomainName = domainName;
    }

    public string ApiKey { get; }

    public string ApiSecret { get; }

    public IEnumerable<GoDaddyCreateDNSRecordRequest> Records { get; }

    public string DomainName { get; }
}