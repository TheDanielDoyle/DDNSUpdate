using DDNSUpdate.Domain;
using FluentValidation;

namespace DDNSUpdate.Application.Providers.GoDaddy.Configuration
{
    public class GoDaddyDNSRecordValidator : AbstractValidator<DNSRecord>
    {
        public GoDaddyDNSRecordValidator()
        {
        }
    }
}
