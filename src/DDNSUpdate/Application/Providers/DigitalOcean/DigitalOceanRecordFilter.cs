using System.Collections.Generic;

namespace DDNSUpdate.Application.Providers.DigitalOcean;

internal class DigitalOceanRecordFilter : IRecordFilter<DigitalOceanRecord, DigitalOceanAccount>
{
    public IReadOnlyCollection<DigitalOceanRecord> FilterNew(
        DigitalOceanAccount account, 
        IReadOnlyCollection<DigitalOceanRecord> records)
    {
        //TODO: Implement
        return records;
    }

    public IReadOnlyCollection<DigitalOceanRecord> FilterUpdated(
        DigitalOceanAccount account, 
        IReadOnlyCollection<DigitalOceanRecord> records)
    {
        //TODO: Implement
        return records;
    }
}