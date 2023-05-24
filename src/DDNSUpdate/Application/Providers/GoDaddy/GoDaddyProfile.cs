using DDNSUpdate.Application.Records;
using DDNSUpdate.Infrastructure.Extensions;
using DDNSUpdate.Infrastructure.Profiles;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace DDNSUpdate.Application.Providers.GoDaddy;

internal sealed class GoDaddyProfile : HostApplicationBuilderProfile
{
    protected override void Add(HostApplicationBuilder builder)
    {
        builder
            .AddSettings<GoDaddySettings>("GoDaddy")
            .Services
            .AddTransient<IUpdateService, GoDaddyUpdateService>()
            .AddTransient<IRecordFilter<GoDaddyRecord>, GoDaddyRecordFilter>()
            .AddTransient<IRecordReader<GoDaddyRecord>, GoDaddyRecordReader>()
            .AddTransient<IRecordWriter<GoDaddyRecord>, GoDaddyRecordWriter>();
    }
}