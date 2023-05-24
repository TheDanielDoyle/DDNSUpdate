using System.Collections.Generic;
using DDNSUpdate.Application.Records;

namespace DDNSUpdate.Application.Providers.GoDaddy;

internal sealed class GoDaddyRecordFilter : IRecordFilter<GoDaddyRecord>
{
    public IReadOnlyCollection<GoDaddyRecord> FilterNew(IReadOnlyCollection<GoDaddyRecord> records)
    {
        //TODO: Implement
        return records;
    }

    public IReadOnlyCollection<GoDaddyRecord> FilterUpdated(IReadOnlyCollection<GoDaddyRecord> records)
    {
        //TODO: Implement
        return records;
    }
}