using ServiceProfiles;
using FluentValidation;
using DDNSUpdate.Application;
using Microsoft.Extensions.DependencyInjection;

namespace DDNSUpdate.Infrastructure.ServiceProfiles
{
    public class ValidationServiceProfile : HostedServiceProfile
    {
        public override void Configure(IHostServiceProfileContext context)
        {
            context.Services.AddValidatorsFromAssembly(ThisAssembly);
            context.Services.AddScoped<IConfigurationValidator, ConfigurationValidator>();
        }
    }
}
