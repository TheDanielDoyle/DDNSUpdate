using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using DDNSUpdate.Application.Records;

namespace DDNSUpdate.Application.Providers.DigitalOcean;

internal class DigitalOceanRecordReader : IRecordReader<DigitalOceanRecord>
{
    public async Task<ReadRecordsResult<DigitalOceanRecord>> ReadAsync(CancellationToken cancellationToken)
    {
        //TODO: Implement
        ReadOnlyCollection<DigitalOceanRecord> records = new(Enumerable.Empty<DigitalOceanRecord>().ToList());
        return await Task.FromResult(new ReadSuccess<DigitalOceanRecord>(records, "DigitalOcean read success"));
    }
}