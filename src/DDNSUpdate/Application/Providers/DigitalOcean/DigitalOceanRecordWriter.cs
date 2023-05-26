using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using DDNSUpdate.Application.Results;

namespace DDNSUpdate.Application.Providers.DigitalOcean;

internal class DigitalOceanRecordWriter : IRecordWriter<DigitalOceanRecord, DigitalOceanAccount>
{
    private readonly IDigitalOceanClient _client;

    public DigitalOceanRecordWriter(IDigitalOceanClient client)
    {
        _client = client;
    }

    public async Task<WriteRecordsResult> WriteAsync(
        DigitalOceanAccount account,
        IReadOnlyCollection<DigitalOceanRecord> newRecords, 
        IReadOnlyCollection<DigitalOceanRecord> updatedRecords,
        CancellationToken cancellationToken)
    {
        //TODO: Implement
        return await Task.FromResult(new WriteSuccess(1, 2, "DigitalOcean write success"));
    }
}