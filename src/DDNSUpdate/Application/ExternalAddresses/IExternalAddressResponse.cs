using DDNSUpdate.Domain;

namespace DDNSUpdate.Application.ExternalAddresses;

public interface IExternalAddressResponse
{
    ExternalAddress ExternalAddress { get; }
}