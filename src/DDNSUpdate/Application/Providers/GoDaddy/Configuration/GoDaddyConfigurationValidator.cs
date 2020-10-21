﻿using FluentValidation;

namespace DDNSUpdate.Application.Providers.GoDaddy.Configuration
{
    class GoDaddyConfigurationValidator : AbstractValidator<GoDaddyConfiguration>
    {
        public GoDaddyConfigurationValidator()
        {
            RuleForEach(p => p.Accounts)
                .SetValidator(new GoDaddyAccountValidator());
        }
    }
}
