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
            .AddScoped<IRecordFilter<GoDaddyRecord, GoDaddyAccount>, GoDaddyRecordFilter>()
            .AddScoped<IRecordReader<GoDaddyRecord, GoDaddyAccount>, GoDaddyRecordReader>()
            .AddScoped<IRecordWriter<GoDaddyRecord, GoDaddyAccount>, GoDaddyRecordWriter>()
            .AddScoped<IUpdateService<GoDaddyAccount>, GoDaddyUpdateService>();
    }
}