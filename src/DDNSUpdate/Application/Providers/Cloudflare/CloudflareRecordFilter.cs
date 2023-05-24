using System.Collections.Generic;
using DDNSUpdate.Application.Records;

namespace DDNSUpdate.Application.Providers.Cloudflare;

internal class CloudflareRecordFilter : IRecordFilter<CloudflareRecord>
{
    public IReadOnlyCollection<CloudflareRecord> FilterNew(IReadOnlyCollection<CloudflareRecord> records)
    {
        //TODO: Implement
        return records;
    }

    public IReadOnlyCollection<CloudflareRecord> FilterUpdated(IReadOnlyCollection<CloudflareRecord> records)
    {
        //TODO: Implement
        return records;
    }
}