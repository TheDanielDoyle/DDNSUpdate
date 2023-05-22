namespace DDNSUpdate.Infrastructure.Settings;

internal interface ISettingsValidator
{
    SettingsValidationResult Validate();
}