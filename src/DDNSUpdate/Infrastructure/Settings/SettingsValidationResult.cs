using OneOf;

namespace DDNSUpdate.Infrastructure.Settings;

[GenerateOneOf]
internal partial class SettingsValidationResult : OneOfBase<SettingsValid, SettingsInvalid>
{
}