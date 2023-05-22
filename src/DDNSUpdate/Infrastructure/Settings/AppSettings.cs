using System;

namespace DDNSUpdate.Infrastructure.Settings;

internal sealed record AppSettings : ISettings
{
    public TimeSpan? UpdateInterval { get; init; }
}