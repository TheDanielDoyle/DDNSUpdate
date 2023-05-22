using Microsoft.Extensions.Hosting;
using OneOf;

namespace DDNSUpdate.Infrastructure.Settings;

[GenerateOneOf]
internal partial class StartupSettingsValidationResult : OneOfBase<(SettingsValid, IHost), SettingsInvalid>
{
}