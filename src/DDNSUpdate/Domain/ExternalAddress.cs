using System.Net;

namespace DDNSUpdate.Domain;

public record ExternalAddress
{
    public IPAddress? IPv4Address { get; init; }

    public string? ToIPv4String()
    {
        return IPv4Address?.ToString();
    }
}