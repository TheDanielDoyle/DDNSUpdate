using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace DDNSUpdate.Infrastructure.Profiles;

internal sealed class ValidationProfile : IHostBuilderProfile
{
    public HostApplicationBuilder Add(HostApplicationBuilder builder)
    {
        builder.Services
            .Scan(scan => scan.FromAssemblyOf<Program>()
                .AddClasses(classes => classes.AssignableTo<IValidator>())
                .AsSelfWithInterfaces()
                .WithTransientLifetime());
        return builder;
    }
}