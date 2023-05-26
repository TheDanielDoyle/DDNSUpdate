using System.Collections.Generic;
using Microsoft.Extensions.Logging;

namespace DDNSUpdate.Application.Providers.DigitalOcean;

internal class DigitalOceanUpdateService : UpdateService<DigitalOceanRecord, DigitalOceanAccount>
{
    private readonly DigitalOceanSettings _settings;
    
    public DigitalOceanUpdateService(
        DigitalOceanSettings settings,
        ILogger<DigitalOceanUpdateService> logger,
        IRecordFilter<DigitalOceanRecord, DigitalOceanAccount> filter, 
        IRecordReader<DigitalOceanRecord, DigitalOceanAccount> reader, 
        IRecordWriter<DigitalOceanRecord, DigitalOceanAccount> writer) : base(logger, filter, reader, writer)
    {
        _settings = settings;
    }

    protected override IEnumerable<DigitalOceanAccount>? GetAccounts()
    {
        return _settings.Accounts;
    }
}