using FluentValidation;

namespace DDNSUpdate.Application.Providers.DigitalOcean.Configuration
{
    public class DigitalOceanConfigurationValidator : AbstractValidator<DigitalOceanConfiguration>
    {
        public DigitalOceanConfigurationValidator()
        {
            RuleForEach(p => p.Accounts).SetValidator(new DigitalOceanAccountValidator());
        }
    }
}