using DDNSUpdate.Infrastructure.Extensions;
using DDNSUpdate.Infrastructure.Profiles;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace DDNSUpdate.Application.Providers.DigitalOcean;

internal sealed class DigitalOceanProfile : HostApplicationBuilderProfile
{
    protected override void Add(HostApplicationBuilder builder)
    {
        builder
            .AddSettings<DigitalOceanSettings>("DigitalOcean")
            .Services
            .AddScoped<IRecordFilter<DigitalOceanRecord, DigitalOceanAccount>, DigitalOceanRecordFilter>()
            .AddScoped<IRecordReader<DigitalOceanRecord, DigitalOceanAccount>, DigitalOceanRecordReader>()
            .AddScoped<IRecordWriter<DigitalOceanRecord, DigitalOceanAccount>, DigitalOceanRecordWriter>()
            .AddScoped<IUpdateService<DigitalOceanAccount>, DigitalOceanUpdateService>();

        builder
            .Services
            .AddHttpClient<IDigitalOceanClient, DigitalOceanClient>();
    }
}