using System;

namespace DDNSUpdate.Infrastructure.Configuration
{
    public record LoggingConfiguration
    {
        public bool EnableFile { get; set; }

        public Uri? LogstashEndpoint { get; set; }

        public Uri? SeqEndpoint { get; set; }
    }
}
