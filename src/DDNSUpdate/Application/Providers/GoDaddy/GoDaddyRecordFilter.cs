using System.Collections.Generic;

namespace DDNSUpdate.Application.Providers.GoDaddy;

internal sealed class GoDaddyRecordFilter : IRecordFilter<GoDaddyRecord, GoDaddyAccount>
{
    public IReadOnlyCollection<GoDaddyRecord> FilterNew(
        GoDaddyAccount account, 
        IReadOnlyCollection<GoDaddyRecord> records)
    {
        //TODO: Implement
        return records;
    }

    public IReadOnlyCollection<GoDaddyRecord> FilterUpdated(
        GoDaddyAccount account, 
        IReadOnlyCollection<GoDaddyRecord> records)
    {
        //TODO: Implement
        return records;
    }
}