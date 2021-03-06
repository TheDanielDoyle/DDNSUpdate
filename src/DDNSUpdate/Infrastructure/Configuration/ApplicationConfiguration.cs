﻿using System;
using System.Collections.Generic;

namespace DDNSUpdate.Infrastructure.Configuration
{
    public class ApplicationConfiguration
    {
        public static readonly TimeSpan MinimumUpdateInterval = TimeSpan.FromMinutes(1);

        public IList<ExternalAddressProvider> ExternalAddressProviders { get; set; } = new List<ExternalAddressProvider>();

        public TimeSpan UpdateInterval { get; set; }
    }
}
