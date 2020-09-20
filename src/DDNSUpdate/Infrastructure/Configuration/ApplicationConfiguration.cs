using System;

namespace DDNSUpdate.Infrastructure.Configuration
{
    public class ApplicationConfiguration
    {
        public static readonly TimeSpan MinimumUpdateInterval = TimeSpan.FromMinutes(1);

        public TimeSpan UpdateInterval { get; set; }
    }
}
