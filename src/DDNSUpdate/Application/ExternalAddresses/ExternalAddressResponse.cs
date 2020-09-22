using System.Net;

namespace DDNSUpdate.Application.ExternalAddresses
{
    public class ExternalAddressResponse : IExternalAddressResponse
    {
        public ExternalAddressResponse(IPAddress ipAddress)
        {
            IPAddress = ipAddress;
        }

        public IPAddress IPAddress { get; }
    }
}
