using FluentValidation;

namespace DDNSUpdate.Application.Providers.DigitalOcean;

internal sealed class DigitalOceanSettingsValidator : AbstractValidator<DigitalOceanSettings>
{
    public DigitalOceanSettingsValidator()
    {
        RuleFor(s => s.ApiUrl)
            .NotNull()
            .WithMessage("DigitalOcean {PropertyName} must be set");
    }
}