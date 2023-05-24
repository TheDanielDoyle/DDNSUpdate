using DDNSUpdate.Application.Records;
using Microsoft.Extensions.Logging;

namespace DDNSUpdate.Application.Providers.DigitalOcean;

internal class DigitalOceanUpdateService : UpdateService<DigitalOceanRecord>
{
    public DigitalOceanUpdateService(
        ILogger<DigitalOceanUpdateService> logger,
        IRecordFilter<DigitalOceanRecord> filter, 
        IRecordReader<DigitalOceanRecord> reader, 
        IRecordWriter<DigitalOceanRecord> writer) : base(logger, filter, reader, writer)
    {
    }
}