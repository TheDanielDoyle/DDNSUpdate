using System;
using FluentValidation;

namespace DDNSUpdate.Infrastructure.Settings;

internal sealed class AppSettingsValidator : AbstractValidator<AppSettings>
{
    public AppSettingsValidator()
    {
        RuleFor(x => x.UpdateInterval)
            .Must(t => t is not null && t.Value >= TimeSpan.FromMinutes(1))
            .WithMessage("{PropertyName} must be one minute or greater");
    }
}