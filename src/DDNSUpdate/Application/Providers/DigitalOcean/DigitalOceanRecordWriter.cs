using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using DDNSUpdate.Application.Records;

namespace DDNSUpdate.Application.Providers.DigitalOcean;

internal class DigitalOceanRecordWriter : IRecordWriter<DigitalOceanRecord>
{
    public async Task<WriteRecordsResult> WriteAsync(
        IReadOnlyCollection<DigitalOceanRecord> newRecords, 
        IReadOnlyCollection<DigitalOceanRecord> updatedRecords,
        CancellationToken cancellationToken)
    {
        //TODO: Implement
        return await Task.FromResult(new WriteSuccess(1, 2, "DigitalOcean write success"));
    }
}