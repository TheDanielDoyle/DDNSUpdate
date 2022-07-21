using DDNSUpdate.Application.Providers.GoDaddy.Domain;
using FluentValidation;

namespace DDNSUpdate.Application.Providers.GoDaddy.Configuration;

public class GoDaddyDomainValidator : AbstractValidator<GoDaddyDomain>
{
    private static readonly string NameErrorMessage = "You must have a GoDaddy Domain Name.";

    private static readonly string RecordsErrorMessage = "You must provide some Domain Records";

    public GoDaddyDomainValidator()
    {
        RuleFor(p => p.Name)
            .NotEmpty()
            .WithMessage(NameErrorMessage);

        RuleFor(p => p.Records)
            .NotEmpty()
            .WithMessage(RecordsErrorMessage);

        RuleForEach(p => p.Records)
            .SetValidator(new GoDaddyDNSRecordValidator());
    }
}