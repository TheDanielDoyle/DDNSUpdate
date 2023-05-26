using Microsoft.Extensions.Logging;

namespace DDNSUpdate.Application.Providers.DigitalOcean;

internal class DigitalOceanUpdateService : UpdateService<DigitalOceanRecord, DigitalOceanAccount>
{
    public DigitalOceanUpdateService(
        ILogger<DigitalOceanUpdateService> logger,
        IRecordFilter<DigitalOceanRecord, DigitalOceanAccount> filter, 
        IRecordReader<DigitalOceanRecord, DigitalOceanAccount> reader, 
        IRecordWriter<DigitalOceanRecord, DigitalOceanAccount> writer) : base(logger, filter, reader, writer)
    {
    }
}