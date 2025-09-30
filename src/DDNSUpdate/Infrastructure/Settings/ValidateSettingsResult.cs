using Dunet;

namespace DDNSUpdate.Infrastructure.Settings;

[Union]
internal partial record ValidateSettingsResult
{
    public partial record Invalid(ValidationResults Results);
    
    public partial record Valid(ValidationResults Results);
}