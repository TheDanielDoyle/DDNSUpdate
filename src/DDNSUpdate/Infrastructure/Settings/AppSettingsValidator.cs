using System;
using FluentValidation;

namespace DDNSUpdate.Infrastructure.Settings;

internal sealed class AppSettingsValidator : AbstractValidator<AppSettings>
{
    public AppSettingsValidator()
    {
        RuleFor(x => x.UpdateInterval)
            .Must(t => t is not null && t.Value >= TimeSpan.FromMinutes(5))
            .WithMessage("{PropertyName} must be five minutes or greater");
    }
}