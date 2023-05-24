using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using DDNSUpdate.Application.Records;

namespace DDNSUpdate.Application.Providers.GoDaddy;

internal sealed class GoDaddyRecordReader : IRecordReader<GoDaddyRecord>
{
    public async Task<ReadRecordsResult<GoDaddyRecord>> ReadAsync(CancellationToken cancellationToken)
    {
        //TODO: Implement
        ReadOnlyCollection<GoDaddyRecord> records = new(Enumerable.Empty<GoDaddyRecord>().ToList());
        return await Task.FromResult(new ReadSuccess<GoDaddyRecord>(records, "GoDaddy read success"));
    }
}