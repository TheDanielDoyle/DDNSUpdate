using System.Net;

namespace DDNSUpdate.Domain
{
    public class ExternalAddress
    {
        public IPAddress? IPv4Address { get; set; }

        public string? ToIPv4String()
        {
            return IPv4Address?.ToString();
        }
    }
}
