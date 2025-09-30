namespace DDNSUpdate.Infrastructure.Settings;

internal interface ISettingsValidator
{
    ValidateSettingsResult Validate();
}