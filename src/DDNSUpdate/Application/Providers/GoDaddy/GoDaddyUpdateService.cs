using System.Collections.Generic;
using Microsoft.Extensions.Logging;

namespace DDNSUpdate.Application.Providers.GoDaddy;

internal sealed class GoDaddyUpdateService : UpdateService<GoDaddyRecord, GoDaddyAccount>
{
    private readonly GoDaddySettings _settings;
    
    public GoDaddyUpdateService(
        GoDaddySettings settings,
        ILogger<GoDaddyUpdateService> logger,
        IRecordFilter<GoDaddyRecord, GoDaddyAccount> filter,
        IRecordReader<GoDaddyRecord, GoDaddyAccount> reader,
        IRecordWriter<GoDaddyRecord, GoDaddyAccount> writer) : base(logger, filter, reader, writer)
    {
        _settings = settings;
    }

    protected override IEnumerable<GoDaddyAccount>? GetAccounts()
    {
        return _settings.Accounts;
    }
}