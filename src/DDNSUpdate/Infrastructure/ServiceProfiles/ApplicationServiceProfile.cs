using DDNSUpdate.Application;
using DDNSUpdate.Application.Configuration;
using DDNSUpdate.Application.ExternalAddresses;
using DDNSUpdate.Infrastructure.Configuration;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using ServiceProfiles;
using ServiceCollectionExtensions = AutoMapper.ServiceCollectionExtensions;

namespace DDNSUpdate.Infrastructure.ServiceProfiles
{
    public class ApplicationServiceProfile : HostServiceProfile
    {
        public override void Configure(IHostServiceProfileContext context)
        {
            context.Services.Configure<ApplicationConfiguration>(context.Configuration);

            ServiceCollectionExtensions.AddAutoMapper(context.Services, ThisAssembly);
            context.Services.AddHostedService<DDNSUpdateHostedService>();
            context.Services.AddHttpClient<IExternalAddressClient, ExternalAddressClient>();
            context.Services.AddValidatorsFromAssembly(ThisAssembly);

            context.Services.AddScoped<IConfigurationValidator, ConfigurationValidator>();

            context.Services.AddSingleton<IScopeBuilder, ScopeBuilder>();
            context.Services.AddSingleton<IDDNSUpdateInvoker, DDNSUpdateInvoker>();

            context.Services.AddTransient<ServiceFactory>(p => p.GetService);
        }
    }
}
