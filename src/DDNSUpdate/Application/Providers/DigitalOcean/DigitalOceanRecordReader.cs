using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using DDNSUpdate.Application.Results;

namespace DDNSUpdate.Application.Providers.DigitalOcean;

internal class DigitalOceanRecordReader : IRecordReader<DigitalOceanRecord, DigitalOceanAccount>
{
    private readonly IDigitalOceanClient _client;

    public DigitalOceanRecordReader(IDigitalOceanClient client)
    {
        _client = client;
    }

    public async Task<ReadRecordsResult<DigitalOceanRecord>> ReadAsync(
        DigitalOceanAccount account,
        CancellationToken cancellationToken)
    {
        //TODO: Implement
        ReadOnlyCollection<DigitalOceanRecord> records = new(Enumerable.Empty<DigitalOceanRecord>().ToList());
        return await Task.FromResult(new ReadSuccess<DigitalOceanRecord>(records, "DigitalOcean read success"));
    }
}