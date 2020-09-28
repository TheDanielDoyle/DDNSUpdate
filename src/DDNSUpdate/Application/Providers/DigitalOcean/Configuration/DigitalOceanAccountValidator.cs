using DDNSUpdate.Application.Providers.DigitalOcean.Domain;
using FluentValidation;

namespace DDNSUpdate.Application.Providers.DigitalOcean.Configuration
{
    public class DigitalOceanAccountValidator : AbstractValidator<DigitalOceanAccount>
    {
        public static readonly string TokenErrorMessage = "You must provide a DigitalOcean account token.";

        public DigitalOceanAccountValidator()
        {
            RuleForEach(p => p.Domains).SetValidator(new DigitalOceanDomainValidator());

            RuleFor(p => p.Token)
                .NotEmpty()
                .WithMessage(TokenErrorMessage);
        }
    }
}
