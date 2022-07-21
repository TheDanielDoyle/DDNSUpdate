using System;

namespace DDNSUpdate.Infrastructure.Configuration;

public record ExternalAddressProvider
{
    public Uri Uri { get; set; } = default!;
}