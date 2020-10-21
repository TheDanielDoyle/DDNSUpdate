using DDNSUpdate.Application.Providers.GoDaddy.Domain;
using FluentValidation;

namespace DDNSUpdate.Application.Providers.GoDaddy.Configuration
{
    public class GoDaddyAccountValidator : AbstractValidator<GoDaddyAccount>
    {
        private static readonly string ApiKeyErrorMessage = "You must provide a GoDaddy account Api Key.";

        private static readonly string ApiSecretErrorMessage = "You must provide a GoDaddy account Api Secret.";

        public GoDaddyAccountValidator()
        {
            RuleFor(p => p.ApiKey)
                .NotEmpty()
                .WithMessage(ApiKeyErrorMessage);

            RuleFor(p => p.ApiSecret)
                .NotEmpty()
                .WithMessage(ApiSecretErrorMessage);

            RuleForEach(p => p.Domains)
                .SetValidator(new GoDaddyDomainValidator());
        }
    }
}
