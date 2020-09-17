using ServiceProfiles;
using FluentValidation;

namespace DDNSUpdate.Infrastructure.ServiceProfiles
{
    public class ValidationServiceProfile : HostedServiceProfile
    {
        public override void Configure(IHostServiceProfileContext context)
        {
            context.Services.AddValidatorsFromAssembly(ThisAssembly);
        }
    }
}
