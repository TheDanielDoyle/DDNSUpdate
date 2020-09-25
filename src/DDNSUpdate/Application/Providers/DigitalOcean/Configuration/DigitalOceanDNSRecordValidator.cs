using Autofac.Core;
using DDNSUpdate.Domain;
using FluentValidation;

namespace DDNSUpdate.Application.Configuration
{
    public class DigitalOceanDNSRecordValidator : AbstractValidator<DNSRecord>
    {
        private const string _propertyDisallowedMessage = "The {PropertyName} field should not be set in configuration.";
        private const int _ttlMinimumSeconds = 30;

        public static readonly string DataErrorMessage = _propertyDisallowedMessage;
        public static readonly string FlagsErrorMessage = _propertyDisallowedMessage;
        public static readonly string IdErrorMessage = _propertyDisallowedMessage;
        public static readonly string PortErrorMessage = _propertyDisallowedMessage;
        public static readonly string PriorityErrorMessage = _propertyDisallowedMessage;
        public static readonly string TagErrorMessage = _propertyDisallowedMessage;
        public static readonly string TTLErrorMessage = $"The {{PropertyName}} must be {_ttlMinimumSeconds} seconds or greater.";
        public static readonly string TypeErrorMessage = "Use either A or AAA for the {PropertyName}.";
        public static readonly string WeightErrorMessage = _propertyDisallowedMessage;

        public DigitalOceanDNSRecordValidator()
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

            RuleFor(p => p.Port)
                .NotEmpty()
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
                .WithMessage(TypeErrorMessage);

            RuleFor(p => p.Weight)
                .Empty()
                .WithMessage(WeightErrorMessage);
        }
    }
}