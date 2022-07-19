using System.Collections.Generic;

namespace DDNSUpdate.Application.Providers.GoDaddy.Response;

public class GoDaddyGetDNSRecordsResponse
{
    public IEnumerable<GoDaddyGetDNSRecordResponse> Records { get; }

    public GoDaddyGetDNSRecordsResponse(IEnumerable<GoDaddyGetDNSRecordResponse> records)
    {
        Records = records;
    }
}