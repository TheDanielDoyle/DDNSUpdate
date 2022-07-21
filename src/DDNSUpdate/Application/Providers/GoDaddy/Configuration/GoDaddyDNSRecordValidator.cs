using DDNSUpdate.Domain;
using FluentValidation;

namespace DDNSUpdate.Application.Providers.GoDaddy.Configuration;

public class GoDaddyDNSRecordValidator : AbstractValidator<DNSRecord>
{
    private const string _propertyDisallowedMessage = "The {PropertyName} field should not be set in configuration.";
    private const int _ttlMinimumSeconds = 600;

    public static readonly string DataErrorMessage = _propertyDisallowedMessage;
    public static readonly string FlagsErrorMessage = _propertyDisallowedMessage;
    public static readonly string IdErrorMessage = _propertyDisallowedMessage;
    public static readonly string NameErrorMessage = "{PropertyName} must be set.";
    public static readonly string PortErrorMessage = _propertyDisallowedMessage;
    public static readonly string PriorityErrorMessage = _propertyDisallowedMessage;
    public static readonly string TagErrorMessage = _propertyDisallowedMessage;
    public static readonly string TTLErrorMessage = $"{{PropertyName}} must be {_ttlMinimumSeconds} seconds or greater.";
    public static readonly string TypeErrorMessage = "Use either A or AAA for the {PropertyName}.";
    public static readonly string WeightErrorMessage = _propertyDisallowedMessage;

    public GoDaddyDNSRecordValidator()
    {
        RuleFor(p => p.Data)
            .Empty()
            .WithMessage(DataErrorMessage);

        RuleFor(p => p.Flags)
            .Empty()
            .WithMessage(FlagsErrorMessage);

        RuleFor(p => p.Id)
            .Empty()
            .WithMessage(IdErrorMessage);

        RuleFor(p => p.Name)
            .NotEmpty()
            .WithMessage(NameErrorMessage);

        RuleFor(p => p.Port)
            .Empty()
            .WithMessage(PortErrorMessage);

        RuleFor(p => p.Priority)
            .Empty()
            .WithMessage(PriorityErrorMessage);

        RuleFor(p => p.Tag)
            .Empty()
            .WithMessage(TagErrorMessage);

        RuleFor(p => p.TTL)
            .GreaterThanOrEqualTo(_ttlMinimumSeconds)
            .WithMessage(TTLErrorMessage);

        RuleFor(p => p.Type)
            .NotEmpty()
            .WithMessage(TypeErrorMessage)
            .Must(recordtype =>
            {
                return recordtype == DNSRecordType.A || recordtype == DNSRecordType.AAAA;
            })
            .WithMessage(TypeErrorMessage);

        RuleFor(p => p.Weight)
            .Empty()
            .WithMessage(WeightErrorMessage);
    }
}