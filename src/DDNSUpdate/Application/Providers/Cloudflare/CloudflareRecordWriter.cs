using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using DDNSUpdate.Application.Records;

namespace DDNSUpdate.Application.Providers.Cloudflare;

internal class CloudflareRecordWriter : IRecordWriter<CloudflareRecord>
{
    public async Task<WriteRecordsResult> WriteAsync(
        IReadOnlyCollection<CloudflareRecord> newRecords, 
        IReadOnlyCollection<CloudflareRecord> updatedRecords,
        CancellationToken cancellationToken)
    {
        //TODO: Implement
        return await Task.FromResult(new WriteSuccess(1, 2, "Cloudflare write success"));
    }
}