using System;
using System.Collections.Generic;

namespace DDNSUpdate.Infrastructure.Configuration
{
    public class ApplicationConfiguration
    {
        public static readonly TimeSpan MinimumUpdateInterval = TimeSpan.FromMinutes(1);

        public IList<Uri> ExternalAddressProviders { get; set; } = new List<Uri>();

        public TimeSpan UpdateInterval { get; set; }
    }
}
