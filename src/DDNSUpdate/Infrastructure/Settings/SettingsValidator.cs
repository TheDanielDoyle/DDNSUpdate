using System;
using System.Collections.Generic;
using System.Linq;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.Extensions.Logging;

namespace DDNSUpdate.Infrastructure.Settings;

internal class SettingsValidator : ISettingsValidator
{
    private readonly ILogger<SettingsValidator> _logger;
    private readonly IEnumerable<ISettings> _settings;
    private readonly IEnumerable<IValidator> _validators;

    public SettingsValidator(
        ILogger<SettingsValidator> logger,
        IEnumerable<ISettings> settings, 
        IEnumerable<IValidator> validators)
    {
        _logger = logger;
        _settings = settings;
        _validators = validators;
    }

    public SettingsValidationResult Validate()
    {
        ValidationResults validationResults = new(
            _settings.SelectMany(Validate)
        );
        
        return validationResults.IsValid() 
            ? new SettingsValid(validationResults) 
            : new SettingsInvalid(validationResults);
    }

    private IEnumerable<ValidationResult> Validate(ISettings settings)
    {
        return _validators
            .Where(v => v.CanValidateInstancesOfType(settings.GetType()))
            .Select(v =>
            {
                Type validationContextType = typeof(ValidationContext<>)
                    .MakeGenericType(settings.GetType());
                
                IValidationContext validationContext = (IValidationContext)Activator
                    .CreateInstance(validationContextType, settings)!;

                _logger.LogInformation(
                    "Validating {Settings} with {Validator}", settings.GetType().Name, v.GetType().Name);
                
                return v.Validate(validationContext);
            });
    }
}