using System.Collections.Generic;
using DDNSUpdate.Application.Records;

namespace DDNSUpdate.Application.Providers.DigitalOcean;

internal class DigitalOceanRecordFilter : IRecordFilter<DigitalOceanRecord>
{
    public IReadOnlyCollection<DigitalOceanRecord> FilterNew(IReadOnlyCollection<DigitalOceanRecord> records)
    {
        //TODO: Implement
        return records;
    }

    public IReadOnlyCollection<DigitalOceanRecord> FilterUpdated(IReadOnlyCollection<DigitalOceanRecord> records)
    {
        //TODO: Implement
        return records;
    }
}