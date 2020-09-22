using System.Net;

namespace DDNSUpdate.Application.ExternalAddresses
{
    public interface IExternalAddressResponse
    {
        IPAddress IPAddress { get; }
    }
}
