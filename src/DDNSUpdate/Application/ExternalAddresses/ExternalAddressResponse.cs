using DDNSUpdate.Domain;

namespace DDNSUpdate.Application.ExternalAddresses
{
    public class ExternalAddressResponse : IExternalAddressResponse
    {
        public ExternalAddressResponse(ExternalAddress externalAddress)
        {
            ExternalAddress = externalAddress;
        }

        public ExternalAddress ExternalAddress { get; }
    }
}
