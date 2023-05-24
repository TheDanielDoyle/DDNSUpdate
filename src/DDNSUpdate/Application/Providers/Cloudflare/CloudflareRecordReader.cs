using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using DDNSUpdate.Application.Records;

namespace DDNSUpdate.Application.Providers.Cloudflare;

internal class CloudflareRecordReader : IRecordReader<CloudflareRecord>
{
    public async Task<ReadRecordsResult<CloudflareRecord>> ReadAsync(CancellationToken cancellationToken)
    {
        //TODO: Implement
        ReadOnlyCollection<CloudflareRecord> records = new(Enumerable.Empty<CloudflareRecord>().ToList());
        return await Task.FromResult(new ReadSuccess<CloudflareRecord>(records, "Cloudflare read success"));
    }
}