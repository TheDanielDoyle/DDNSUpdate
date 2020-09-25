using DDNSUpdate.Application.Configuration;
using DDNSUpdate.Application.Providers.DigitalOcean.Domain;
using FluentValidation;

namespace DDNSUpdate.Application.Providers.DigitalOcean.Configuration
{
    public class DigitalOceanDomainValidator : AbstractValidator<DigitalOceanDomain>
    {
        public static readonly string NameErrorMessage = "You must have a DigitalOcean Domain Name.";

        public DigitalOceanDomainValidator()
        {
            RuleFor(p => p.Name)
                .NotEmpty()
                .WithMessage(NameErrorMessage);
            
            RuleForEach(p => p.Records).SetValidator(new DigitalOceanDNSRecordValidator());
        }
    }
}