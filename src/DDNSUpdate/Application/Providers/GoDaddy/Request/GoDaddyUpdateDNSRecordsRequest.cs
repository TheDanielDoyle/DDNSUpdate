using DDNSUpdate.Domain;

namespace DDNSUpdate.Application.Providers.GoDaddy.Request;

public class GoDaddyUpdateDNSRecordsRequest
{
    public GoDaddyUpdateDNSRecordsRequest(
        string apiKey,
        string apiSecret,
        string domainName,
        DNSRecordType recordType,
        string recordName,
        GoDaddyUpdateDNSRecord record)
    {
        ApiKey = apiKey;
        ApiSecret = apiSecret;
        DomainName = domainName;
        Record = record;
        RecordName = recordName;
        RecordType = recordType;
    }

    public string ApiKey { get; }

    public string ApiSecret { get; }

    public string DomainName { get; }

    public GoDaddyUpdateDNSRecord Record { get; }

    public string RecordName { get; }

    public DNSRecordType RecordType { get; }
}