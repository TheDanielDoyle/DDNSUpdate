using System.Threading;
using System.Threading.Tasks;
using DDNSUpdate.Application.ExternalAddresses;
using DDNSUpdate.Domain;
using DnsZone;
using FluentResults;

namespace DDNSUpdate.Application.Providers.DigitalOcean
{
    public class DigitalOceanDomainProcessor : IDigitalOceanDomainProcessor
    {
        private readonly IDigitalOceanClient _digitalOceanClient;
        private readonly IDNSRecordCollectionExternalAddressHydrater _dnsRecordHydrater;
        private readonly IDigitalOceanDNSRecordCreator dnsRecordCreator;
        private readonly IDigitalOceanDNSRecordUpdater _dnsRecordUpdater;
        private readonly IDnsZoneFileToDNSRecordCollectionConverter _dnsZoneFileConverter;

        public DigitalOceanDomainProcessor(IDigitalOceanClient digitalOceanClient, IDNSRecordCollectionExternalAddressHydrater dnsRecordHydrater, IDigitalOceanDNSRecordCreator dnsRecordCreator, IDigitalOceanDNSRecordUpdater dnsRecordUpdater, IDnsZoneFileToDNSRecordCollectionConverter dnsZoneFileConverter)
        {
            _digitalOceanClient = digitalOceanClient;
            _dnsRecordHydrater = dnsRecordHydrater;
            this.dnsRecordCreator = dnsRecordCreator;
            _dnsRecordUpdater = dnsRecordUpdater;
            _dnsZoneFileConverter = dnsZoneFileConverter;
        }

        public async Task<Result> ProcessAsync(DigitalOceanDomain domain, ExternalAddress externalAddress, string token, CancellationToken cancellation)
        {
            Result<DnsZoneFile> dnsZoneFileResult = await _digitalOceanClient.GetDNSZoneAsync(domain, token, cancellation);
            if (dnsZoneFileResult.IsFailed)
            {
                return dnsZoneFileResult;
            }

            DNSRecordCollection hydratedDnsRecords = _dnsRecordHydrater.Hydrate(domain.Records, externalAddress);
            DNSRecordCollection activeDnsRecords = _dnsZoneFileConverter.Convert(dnsZoneFileResult.Value);
            
            Result create = await dnsRecordCreator.CreateAsync(activeDnsRecords.WhereNew(hydratedDnsRecords), token, cancellation);
            Result update = await _dnsRecordUpdater.UpdateAsync(activeDnsRecords.WhereUpdated(hydratedDnsRecords), token, cancellation);
            return Result.Merge(create, update);
        }
    }
}