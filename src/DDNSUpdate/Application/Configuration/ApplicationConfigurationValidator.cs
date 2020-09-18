using DDNSUpdate.Infrastructure.Configuration;
using FluentValidation;

namespace DDNSUpdate.Application.Configuration
{
    public class ApplicationConfigurationValidator : AbstractValidator<ApplicationConfiguration>
    {
        public static readonly string ExternalAddressProvidersErrorMessage = $"{nameof(ApplicationConfiguration.ExternalAddressProviders)} cannot be empty.";
        public static readonly string UpdateIntervalErrorMessage = $"{nameof(ApplicationConfiguration.UpdateInterval)} must be greater than {ApplicationConfiguration.MinimumUpdateInterval:mm} minute/s.";

        public ApplicationConfigurationValidator()
        {
            RuleFor(p => p.ExternalAddressProviders)
                .NotEmpty()
                .WithMessage(ExternalAddressProvidersErrorMessage);

            RuleFor(p => p.UpdateInterval)
                .GreaterThanOrEqualTo(ApplicationConfiguration.MinimumUpdateInterval)
                .WithMessage(UpdateIntervalErrorMessage);
        }
    }
}
