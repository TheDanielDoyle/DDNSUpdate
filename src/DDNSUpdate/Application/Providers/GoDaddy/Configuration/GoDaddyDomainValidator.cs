using DDNSUpdate.Application.Providers.GoDaddy.Domain;
using FluentValidation;

namespace DDNSUpdate.Application.Providers.GoDaddy.Configuration
{
    public class GoDaddyDomainValidator : AbstractValidator<GoDaddyDomain>
    {
        public static readonly string NameErrorMessage = "You must have a DigitalOcean Domain Name.";

        public GoDaddyDomainValidator()
        {
            RuleFor(p => p.Name)
                .NotEmpty()
                .WithMessage(NameErrorMessage);

            RuleForEach(p => p.Records)
                .SetValidator(new GoDaddyDNSRecordValidator());
        }
    }
}
